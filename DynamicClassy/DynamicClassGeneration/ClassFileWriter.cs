using DynamicClassGeneration.SyntaxTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public class ClassFileWriter
    {
        private DirectoryInfo _outputFolder;

        public ClassFileWriter(DirectoryInfo outputFolder)
        {
            _outputFolder = outputFolder;
        }

        public bool WriteClassToFile(ref RootClass classToWrite, out string filePath, Encoding encoding)
        {
            if (classToWrite == null)
                throw new ArgumentNullException("classToWrite", "RootClass is required");
            if (_outputFolder == null)
                throw new NullReferenceException("Target Directory is required");
            try
            {
                var fileName = classToWrite.Name + ".cs";
                var fileContents = ClassGenerator.GetClassAsString(ref classToWrite);
                var writePath = Path.Combine(_outputFolder.FullName, fileName);

                File.WriteAllText(writePath, fileContents, encoding);

                filePath = writePath;
                return true;
            }
            catch (Exception)
            {
                filePath = null;
                return false;
            }
        }
    }
}
