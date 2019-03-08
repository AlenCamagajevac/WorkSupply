using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorkSupply.API.Middleware;
using WorkSupply.API.ServiceConfiguration;
using WorkSupply.Persistance.SQL.Data;

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
                    sqlOptions.MigrationsAssembly("WorkSupply.Persistance.SQL")));

            services.AddOAuth(Configuration);

            services.AddSettings(Configuration);
            
            services.AddAutoMapper();

            // Register dependency injection
            services.AddServices();

            services.AddSwaggerDocs(Configuration);
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMetrics();
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
                app.UseHsts();
            }
            
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}