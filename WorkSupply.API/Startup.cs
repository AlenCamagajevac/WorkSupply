using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WorkSupply.API.Middleware;
using WorkSupply.API.ServiceConfiguration;
using WorkSupply.Persistence.SQL.Data;

namespace WorkSupply.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add DBContext
            services.AddDbContext<WorkSupplyContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("WorkSupply.Persistence.SQL");
                    }));

            
            services.AddOAuth(Configuration);
            services.AddAuthPolicies();
            services.AddSettings(Configuration);      
            services.AddAutoMapper();
            services.RegisterServices();
            services.AddSwaggerDocs(Configuration);
            services.AddCorsRules();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        $"/swagger/{Configuration["Version"]}/swagger.json", "WorkSupply API"
                        );
                });
                
                app.UseDeveloperExceptionPage();
            }
            else
            {
                var context = app.ApplicationServices.GetService<WorkSupplyContext>();
                context.Database.Migrate();
                
                app.UseHsts();
            }

            app.UseCors("AnyOrigin");
            
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}