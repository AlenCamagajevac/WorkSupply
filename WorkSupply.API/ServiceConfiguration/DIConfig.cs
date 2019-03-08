using Microsoft.Extensions.DependencyInjection;
using WorkSupply.Core.Service;
using WorkSupply.Services.Services;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class DIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            
            return services;
        }
    }
}