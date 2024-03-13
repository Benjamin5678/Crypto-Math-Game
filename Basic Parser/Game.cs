using Newtonsoft.Json.Linq;
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
        Random rand = new Random();

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

        public void generateTarget()
        {
            List<Token> exampleExpression = new List<Token>();

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                exampleExpression.Add(numbers[i]);
                exampleExpression.Add(new Token { Type = (Token.TokenTypes)rand.Next(0, 7) });
            }
            exampleExpression.Add(numbers[numbers.Count - 1]);
            exampleExpression.Add(new Token { Type = Token.TokenTypes.EOF });

            Parser parser = new Parser(exampleExpression);
            Token ast = parser.parse();

            target = Convert.ToInt32(ast.Value);

            if ( target != ast.Value || ast.Value > 60)
            {
                Console.WriteLine("Regenerating");
                generateTarget();
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
