
namespace BackToMe.Interfaces
{
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public interface IDataRepository<T> // may be would be change signature of methods 
    {
        Task<ActionResult> AddAsync(T hero);

        Task<ActionResult> DeleteAsync(int id);

        Task<ActionResult<T>> GetAsync(int id);

        Task<ActionResult<IList<T>>> GetAllAsync();
    }
}
