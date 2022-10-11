using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Models
{
    public class EngineContext
    {
        private static IServiceProvider _IserviceProvider { get; set; }

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _IserviceProvider = serviceProvider;
        }
        public static T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }
        public static object Resolve(Type type)
        {
            return _IserviceProvider?.GetService(type);
        }
    }
}
