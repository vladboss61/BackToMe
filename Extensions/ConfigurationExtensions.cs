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
        public static string GetLogPath(this IConfiguration configuration, string nameOfLogFile) =>
            Path.Combine(configuration?.GetSection("Logging")?["Path"], $"{nameOfLogFile}{FileExpansion}");
    }
}
