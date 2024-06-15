using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators.ExpressionValidators;
using Calculator.Services.Validators.VariableValidators;

namespace Calculator.Tests.ValidateTests.VariableValidatorTests
{
    [TestClass]
    public class EqualsVariableValidatorTests
    {
        [DataTestMethod]
        [DataRow("x=2")]
        [DataRow("x=1352342234")]
        [DataRow("s=24^3")]
        public void Validate_CorrectVariable_ReturnOkStatus(string source)
        {
            var repeatableValidator = new EqualsVariableValidator();
            var result = new Result();

            repeatableValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("x8^2")]
        [DataRow("x22")]
        public void Validate_NonVariableSource_ReturnErrorStatus(string source)
        {
            var repeatableValidator = new EqualsVariableValidator();
            var result = new Result();

            repeatableValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
