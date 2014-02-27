using DynamicClassGeneration.SyntaxTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DynamicClassGeneration.Tests
{
    [TestFixture]
    public class ClassGeneratorTests
    {
        [Test]
        public void GetClassContent_Name_Namespace()
        {
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar"
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa\n\t{\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_BaseClass()
        {
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                BaseClassName = "TestClassBase"
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa:TestClassBase\n\t{\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_TwoInterfaces()
        {
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Interfaces = new List<Interface>()
                    {
                        new Interface(){Name = "FirstInterface"},
                        new Interface(){Name = "SecondInterface"}
                    }
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa:FirstInterface,SecondInterface\n\t{\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_BaseClass_TwoInterfaces()
        {
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                BaseClassName = "TestClassBase",
                Interfaces = new List<Interface>()
                    {
                        new Interface(){Name = "FirstInterface"},
                        new Interface(){Name = "SecondInterface"}
                    }
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa:TestClassBase,FirstInterface,SecondInterface\n\t{\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_TwoUsingClauses()
        {
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                UsingClauses = new List<UsingClause>()
                    {
                        new UsingClause(){Name = "System"},
                        new UsingClause(){Name = "System.Collections.Generic"},
                    }
            };

            var expected = "using System;\nusing System.Collections.Generic;\nnamespace Joel.Testar\n{\n\tpublic class Heippa\n\t{\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetClassContent_Method_TwoParameters_NoStatements()
        {
            Method MoveMouse = new Method()
            {
                Name = "DoMoveMouse",
                AccessModifier = AccessModifierEnum.INTERNAL,
                ReturnType = null,
                Parameters = new List<MethodParameter>()
                    {
                        new MethodParameter(){Name = "xPos", ParamType = typeof(int)},
                        new MethodParameter(){Name = "yPos", ParamType = typeof(int)}
                    }
            };
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Methods = new List<Method>()
                    {
                        MoveMouse
                    }
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa\n\t{\n\t\tinternal void DoMoveMouse(int xPos,int yPos)\n\t\t{\n\t\t}\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetClassContent_Method_TwoParameters_MoveMouseStatement()
        {
            Method MoveMouse = new Method()
            {
                Name = "DoMoveMouse",
                AccessModifier = AccessModifierEnum.INTERNAL,
                ReturnType = null,
                Parameters = new List<MethodParameter>()
                    {
                        new MethodParameter(){Name = "xPos", ParamType = typeof(int)},
                        new MethodParameter(){Name = "yPos", ParamType = typeof(int)}
                    },
                Statements = new List<Statement>()
                    {
                        new Statement(){Content = "if(xPos == 1){/*move mouse*/}"}
                    }
            };
            PluginClass testClass = new PluginClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Methods = new List<Method>()
                    {
                        MoveMouse
                    }
            };

            var expected = "namespace Joel.Testar\n{\n\tpublic class Heippa\n\t{\n\t\tinternal void DoMoveMouse(int xPos,int yPos)\n\t\t{\n\t\t\tif(xPos == 1){/*move mouse*/}\n\t\t}\n\t}\n}\n";

            var actual = ClassGenerator.GetClassAsString(testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetClassContent_Null_Class()
        {
            PluginClass testClass = null;
            var actual = ClassGenerator.GetClassAsString(testClass);
        }
    }
}
