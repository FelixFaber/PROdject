using DynamicClassGeneration;
using DynamicClassGeneration.SyntaxTree;
using NUnit.Framework;
using Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Compiler.Tests
{
    [TestFixture]
    public class PluginCompilerTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompilePluginIntoAssembly_Null_Plugin()
        {
            PluginClass plugin = null;

            PluginCompiler.CompilePluginIntoAssembly(plugin);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompilePluginIntoAssembly_Plugin_Without_Name()
        {
            PluginClass plugin = new PluginClass();

            PluginCompiler.CompilePluginIntoAssembly(plugin);
        }
        [Test]
        public void CompilePluginIntoAssembly_Plugin_Valid_Case()
        {
            string pluginName = "TestPlugin";

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
            
            PluginClass plugin = new PluginClass()
            {
                Name = pluginName,
                Namespace = "Joel.Testar",
                Interfaces = new List<Interface>()
                    {
                        new Interface(){ Name = "IPlugin" }
                    },
                UsingClauses = new List<UsingClause>()
                    {
                        new UsingClause(){Name = "Plugin.Interfaces"}
                    },
                Methods = new List<Method>()
                    {
                        MoveMouse,
                        new Method(){Name = "Configure"},
                        new Method(){Name = "PreProcess"},
                        new Method(){Name = "Process"},
                        new Method()
                        {
                            Name = "GetResult",
                            ReturnType = typeof(IPluginResult),
                            Statements = new List<Statement>()
                            {
                                new Statement(){Content = "return null;"} 
                            }
                        }
                    }
            };

            var assembly = PluginCompiler.CompilePluginIntoAssembly(plugin);

            var plugins = assembly.GetAllPlugins();

            var firstPlugin = plugins.FirstOrDefault(plug => plug.Name == pluginName);

            Assert.IsNotNull(firstPlugin);
            Assert.IsTrue(typeof(IPlugin).IsAssignableFrom(firstPlugin));
        }
    }
}
