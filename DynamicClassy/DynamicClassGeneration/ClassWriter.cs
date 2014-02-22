using DynamicClassGeneration.SyntaxTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public class ClassWriter
    {
        private DirectoryInfo _outputFolder;

        public ClassWriter(DirectoryInfo outputFolder)
        {
            _outputFolder = outputFolder;
        }
        public bool WriteClass(ref RootClass classToWrite, out string filePath)
        {
            if (classToWrite == null)
                throw new ArgumentNullException("classToWrite", "IClass is required");
            try
            {
                var fileName = classToWrite.Name + ".cs";


                filePath = "C:/" + fileName;

                return true;
            }
            catch (Exception)
            {
                filePath = null;
                return false;
            }
        }
        public string GetClassContent(ref RootClass classToWrite)
        {
            PreProcess(ref classToWrite);
            return GetClassAsString(classToWrite);
        }



        //TODO: Lazy ref, should make clone
        private void PreProcess(ref RootClass classToProcess)
        {
            if (string.IsNullOrWhiteSpace(classToProcess.Name))
                throw new NullReferenceException("Missing Class Name");
            if (string.IsNullOrWhiteSpace(classToProcess.Namespace))
                throw new NullReferenceException("Missing Class Namespace");

            if (classToProcess.UsingClauses == null) classToProcess.UsingClauses = new List<UsingClause>();
            if (classToProcess.Methods == null) classToProcess.Methods = new List<Method>();
        }
        private string GetClassAsString(RootClass classToConvert)
        {
            StringBuilder classContents = new StringBuilder();

            foreach (var statement in classToConvert.UsingClauses)
            {
                classContents.Append("using " + statement.Name + ";");
            }

            classContents.Append("namespace " + classToConvert.Namespace);
            classContents.Append("{");

            classContents.Append(string.Format("{0} class {1}", classToConvert.AccessModifier.GetString(), classToConvert.Name));
            classContents.Append("{");

            foreach (var method in classToConvert.Methods)
            {
                classContents.Append(GetMethodAsString(method));
            }

            classContents.Append("}");
            classContents.Append("}");

            return classContents.ToString();
        }
        private string GetMethodAsString(Method method)
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
