using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Interfaces
{
    public interface IPluginResult
    {
        Dictionary<string, object> ResultSet { get; }
    }
}
