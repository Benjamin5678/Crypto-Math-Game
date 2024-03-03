using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class Parser
    {
        List<Token> tokens;
        int cursor = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        Token current()
        {
            return tokens[cursor];
        }

        Token peek(int n = 1)
        {
            return tokens[cursor + n];
        }

        void eatToken(Token.TokenTypes tokenType)
        {
            if (tokenType == this.current().Type)
            {
                this.cursor++;
            }
            else
            {
                Console.WriteLine("Unexpected token type");
            }
        }

        public Token parse()
        {
            Token leftHandSide = this.parseExpression();

            return leftHandSide;
        }

        // addition/subtraction
        Token parseExpression()
        {
            Token leftHandSide = this.parseTerm();

            while (this.current().Type == Token.TokenTypes.Plus || this.current().Type == Token.TokenTypes.Minus)
            {
                Token.TokenTypes ttype = (this.current().Type == Token.TokenTypes.Plus) ? Token.TokenTypes.Plus : Token.TokenTypes.Minus;

                this.eatToken(ttype);
                Token rightHandSide = this.parseTerm();

                leftHandSide = new BinaryOperator(leftHandSide, rightHandSide, ttype);
            }

            return leftHandSide;
        }

        // multiplecation/division
        Token parseTerm()
        {
            Token leftHandSide = this.parseFactor();

            while (this.current().Type == Token.TokenTypes.Multiply || this.current().Type == Token.TokenTypes.Divide)
            {
                Token.TokenTypes ttype = (this.current().Type == Token.TokenTypes.Multiply) ? Token.TokenTypes.Multiply : Token.TokenTypes.Divide;

                this.eatToken(ttype);
                Token rightHandSide = this.parseFactor();

                leftHandSide = new BinaryOperator(leftHandSide, rightHandSide, ttype);
            }

            return leftHandSide;
        }

        //Highest precedence
        Token parseFactor()
        {
            if (this.current().Type == Token.TokenTypes.Integer)
            {
                Token literal = new Token { Type = Token.TokenTypes.Integer, Value = this.current().Value };
                this.eatToken(Token.TokenTypes.Integer);
                return literal;
            }

            //paranthesis
            if (this.current().Type == Token.TokenTypes.LParen)
            {
                this.eatToken(Token.TokenTypes.LParen);
                Token expr = this.parseExpression();
                this.eatToken(Token.TokenTypes.RParen);

                return expr;
            }

            throw new Exception("Expected a parenthesis token or an integer");
        }
    }
}
