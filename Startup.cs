using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackToMe
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Logger = loggerFactory
                .AddConsole()
                .CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(c => { c.RootPath = "ClientApp/dist"; });            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Logger.Log(LogLevel.Debug, "Configuration Info");
            if (env.IsDevelopment())
            {
                Logger.Log(LogLevel.Debug, "Development mode");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Not development mode");
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


            app.UseHttpsRedirection();
            // configure the app to serve index.html from /wwwroot folder                
            app.UseStaticFiles();
            app.UseSpaStaticFiles();            
            // configure the app for usage as api
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                Logger.Log(LogLevel.Debug, "UseMVC");
                routes.MapRoute(name: "default", template: "{controller}/{action=index}/{id}");
            });

            app.UseSpa(spa =>// sap is single page application
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501                

                spa.Options.SourcePath = "ClientApp";
                spa.Options.StartupTimeout = new System.TimeSpan(0, 0, 80);

                if (env.IsDevelopment())
                {
                    Logger.Log(LogLevel.Debug, "SPA");
                    spa.UseAngularCliServer("start");
                }

                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");                
            });                        
        }
    }
}
