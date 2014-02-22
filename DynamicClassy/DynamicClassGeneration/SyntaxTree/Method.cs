using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.SyntaxTree
{
    public class Method
    {
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public IEnumerable<MethodParameter> Parameters { get; set; }
        public IEnumerable<Statement> Statements { get; set; }
        public AccessModifierEnum AccessModifier { get; set; }
    }
}
