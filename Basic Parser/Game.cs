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

        public enum difficulties
        {
            easy = 2, //Plus and Minus
            medium = 4, //Multiply and Divide
            hard = 8 //
        }

        public bool validateInput(string input)
        {
            List<Token> tokens = lexer.tokenize(input);

            //Same numbers as puzzle?
            int i = 0;
            foreach(Token t in tokens)
            {
                if(t.Type == Token.TokenTypes.Integer)
                {
                    if (numbers.Count < (i + 1) || t.Value != numbers[i].Value)
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

            Console.WriteLine( evaluate(numbers) );

            //Evaluation is same?
            if ( evaluate(tokens) != target)
            {
                return false;
            }

            //Solution found!
            
            //Add solution
            Parser parser = new Parser(tokens);
            Token ast = parser.parse();
            foreach (Token solution in solutionsFound)
            {
                if (BinaryOperator.treeIsEqual((BinaryOperator) solution, (BinaryOperator) ast)){
                    return false;
                }
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

        public void generateTarget(difficulties difficulty)
        {
            List<Token> exampleExpression = new List<Token>();

            int diff = (int)difficulty; //The ammount of operators used. 8 is all. 2 is + and -. See Token.TokenTypes

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                exampleExpression.Add(numbers[i]);
                exampleExpression.Add(new Token { Type = (Token.TokenTypes)rand.Next(0, diff) }); //add random operator
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
                generateTarget(difficulty);
            }
            

            if ( target != evaluation || evaluation > 60 || evaluation < 1) //make sure puzzle is a reasonable number
            {
                generateTarget(difficulty);
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
