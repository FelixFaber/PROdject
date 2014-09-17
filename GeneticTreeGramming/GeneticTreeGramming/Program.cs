using GeneticTreeGramming.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = Expression.CreateRandomExpression();
            var result = expression.Evaluate();
        }
    }
}
