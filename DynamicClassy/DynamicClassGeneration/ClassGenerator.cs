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
        public static string GetClassAsString(ref RootClass classToWrite)
        {
            if (classToWrite == null)
                throw new ArgumentNullException("classToWrite", "IClass is required");

            PreProcess(ref classToWrite);
            return GetClassAsString(classToWrite);
        }

        //TODO: Lazy ref, should make clone
        private static void PreProcess(ref RootClass classToProcess)
        {
            if (string.IsNullOrWhiteSpace(classToProcess.Name))
                throw new NullReferenceException("Missing Class Name");
            if (string.IsNullOrWhiteSpace(classToProcess.Namespace))
                throw new NullReferenceException("Missing Class Namespace");

            if (classToProcess.UsingClauses == null) classToProcess.UsingClauses = new List<UsingClause>();
            if (classToProcess.Methods == null) classToProcess.Methods = new List<Method>();
            if (classToProcess.Interfaces == null) classToProcess.Interfaces = new List<Interface>();
        }
        private static string GetClassAsString(RootClass classToConvert)
        {
            StringBuilder classContents = new StringBuilder();

            foreach (var statement in classToConvert.UsingClauses)
            {
                classContents.Append("using " + statement.Name + ";");
            }

            classContents.Append("namespace " + classToConvert.Namespace);
            classContents.Append("{");

            classContents.Append(string.Format("{0} class {1}", classToConvert.AccessModifier.GetString(), classToConvert.Name));
            classContents.Append(GetClassInheritence(classToConvert));
            classContents.Append("{");

            foreach (var method in classToConvert.Methods)
            {
                classContents.Append(GetMethodAsString(method));
            }

            classContents.Append("}");
            classContents.Append("}");

            return classContents.ToString();
        }
        private static string GetClassInheritence(RootClass classToConvert)
        {
            StringBuilder inheritence = new StringBuilder();

            
            bool hasBaseClass = !string.IsNullOrWhiteSpace(classToConvert.BaseClassName);
            bool hasInterfaces = classToConvert.Interfaces.Count() > 0;

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

            string inheritenceString = inheritence.ToString();
            if(inheritenceString.Contains(','))
                inheritenceString = inheritenceString.Remove(inheritenceString.LastIndexOf(','));
            return inheritenceString;
        }
        private static string GetMethodAsString(Method method)
        {
            StringBuilder methodContents = new StringBuilder();

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
