using System;
using BackToMe.Controllers.BusinessLogic;

namespace BackToMe
{
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using BackToMe.Extensions;
    using BackToMe.Interfaces;
    using BackToMe.Models;
    using BackToMe.Context;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public ILoggerFactory LoggerFactory { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
            LoggerFactory = loggerFactory;
        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLocalDataContext();
            services.AddTransient<ILogBuilder, LogInformationBuilder>();

            //services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

            services.AddHttpClient();            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //TODO: INVESTIGATE THIS METHOD 
            //services.AddHttpClient();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. Middleware 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.Use(async (context, next) => {
            //    await next();
            //    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/"))
            //    {
            //        context.Request.Path = "/index.html";
            //        await next();
            //    }
            //});

            //app.UseHttpsRedirection();
            // configure the app to serve index.html from /wwwroot folder                
            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();
            // configure the app for usage as api

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvcWithDefaultRoute();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action=index}/{id}");
            });
            
             app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            //app.UseSpa(spa =>// sap is single page application
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501                                
            //    spa.Options.SourcePath = "ClientApp";
            //    spa.Options.StartupTimeout = new System.TimeSpan(0, 0, 80);

            //    if (env.IsDevelopment())
            //    {
            //        logger.Log(LogLevel.Debug, "SPA is configured");
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }

            //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200"); // 
            //});

            // позволяет серверу принимать запросы с порта 4200
            //app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));            
        }
    }
}
