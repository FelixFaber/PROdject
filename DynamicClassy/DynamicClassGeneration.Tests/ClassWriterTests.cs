using DynamicClassGeneration.Interfaces;
using DynamicClassGeneration.Tests.TestContent;
using NUnit.Framework;
using System;

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
            IClass testClass = new TestClass()
            {
                Name = "Heippa",
                Namespace = "Joel.Testar"
            };
            
            var expected = "";
            var actual = _writer.GetClassContent(ref testClass);

            Assert.AreEqual(expected, actual);
        }
    }
}
