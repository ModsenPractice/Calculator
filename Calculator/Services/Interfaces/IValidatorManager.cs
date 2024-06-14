using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    public interface IValidatorManager
    {
        Result ValidateExpression(string source);

        Result ValidateFunction(string source);

        Result ValidateVariable(string source);
    }
}
