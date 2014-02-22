using DynamicClassGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Tests.TestContent
{
    public class TestClass : IClass
    {
        public string Name
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public IEnumerable<IUsingStatement> UsingStatements
        {
            get;
            set;
        }

        public IEnumerable<IMethod> Methods
        {
            get;
            set;
        }

        public IEnumerable<ICodeStatement> Component
        {
            get;
            set;
        }

        public AccessModifierEnum AccessModifier
        {
            get;
            set;
        }
    }
}
