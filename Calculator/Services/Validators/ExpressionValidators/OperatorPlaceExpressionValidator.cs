using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators.ExpressionValidators
{
    public class OperatorPlaceExpressionValidator : AbstractExpressionValidator
    {
        private const string WrongOperatorPlaceErrorMessage = "Wrong operator place";
        private const string Operators = "+*/-^";
        private const string OperatorsWithoutMinus = "+*/^";

        public override void Validate(string source, Result result)
        {
            if (IsWrongOperatorPlace(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(WrongOperatorPlaceErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsWrongOperatorPlace(string source)
        {
            if (OperatorsWithoutMinus.Contains(source[0]) || Operators.Contains(source[^1]))
            {
                return true;
            }

            for (int i = 1; i < source.Length - 1; i++)
            {
                if (Operators.Contains(source[i]) && source[i + 1] == ')')
                {
                    return true;
                }

                if (OperatorsWithoutMinus.Contains(source[i]) && source[i - 1] == '(')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
