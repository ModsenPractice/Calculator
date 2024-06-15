using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;

namespace Calculator.Services.Interfaces
{
    public interface IFunctionValidator
    {
        void Validate(string source, Result result);
        IFunctionValidator SetNextValidator(IFunctionValidator expressionValidator);
    }
}
