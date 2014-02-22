using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface IMethod
    {
        string Name { get; set; }
        Type ReturnType { get; set; }
        IEnumerable<IMethodParameter> Parameters { get; set; }
        IEnumerable<ICodeStatement> Statements { get; set; }
        AccessModifierEnum AccessModifier { get; set; }
    }
}
