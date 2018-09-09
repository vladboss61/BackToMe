using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToMe.Controllers.BusinessLogic
{
    internal interface IServiceLocator
    {
        void Register<T>(Func<T> service);
        T Resolve<T>();
    }
}
