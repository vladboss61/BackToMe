namespace BackToMe.Controllers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Context;
    using Extensions;
    using Interfaces;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;


    internal class HeroesDBaseRepository : IDataRepository<Hero>
    {
        public ILogger Logger { get; }
        public HeroesDbContext HeroesDbContext { get; } 
        public ILogBuilder LogInformationBuilder { get; }
        public IConfiguration Configuration { get; }

        public HeroesDBaseRepository(
         HeroesDbContext heroesDbContext,
         ILoggerFactory loggerFactory,
         ILogBuilder logInformationBuilder,
         IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); ;
            LogInformationBuilder = logInformationBuilder ??  throw new ArgumentNullException(nameof(logInformationBuilder));
            HeroesDbContext = heroesDbContext ?? throw new ArgumentNullException(nameof(heroesDbContext));

            Logger = loggerFactory
                                .AddFile(Configuration.GetLogPath(nameOfLogFile: nameof(HeroesDBaseRepository)))
                                .CreateLogger(nameof(HeroesDBaseRepository));
        }

        public async Task<IActionResult> AddAsync(Hero hero)
        {                    
            Logger.Log(LogLevel.Information, 
                        LogInformationBuilder.FromSource(nameof(HeroesDBaseRepository))
                                     .FromOperation(nameof(AddAsync))
                                     .Information("Add Hero")
                                     .Build());
            await HeroesDbContext
                .Heroes
                .AddAsync(hero);

            return new StatusCodeResult(201);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            Logger.Log(LogLevel.Information,
                LogInformationBuilder.FromSource(nameof(HeroesDBaseRepository))
                    .FromOperation(nameof(AddAsync))
                    .Information("Delete Hero")
                    .Build());

            return await Task.Run<ActionResult>(() =>
            {
                var findHero = HeroesDbContext
                    .Heroes
                    .FirstOrDefault(hero => hero.Id.Equals(id));

                if (findHero is null)
                {
                    return new StatusCodeResult(404);
                }

                HeroesDbContext.Remove(findHero);
                return new StatusCodeResult(201);
            }).ConfigureAwait(false);
        }

        public async Task<ActionResult<Hero>> Get(int id)
        {
            //var findHero = await HeroesDbContext.Heroes.FirstOrDefaultAsync((hero)=> hero.Id.Equals(id));
            //if (findHero == null)
            //{
            //    return new NotFoundResult();
            //}

            return new OkObjectResult(await Task.FromResult(new Hero { Id = 1, Age = 200, Name = "Lord off", Sex = true }));
        }

        public async Task<ActionResult<IList<Hero>>> GetAll()
        {
            //List<Hero> heroes = HeroesDbContext.Heroes.AsEnumerable().ToList();

            //return new OkObjectResult(await Task.FromResult(heroes));
            return new ObjectResult(await Task.FromResult(new List<Hero>() { 
                new Hero {Id = 1, Age = 200, Name = "Lord off", Sex = true}, 
                new Hero { Id = 2, Age = 200, Name = "Lord off", Sex = true }}));
        }
    }
}