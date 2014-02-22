using DynamicClassGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public class ClassWriter : IClassWriter
    {
        private DirectoryInfo _outputFolder;

        public ClassWriter(DirectoryInfo outputFolder)
        {
            _outputFolder = outputFolder;
        }
        public bool WriteClass(ref IClass classToWrite, out string filePath)
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
        public string GetClassContent(ref IClass classToWrite)
        {
            PreProcess(ref classToWrite);
            return GetClassAsString(classToWrite);
        }



        //TODO: Lazy ref, should make clone
        private void PreProcess(ref IClass classToProcess)
        {
            if (string.IsNullOrWhiteSpace(classToProcess.Name))
                throw new NullReferenceException("Missing Class Name");
            if (string.IsNullOrWhiteSpace(classToProcess.Namespace))
                throw new NullReferenceException("Missing Class Namespace");

            if (classToProcess.UsingStatements == null) classToProcess.UsingStatements = new List<IUsingStatement>();
            if (classToProcess.Methods == null) classToProcess.Methods = new List<IMethod>();
        }
        private string GetClassAsString(IClass classToConvert)
        {
            StringBuilder classContents = new StringBuilder();

            foreach (var statement in classToConvert.UsingStatements)
            {
                classContents.AppendLine("using" + statement.Name + ";");
            }
            classContents.AppendLine(string.Empty);

            classContents.AppendLine("namespace " + classToConvert.Namespace);
            classContents.AppendLine("{");

            classContents.AppendLine(string.Format("{0} class {1}", classToConvert.AccessModifier.GetString(), classToConvert.Name));
            classContents.AppendLine("{");
            classContents.AppendLine(string.Empty);

            foreach (var method in classToConvert.Methods)
            {
                classContents.AppendLine(GetMethodAsString(method));
                classContents.AppendLine(string.Empty);
            }

            classContents.AppendLine("}");
            classContents.AppendLine("}");

            return classContents.ToString();
        }
        private string GetMethodAsString(IMethod method)
        {
            StringBuilder methodContents = new StringBuilder();

            var accessModifier = method.AccessModifier.GetString();

            methodContents.AppendLine(accessModifier + " " + method.Name);


            return methodContents.ToString();
        }
    }
}
