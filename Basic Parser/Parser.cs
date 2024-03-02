using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    internal class Parser
    {
        List<Token> tokens;
        int cursor = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        Token current()
        {
            return tokens[cursor];
        }

        Token peek(int n = 1)
        {
            return tokens[cursor + n];
        }

        void eatToken(Token.TokenTypes tokenType)
        {
            if (tokenType == this.current().Type) { }
        }
    }
}
