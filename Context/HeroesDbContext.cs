namespace BackToMe.Context
{
    using System.Collections.Generic;
    using Interfaces;
    using Extensions;
    using Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public sealed class HeroesDbContext : DbContext, IDbContext<Hero>
    {
        public IConfiguration Configuration { get; }

        public HeroesDbContext(DbContextOptions options,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));

            ILogger<HeroesDbContext> logger = loggerFactory
                .AddFile(configuration
                    .GetLogPath(nameof(HeroesDbContext)))
                .CreateLogger<HeroesDbContext>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionToDb());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>()
                .HasData(Initializer.GetDefaultHeroes);

            base.OnModelCreating(modelBuilder);
        }
        public  DbSet<Hero> Heroes { get; }

        public DbSet<Hero> DataContext => Heroes;

        private static class Initializer
        {
            public static List<Hero> GetDefaultHeroes => new List<Hero>()
            {
                new Hero() {Id = 1, Name = "Vlad", Age = 20, Sex = true},
                new Hero() {Id = 2, Name = "Vlad2", Age = 21, Sex = false},
                new Hero() {Id = 3, Name = "Vlad3", Age = 22, Sex = true}
            };
        }
    }
}
