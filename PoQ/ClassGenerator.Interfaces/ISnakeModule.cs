using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Interfaces
{
    public interface ISnakeModule : IModule
    {
        float AppleDesireCoefficient { get; }
        int SnakeSafetyZoneWidth { get; }
    }
}
