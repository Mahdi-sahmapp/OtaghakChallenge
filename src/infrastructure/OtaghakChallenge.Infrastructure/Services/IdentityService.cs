using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Infrastructure.Services
{
    public static class IdentityService
    {
        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure(ConfigureOptionsIdentity());
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = OptionsTokenValidationParameters(configuration);
                    options.Events = JwtOptionsEvents();
                });
        }

        private static TokenValidationParameters OptionsTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"] ?? string.Empty)),
                ValidateIssuer = true,
                ValidIssuer = configuration["JWTConfiguration:Issuer"],
                ValidateAudience = Convert.ToBoolean(configuration["JWTConfiguration:Audience"]),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
        }

        private static Action<IdentityOptions> ConfigureOptionsIdentity()
        {
            return options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            };
        }

        private static JwtBearerEvents JwtOptionsEvents()
        {
            return new JwtBearerEvents
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "application/json";
                    throw new UnauthorizedAccessException("مشکلی بابت احراز هویت شما رخ داده است");
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    if (context.Response.StatusCode == 0)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                    }
                    return context.Response.WriteAsync("شما احراز هویت نشده اید");
                },
                OnForbidden = context =>
                {
                    if (context.Response.StatusCode == 0)
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                    }
                    return context.Response.WriteAsync("شما به این بخش دسترسی ندارید لطفا ابتدا وارد سایت شوید");
                }
            };
        }
    }
}
