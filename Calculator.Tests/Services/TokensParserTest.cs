using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Interfaces;
using Calculator.Services.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.Services
{
    [TestClass]
    public class TokensParserTest
    {
        private readonly ITokensParser _parser;
        public TokensParserTest()
        {
            _parser = new TokensParser();    
        }

        [TestMethod]
        public void ParseSingleOperand()
        {
            //512.612
            IEnumerable<Token> toParse = [
                new(TokenType.Operand, "512.612"),
            ];

            var result = _parser.Parse(toParse);

            CollectionAssert.AreEqual(toParse.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseBasicOperations()
        {
            //2+2-2/2*2^2
            IEnumerable<Token> toParse = [
                new(TokenType.Operand, "2"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "2"),
                new(TokenType.Minus, "-"),
                new(TokenType.Operand, "2"),
                new(TokenType.Division, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Operand, "2"),
                new(TokenType.Power, "^"),
                new(TokenType.Operand, "2"),
            ];

            var result = _parser.Parse(toParse);

            //22+22/22^*-
            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Division, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "2"),
                new(TokenType.Power, "^"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Minus, "-"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseComplexExpression()
        {
            //1.2+(22.2-333)*4444/55555*sin(2+5)
            IEnumerable<Token> toParse = [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Plus, "+"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Minus, "-"),
                new(TokenType.Operand, "333"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Operand, "4444"),
                new(TokenType.Division, "/"),
                new(TokenType.Operand, "55555"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Function, "sin"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "2"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.ClosePar, ")"),
            ];

            var result = _parser.Parse(toParse);

            //1.2 22.2 333 - 4444 * 55555 / 2 5 + sin * +
            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Operand, "333"),
                new(TokenType.Minus, "-"),
                new(TokenType.Operand, "4444"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Operand, "55555"),
                new(TokenType.Division, "/"),
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "5"),
                new(TokenType.Plus, "+"),
                new(TokenType.Function, "sin"),
                new(TokenType.Multiplication, "*"),
                new(TokenType.Plus, "+"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseMultipleUnary()
        {
            //-1.2+(+22.2)+5
            IEnumerable<Token> toParse = [
                new(TokenType.Unary, "-"),
                new(TokenType.Operand, "1.2"),
                new(TokenType.Plus, "+"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Unary, "+"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "5"),
            ];

            var result = _parser.Parse(toParse);

            IEnumerable <Token> expectedResult =
            [
                new(TokenType.Operand, "1.2"),
                new(TokenType.Unary, "-"),
                new(TokenType.Operand, "22.2"),
                new(TokenType.Unary, "+"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.Plus, "+"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void ParseNestedFunctions()
        {
            //sin(cos(2+5)/ln(3))
            IEnumerable<Token> toParse = [
                new(TokenType.Function, "sin"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Function, "cos"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "2"),
                new(TokenType.Plus, "+"),
                new(TokenType.Operand, "5"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.Division, "/"),
                new(TokenType.Function, "ln"),
                new(TokenType.OpenPar, "("),
                new(TokenType.Operand, "3"),
                new(TokenType.ClosePar, ")"),
                new(TokenType.ClosePar, ")"),
            ];

            var result = _parser.Parse(toParse);

            IEnumerable<Token> expectedResult =
            [
                new(TokenType.Operand, "2"),
                new(TokenType.Operand, "5"),
                new(TokenType.Plus, "+"),
                new(TokenType.Function, "cos"),
                new(TokenType.Operand, "3"),
                new(TokenType.Function, "ln"),
                new(TokenType.Division, "/"),
                new(TokenType.Function, "sin"),
            ];

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }
    }
}
