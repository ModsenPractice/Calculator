using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class OperatorWithBracketsValidatorTests
    {
        [DataTestMethod]
        [DataRow("-(24+3)")]
        [DataRow("y-(-a)")]
        [DataRow("x+a")]
        [DataRow("(24+1)+(-12)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var digitValidator = new OperatorWithBracketsValidator();
            var result = new Result();

            digitValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("(25-1)(12*5)")]
        [DataRow("-24*24(23+5)")]
        public void Validate_ExpressionWithWrongOperatorPlace_ReturnErrorStatus(string source)
        {
            var digitValidator = new OperatorWithBracketsValidator();
            var result = new Result();

            digitValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
