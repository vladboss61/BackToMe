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
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
            LoggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ILogger<Startup> logger = LoggerFactory
                .AddFile(Configuration.GetLogPath("Error CIL args"))
                .CreateLogger<Startup>();    
            
            services.AddScoped<IDataRepository<Hero>, HeroesDBaseRepository>();
            services.AddTransient<ILogBuilder, LogInformationBuilder>();

            var logInformationBuilder = services.BuildServiceProvider().GetRequiredService<ILogBuilder>();

            switch (CILHelper.Mode(Environment.GetCommandLineArgs()))
            {
                case DataContextType.Sql:
                    logger.Log(LogLevel.Information, logInformationBuilder
                        .FromSource(nameof(Startup))
                        .FromOperation(nameof(ConfigureServices))
                        .Information("SQL Data Mode.")
                        .Build());

                    services.AddDbContext<HeroesDbContext>(
                        options => options
                            .UseSqlServer(Configuration.GetCurrentConnectionToDb()));
                    break;
                case DataContextType.Memory:
                    logger.Log(LogLevel.Information, logInformationBuilder
                        .FromSource(nameof(Startup))
                        .FromOperation(nameof(ConfigureServices))
                        .Information("Memory Data Mode.")
                        .Build());

                    services.AddDbContext<HeroesDbContext>(
                        options => options
                            .UseInMemoryDatabase(Configuration.GetCurrentConnectionToDb()));
                    break;
                default:
                    logger.Log(LogLevel.Error, logInformationBuilder
                        .FromSource(nameof(Startup))
                        .FromOperation(nameof(ConfigureServices))
                        .Information("Error into args")
                        .Build());

                    throw new InvalidOperationException(
                        $"{nameof(ConfigureServices)}: Cannot choice a mode of data base memory or sql.");
            }                      
            //services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient();            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            //TODO: INVESTIGATE THIS METHOD 
            //services.AddHttpClient();            
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

            //app.UseHttpsRedirection();
            // configure the app to serve index.html from /wwwroot folder                
            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();
            // configure the app for usage as api

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvcWithDefaultRoute();
            
            app.UseMvc(configureRoutes: routes =>
            {
                logger.LogDebug("Use MVC Routing");
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
