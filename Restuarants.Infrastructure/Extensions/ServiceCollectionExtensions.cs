﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
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
                .AddEntityFrameworkStores<RestuarantDbContext>();

            services.AddScoped<IRestuarantSeeder, RestuarantSeeder>();
            services.AddScoped<IRestuarantsRepository, RestuarantsRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
        }
    }
}
