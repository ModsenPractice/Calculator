using Calculator.Models;
using Calculator.Models.Enum;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
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

        public IEnumerable<Token> Parse(IEnumerable<Token> source)
        {
            var operationStack = new Stack<Token>();
            var result = new List<Token>();

            foreach(var token in source)
            {
                switch(token.Type)
                {
                    case TokenType.Operand:
                        result.Add(token);
                        break;

                    case TokenType.OpenPar:
                    case TokenType.Function:
                        operationStack.Push(token);
                        break;

                    case TokenType.ClosePar:
                        while (operationStack.Count > 0 &&
                            operationStack.Peek().Type != TokenType.OpenPar)
                        {
                            result.Add(operationStack.Pop());
                        }
                        operationStack.Pop();
                        if(operationStack.Count > 0 &&
                            operationStack.Peek().Type == TokenType.Function)
                        {
                            result.Add(operationStack.Pop());
                        }
                        break;

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

            while(operationStack.Count > 0)
            {
                result.Add(operationStack.Pop());
            }

            return result;
        }
    }
}
