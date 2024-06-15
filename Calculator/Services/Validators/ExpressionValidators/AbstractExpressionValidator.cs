using Calculator.Models;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Validators.ExpressionValidators
{
    public abstract class AbstractExpressionValidator : IExpressionValidator
    {
        private IExpressionValidator? _nextValidator;

        public IExpressionValidator SetNextValidator(IExpressionValidator expressionValidator)
        {
            _nextValidator = expressionValidator;
            return expressionValidator;
        }

        public virtual void Validate(string source, Result result)
        {
            _nextValidator?.Validate(source, result);
        }
    }
}
