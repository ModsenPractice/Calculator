using Calculator.Models.Enum;
using Calculator.Services.Validators;
using Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Services.Validators.ExpressionValidators;

namespace Calculator.Tests.ValidateTests.ExpressionValidatorTests
{
    public class DigitExpressionValidatorTests
    {
        [DataTestMethod]
        [DataRow("12*(25+3)")]
        [DataRow("x*a+25")]
        [DataRow("y--a")]
        [DataRow("x+a")]
        [DataRow("(24+1)+(-12)")]
        public void Validate_CorrectExpression_ReturnOkStatus(string source)
        {
            var digitValidator = new DigitExpressionValidator();
            var result = new Result();

            digitValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("(---)")]
        [DataRow("(-#^$)")]
        public void Validate_ExpressionWithoutDigitsOrVariables_ReturnErrorStatus(string source)
        {
            var digitValidator = new DigitExpressionValidator();
            var result = new Result();

            digitValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
