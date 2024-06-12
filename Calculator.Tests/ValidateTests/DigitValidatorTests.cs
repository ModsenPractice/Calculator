using Calculator.Models.Enum;
using Calculator.Services.Validators;
using Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.ValidateTests
{
    public class DigitValidatorTests
    {
        [DataTestMethod]
        [DataRow("12*(25+3)")]
        [DataRow("x*a+25")]
        [DataRow("y--a")]
        [DataRow("x+a")]
        [DataRow("(24+1)+(-12)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var bracketsValidator = new DigitValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
        }

        [DataTestMethod]
        [DataRow("(---)")]
        [DataRow("(-#^$)")]
        public void Validate_ExpressionWithoutDigitsOrVariables_ReturnErrorStatus(string source)
        {
            var bracketsValidator = new DigitValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
        }
    }
}
