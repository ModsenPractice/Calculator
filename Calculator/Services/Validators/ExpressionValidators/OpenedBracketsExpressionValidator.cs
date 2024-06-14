using Calculator.Models;
using Calculator.Models.Enum;

namespace Calculator.Services.Validators.ExpressionValidators
{
    public class OpenedBracketsExpressionValidator : AbstractExpressionValidator
    {
        private const string OpenBracketsWasntClosedErrorMessage = "Open bracket wasn't closed";

        public override void Validate(string source, Result result)
        {
            if (IsOpenBracketsWasntClosed(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(OpenBracketsWasntClosedErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsOpenBracketsWasntClosed(string source)
        {
            var bracketsCount = 0;

            foreach (char sourceChar in source)
            {
                if (sourceChar == '(')
                {
                    bracketsCount++;
                }
                else if (sourceChar == ')' && bracketsCount-- == 0)
                {
                    return true;
                }
            }

            return bracketsCount != 0;
        }
    }
}
