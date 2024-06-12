using Calculator.Models.Enum;
using Calculator.Models;

namespace Calculator.Services.Validators
{
    public class OperatorPlaceValidator : AbstractValidator
    {
        private const string WrongOperatorPlaceErrorMessage = "";
        private const string Operators = "+*/";

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
            if (Operators.Contains(source[0]) || (source[0] == '(' && Operators.Contains(source[1])) || Operators.Contains(source[source.Length - 1]))
            {
                return true;
            }

            for (int i = 1; i < source.Length - 1; i++)
            {
                if (Operators.Contains(source[i]) && source[i + 1] == ')')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
