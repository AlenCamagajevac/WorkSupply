using Microsoft.Extensions.DependencyInjection;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AddAuthPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministrator", policy =>
                {
                    policy.RequireRole(ApplicationRole.GetRoleName(Role.Admin));
                });
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireEmployer", policy =>
                {
                    policy.RequireRole(ApplicationRole.GetRoleName(Role.Employer));
                });
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireEmployee", policy =>
                {
                    policy.RequireRole(ApplicationRole.GetRoleName(Role.Employee));
                });
            });
            
            return services;
        }
    }
}