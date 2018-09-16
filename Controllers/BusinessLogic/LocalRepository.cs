namespace BackToMe.Controllers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Interfaces;
    using Models;


    public class LocalRepository : IDataRepository<Hero>
    {
        //TODO:Inject
        public LocalRepository()
        {

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

        public Task<ActionResult<IList<Hero>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
