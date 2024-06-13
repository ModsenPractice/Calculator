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
    public class TokensParser : ITokensParser
    {
        private static readonly Dictionary<TokenType, int> _typePriority = new()
        {
            {TokenType.OpenPar, 1},
            {TokenType.Plus, 2},
            {TokenType.Minus, 2},
            {TokenType.Multiplication, 3},
            {TokenType.Division, 3},
            {TokenType.Power, 4},
            {TokenType.Unary, 5},
        };

        /// <summary>
        /// Converts collection of token into collection of tokens in infix notation in reverse polish notation
        /// </summary>
        /// <param name="source">Valid list of tokens in infix</param>
        /// <returns>List of tokenst in reverse polish notation</returns>
        public IEnumerable<Token> Parse(IEnumerable<Token> source)
        {
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
                    case TokenType.Plus:
                    case TokenType.Minus:
                    case TokenType.Multiplication:
                    case TokenType.Division:
                    case TokenType.Power:
                    case TokenType.Unary:
                        while (operationStack.Count > 0 &&
                            _typePriority[operationStack.Peek().Type] >= _typePriority[token.Type])
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
