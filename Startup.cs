using System.IO;
using BackToMe.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using BackToMe.Extensions;

namespace BackToMe
{
    public class Startup
    {
        private const string CurrentConnectionToDb = "HeroesDBConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HeroesDbContext>(
                options => options
                    .UseSqlServer(Configuration
                        .GetConnectionString(CurrentConnectionToDb)));          
            
            services.AddSpaStaticFiles(c => { c.RootPath = "ClientApp/dist"; });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory
                .AddConsole()
                .AddFile(Configuration.GetLogPath(nameof(Startup)))
                .CreateLogger<Startup>();

            logger.LogDebug("Start configure File");
            if (env.IsDevelopment())
            {
                logger.LogDebug($"Is dev mode: {env.IsDevelopment()}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogDebug("Not Dev");
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
                logger.LogDebug("Use MVC Routing");
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
                    logger.Log(LogLevel.Debug, "SPA is configured");
                    spa.UseAngularCliServer("start");
                }

                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });
        }
    }
}
