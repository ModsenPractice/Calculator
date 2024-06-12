using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class FilledBracketsValidatorTests
    {
        [DataTestMethod]
        [DataRow("(12+4+(24-2))")]
        [DataRow("12*(25+3)")]
        [DataRow("(1+2*(3-4))")]
        [DataRow("((5+6)*7-8)")]
        [DataRow("9+(10*(11-12))")]
        [DataRow("24+1")]
        [DataRow("(24+1)+(-12)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var bracketsValidator = new FilledBracketsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
        }

        [DataTestMethod]
        [DataRow("(45*())")]
        [DataRow("()*(25+37)")]
        [DataRow("10+20*(30-40)+()")]
        [DataRow("(12+())")]
        [DataRow("((()))")]
        [DataRow("()")]
        public void Validate_ExpressionWithEmptyBrackets_ReturnErrorStatus(string source)
        {
            var bracketsValidator = new FilledBracketsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
        }
    }
}
