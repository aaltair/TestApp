using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using TestApp.Extentions;

namespace TestApp
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
            services.AddControllers().AddFluentValidation();
            services.AddDatabaseConfiguration(Configuration)
                .AddAutoMapperConfiguration(Configuration)
                .AutoServiceRegister(GetAssembliesList())
                .AddIdentityConfiguration(Configuration)
                .AddAuthenticationConfiguration(Configuration)
                .AddOptionsConfiguration(Configuration)
                .AddOptionsConfiguration(Configuration)
                .AddSwaggerConfiguration(Configuration, GetSwaggerXmlPath())
                .ManualServiceRegister(Configuration);


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
          
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMiddlewares();
            app.UseSwaggerConfig(Configuration);
            app.UseRouting();
            app.UseAuthorization();
  

    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private string GetSwaggerXmlPath() => Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

        private IEnumerable<Assembly> GetAssembliesList() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains($"{nameof(TestApp.Persistent)}") |
                            a.FullName.Contains($"{nameof(TestApp.Services)}"));
    }
}
