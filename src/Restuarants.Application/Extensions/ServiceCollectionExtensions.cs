﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restuarants.Application.Users;
using Restuarants.Application.Users.Abstract;

namespace Restuarants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly).AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}
