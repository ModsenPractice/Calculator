using Calculator.Models;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Validators
{
    public class AbstractValidator : IValidator
    {
        private IValidator? _nextValidator;

        public IValidator SetNextValidator(IValidator validator)
        {
            _nextValidator = validator;
            return validator;
        }

        public virtual void Validate(string source, Result result)
        {
            _nextValidator?.Validate(source, result);
        }
    }
}
