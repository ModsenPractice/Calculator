using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class RepitableOperatorsValidatorTests
    {
        [DataTestMethod]
        [DataRow("(12+4+(24-2.12))")]
        [DataRow("12*(25+3)")]
        [DataRow("(1+2*(3-4))")]
        [DataRow("((5+6)*7-8)")]
        [DataRow("4--2.24")]
        [DataRow("24+1.21")]
        [DataRow("(24+1.12)+(-12)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var bracketsValidator = new RepitableOperatorsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
        }

        [DataTestMethod]
        [DataRow("1++24")]
        [DataRow("24+56+1//4")]
        [DataRow("78**4++4")]
        [DataRow("65++2")]
        public void Validate_ExpressionWithRepitableOperators_ReturnErrorStatus(string source)
        {
            var bracketsValidator = new RepitableOperatorsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
        }
    }
}
