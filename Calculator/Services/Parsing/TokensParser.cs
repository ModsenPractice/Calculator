using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Parsing
{
    public class TokensParser : IParser<IEnumerable<Token>>
    {
        private static readonly Dictionary<string, int> _operationPriority = new()
        {
            {"(", 1},
            {"+", 2},
            {"-", 2},
            {"*", 3},
            {"/", 3},
            {"^", 4},
        };

        private readonly ITokenizer _tokenizer;
        public TokensParser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }


        /// <summary>
        /// Converts infix string into reverse polish notation collection of tokens
        /// </summary>
        /// <param name="input">Expression string in infix notation</param>
        /// <returns>List of tokenst in form of reverse polish notation</returns>
        public IEnumerable<Token> Parse(string input)
        {
            var source = _tokenizer.Tokenize(input);

            var operationStack = new Stack<Token>();
            var result = new List<Token>();

            foreach (var token in source)
            {
                switch (token.Type)
                {
                    //if token is operand push it into result sequence
                    case TokenType.Operand:
                        result.Add(token);
                        break;

                    //if token is open bracket or function push token to stack
                    case TokenType.OpenPar:
                    case TokenType.Function:
                    case TokenType.Unary:
                        operationStack.Push(token);
                        break;

                    //if token is closing bracket take all tokens from stack to result sequence
                    //until facing open bracket in stack. Pop open bracket
                    //if brackets block represent function block put function from stack to result sequence
                    case TokenType.ClosePar:
                        while (operationStack.Count > 0 &&
                            operationStack.Peek().Type != TokenType.OpenPar)
                        {
                            result.Add(operationStack.Pop());
                        }
                        operationStack.Pop();
                        if (operationStack.Count > 0 &&
                            operationStack.Peek().Type == TokenType.Function)
                        {
                            result.Add(operationStack.Pop());
                        }
                        break;

                    //if token is operation put operations from stack to result sequence
                    //while top statck operation priority higher or equal current operation
                    //and push operation to stack
                    case TokenType.Operations:
                        while (operationStack.Count > 0 &&
                            _operationPriority[operationStack.Peek().Value] >= _operationPriority[token.Value])
                        {
                            result.Add(operationStack.Pop());
                        }
                        operationStack.Push(token);
                        break;
                }
            }

            //Push all remaining tokens from stack to result sequence
            while (operationStack.Count > 0)
            {
                result.Add(operationStack.Pop());
            }

            return result;
        }
    }
}
