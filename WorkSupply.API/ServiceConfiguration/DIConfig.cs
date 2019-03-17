using Microsoft.Extensions.DependencyInjection;
using WorkSupply.Core.Persistence;
using WorkSupply.Core.Service;
using WorkSupply.Persistence.SQL;
using WorkSupply.Services.Services;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class DIConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IWorkLogService, WorkLogService>();
            
            return services;
        }
    }
}