namespace BackToMe.Models
{
    using System;
    using System.Threading.Tasks;
    using BackToMe.Context;
    using BackToMe.Extensions;
    using BackToMe.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    internal class HeroesDBaseRepository : IDataRepository
    {
        public ILogger Logger { get; }
        public HeroesDbContext HeroesDbContext { get; } 
        public LogInformator LogInformator { get; }
        public IConfiguration Configuration { get; }
        public HeroesDBaseRepository(
         HeroesDbContext heroesDbContext,
         ILoggerFactory loggerFactory,
         LogInformator logInformator,
         IConfiguration configuration)
        {
            Configuration = configuration;
            LogInformator = logInformator ??  throw new ArgumentNullException(nameof(logInformator)); 
            var Logger = loggerFactory
                                .AddFile(Configuration.GetLogPath(nameOfLogFile: nameof(HeroesDBaseRepository)))
                                .CreateLogger(nameof(HeroesDBaseRepository));
            HeroesDbContext = heroesDbContext ?? throw new ArgumentNullException(nameof(heroesDbContext));            
        }

        public Task AddHeroeAsync(Heroe heroe)
        {                    
            Logger.Log(LogLevel.Information, 
                        LogInformator.FromSource(nameof(HeroesDBaseRepository))
                                     .FromOperation(nameof(AddHeroeAsync))
                                     .Information("AddHeroes").Build());
             return Task.CompletedTask;
            //accsses for data base by EF 
        }

        public Task DeleteHeroeAsync(int id)
        {
            return Task.CompletedTask;
            //accsses for data base by EF 
        }
    }
}