using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackToMe.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using BackToMe.Extensions;
using BackToMe.Models;

namespace BackToMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private const LogLevel LoggingType = LogLevel.Debug;        

        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public HeroesDbContext HeroesDbContext { get; }

        //TODO::May be inject IServiceProvider ??
        public HeroesController(
            ILoggerFactory loggerFactory, 
            IConfiguration configuration,
            HeroesDbContext heroesDbContext)
        {
            HeroesDbContext = heroesDbContext;
            Configuration = configuration ?? throw new ArgumentNullException(nameof(loggerFactory));
            Logger = loggerFactory
                         .AddConsole()
                         .AddFile(Configuration.GetLogPath(nameof(HeroesController)), minimumLevel: LoggingType)
                         .CreateLogger<ValuesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        // GET api/values
        [HttpGet("/api/heroes")]
        public async Task<ActionResult<IEnumerable<Heroe>>> GetHeroes()
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)}");
            return await Task.FromResult(new [] {
                    new Heroe(){Id = 1,Name = "Vlad", Age =20, Sex = true},
                    new Heroe(){Id = 1,Name = "Vlad", Age =20, Sex = true}})
                .ConfigureAwait(true);
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroe>> Get(int id)
        {
            Logger.Log(LogLevel.Information, $"{nameof(Get)} id.");
            return await Task.FromResult(new Heroe() { Id = 1, Name = "Vlad", Age = 20, Sex = true })
                .ConfigureAwait(false);
        }

        // POST api/
        [HttpPost]
        public async Task<Heroe> Post([FromBody] string value)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Post)}");
            return await Task.FromResult(new Heroe() { Id = 1, Name = "Vlad", Age = 20, Sex = true })
                .ConfigureAwait(false);
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Put)}");

        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Delete)}");
        }
    }
}
