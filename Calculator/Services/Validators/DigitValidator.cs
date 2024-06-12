using Calculator.Models.Enum;
using Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Validators
{
    public class DigitValidator : AbstractValidator
    {
        private const string DontContainDigitsOrVariableErrorException = "";

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
