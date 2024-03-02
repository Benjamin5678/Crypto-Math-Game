using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class Lexer
    {
        string stream = "";
        int cursor = 0;

        char current()
        {
            return this.stream[this.cursor];
        }

        public List<Token> tokenize(string input)
        {
            this.stream = input;
            this.cursor = 0;

            List<Token> tokens = new List<Token>();

            while (this.cursor < this.stream.Length)
            {
                switch (this.current())
                {
                    case ' ':
                        break;

                    case '\n':
                        break;

                    case '\t':
                        break;

                    case '+':
                        tokens.Add(new Token { Type = Token.TokenTypes.Plus });
                        break;
                    
                    case '-':
                        tokens.Add(new Token { Type = Token.TokenTypes.Minus });
                        break;

                    case '*':
                        tokens.Add(new Token { Type = Token.TokenTypes.Multiply });
                        break;

                    case '/':
                        tokens.Add(new Token { Type = Token.TokenTypes.Divide });
                        break;

                    case '^':
                        tokens.Add(new Token { Type = Token.TokenTypes.Exponent });
                        break;

                    case 'L':
                        tokens.Add(new Token { Type = Token.TokenTypes.DividedInto });
                        break;

                    case 'R':
                        tokens.Add(new Token { Type = Token.TokenTypes.Root });
                        break;

                    default:
                        //Check for numeric value
                        if (Char.IsDigit(this.current()))
                        {
                            string strNumber = "";

                            while (this.cursor < this.stream.Length && Char.IsDigit(this.current()))
                            {
                                strNumber += this.current().ToString();
                                this.cursor++;
                            }
                            this.cursor--;

                            tokens.Add(new Token { Type = Token.TokenTypes.Integer, Value = Convert.ToInt32(strNumber)});
                        }
                        else
                        {
                            Console.WriteLine($"Error: Invalid character at position {this.cursor}");
                        }

                        break;
                }

                this.cursor++;
            }

            tokens.Add(new Token { Type = Token.TokenTypes.EOF });

            return tokens;
        }
    }
}
