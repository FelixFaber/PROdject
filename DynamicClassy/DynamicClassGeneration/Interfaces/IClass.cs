﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface IClass
    {
        string Name { get; set; }
        string Namespace { get; set; }
        IEnumerable<IUsingStatement> UsingStatements { get; set; }
        IEnumerable<IMethod> Methods { get; set; }
        IEnumerable<ICodeStatement> Component { get; set; }
        AccessModifierEnum AccessModifier { get; set; }
    }
}
