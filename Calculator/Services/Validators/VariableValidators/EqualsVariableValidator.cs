using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Models.Enum;

namespace Calculator.Services.Validators.VariableValidators
{
    public class EqualsVariableValidator : AbstractVariableValidator
    {
        private const string EqualsVariableValidateErrorMessage = "Equal is missed in variable";

        public override void Validate(string source, Result result)
        {
            if (!IsSingleEqualInVariable(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(EqualsVariableValidateErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsSingleEqualInVariable(string source)
        {
            return source.Count(c => c == '=') == 1;
        }
    }
}
