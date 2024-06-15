using Calculator.Models;
using Calculator.Models.Enum;

namespace Calculator.Services.Validators.FunctionValidators
{
    public class EqualsFunctionValidator : AbstractFunctionValidator
    {
        private const string EqualsFunctionValidateErrorMessage = "Equal is missed in function";

        public override void Validate(string source, Result result)
        {
            if (!IsSingleEqualInFunction(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(EqualsFunctionValidateErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsSingleEqualInFunction(string source)
        {
            return source.Count(c => c == '=') == 1;
        }
    }
}
