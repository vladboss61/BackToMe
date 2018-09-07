using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using BackToMe.Extensions;

namespace BackToMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ValuesController : ControllerBase
    { 
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }

        public ValuesController(
            ILoggerFactory loggerFactory, 
            IConfiguration configuration)
        {
            Configuration = configuration;
            Logger = loggerFactory
                         .AddConsole()
                         .AddFile(configuration.GetLogPath(nameof(ValuesController)))
                         .CreateLogger<ValuesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));            
        }

        // GET api/values
        [HttpGet("/api/values")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)}");                        
            return await Task.FromResult(new string[] { "value1", "value2" })
                .ConfigureAwait(true);
        }

        // GET api/values/5
        [HttpGet("{id}")]        
        public async Task<ActionResult<string>> Get(int id)
        {            
            Logger.Log(LogLevel.Debug, $"{nameof(Get)} id.");
            return await Task.FromResult("value")
                .ConfigureAwait(true);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Post)}");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Put)}");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Delete)}");
        }
    }
}
