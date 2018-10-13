namespace BackToMe.Interfaces
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContext<TEntity> where TEntity : class
    {
        DbSet<TEntity> DataContext { get; }
    }
}