using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackToMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ValuesController : ControllerBase
    { 
        public ILogger Logger { get; }        

        public ValuesController(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory
                         .AddConsole()
                         .CreateLogger<ValuesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));            
        }

        // GET api/values
        [HttpGet("/api/values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)}");                        
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]        
        public ActionResult<string> Get(int id)
        {
            Logger.Log(LogLevel.Debug, $"{nameof(Get)} id.");
            return "value";
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
