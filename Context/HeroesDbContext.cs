namespace BackToMe.Context
{
    using Extensions;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public sealed class HeroesDbContext : DbContext
    {        
        public HeroesDbContext(DbContextOptions options, 
            ILoggerFactory loggerFactory, 
            IConfiguration configuration) 
            : base(options)
        {
            ILogger<HeroesDbContext> logger = loggerFactory
                .AddFile(configuration
                    .GetLogPath(nameOfLogFile: nameof(HeroesDbContext)))
                .CreateLogger<HeroesDbContext>();

            logger.LogDebug("Created data base! Before EnsureCreated");

            Database.EnsureCreated();                        

            logger.LogDebug("Created data base! After EnsureCreated");
        }
        public DbSet<Hero> Heroes { get; }        
    }
}
