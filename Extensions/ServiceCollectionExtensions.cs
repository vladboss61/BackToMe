using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BackToMe.Extensions
{
    using System;
    using Context;
    using Controllers.BusinessLogic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Interfaces;
    using Models;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataRepository<Hero>, HeroesDBaseRepository>();
            services.AddScoped<IDbContext<Hero>, HeroesDbContext>();

            switch (CommandLineHelper.ModeOfEntityRepository(Environment.GetCommandLineArgs(), configuration))
            {
                case DataContextType.Sql:
                    services.AddDbContext<HeroesDbContext>(
                        options => options
                            .UseSqlServer(configuration.GetConnectionToDb()));
                    break;
                case DataContextType.Memory:

                    services.AddDbContext<HeroesDbContext>(
                        options => options
                            .UseInMemoryDatabase(configuration.GetConnectionToDb()));
                    break;

                default:
                    throw new InvalidOperationException("Cannot choice a mode of data base memory or sql.");
            }
        }

        public static void AddLocalDataContext(this IServiceCollection services)
        {
            services.AddScoped<IDataRepository<Hero>, LocalRepository>();
            services.AddScoped<TextReader, StreamReader>((provider) =>
            {
                var jsonPath = provider.GetService<IConfiguration>().GetLocalDataContext();
                return new StreamReader(jsonPath);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<JsonSerializer>();
        }
    }
}
