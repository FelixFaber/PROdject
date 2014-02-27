using DynamicClassGeneration;
using DynamicClassGeneration.SyntaxTree;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace Plugin.Compiler
{
    public class PluginCompiler
    {
        /// <param name="compilerVersion">Defaults to 'v4.0'</param>
        public static Assembly CompilePluginIntoAssembly(PluginClass plugin, string compilerVersion = null)
        {
            if (plugin == null)
                throw new ArgumentNullException("plugin", "PluginClass is required");
            if (string.IsNullOrWhiteSpace(plugin.Name))
                throw new ArgumentException("Plugin name is required");

            var sourceCode = ClassGenerator.GetClassAsString(plugin);
            var provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", compilerVersion ?? "v4.0" } });
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("Plugin.Interfaces.dll");
            cp.GenerateExecutable = false;
            cp.OutputAssembly = plugin.Name + ".dll";
            cp.GenerateInMemory = true;
            CompilerResults cr = provider.CompileAssemblyFromSource(cp, sourceCode);
            if (cr.Errors.Count > 0)
            {

                throw new PluginCompilationException(cr.Errors, "Plugin "+plugin.Name + "failed compilation");
            }
            return cr.CompiledAssembly;
        }
    }
}
