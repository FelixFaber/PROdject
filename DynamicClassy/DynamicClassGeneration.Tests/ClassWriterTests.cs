using DynamicClassGeneration.SyntaxTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DynamicClassGeneration.Tests
{
    [TestFixture]
    public class ClassFileWriterTests
    {
        private ClassFileWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new ClassFileWriter(null);
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

            var actual = _writer.WriteClassToFile(testClass, out filePath, Encoding.UTF8);
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteFileToDisk_Null_Class()
        {
            RootClass testClass = null;
            var filePath = string.Empty;
            var actual = _writer.WriteClassToFile(testClass, out filePath, Encoding.UTF8);
        }

        [Test]
        public void WriteFileToDisk_IntegrationTest()
        {
            string targetFolder = TestUtil.AssemblyDirectory;
            ClassFileWriter writer = new ClassFileWriter(new DirectoryInfo(targetFolder));

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

            var writeSuccess = writer.WriteClassToFile(testClass, out actualFilePath, Encoding.UTF8);

            Assert.IsTrue(writeSuccess);
            Assert.AreEqual(expectedFilePath, actualFilePath);

            string expectedFileContent = ClassGenerator.GetClassAsString(testClass);
            string actualfileContent = File.ReadAllText(actualFilePath, Encoding.UTF8);

            Assert.AreEqual(expectedFileContent, actualfileContent);
        }
    }
}
