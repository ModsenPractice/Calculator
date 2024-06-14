using Calculator.Models;
using Calculator.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Validators
{
    public class OperatorWithBracketsValidator : AbstractValidator
    {
        private const string WrongOperatorPlaceWithBracketsErrorMessage = "";
        private const string Operators = "+-*/^";

        public override void Validate(string source, Result result)
        {
            if (IsWrongPlaceOfOperatorsWithBrackets(source))
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessages.Add(WrongOperatorPlaceWithBracketsErrorMessage);
            }

            base.Validate(source, result);
        }

        private bool IsWrongPlaceOfOperatorsWithBrackets(string source)
        {
            for (var i = 1; i < source.Length - 1; i++)
            {
                if (source[i] == '(' && !Operators.Contains(source[i - 1]) &&
                    source[i] == ')' && !Operators.Contains(source[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
