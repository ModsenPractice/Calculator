using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Exceptions;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public class Tokenizer : ITokenizer
    {
        private static readonly string[] _builtInFunctions = ["sin", "cos", "ln", "th", "exp"];

        //Map invariants of known operations to operations itself
        private static readonly Dictionary<char, char> _operationsInvarians = new Dictionary<char, char>()
        {
            {'÷', '/'},
            {'×', '*'},
        };

        //Map known character to corresponding token types
        private static readonly Dictionary<char, TokenType> _knownCharTokenType = new Dictionary<char, TokenType>()
        {
            { '+', TokenType.Plus },
            { '-', TokenType.Minus },
            { '/', TokenType.Division },
            { '*', TokenType.Multiplication },
            { '^', TokenType.Power },
            { '(', TokenType.OpenPar },
            { ')', TokenType.ClosePar },
        };

        /// <summary>
        /// Creates collection of tokens from string expression
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <returns>collection of tokens representing expression</returns>
        public IEnumerable<Token> Tokenize(string source)
        {
            var normilizedSource = NormalizeString(source);

            var tokens = new List<Token>();

            var curr = 0;

            while (curr < source.Length)
            {
                var currChar = normilizedSource[curr];
                if (char.IsDigit(currChar))
                {
                    tokens.Add(GetNumber(source, ref curr));
                }
                else if (char.IsLetter(currChar))
                {
                    tokens.Add(GetFunction(source, ref curr));
                }
                else
                {
                    tokens.Add(new Token() { Type = _knownCharTokenType[currChar], Value = currChar.ToString() });
                    curr++;
                }
            }

            return tokens;
        }

        /// <summary>
        /// Replace invariant character to common ones
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <returns>String expression with replaced characters</returns>
        private static string NormalizeString(string source)
        {
            var result = source;
            foreach (var kvp in _operationsInvarians)
            {
                if(source.Contains(kvp.Key))
                {
                    result = result.Replace(kvp.Key, kvp.Value);
                }
            }

            return result;
        }

        private static readonly char[] _validCharAfterNumber = ['+', '-', '*', '/', '^', ')'];
        private static readonly char _point = '.';
        /// <summary>
        /// Retrieves a number from string, starting at certain position of source string
        /// and moves current pointer position behind number
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <param name="left">Start of number</param>
        /// <returns>Token of Operand type with number as value</returns>
        /// <exception cref="UnexpectedCharacterException"></exception>
        private static Token GetNumber(string source, ref int left)
        {
            var right = left + 1;

            //while char is not operation, skip it
            while (right < source.Length &&
                !_validCharAfterNumber.Contains(source[right]))
            {
                if(!char.IsDigit(source[right]) && source[right] != _point)
                {
                    throw new UnexpectedCharacterException(source[right], right);
                }
                right++;
            }

            var number = source.Substring(left, right - left);

            left = right;

            return new Token() { Type = TokenType.Operand, Value = number };
        }
        
        private static readonly char _opBracket = '(';
        /// <summary>
        /// Retrieves a function from string, starting at certain position of source string
        /// and moves current pointer position behind function name
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <param name="left">Start of function name</param>
        /// <returns>Token of Function type with function name as value</returns>
        /// <exception cref="UnexpectedCharacterException"></exception>
        /// <exception cref="UnrecognizedBuiltInFunctionException"></exception>
        private static Token GetFunction(string source, ref int left)
        {
            var right = left + 1;

            //while char is not open bracket, skip it
            while (right < source.Length && source[right] != _opBracket)
            {
                if (!char.IsLetterOrDigit(source[right]))
                {
                    throw new UnexpectedCharacterException(source[right], right);
                }
                right++;
            }

            var funcName = source.Substring(left, right - left);

            if (!_builtInFunctions.Contains(funcName.ToLower()))
            {
                throw new UnrecognizedBuiltInFunctionException(funcName);
            }

            left = right;

            return new Token() { Type = TokenType.Function, Value = funcName };
        }

    }
}
