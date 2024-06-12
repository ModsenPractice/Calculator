using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators
{
    public class RepitableOperatorsValidator : AbstractValidator
    {
        private const string RepitableOperatorsErrorMessage = "";
        private const string Operators = "+*/";

        public override void Validate(string source, Result result)
        {
            if (IsContainRepitableOperators(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(RepitableOperatorsErrorMessage);
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
