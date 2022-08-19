using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PizzaHome.API.Authorization;
using PizzaHome.API.Middlewares;
using PizzaHome.Core.Interfaces;
using PizzaHome.Infrastructure;
using PizzaHome.Services.Services;
using System.Text;

namespace PizzaHome.API.Extensions
{
    public static class CustomExtentions
    {
        public static void CustomServices(this IServiceCollection service) {


            service.AddSingleton<DbService>();
            service.AddScoped<IShopService, ShopService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            service.AddTransient<IAuthorizationHandler, PizzaHomeManagementRequirementHandler>();
            service.AddAuthorization(options => {

                options.AddPolicy("AdminAndUser", policy => policy.RequireRole("Admin", "User"));
                options.AddPolicy("PizzaHomeManagementPolicy", policyBuilder =>
                  policyBuilder.AddRequirements(
                      new PizzaHomeManagementRequirement()

                ));

            });

        }

        public static void CustomMiddlewares(this IApplicationBuilder app) {

            app.UseMiddleware<ExceptionHandlingMiddleware>();

        }

        public static void JwtAuthConfiguration(this IServiceCollection service, string issure, string audience, string signKey)
        {

            service.AddAuthentication("JwtAuth")
            .AddJwtBearer("JwtAuth", options => {

                var keyBytes = Encoding.UTF8.GetBytes(signKey);
                var key = new SymmetricSecurityKey(keyBytes);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issure,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

            });

        }
    }
}
