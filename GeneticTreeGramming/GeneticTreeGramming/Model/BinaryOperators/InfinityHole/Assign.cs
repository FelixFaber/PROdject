using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.BinaryOperators.InfinityHole
{
    public class Assign : BinaryOperator
    {
        public override double Evaluate()
        {
            return right.Evaluate();
        }
    }
}
