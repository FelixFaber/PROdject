using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model.BinaryOperators
{
    public class Divide : BinaryOperator
    {
        public override double Evaluate()
        {
            var rightVal = right.Evaluate();

            if (rightVal == 0)
                throw new DivideByZeroException();

            return left.Evaluate() / right.Evaluate();
        }
    }
}
