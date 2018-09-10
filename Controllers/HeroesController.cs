namespace BackToMe.Controllers
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
    public class HeroesController : ControllerBase
    {    
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public IDataRepository DataRepository { get; }
        public LogInformator LogInform { get; } 

        //TODO::May be inject IServiceProvider ??
        public HeroesController(
            ILoggerFactory loggerFactory, 
            IConfiguration configuration,
            IDataRepository dataRepository,
            LogInformator logInformator)
        {
            LogInform = logInformator ?? throw new ArgumentNullException(nameof(logInformator));
            DataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(loggerFactory));
            Logger = loggerFactory
                         .AddConsole()
                         .AddFile(Configuration.GetLogPath(nameOfLogFile: nameof(HeroesController)))
                         .CreateLogger<ValuesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        // GET api/values
        [HttpGet("/api/heroes")]
        public async Task<ActionResult<IEnumerable<Heroe>>> GetHeroes()
        {
            Logger.Log(LogLevel.Debug, 
                LogInform.FromSource(nameof(HeroesController))
                         .FromOperation(nameof(GetHeroes))
                         .Build());

            return await Task.FromResult(new [] {
                    new Heroe(){Id = 1,Name = "Vlad", Age =20, Sex = true},
                    new Heroe(){Id = 1,Name = "Vlad", Age =20, Sex = true}})
                .ConfigureAwait(false);
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroe>> Get(int id)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)} id.");
            return await Task.FromResult(new Heroe() { Id = 1, Name = "Vlad", Age = 20, Sex = true })
                .ConfigureAwait(false);
        }

        // POST api/
        [HttpPost]
        public async Task Post(
            [FromBody] Heroe heroe)
        {
            Logger.Log(LogLevel.Debug, 
                                LogInform.FromSource(nameof(HeroesController))
                                         .FromOperation(nameof(Post))
                                         .Information("Request post is loaded")
                                         .Build());

            await DataRepository.AddHeroeAsync(heroe);
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
