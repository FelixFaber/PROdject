using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model
{
    public abstract class BinaryOperator : ExpressionTree
    {
        protected ExpressionTree left, right;

        public void Init(ExpressionTree left, ExpressionTree right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
