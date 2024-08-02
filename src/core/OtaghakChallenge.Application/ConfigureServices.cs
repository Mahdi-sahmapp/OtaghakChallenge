using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Application.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
