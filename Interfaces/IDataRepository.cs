namespace BackToMe.Interfaces
{
    using System.Threading.Tasks;
    using BackToMe.Models;

    public interface IDataRepository // may be would be change signature of methods 
    {
        Task AddHeroeAsync(Heroe heroe);
        Task DeleteHeroeAsync(int id);
    }
}
