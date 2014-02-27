using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Compiler
{
    public class PluginCompilationException : Exception
    {
        CompilerErrorCollection Errors { get;  set; }
        public string CompilationErrors
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                foreach(var error in Errors)
                {
                    errors.AppendLine(error.ToString());
                }

                return errors.ToString();
            }
        }

        public PluginCompilationException(CompilerErrorCollection errors, string message)
            :base(message)
        {
            Errors = errors;
        }
    }
}
