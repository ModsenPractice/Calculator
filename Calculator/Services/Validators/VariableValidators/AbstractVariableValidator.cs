using Calculator.Models;
using Calculator.Services.Interfaces;

namespace Calculator.Services.Validators.VariableValidators
{
    public abstract class AbstractVariableValidator : IVariableValidator
    {
        private IVariableValidator? _nextValidator;

        public IVariableValidator SetNextValidator(IVariableValidator functionValidator)
        {
            _nextValidator = functionValidator;
            return functionValidator;
        }

        public virtual void Validate(string source, Result result)
        {
            _nextValidator?.Validate(source, result);
        }
    }
}
