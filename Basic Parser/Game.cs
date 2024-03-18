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

        public List<Token> solutionsFound = new List<Token>();

        public bool validateInput(string input)
        {
            List<Token> tokens = lexer.tokenize(input);

            //Same numbers as puzzle?
            int i = 0;
            foreach(Token t in tokens)
            {
                if(t.Type == Token.TokenTypes.Integer)
                {
                    if (numbers.Count > (i + 1) || t.Value != numbers[i].Value)
                    {
                        return false;
                    }
                    i++;
                }
            }

            //Same ammount of numbers?
            if ( i != numbers.Count)
            {
                return false;
            }

            //Evaluation is same?
            if ( evaluate(tokens) != target)
            {
                return false;
            }

            //Solution found!
            
            //Add solution
            Parser parser = new Parser(tokens);
            Token ast = parser.parse();
            if (solutionsFound.Contains(ast)) //Is solution new?
            {
                return false;
            }
            solutionsFound.Add(ast);

            return true;
        }

        public void generateNumbers(int ammount = 5)
        {
            Random rand = new Random();
            for (int i =  0; i < ammount; i++)
            {
                numbers.Add(new Token { Type = Token.TokenTypes.Integer, Value = rand.Next(1, 10) } ); // Random number between 1 and 9
            }
        }

        public void generateTarget(bool easyMode = false)
        {
            List<Token> exampleExpression = new List<Token>();

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                int difficulty = 8; //The ammount of operators used. 8 is all. 2 is + and -. See Token.TokenTypes

                if (easyMode)
                {
                    difficulty = 2;
                }

                exampleExpression.Add(numbers[i]);
                exampleExpression.Add(new Token { Type = (Token.TokenTypes)rand.Next(0, difficulty) }); //add random operator
            }
            exampleExpression.Add(numbers[numbers.Count - 1]);
            exampleExpression.Add(new Token { Type = Token.TokenTypes.EOF });

            double evaluation = evaluate(exampleExpression);

            try
            {
                target = Convert.ToInt32(evaluation);
            }
            catch (OverflowException) //apparently this happens sometimes
            {
                generateTarget();
            }
            

            if ( target != evaluation || evaluation > 60 || evaluation < 1) //make sure puzzle is a reasonable number
            {
                generateTarget();
            }
        }

        public double evaluate(List<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Token ast = parser.parse();
            return ast.Value;
        }

        public double evaluate(string input)
        {
            List<Token> tokens = lexer.tokenize(input);
            return evaluate(tokens);
        }
    }
}
