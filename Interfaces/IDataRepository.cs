
namespace BackToMe.Interfaces
{
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public interface IDataRepository<T> // may be would be change signature of methods 
    {
        Task<IActionResult> AddAsync(T hero);

        Task<IActionResult> DeleteAsync(int id);

        Task<ActionResult<T>> Get(int id);

        Task<ActionResult<IList<T>>> GetAll();
    }
}
