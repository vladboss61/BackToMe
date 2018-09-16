namespace BackToMe.Controllers.BusinessLogic
{
    using System.Collections.Generic;
    using Models;

    public static class CLIHelper
    {
        //TODO: Minding...
        public static DataContextType ModeOfEntityRepository(IList<string> args)
        {          
            if (args.Contains("-memory"))
            {
                return DataContextType.Memory;
            }

            if  (args.Contains("-sql"))
            {
                return DataContextType.Sql;
            }

            return DataContextType.Sql;
        }
    }
}
