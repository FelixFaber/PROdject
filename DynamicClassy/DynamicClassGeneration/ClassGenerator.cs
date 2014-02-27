using DynamicClassGeneration.SyntaxTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public static class ClassGenerator
    {    
        public static string GetClassAsString(PluginClass classToWrite)
        {
            if (classToWrite == null)
                throw new ArgumentNullException("classToWrite", "SUG MIN KUK is required");

            var preProcessedClass = PreProcess(classToWrite);
            return MakeClassString(preProcessedClass);
        }

        private static PluginClass PreProcess(PluginClass classToProcess)
        {
            if (string.IsNullOrWhiteSpace(classToProcess.Name))
                throw new NullReferenceException("Missing Class Name");
            if (string.IsNullOrWhiteSpace(classToProcess.Namespace))
                throw new NullReferenceException("Missing Class Namespace");

            var preProcessedClass = classToProcess.Clone();

            if (preProcessedClass.UsingClauses == null) preProcessedClass.UsingClauses = new List<UsingClause>();
            if (preProcessedClass.Methods == null) preProcessedClass.Methods = new List<Method>();
            if (preProcessedClass.Interfaces == null) preProcessedClass.Interfaces = new List<Interface>();

            return preProcessedClass;
        }
        private static string MakeClassString(PluginClass classToConvert)
        {
            var classContents = new StringBuilder();

            foreach (var statement in classToConvert.UsingClauses)
            {
                classContents.Append("using " + statement.Name + ";\n");
            }

            classContents.Append("namespace " + classToConvert.Namespace + "\n");
            classContents.Append("{\n");

            classContents.Append(string.Format("\t{0} class {1}", classToConvert.AccessModifier.GetString(), classToConvert.Name));
            classContents.Append(MakeClassInheritenceString(classToConvert));
            classContents.Append("\n\t{\n");

            foreach (var method in classToConvert.Methods)
            {
                classContents.Append(MakeMethodString(method));
            }

            classContents.Append("\t}\n");
            classContents.Append("}\n");

            return classContents.ToString();
        }
        private static string MakeClassInheritenceString(PluginClass classToConvert)
        {
            var inheritence = new StringBuilder();
            var hasBaseClass = !string.IsNullOrWhiteSpace(classToConvert.BaseClassName);
            var hasInterfaces = classToConvert.Interfaces.Count() > 0;

            if (hasBaseClass || hasInterfaces)
            {
                inheritence.Append(":");

                if (hasBaseClass)
                { 
                    inheritence.Append(classToConvert.BaseClassName);
                    if (hasInterfaces)
                        inheritence.Append(",");
                }
                foreach (var mInterface in classToConvert.Interfaces)
                {
                    inheritence.Append(mInterface.Name + ",");
                }
            }

            var inheritenceString = inheritence.ToString();
            if(inheritenceString.Contains(','))
                inheritenceString = inheritenceString.Remove(inheritenceString.LastIndexOf(','));
            return inheritenceString;
        }
        private static string MakeMethodString(Method method)
        {
            var methodContents = new StringBuilder();
            var accessModifier = method.AccessModifier.GetString();
            var returnType = method.ReturnType.GetOutputString();

            methodContents.Append("\t\t"+accessModifier + " " + returnType + " " + method.Name);

            string methodParams = "(";
            if (method.Parameters != null)
            {
                
                foreach (var param in method.Parameters)
                {
                    methodParams += param.ParamType.GetOutputString() + " " + param.Name + ",";
                }
            }
            methodParams += ")";
            if(methodParams.Contains(','))
                methodParams = methodParams.Remove(methodParams.LastIndexOf(','), 1);
            methodContents.Append(methodParams);

            methodContents.Append("\n\t\t{");

            if (method.Statements != null)
            {
                foreach (var statement in method.Statements)
                {
                    methodContents.Append("\n\t\t\t"+statement.Content);
                }
            }

            methodContents.Append("\n\t\t}\n");

            return methodContents.ToString();
        }
    }
}
