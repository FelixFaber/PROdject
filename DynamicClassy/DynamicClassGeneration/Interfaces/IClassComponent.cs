using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface IClassComponent
    {
        string Name { get; set; }
        string Contents { get; set; }
        AccessModifierEnum AccessModifier { get; set; }
    }
}
