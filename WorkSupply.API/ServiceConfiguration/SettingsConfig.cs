using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkSupply.API.AppSettings;

namespace WorkSupply.API.Middleware
{
    public static class SettingsConfig
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSecurityToken"));
            
            return services;
        }
    }
}