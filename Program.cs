using System;
using System.IO;
using System.Collections.Generic;
using BackToMe.Controllers.BusinessLogic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BackToMe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var CLArgs = Environment.GetCommandLineArgs();

            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddCommandLine(args)
                 .AddJsonFile("Properties/launchSettings.json", true, false)
                 .Build();

            var section = config.GetSection("iisSettings")["windowsAuthentication"];

            var config2 = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            object str = 20;
            var locator = new Locator();
            locator.Register(() => str);
            var resolved  = locator.Resolve<object>();

            var locator2 = new Locator();
            locator2.Register(() => "12313");

            var st = locator.Resolve<String>();
            
            var r = ReferenceEquals(resolved, str);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)     
                .UseStartup<Startup>();
        }
    }   
}
