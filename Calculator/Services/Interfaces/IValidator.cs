using Calculator.Models;

namespace Calculator.Services.Interfaces
{
    public interface IValidator
    {
        void Validate(string source, Result result);
        IValidator SetNextValidator(IValidator validator);
    }
}
