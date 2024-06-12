using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services;
using Calculator.Services.Exceptions;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.Services
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
        public void AllOperationsTokenization()
        {
            var result = _tokenizer.Tokenize("1.2+(22.2-333)*4444/55555*sin(2+5)");

            IEnumerable<Token> expectedResult =
            [
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

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void UnaryOperationsTokenization()
        {
            var result = _tokenizer.Tokenize("-1.2+(+22.2)+5");

            IEnumerable<Token> expectedResult =
            [
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

            CollectionAssert.AreEqual(expectedResult.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void IncorrectStringTokenization()
        {
            try
            {
                _ = _tokenizer.Tokenize("41g1+5");
                Assert.Fail("Expected exception was not thrown");
            }
            catch(UnexpectedCharacterException ex)
            {
                Assert.AreEqual(ex.Message,
                    "Unexpected character faced during string parsing: g, position: 2");
            }
            catch(Exception ex)
            {
                Assert.Fail($"Unexpected exception was thrown: {ex.GetType()}, {ex.Message}");
            }
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
    }
}
