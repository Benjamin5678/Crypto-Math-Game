using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Parser
{
    public class Token
    {
        public enum TokenTypes
        {
            Plus,
            Minus,
            Multiply,
            Divide,
            Exponent,
            DividedInto,
            Root,
            Integer,
            EOF
        }

        public TokenTypes Type { get; set; }
        public int Value { get; set; }
    }
}
