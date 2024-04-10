using Basic_Parser;
using Newtonsoft.Json;
using Spectre.Console;

//###Main Code###

Game game = new Game();
string input;

//Information
Console.WriteLine("Crypto! Math Game");
Console.WriteLine("Simply use the symbols below to make the expression true. It's trickier than it looks!\n");
Console.WriteLine
    ("Operators:\n" +
    "+ | Plus (1 point)\n" +
    "- | Minus (1 point) (Can't be used to negate first number)\n" +
    "* | Multiply (2 points)\n" +
    "/ | Divide (2 points)\n" +
    "L | Divided Into (3 points) | 5 L 10 = 2\n" +
    "^ | Exponent (5 points) | 2 ^ 3 = 8\n" +
    "R | Root (5 point) | 3 R 8 = 2\n" +
    "_ | Combine Numbers (5 points) | 5 _ 2 = 52\n" +
    "() | Parenthesis (1 point each) (Can't be used for multiplication)\n");

//Select Difficulty
Console.Write("Which difficulty? (easy, medium, hard) -> ");

input = Console.ReadLine();

string difficulty = input;


//Generate Puzzle
game.generateNumbers();
Table numberTable = new Table();
foreach (var number in game.numbers)
{
    numberTable.AddColumn(number.Value.ToString());
}
numberTable.AddColumn("=");

game.generateTarget(game.difficulties[difficulty]);
numberTable.AddColumn(game.target.ToString());

AnsiConsole.Write(numberTable);

//Main Game Loop
while (true)
{
    Console.Write("Input an answer: ");
    input = Console.ReadLine();

    bool correct = game.validateInput(input);

    if (correct)
    {
        Console.WriteLine("Correct!");
    }
    else
    {
        Console.WriteLine("Incorrect or already found.");
    }

    Console.WriteLine($"Solutions found: {game.solutionsFound.Count}");
    Console.WriteLine($"Score: {game.score}");

    Console.WriteLine();
}


//###Parser Test Code###

//string expr = "2 + 3 _ 2 * 2";
//Lexer lexer = new Lexer();
//List<Token> tokens = lexer.tokenize(expr);
//Parser parser = new Parser(tokens);
//Token ast = parser.parse();

//Console.WriteLine(ast.Value);