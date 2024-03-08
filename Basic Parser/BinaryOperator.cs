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
                    if ((10 * lhs.Value + rhs.Value) > 999)
                    {
                        throw new Exception("Tried to combine a 4 digit number with underscores against the rules.");
                    }
                    return 10 * lhs.Value + rhs.Value;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
