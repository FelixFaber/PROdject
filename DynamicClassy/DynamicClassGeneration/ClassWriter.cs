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
        public bool WriteClass(IClass classToWrite, out string filePath)
        {
            try
            {
                var fileName = classToWrite.Name + ".cs";

                var fileContent = GetClassAsString(classToWrite);


                filePath = "C:/" + fileName;

                return true;
            }
            catch (Exception e)
            {
                filePath = null;
                return false;
            }
        }
        private string GetClassAsString(IClass classToConvert)
        {
            StringBuilder classContents = new StringBuilder();

            if(classToConvert.Components != null)
            {
                foreach (var component in classToConvert.Components)
                {
                    classContents.AppendLine(GetComponentsAsString(component));
                    classContents.AppendLine(string.Empty);
                }
            }

            return classContents.ToString();
        }
        private string GetComponentsAsString(IClassComponent component)
        {
            StringBuilder componentContents = new StringBuilder();

            var accessModifier = component.AccessModifier.GetString();
            
            componentContents.AppendLine(accessModifier+" "+component.Name);


            return componentContents.ToString();
        }
    }
}
