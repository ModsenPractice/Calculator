using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Exceptions;
using Calculator.Services.Interfaces;

namespace Calculator.Services.Parsing.Utility
{
    public class Tokenizer : ITokenizer
    {
        private static readonly string[] _builtInFunctions = ["sin", "cos", "ln", "tg", "exp"];
        private static readonly char[] _possibleUnary = ['+', '-'];

        //Map invariants of known operations to operations itself
        private static readonly Dictionary<char, char> _operationsInvarians = new()
        {
            {'÷', '/'},
            {'×', '*'},
        };

        //Map known character to corresponding token types
        private static readonly Dictionary<char, TokenType> _knownCharTokenType = new()
        {
            { '+', TokenType.Operations },
            { '-', TokenType.Operations },
            { '/', TokenType.Operations },
            { '*', TokenType.Operations },
            { '^', TokenType.Operations },
            { '(', TokenType.OpenPar },
            { ')', TokenType.ClosePar },
        };

        public Tokenizer()
        {
        }

        /// <summary>
        /// Creates collection of tokens from string expression
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <returns>collection of tokens representing expression</returns>
        public IEnumerable<Token> Tokenize(string source)
        {
            var input = NormalizeString(source);

            var tokens = new List<Token>();

            var curr = 0;

            while (curr < source.Length)
            {
                var currChar = input[curr];
                if (char.IsDigit(currChar))
                {
                    tokens.Add(GetNumber(input, ref curr));
                }
                else if (char.IsLetter(currChar))
                {
                    tokens.Add(GetFunction(input, ref curr));
                }
                else if (IsUnary(input, curr))
                {
                    tokens.Add(new Token() { Type = TokenType.Unary, Value = currChar.ToString() });
                    curr++;
                }
                else
                {
                    tokens.Add(new Token() { Type = _knownCharTokenType[currChar], Value = currChar.ToString() });
                    curr++;
                }
            }

            return tokens;
        }

        private static bool IsUnary(string source, int pos)
        {
            if (!_possibleUnary.Contains(source[pos])) { return false; }

            return pos == 0 || source[pos - 1] == '(';
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
                if (source.Contains(kvp.Key))
                {
                    result = result.Replace(kvp.Key, kvp.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves a number from string, starting at certain position of source string
        /// and moves current pointer position behind number
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <param name="left">Start of number</param>
        /// <returns>Token of Operand type with number as value</returns>
        private static Token GetNumber(string source, ref int left)
        {
            var right = left + 1;

            //while char is not operation, skip it
            while (right < source.Length && IsNumberPart(source, right))
            {
                right++;
            }

            var number = source.Substring(left, right - left);

            left = right;

            return new Token() { Type = TokenType.Operand, Value = number };
        }

        private static bool IsNumberPart(string source, int pos)
        {
            if (char.IsDigit(source[pos])) { return true; }

            if (source[pos] == '.')
            {
                if (pos == 0 || !char.IsDigit(source[pos - 1]))
                {
                    throw new UnexpectedCharacterException(source, pos);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves a function from string, starting at certain position of source string
        /// and moves current pointer position behind function name
        /// </summary>
        /// <param name="source">Source string expression</param>
        /// <param name="left">Start of function name</param>
        /// <returns>Token of Function type with function name as value</returns>
        /// <exception cref="UnrecognizedBuiltInFunctionException"></exception>
        private static Token GetFunction(string source, ref int left)
        {
            var right = left + 1;

            while (right < source.Length && char.IsLetterOrDigit(source[right]))
            {
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