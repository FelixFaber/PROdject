using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.Operands
{
    public class Double : Operand
    {
        public double Value {get;set;}

        public override double Evaluate()
        {
            return Value;
        }
    }
}
