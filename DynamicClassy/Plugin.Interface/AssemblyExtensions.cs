using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Interfaces
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetAllPlugins(this Assembly self)
        {
            var results = from type in self.GetTypes()
                          where typeof(IPlugin).IsAssignableFrom(type)
                          select type;
            return results;
        }
    }
}
