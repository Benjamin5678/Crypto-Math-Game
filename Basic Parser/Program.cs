using Basic_Parser;
using Newtonsoft.Json;

Lexer lexer = new Lexer();

string expression = "1 2 3 4 5";
List<Token> tokens = lexer.tokenize(expression);

Parser parser = new Parser(tokens);

Token ast = parser.parse();

string json = JsonConvert.SerializeObject(ast);
Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented));