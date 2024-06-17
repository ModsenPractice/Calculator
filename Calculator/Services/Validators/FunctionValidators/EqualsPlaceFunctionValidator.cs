using Calculator.Models;
using Calculator.Models.Enum;

namespace Calculator.Services.Validators.FunctionValidators
{
    public class EqualsPlaceFunctionValidator : AbstractFunctionValidator
    {
        private const string EqualsPlaceValidateErrorMessage = "Equal should place after closed bracket"; 

        public override void Validate(string source, Result result)
        {
            if (IsWrongEqualsPlaceInFunction(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(EqualsPlaceValidateErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsWrongEqualsPlaceInFunction(string source)
        {
            for (int i = 1; i < source.Length; i++)
            {
                if (source[i - 1] == ')' && source[i] == '=')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
