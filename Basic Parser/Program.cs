using Basic_Parser;
using Newtonsoft.Json;

Lexer lexer = new Lexer();

string expression = "10 * ( 10 + 3 * 3 - 2 )";
List<Token> tokens = lexer.tokenize(expression);

Parser parser = new Parser(tokens);

Token ast = parser.parse();

string json = JsonConvert.SerializeObject(ast);
Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented));