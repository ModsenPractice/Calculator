using Calculator.Models;

namespace Calculator.Services.Interfaces
{
    public interface IVariableValidator
    {
        void Validate(string source, Result result);
        IVariableValidator SetNextValidator(IVariableValidator expressionValidator);
    }
}
