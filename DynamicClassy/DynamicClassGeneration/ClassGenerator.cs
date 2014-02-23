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
        public static string GetClassAsString(RootClass classToWrite)
        {
            if (classToWrite == null)
                throw new ArgumentNullException("classToWrite", "IClass is required");

            var preProcessedClass = PreProcess(classToWrite);
            return MakeClassString(preProcessedClass);
        }

        private static RootClass PreProcess(RootClass classToProcess)
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
        private static string MakeClassString(RootClass classToConvert)
        {
            var classContents = new StringBuilder();

            foreach (var statement in classToConvert.UsingClauses)
            {
                classContents.Append("using " + statement.Name + ";");
            }

            classContents.Append("namespace " + classToConvert.Namespace);
            classContents.Append("{");

            classContents.Append(string.Format("{0} class {1}", classToConvert.AccessModifier.GetString(), classToConvert.Name));
            classContents.Append(MakeClassInheritenceString(classToConvert));
            classContents.Append("{");

            foreach (var method in classToConvert.Methods)
            {
                classContents.Append(MakeMethodString(method));
            }

            classContents.Append("}");
            classContents.Append("}");

            return classContents.ToString();
        }
        private static string MakeClassInheritenceString(RootClass classToConvert)
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

            methodContents.Append(accessModifier + " " + returnType + " " + method.Name);

            methodContents.Append("(");
            if (method.Parameters != null)
            {
                foreach (var param in method.Parameters)
                {
                    methodContents.Append(param.ParamType.GetOutputString() + " " + param.Name + ",");
                }
            }
            methodContents.Append(")");
            methodContents = methodContents.Remove(methodContents.ToString().LastIndexOf(','), 1);

            methodContents.Append("{");

            if (method.Statements != null)
            {
                foreach (var statement in method.Statements)
                {
                    methodContents.Append(statement.Content);
                }
            }

            methodContents.Append("}");

            return methodContents.ToString();
        }
    }
}
