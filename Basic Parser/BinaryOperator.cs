using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class BinaryOperator : Token
    {
        public TokenTypes Operator;
        public Token lhs;
        public Token rhs;

        public BinaryOperator(Token lhs, Token rhs, TokenTypes Operator) 
        { 
            Type = TokenTypes.BinaryOperator;
            this.lhs = lhs;
            this.rhs = rhs;
            this.Operator = Operator;
            Value = calculateValue();
        }

        double calculateValue()
        {
            switch (Operator)
            {
                case TokenTypes.Plus:
                    return lhs.Value + rhs.Value;

                case TokenTypes.Minus:
                    return lhs.Value - rhs.Value;

                case TokenTypes.Multiply:
                    return lhs.Value * rhs.Value;

                case TokenTypes.Divide:
                    return lhs.Value / rhs.Value;

                case TokenTypes.DividedInto:
                    return rhs.Value / lhs.Value;

                case TokenTypes.Exponent:
                    return Math.Pow(lhs.Value, rhs.Value);

                case TokenTypes.Root:
                    return Math.Pow(rhs.Value, 1/lhs.Value);

                case TokenTypes.Underscore:
                    return lhs.Value * 10 + rhs.Value;

                default:
                    throw new NotImplementedException();
            }
        }

        public static bool treeIsEqual (Token node1, Token node2)
        {
            //If both nodes are binary, check they are the same 
            if ( node1.GetType().Equals(typeof(BinaryOperator)) && node2.GetType().Equals(typeof(BinaryOperator)) )
            {
                Token.TokenTypes node1Operator = ((BinaryOperator)node1).Operator;
                Token.TokenTypes node2Operator = ((BinaryOperator)node2).Operator;

                //First check operators are same
                if (node1Operator != node2Operator )
                {
                    return false;
                }

                Token node1Left = ((BinaryOperator)node1).lhs;
                Token node1Right = ((BinaryOperator)node1).lhs;

                Token node2Left = ((BinaryOperator)node2).lhs;
                Token node2Right = ((BinaryOperator)node2).lhs;

                //Check left and right hand side of each respectively is the same
                return treeIsEqual(node1Left, node2Left) && treeIsEqual(node1Right, node2Right);
            }
            //If both nodes are literal numbers, check value is the same
            else if( !(node1.GetType().Equals(typeof(BinaryOperator))) && !(node2.GetType().Equals(typeof(BinaryOperator))))
            {
                return node1.Value == node2.Value;
            }
            else
            {
                return false;
            }
        }
    }
}
