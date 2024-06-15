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
using Calculator.Services.Validators.ExpressionValidators;
using Calculator.Services.Validators.FunctionValidators;
using Calculator.Services.Validators.VariableValidators;

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
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);

            var result = validatorManager.ValidateExpression(source);

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
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);

            var result = validatorManager.ValidateExpression(source);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }

        [DataTestMethod]
        [DataRow("f(x)=(5+3*(12/x))-sqrt(16)")]
        [DataRow("x(g)=log(1000)+(g^3)-(6/2)")]
        public void Validate_CorrectFunction_ReturnOkStatus(string source)
        {
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);

            var result = validatorManager.ValidateFunction(source);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("5+3(12/4))(769+6)-sqrt(16)")]
        [DataRow("f1(x)=-25+(--23*5)")]
        [DataRow("f(x,y)=7+8*3/()sqrt(49)-ln(1")]
        [DataRow("f(x,y=6++1*sqrt(-53)-2--(-24) +()")]
        public void Validate_WrongFunctionWithMultipleErrors_ReturnErrorStatus(string source)
        {
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);

            var result = validatorManager.ValidateFunction(source);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }

        [DataTestMethod]
        [DataRow("y=35")]
        [DataRow("x=(10^3/2)+(15*4)-20")]
        public void Validate_CorrectVariable_ReturnOkStatus(string source)
        {
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);


            var result = validatorManager.ValidateVariable(source);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count == 0);
        }

        [DataTestMethod]
        [DataRow("y=7+8*3/()sqrt(49)-ln(1")]
        [DataRow("x=6++1*sqrt(-53)-2--(-24)+()")]
        public void Validate_WrongVariableWithMultipleErrors_ReturnErrorStatus(string source)
        {
            var expressionValidators = new List<IExpressionValidator>
            {
                new FilledBracketsExpressionValidator(),
                new OperatorPlaceExpressionValidator(),
                new OpenedBracketsExpressionValidator(),
                new DigitExpressionValidator(),
                new RepeatableOperatorsExpressionValidator()
            };
            var functionValidator = new List<IFunctionValidator>
            {
                new EqualsPlaceFunctionValidator(),
                new EqualsFunctionValidator()
            };
            var variableValidator = new List<IVariableValidator>
            {
                new EqualsVariableValidator()
            };
            var validatorManager = new ValidatorManager(expressionValidators, functionValidator, variableValidator);

            var result = validatorManager.ValidateVariable(source);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.IsTrue(result.ErrorMessages.Count != 0);
        }
    }
}
