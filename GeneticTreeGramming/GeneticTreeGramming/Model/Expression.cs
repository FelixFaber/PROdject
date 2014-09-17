using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTreeGramming.Model
{
    public class Expression
    {
        private static readonly Random _random = new Random();

        private ExpressionTree _expressionTree;

        private Expression() { }

        public double Evaluate()
        {
            return _expressionTree.Evaluate();
        }

        public static Expression CreateRandomExpression()
        {
            var expression = new Expression();

            var rootNode = GetRandomTreeNode();
            expression._expressionTree = rootNode;

            return expression;
        }

        private static ExpressionTree GetRandomTreeNode()
        {
            int randValue = _random.Next(5);

            if (randValue <= 1)
            {
                var operand = GetRandomOperand();

                if (operand is GeneticTreeGramming.Model.Operands.Double)
                {
                    ((GeneticTreeGramming.Model.Operands.Double)operand).Value = _random.NextDouble() + _random.Next(11);
                }
                else if (operand is GeneticTreeGramming.Model.Operands.Integer)
                {
                    ((GeneticTreeGramming.Model.Operands.Integer)operand).Value = _random.Next(11);
                }

                return operand;
            }
            else
            {
                var binaryOp = GetRandomBinaryOperator();
                var left = GetRandomTreeNode();
                var right = GetRandomTreeNode();
                binaryOp.Init(left, right);
                return binaryOp;
            }
        }


        private static T CreateRandomInstance<T>(List<Type> types)
        {
            int randValue = _random.Next(0, types.Count);
            var type = types[randValue];
            var instance = (T)Activator.CreateInstance(type);
            return instance;
        }

        private static Operand GetRandomOperand()
        {
            var @namespace = "GeneticTreeGramming.Model.Operands";
            var allOperandTypes = GetAllTypesInNamespace(@namespace);
            return CreateRandomInstance<Operand>(allOperandTypes);
        }

        private static BinaryOperator GetRandomBinaryOperator()
        {
            var @namespace = "GeneticTreeGramming.Model.BinaryOperators";
            var allBinaryOperatorTypes = GetAllTypesInNamespace(@namespace);
            return CreateRandomInstance<BinaryOperator>(allBinaryOperatorTypes);
        }

        private static List<Type> GetAllTypesInNamespace(string @namespace)
        {
            var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace == @namespace
                    select type;

            return types.ToList();
        }
    }
}
