using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> Implement<TInterface>()
        {
           // List<Type> result = new List<Type>();
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetEntryAssembly();
            var result =ass.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(TInterface)) && !t.IsAbstract).ToList();

            return result;
        }
    }
}
