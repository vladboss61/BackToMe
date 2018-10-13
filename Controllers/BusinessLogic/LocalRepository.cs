
using System.Collections;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BackToMe.Controllers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Interfaces;
    using Models;
    using Newtonsoft.Json;


    public class LocalRepository : IDataRepository<Hero>
    {
        public TextReader Reader { get; }

        public JsonSerializer Serializer { get; }

        public IConfiguration Configuration { get; }

        public LocalRepository(IConfiguration configuration, TextReader reader, JsonSerializer serializer)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public Task<IActionResult> AddAsync(Hero hero)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Hero>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IList<Hero>>> GetAll()
        {
            using (Reader)
            {
                var result = (IList<Hero>)Serializer.Deserialize(Reader, typeof(IList<Hero>));

                return new ObjectResult( await Task.Run(() => result).ConfigureAwait(false));                
            }
        }
    }
}
