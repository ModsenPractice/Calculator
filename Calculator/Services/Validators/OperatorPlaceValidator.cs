using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators
{
    public class OperatorPlaceValidator : AbstractValidator
    {
        private const string WrongOperatorPlaceErrorMessage = "";
        private const string Operators = "+*/-";
        private const string OperatorsWithouMinus = "+*/";

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
            if (OperatorsWithouMinus.Contains(source[0]) || Operators.Contains(source[^1]))
            {
                return true;
            }

            for (int i = 1; i < source.Length - 1; i++)
            {
                if (Operators.Contains(source[i]) && source[i + 1] == ')')
                {
                    return true;
                }

                if (OperatorsWithouMinus.Contains(source[i]) && source[i - 1] == '(')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
