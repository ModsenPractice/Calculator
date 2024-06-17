using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Services.Interfaces;

namespace Calculator.Services.Validators.FunctionValidators
{
    public abstract class AbstractFunctionValidator : IFunctionValidator
    {
        private IFunctionValidator? _nextValidator;

        public IFunctionValidator SetNextValidator(IFunctionValidator functionValidator)
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
