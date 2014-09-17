using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.BinaryOperators.InfinityHole
{
    public class Pow : BinaryOperator
    {
        public override double Evaluate()
        {
            return Math.Pow(left.Evaluate(), right.Evaluate());
        }
    }
}
