using Microsoft.Extensions.DependencyInjection;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class CorsConfig
    {
        public static IServiceCollection AddCorsRules(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            return services;
        }
    }
}