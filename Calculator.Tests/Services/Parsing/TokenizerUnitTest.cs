﻿using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Exceptions;
using Calculator.Services.Interfaces;
using Calculator.Services.Parsing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.Services.Parsing
{
    [TestClass]
    public class TokenizerUnitTest
    {
        private readonly ITokenizer _tokenizer;
        public TokenizerUnitTest()
        {
            _tokenizer = new Tokenizer();
        }

        [TestMethod]
        public void IntegerTokenization()
        {
            var result = _tokenizer.Tokenize("512");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "512"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void FloatTokenization()
        {
            var result = _tokenizer.Tokenize("512.612");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "512.612"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void BasicOperationsTokenization()
        {
            var result = _tokenizer.Tokenize("2+2-2/2*2^2");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "-"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "*"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "^"),
                new(TokenType.Operand, "2"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void FirstTokenUnaryTokenization()
        {
            var result = _tokenizer.Tokenize("-512.612");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Unary, "-"),
                new(TokenType.Operand, "512.612"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }


        [TestMethod]
        public void MultipleUnaryTokenization()
        {
            var result = _tokenizer.Tokenize("-1.2+(+22.2)+5");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Unary, "-"),
                new(TokenType.Operand, "1.2"),
                new(TokenType.Operations, "+"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Unary, "+"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "5"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }


        [TestMethod]
        public void ComplexExpressionTokenization()
        {
            var result = _tokenizer.Tokenize("1.2+(22.2-333)*4444/55555*sin(2+5)");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Operations, "+"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Operations, "-"),
                new(TokenType.Operand, "333"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Operations, "*"),
                new(TokenType.Operand, "4444"),
                new(TokenType.Operations, "/"),
                new(TokenType.Operand, "55555"),
                new(TokenType.Operations, "*"),
                new(TokenType.Function, "sin"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.ClosePar, ")"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void NestedFunctionsTokenization()
        {
            var result = _tokenizer.Tokenize("sin(cos(2+5)/ln(3))");

            IEnumerable<Token> expectedResult = [
                new(TokenType.Function, "sin"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Function, "cos"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Operations, "/"),
                new(TokenType.Function, "ln"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "3"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.ClosePar, ")"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void UnknownedFunctionTokenization()
        {
            try
            {
                _ = _tokenizer.Tokenize("sh(2)");
                Assert.Fail("Expected exception was not thrown");
            }
            catch (UnrecognizedBuiltInFunctionException ex)
            {
                Assert.AreEqual(ex.Message,
                    "Unrecognized built-in function name: sh");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception was thrown: {ex.GetType()}, {ex.Message}");
            }
        }

        [TestMethod]
        public void DoublePointParsing()
        {
            try
            {
                _ = _tokenizer.Tokenize("2..5");
                Assert.Fail("Expected exception was not thrown");
            }
            catch (UnexpectedCharacterException ex)
            {
                Assert.AreEqual(ex.Message,
                    "Unexpected character faced during string parsing. string: 2..5, position: 2");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception was thrown: {ex.GetType()}, {ex.Message}");
            }
        }
    }
}