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

        int calculateValue()
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

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
