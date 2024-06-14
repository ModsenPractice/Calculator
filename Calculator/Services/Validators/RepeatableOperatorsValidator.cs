using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators
{
    public class RepeatableOperatorsValidator : AbstractValidator
    {
        private const string RepeatableOperatorsErrorMessage = "";
        private const string Operators = "+*/-^";

        public override void Validate(string source, Result result)
        {
            if (IsContainRepitableOperators(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(RepeatableOperatorsErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsContainRepitableOperators(string source)
        {
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (Operators.Contains(source[i]) && Operators.Contains(source[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
