using DynamicClassGeneration.SyntaxTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DynamicClassGeneration.Tests
{
    [TestFixture]
    public class ClassWriterTests
    {
        private ClassWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new ClassWriter(null);
        }

        [Test]
        public void GetClassContent_Name_Namespace()
        {
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar"
            };

            var expected = "namespace Joel.Testar{public class Heippa{}}";

            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_BaseClass()
        {
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                BaseClassName = "TestClassBase"
            };

            var expected = "namespace Joel.Testar{public class Heippa:TestClassBase{}}";

            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_TwoInterfaces()
        {
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Interfaces = new List<Interface>()
                    {
                        new Interface(){Name = "FirstInterface"},
                        new Interface(){Name = "SecondInterface"}
                    }
            };

            var expected = "namespace Joel.Testar{public class Heippa:FirstInterface,SecondInterface{}}";

            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_BaseClass_TwoInterfaces()
        {
            RootClass testClass = new RootClass()
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

            var expected = "namespace Joel.Testar{public class Heippa:TestClassBase,FirstInterface,SecondInterface{}}";

            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetClassContent_Name_Namespace_TwoUsingClauses()
        {
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                UsingClauses = new List<UsingClause>()
                    {
                        new UsingClause(){Name = "System"},
                        new UsingClause(){Name = "System.Collections.Generic"},
                    }
            };

            var expected = "using System;using System.Collections.Generic;namespace Joel.Testar{public class Heippa{}}";

            var actual = _writer.GetClassContent(ref testClass);

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
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Methods = new List<Method>()
                    {
                        MoveMouse
                    }
            };

            var expected = "namespace Joel.Testar{public class Heippa{internal void DoMoveMouse(int xPos,int yPos){}}}";

            var actual = _writer.GetClassContent(ref testClass);

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
            RootClass testClass = new RootClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar",
                Methods = new List<Method>()
                    {
                        MoveMouse
                    }
            };

            var expected = "namespace Joel.Testar{public class Heippa{internal void DoMoveMouse(int xPos,int yPos){if(xPos == 1){/*move mouse*/}}}}";

            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
    }
}
