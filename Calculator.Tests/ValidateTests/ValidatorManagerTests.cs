using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services;
using Calculator.Services.Interfaces;
using Calculator.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class ValidatorManagerTests
    {
        [DataTestMethod]
        [DataRow("(5+3*(12/4))-sqrt(16)")]
        [DataRow("(10^3/2)+(15*4)-20")]
        [DataRow("(8*5+2)/(9-3)+π")]
        [DataRow("log(1000)+(5^3)-(6/2)")]
        [DataRow("(cos(0)+sin(π/2))*10-5")]
        public void Validate_CorrectExpression_ReturnOkStatus(string source)
        {
            var validators = new List<IValidator>() 
            {
                new FilledBracketsValidator(),
                new OperatorPlaceValidator(),
                new OpenedBracketsValidator(),
                new DigitValidator(),
                new RepeatableOperatorsValidator()
            };
            var validatorManager = new ValidatorManager(validators);

            var result = validatorManager.Validate(source);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("5+3(12/4))(769+6)-sqrt(16)")]
        [DataRow("-25+(--23*5)")]
        [DataRow("7+8*3/()sqrt(49)-ln(1")]
        [DataRow("6++1*sqrt(-53)-2--(-24) +()")]
        public void Validate_WrongExpressionWithMultipleErrors_ReturnErrorStatus(string source)
        {
            var validators = new List<IValidator>()
            {
                new FilledBracketsValidator(),
                new OperatorPlaceValidator(),
                new OpenedBracketsValidator(),
                new DigitValidator(),
                new RepeatableOperatorsValidator(),
                new OperatorWithBracketsValidator()
            };
            var validatorManager = new ValidatorManager(validators);

            var result = validatorManager.Validate(source);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
