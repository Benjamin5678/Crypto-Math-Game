using Basic_Parser;
using Newtonsoft.Json;

Game game = new Game();

string expression = "(1 + 2) * 3 L 4_ 5";
game.generateNumbers();
foreach(var number in game.numbers)
{
    Console.WriteLine(number.Value);
}
double evaluation = game.evaluateInput(expression);

Console.WriteLine(evaluation);