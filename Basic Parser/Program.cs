using Basic_Parser;

Lexer lexer = new Lexer();

string expression = "3 ^/9 * 5L7R2  +-3  ";
List<Token> tokens = lexer.tokenize(expression);

foreach (Token token in tokens)
{
    Console.Write(token.Type);

    if (token.Type == Token.TokenTypes.Integer)
    {
        Console.Write($": {token.Value}");
    }

    Console.WriteLine();
}