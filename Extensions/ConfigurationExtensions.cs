using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackToMe.Extensions
{
    internal static class ConfigurationExtensions
    {
        private const string FileExpansion = ".txt";
        private const string CurrentConnectionToDb = "HeroesDBConnection";        
        public static string GetCurrentConnectionToDb(this IConfiguration configuration) => 
            configuration.GetConnectionString(CurrentConnectionToDb); 
        public static string GetLogPath(this IConfiguration configuration, string nameOfLogFile) =>
            Path.Combine(configuration?.GetSection("Logging")?["Path"], $"{nameOfLogFile}{FileExpansion}");
    }
}
