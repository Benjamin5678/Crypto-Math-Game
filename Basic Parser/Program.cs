using Basic_Parser;
using Newtonsoft.Json;
using Spectre.Console;

//###Main Code###

Game game = new Game();
string input;

Console.WriteLine("Crypto! Math Game");

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