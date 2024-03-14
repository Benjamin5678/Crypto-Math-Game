using Basic_Parser;
using Newtonsoft.Json;

Game game = new Game();

game.generateNumbers();
foreach(var number in game.numbers)
{
    Console.WriteLine(number.Value);
}

game.generateTarget();
Console.WriteLine(game.target);

Console.Write("Input an answer: ");
string input = Console.ReadLine();

bool win = game.validateInput(input);

if (win)
{
    Console.WriteLine("You win!");
}
else
{
    Console.WriteLine("You lost!");
}