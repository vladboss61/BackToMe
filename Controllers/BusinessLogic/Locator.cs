namespace BackToMe.Controllers.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    internal class Locator : IServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void Register<T>(Func<T> instance)
        {
            services[typeof(T)] = instance();
        }

        public T Resolve<T>()
        {
            var key = typeof(T);
            if (services.ContainsKey(key))
            {
                return (T)services[key];
            }

            throw new InvalidOperationException($"{nameof(T)} is not registered.");
        }
    }
}
