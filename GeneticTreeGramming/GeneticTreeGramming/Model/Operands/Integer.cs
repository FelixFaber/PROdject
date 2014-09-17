using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.Operands
{
    public class Integer : Operand
    {
        public int Value { get; set; }

        public override double Evaluate()
        {
            return (double)Value;
        }
    }
}
