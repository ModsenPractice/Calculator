using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services;

namespace Calculator.Tests
{
    [TestClass]
    public class ExpressionEvaluatorTests
    {
        private ExpressionEvaluator _evaluator;

        [TestInitialize]
        public void Init()
        {
            _evaluator = new ExpressionEvaluator();
        }

        [TestMethod]
        public void EvaluateExpression_ValidExpression_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<Token>
            {
                new Token { Type = TokenType.Operand, Value = "3" },
                new Token { Type = TokenType.Operand, Value = "4" },
                new Token { Type = TokenType.Operations, Value = "+" },
                new Token { Type = TokenType.Operand, Value = "2" },
                new Token { Type = TokenType.Operations, Value = "*" }
            };

            // Act
            var result = _evaluator.EvaluateExpression(tokens);

            // Assert
            Assert.AreEqual("14", result);
        }

        [TestMethod]
        public void EvaluateExpression_InvalidExpression_ThrowsException()
        {
            // Arrange
            var tokens = new List<Token>
            {
                new Token { Type = TokenType.Operand, Value = "2" },
                new Token { Type = TokenType.Operations, Value = "+" }
            };

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _evaluator.EvaluateExpression(tokens));
        }

        [TestMethod]
        public void EvaluateExpression_ExpressionWithNegativeNumber_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<Token>
            {
                new Token { Type = TokenType.Unary, Value = "-" },
                new Token { Type = TokenType.Operand, Value = "2" },
                new Token { Type = TokenType.Operand, Value = "3" },
                new Token { Type = TokenType.Operations, Value = "+" }
            };

            // Act
            var result = _evaluator.EvaluateExpression(tokens);

            // Assert
            Assert.AreEqual("1", result);
        }
    }
}