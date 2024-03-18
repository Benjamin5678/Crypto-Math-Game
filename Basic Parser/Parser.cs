using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class Parser
    {
        public List<Token> tokens;
        public int cursor = 0;

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
            Token leftHandSide = this.parsePower();

            while (this.current().Type == Token.TokenTypes.Multiply || this.current().Type == Token.TokenTypes.Divide || this.current().Type == Token.TokenTypes.DividedInto)
            {
                Token.TokenTypes ttype = this.current().Type; // I dont know why the other ones arent like this, this is probably a bad workaround

                this.eatToken(ttype);
                Token rightHandSide = this.parsePower();

                leftHandSide = new BinaryOperator(leftHandSide, rightHandSide, ttype);
            }

            return leftHandSide;
        }

        //powers
        Token parsePower()
        {
            Token leftHandSide = this.parseFactor();

            while (this.current().Type == Token.TokenTypes.Exponent || this.current().Type == Token.TokenTypes.Root)
            {
                Token.TokenTypes ttype = (this.current().Type == Token.TokenTypes.Exponent) ? Token.TokenTypes.Exponent : Token.TokenTypes.Root;

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
                if (this.peek().Type == Token.TokenTypes.Underscore)
                {
                    Token lhs = new Token { Type = Token.TokenTypes.Integer, Value = this.current().Value };
                    this.eatToken(Token.TokenTypes.Integer);
                    this.eatToken(Token.TokenTypes.Underscore);
                    Token rhs = new Token { Type = Token.TokenTypes.Integer, Value = this.current().Value };
                    this.eatToken(Token.TokenTypes.Integer);

                    return new BinaryOperator(lhs, rhs, Token.TokenTypes.Underscore);
                }
                else
                {
                    Token literal = new Token { Type = Token.TokenTypes.Integer, Value = this.current().Value };
                    this.eatToken(Token.TokenTypes.Integer);
                    return literal;
                }
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
