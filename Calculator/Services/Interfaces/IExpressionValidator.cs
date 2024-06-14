using Calculator.Models;

namespace Calculator.Services.Interfaces
{
    public interface IExpressionValidator
    {
        void Validate(string source, Result result);
        IExpressionValidator SetNextValidator(IExpressionValidator expressionValidator);
    }
}
