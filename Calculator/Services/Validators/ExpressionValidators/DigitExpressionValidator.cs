using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators.ExpressionValidators
{
    public class DigitExpressionValidator : AbstractExpressionValidator
    {
        private const string DontContainDigitsOrVariableErrorException = "Expression contain special simbols";

        public override void Validate(string source, Result result)
        {
            if (IsDontContainDigitsOrVariable(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(DontContainDigitsOrVariableErrorException);
            }

            base.Validate(source, result);
        }

        private bool IsDontContainDigitsOrVariable(string source)
        {
            foreach (var sourceChar in source)
            {
                if (char.IsLetter(sourceChar) || char.IsDigit(sourceChar))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
