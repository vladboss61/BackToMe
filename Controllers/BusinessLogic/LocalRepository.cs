
namespace BackToMe.Controllers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.IO;
    using System.Runtime.Serialization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Interfaces;
    using Models;
    using Newtonsoft.Json;


    public class LocalRepository : IDataRepository<Hero>
    {
        public TextReader Reader { get; }

        public JsonSerializer Serializer { get; }

        public IConfiguration Configuration { get; }

        public IHttpContextAccessor HttpContext { get; }

        public LocalRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, TextReader reader, JsonSerializer serializer)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            HttpContext = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public async Task<ActionResult> AddAsync(Hero hero)
        {
            try
            {
                var heroes = GetAllHeroes();

                if (heroes.Contains(hero))
                {
                    return new OkResult();
                }

                await Task.Run(() => heroes.Add(hero));

                var queryString = HttpContext.HttpContext.Request.Path.ToUriComponent();

                return new CreatedAtRouteResult(queryString, hero);
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }

        public Task<ActionResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Hero>> GetAsync(int id)
        {
            var hero = await Task.Run(() => GetAllHeroes().FirstOrDefault(_ => _.Id.Equals(id)));

            if(hero == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(hero);
        }

        public async Task<ActionResult<IList<Hero>>> GetAllAsync()
        {
            using (Reader)
            {
                var result = GetAllHeroes();

                return new ObjectResult(await Task.Run(() => result).ConfigureAwait(false));
            }
        }

        private IList<Hero> GetAllHeroes()
        {
            using (Reader)
            {
                var customJsonHeroes = (Serializer.Deserialize(Reader, typeof(IList<Hero>)) as IList<Hero>)
                                       ?? throw new InvalidOperationException("Invalid deserialize heroes", new SerializationException());

                return customJsonHeroes;
            }
        }
    }
}