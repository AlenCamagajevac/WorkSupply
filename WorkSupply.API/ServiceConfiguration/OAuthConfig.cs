using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Persistence.SQL.Data;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class OAuthConfig
    {
        public static IServiceCollection AddOAuth(this IServiceCollection services, IConfiguration configuration)
        {
            //Add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                })
                .AddEntityFrameworkStores<WorkSupplyContext>()
                .AddDefaultTokenProviders();


            //Return 401 on unauthorized requests
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api"))
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                        return Task.FromResult(0);
                    }
                };
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtSecurityToken:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSecurityToken:Issuer"],

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = 
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityToken:Key"])),

                        ClockSkew = TimeSpan.Zero
                    };

                });

            return services;
        }
    }
}