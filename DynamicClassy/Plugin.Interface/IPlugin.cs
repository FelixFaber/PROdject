using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Interfaces
{
    public interface IPlugin
    {
        void Configure();
        void PreProcess();
        void Process();
        IPluginResult GetResult();
    }
}
