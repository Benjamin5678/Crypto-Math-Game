using Basic_Parser;
using Newtonsoft.Json;
using Spectre.Console;

Game game = new Game();

Console.WriteLine("Crypto! Math Game");

bool easyMode = false;
Console.Write("Use easy mode? (enter yes) > ");
if (Console.ReadLine() == "yes") { easyMode = true; }

game.generateNumbers();
Table numberTable = new Table();
foreach(var number in game.numbers)
{
    numberTable.AddColumn(number.Value.ToString());
}
numberTable.AddColumn("=");

game.generateTarget(easyMode);
numberTable.AddColumn(game.target.ToString());

AnsiConsole.Write(numberTable);

while (true)
{
    Console.Write("Input an answer: ");
    string input = Console.ReadLine();

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
}