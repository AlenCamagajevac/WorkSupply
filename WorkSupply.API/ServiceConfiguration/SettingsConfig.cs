using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkSupply.Core.Models.Settings;

namespace WorkSupply.API.ServiceConfiguration
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