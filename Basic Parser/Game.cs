using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class Game
    {
        Lexer lexer = new Lexer();

        public List<Token> numbers = new List<Token>();
        public int target;

        public void generateNumbers(int ammount = 5)
        {
            Random rand = new Random();
            for (int i =  0; i < ammount; i++)
            {
                numbers.Add(new Token { Type = Token.TokenTypes.Integer, Value = rand.Next(1, 10) } );
            }
        }

        public double evaluateInput(string input)
        {
            List<Token> tokens = lexer.tokenize(input);
            Parser parser = new Parser(tokens);
            Token ast = parser.parse();
            return ast.Value;
        }
    }
}
