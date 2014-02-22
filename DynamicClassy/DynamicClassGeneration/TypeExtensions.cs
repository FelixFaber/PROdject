using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public static class TypeExtensions
    {
        public static string GetOutputString(this Type self)
        {
            if (self == null)
                return "void";
            else if (self == typeof(short))
                return "short";
            else if (self == typeof(int))
                return "int";
            else if (self == typeof(long))
                return "long";
            else if (self == typeof(double))
                return "double";
            else if (self == typeof(float))
                return "float";
            else if (self == typeof(decimal))
                return "decimal";
            else if (self == typeof(string))
                return "string";
            else if (self == typeof(char))
                return "char";
            else
                throw new NotSupportedException("Type " + self.FullName + " is not yet supported! Add it!");
        }
    }
}
