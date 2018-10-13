using System;

namespace BackToMe.Controllers.BusinessLogic
{
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using Models;

    public static class CommandLineHelper
    {
        //TODO: Minding...
        public static DataContextType ModeOfEntityRepository(IList<string> args, IConfiguration configuration)
        {    
            
            if (args.Contains("-memory"))
            {
                return DataContextType.Memory;
            }

            if  (args.Contains("-sql"))
            {
                return DataContextType.Sql;
            }

            if(Enum.TryParse(configuration["DefaultMode"], true, out DataContextType defaultMode))
            {
                return defaultMode;
            }

            throw new InvalidOperationException("Default mode of source of data is incorrect.");
        }
    }
}
