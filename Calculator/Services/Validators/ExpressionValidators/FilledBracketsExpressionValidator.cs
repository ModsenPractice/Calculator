using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators.ExpressionValidators
{
    public class FilledBracketsExpressionValidator : AbstractExpressionValidator
    {
        private const string OpenedBracketsContainNothingErrorMessage = "Expression contain brackets with nothing";

        public override void Validate(string source, Result result)
        {
            if (IsOpenedBracketsContainNothing(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(OpenedBracketsContainNothingErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsOpenedBracketsContainNothing(string source)
        {
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (source[i] == '(' && source[i + 1] == ')')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
