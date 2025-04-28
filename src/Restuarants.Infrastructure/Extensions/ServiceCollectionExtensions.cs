using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using Restuarants.Infrastructure.Authorization;
using Restuarants.Infrastructure.Authorization.Requirements;
using Restuarants.Infrastructure.Authorization.Services;
using Restuarants.Infrastructure.Persistance;
using Restuarants.Infrastructure.Repositories;
using Restuarants.Infrastructure.SeedData;

namespace Restuarants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestuarantConnect");
            services.AddDbContext<RestuarantDbContext>(options =>
            {
                options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
            });

            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimPrincipalFactory>()
                .AddEntityFrameworkStores<RestuarantDbContext>();

            services.AddScoped<IRestuarantSeeder, RestuarantSeeder>();
            services.AddScoped<IRestuarantsRepository, RestuarantsRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IRestuarantAuthorizationService, RestuarantAuthorizationService>();


            // Claim based Authorization
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder
                .RequireClaim(ApplicationClaimTypes.Nationality, "South African", "Zambian"))
            .AddPolicy(PolicyNames.AtLeast20Yrs, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtLeast2Restuarants, builder => builder.AddRequirements(new CreatedMultipleRestuarantsRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestuarantsRequirementHandler>();
        }
    }
}
