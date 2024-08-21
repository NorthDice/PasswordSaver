using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordSaver.Authentification;
using PasswordSaver.Enums;
using PasswordSaver.Models;
using System.Text;

namespace PasswordSaver.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IOptions<JwtOptions> jwtOptions)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

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
