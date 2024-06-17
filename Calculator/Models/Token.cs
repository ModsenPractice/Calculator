using Calculator.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; } = null!;

        public Token() { }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Token token &&
                   Type == token.Type &&
                   Value == token.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

    }
}
