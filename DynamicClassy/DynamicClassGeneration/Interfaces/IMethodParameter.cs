using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface IMethodParameter
    {
        Type ParamType { get; set; }
        string Name { get; set; }
    }
}
