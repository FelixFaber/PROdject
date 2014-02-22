using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface ICodeStatement
    {
        string Name { get; set; }
        string Contents { get; set; }
    }
}
