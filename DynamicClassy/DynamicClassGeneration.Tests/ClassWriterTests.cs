using DynamicClassGeneration.SyntaxTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetClassContent_Null_Class()
        {
            RootClass testClass = null;
            var actual = _writer.GetClassContent(ref testClass);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void WriteFileToDisk_NoDirectorySet()
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

            var filePath = string.Empty;

            var actual = _writer.WriteClassToFile(ref testClass, out filePath);
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteFileToDisk_Null_Class()
        {
            RootClass testClass = null;
            var filePath = string.Empty;
            var actual = _writer.WriteClassToFile(ref testClass, out filePath);
        }

        [Test]
        public void WriteFileToDisk_IntegrationTest()
        {
            string targetFolder = TestUtil.AssemblyDirectory;
            ClassWriter writer = new ClassWriter(new DirectoryInfo(targetFolder));

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

            var expectedFilePath = Path.GetFullPath(Path.Combine(targetFolder, testClass.Name + ".cs"));
            var actualFilePath = string.Empty;

            var writeSuccess = writer.WriteClassToFile(ref testClass, out actualFilePath);

            Assert.IsTrue(writeSuccess);
            Assert.AreEqual(expectedFilePath, actualFilePath);
        }
    }
}
