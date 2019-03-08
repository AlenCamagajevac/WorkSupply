using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using License = System.ComponentModel.License;

namespace WorkSupply.API.ServiceConfiguration
{
    public static class DocumentationConfig
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
        {
            var version = configuration["Version"];
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new Info
                {
                    Version = version,
                    Title = "WorkSupply API",
                    Description = "REST API for WorkSupply time tracking application",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Alen Čamagajevac",
                        Email = "alen@protostar.ai",
                        Url = "protostar.ai"
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            return services;
        }
    }
}