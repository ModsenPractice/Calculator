using Calculator.Models;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Parsing
{
    public class VariableParser : IParser<Variable>
    {
        public Variable Parse(string input)
        {
            var parts = input.Split('=');

            Variable variable = new()
            {
                Name = parts[0],
                Value = parts[1]
            };

            return variable;
        }
    }
}
