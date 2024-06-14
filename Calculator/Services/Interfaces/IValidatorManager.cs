using Calculator.Models;

namespace Calculator.Services.Interfaces
{
    public interface IValidatorManager
    {
        Result ValidateExpression(string source);
        Result ValidateFunction(string source);
        Result ValidateVariable(string source);
    }
}
