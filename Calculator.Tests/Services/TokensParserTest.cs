using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services;
using Calculator.Services.Interfaces;
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
        public void ParseMultipleOperations()
        {
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
            
            IEnumerable <Token> expectedResult =
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
        public void ParseUnary()
        {
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
    }
}
