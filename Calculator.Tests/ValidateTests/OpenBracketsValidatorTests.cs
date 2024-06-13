using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class OpenBracketsValidatorTests
    {
        [DataTestMethod]
        [DataRow("(12.2+4+(24-2))")]
        [DataRow("12*(25+3)")]
        [DataRow("(1+2*(3-4))")]
        [DataRow("((5+6)*7-8.42)")]
        [DataRow("9+(10*(11-12.12))")]
        [DataRow("24+1")]
        [DataRow("(24+1)+(-12.2)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var bracketsValidator = new OpenedBracketsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow(")))(((")]
        [DataRow(")()()()(")]
        [DataRow("(10+20*(30-40)")]
        [DataRow("(70((-80+)*2)")]
        [DataRow("))))")]
        [DataRow("(")]
        [DataRow("(((((((())))")]
        public void Validate_ExpressionWithNotClosedBrackets_ReturnErrorStatus(string source)
        {
            var bracketsValidator = new OpenedBracketsValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
