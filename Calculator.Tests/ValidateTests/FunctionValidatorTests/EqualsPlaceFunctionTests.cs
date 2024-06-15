using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators.FunctionValidators;

namespace Calculator.Tests.ValidateTests.FunctionValidatorTests
{
    [TestClass]
    public class EqualsPlaceFunctionTests
    {
        [DataTestMethod]
        [DataRow("f(x)=2")]
        [DataRow("f(x)=1352342234")]
        [DataRow("f(x)=24^3")]
        public void Validate_CorrectVariable_ReturnOkStatus(string source)
        {
            var repeatableValidator = new EqualsFunctionValidator();
            var result = new Result();

            repeatableValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("f(t)+5=8^2")]
        [DataRow("f(s)-8=x22")]
        public void Validate_SourceWithWrongEqualPlace_ReturnErrorStatus(string source)
        {
            var repeatableValidator = new EqualsPlaceFunctionValidator();
            var result = new Result();

            repeatableValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
