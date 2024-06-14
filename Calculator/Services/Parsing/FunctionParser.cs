using Calculator.Models;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Parsing
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(string input)
        {
            var parts = input.Split('=');
            var declarationParts = parts[0].Split('(');

            var name = declarationParts[0];
            var paramNames = declarationParts[1].TrimEnd(')').Split(',');
            var expression =parts[1];

            Function func = new() { Expression = expression, Name = name, Params = paramNames };

            return func;
        }
    }
}
