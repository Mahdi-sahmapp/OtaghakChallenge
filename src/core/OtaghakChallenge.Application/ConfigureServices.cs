using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OtaghakChallenge.Application.Features.Products.Query;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Application.Services;
using OtaghakChallenge.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ProductDto).Assembly));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISmsServices, SmsServices>();
            return services;
        }
    }
}
