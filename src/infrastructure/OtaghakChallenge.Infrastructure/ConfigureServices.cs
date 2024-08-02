using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Infrastructure.Services;
using OtaghakChallenge.Persistence.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {            
            services.AddScoped<ITokenServices, TokenService>();
            return services;
        }
    }
}
