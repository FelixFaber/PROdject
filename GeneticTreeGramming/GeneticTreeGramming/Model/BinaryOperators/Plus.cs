using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.BinaryOperators
{
    public class Plus : BinaryOperator
    {
        public override double Evaluate()
        {
            return left.Evaluate() + right.Evaluate();
        }
    }
}
