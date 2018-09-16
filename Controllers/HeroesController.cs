﻿namespace BackToMe.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;

    using BackToMe.Extensions;
    using BackToMe.Models;
    using BackToMe.Context;
    using BackToMe.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public sealed class HeroesController : ControllerBase
    {    
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public IDataRepository<Hero> DataRepository { get; }
        public ILogBuilder LogInform { get; } 

        public HeroesController(
            ILoggerFactory loggerFactory, 
            IConfiguration configuration,
            IDataRepository<Hero> dataRepository,
            ILogBuilder logInformationBuilder)
        {
            LogInform = logInformationBuilder ?? throw new ArgumentNullException(nameof(logInformationBuilder));
            DataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(loggerFactory));

            Logger = loggerFactory
                         .AddConsole()
                         .AddFile(Configuration.GetLogPath(nameOfLogFile: nameof(HeroesController)))
                         .CreateLogger<ValuesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        // GET api/values
        [HttpGet("/api/heroes")]
        public async Task<ActionResult<IList<Hero>>> GetHeroes()
        {
            Logger.Log(LogLevel.Debug, 
                LogInform.FromSource(nameof(HeroesController))
                         .FromOperation(nameof(GetHeroes))
                         .Build());

            return await DataRepository.GetAll();
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> Get(int id)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)} id.");
            return await Task.FromResult(new Hero() { Id = 1, Name = "Vlad", Age = 20, Sex = true })
                .ConfigureAwait(false);
        }

        // POST api/
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody]Hero hero)
        {
            Logger.Log(LogLevel.Debug, 
                                LogInform.FromSource(nameof(HeroesController))
                                         .FromOperation(nameof(Post))
                                         .Information("Request post is loaded")
                                         .Build());

            return await DataRepository.AddAsync(hero);
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Hero value)
        {
            
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DataRepository.DeleteAsync(id);
        }
    }
}
