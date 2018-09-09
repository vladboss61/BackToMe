using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using BackToMe.Extensions;
using BackToMe.Models;


namespace BackToMe.Context
{
    public sealed class HeroesDbContext : DbContext
    {        
        public HeroesDbContext(DbContextOptions options, 
            ILoggerFactory loggerFactory, 
            IConfiguration configuration) 
            : base(options)
        {
            var logger = loggerFactory
                .AddFile(configuration
                    .GetLogPath(nameOfLogFile: nameof(HeroesDbContext)))
                .CreateLogger<HeroesDbContext>();

            logger.LogDebug("Created data base! Before EnsureCreated");

            Database.EnsureCreated();

            logger.LogDebug("Created data base! After EnsureCreated");
        }
        public DbSet<Heroe> Heroes { get; }
        
    }
}
