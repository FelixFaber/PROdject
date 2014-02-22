using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration.Interfaces
{
    public interface IClassWriter
    {
        bool WriteClass(IClass classToWrite, out string filePath);
    }
}
