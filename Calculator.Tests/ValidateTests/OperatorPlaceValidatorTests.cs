﻿using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Validators;

namespace Calculator.Tests.ValidateTests
{
    [TestClass]
    public class OperatorPlaceValidatorTests
    {
        [DataTestMethod]
        [DataRow("(12+4.11+(24-2.24))")]
        [DataRow("12*(25+3)")]
        [DataRow("(1+2.215*(3-4))")]
        [DataRow("((5+6)*7-8)")]
        [DataRow("9+(10*(11-12))")]
        [DataRow("24+1")]
        [DataRow("(24+1)+(-12)")]
        public void Validate_CorrectExpresision_ReturnOkStatus(string source)
        {
            var bracketsValidator = new OperatorPlaceValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Ok, result.Status);
        }

        [DataTestMethod]
        [DataRow("*623+1")]
        [DataRow("(*")]
        [DataRow("34.2*23+")]
        [DataRow("(235+53+)")]
        [DataRow("+")]
        [DataRow("/24/35/")]
        [DataRow("(/52/11)")]
        public void Validate_ExpressionWithWrongOperatorPlace_ReturnErrorStatus(string source)
        {
            var bracketsValidator = new OperatorPlaceValidator();
            var result = new Result();

            bracketsValidator.Validate(source, result);

            Assert.AreEqual(ResultStatus.Error, result.Status);
        }
    }
}