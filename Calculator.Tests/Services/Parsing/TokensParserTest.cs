using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Interfaces;
using Calculator.Services.Parsing;
using Calculator.Services.Parsing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.Services.Parsing
{
    [TestClass]
    public class TokensParserTest
    {
        private readonly IParser<IEnumerable<Token>> _parser;
        public TokensParserTest()
        {
            var tokenizer = new Tokenizer();
            _parser = new TokensParser(tokenizer);
        }

        [TestMethod]
        public void ParseSingleOperand()
        {
            var result = _parser.Parse("512.612");

            IEnumerable<Token> expectedResult = [
                new(TokenType.Operand, "512.612"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseBasicOperations()
        {
            var result = _parser.Parse("2+2-2/2*2^2");

            //22+22/22^*-
            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operations, "^"),
                new(TokenType.Operations, "*"),
                new(TokenType.Operations, "-"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseComplexExpression()
        {
            var result = _parser.Parse("1.2+(22.2-333)*4444/55555*sin(2+5)");

            //1.2 22.2 333 - 4444 * 55555 / 2 5 + sin * +
            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Operand, "333"),
                new(TokenType.Operations, "-"),
                new(TokenType.Operand, "4444"),
                new(TokenType.Operations, "*"),
                new(TokenType.Operand, "55555"),
                new(TokenType.Operations, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "5"),
                new(TokenType.Operations, "+"),
                new(TokenType.Function, "sin"),
                new(TokenType.Operations, "*"),
                new(TokenType.Operations, "+"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseMultipleUnary()
        {
            var result = _parser.Parse("-1.2+(+22.2)+5");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Unary, "-"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Unary, "+"),
                new(TokenType.Operations, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.Operations, "+"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseNestedFunctions()
        {
            var result = _parser.Parse("sin(cos(2+5)/ln(3))");

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "5"),
                new(TokenType.Operations, "+"),
                new(TokenType.Function, "cos"),
                new(TokenType.Operand, "3"),
                new(TokenType.Function, "ln"),
                new(TokenType.Operations, "/"),
                new(TokenType.Function, "sin"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }
    }
}
