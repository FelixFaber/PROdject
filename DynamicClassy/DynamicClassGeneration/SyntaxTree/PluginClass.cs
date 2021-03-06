﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.SyntaxTree
{
    public class PluginClass : ICloneable
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string BaseClassName { get; set; }
        public IEnumerable<UsingClause> UsingClauses { get; set; }
        public IEnumerable<Method> Methods { get; set; }
        public IEnumerable<Interface> Interfaces { get; set; }
        public AccessModifierEnum AccessModifier { get; set; }

        public PluginClass Clone() { return (PluginClass)this.MemberwiseClone(); }
        object ICloneable.Clone() { return Clone(); }
    }
}
