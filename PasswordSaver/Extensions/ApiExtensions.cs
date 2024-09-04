using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordSaver.Authentification;
using PasswordSaver.Enums;
using PasswordSaver.Models;
using System.Diagnostics;
using System.Text;

namespace PasswordSaver.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IOptions<JwtOptions> jwtOptions)
        {

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            
                            var token = context.Request.Cookies["tasty-cookies"];
                            if (token != null)
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            
                            Debug.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login"; // Путь к странице входа
                    options.AccessDeniedPath = "/account/accessdenied"; // Путь при отказе в доступе
                });

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        }

        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
            this TBuilder builder, params Permissions[] permissions)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy => 
            policy.AddRequirements(new PermissionRequirement(permissions)));
        }
    }
}
