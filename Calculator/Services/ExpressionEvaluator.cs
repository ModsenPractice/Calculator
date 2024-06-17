using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Services.Interfaces;
using Calculator.Models;
using Calculator.Models.Enum;

namespace Calculator.Services
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        public string EvaluateExpression(IEnumerable<Token> tokens)
        {
            var stack = new Stack<double>();

            var unaryTokenEncountered = false; // Флаг для определения унарного знака числа

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Operand:
                        var operand = double.Parse(token.Value);
                        if (unaryTokenEncountered)
                        {
                            operand *= -1; // Применение унарного знака
                            unaryTokenEncountered = false; // Сброс флага унарного знака
                        }
                        stack.Push(operand);
                        break;
                    case TokenType.Operations:
                        if (stack.Count < 2)
                        {
                            throw new InvalidOperationException("Invalid expression");
                        }
                        var operand2 = stack.Pop();
                        var operand1 = stack.Pop();
                        var result = PerformOperation(token.Value, operand1, operand2);
                        stack.Push(result);
                        break;
                    case TokenType.Function:
                        if (stack.Count < 1)
                        {
                            throw new InvalidOperationException("Invalid expression");
                        }
                        var funcOperand = stack.Pop();
                        var funcResult = PerformFunction(token.Value, funcOperand);
                        stack.Push(funcResult);
                        break;
                    case TokenType.Unary: // Добавлен обработчик унарного токена
                        unaryTokenEncountered = true; // Устанавливаем флаг унарного знака
                        break;
                    default:
                        throw new InvalidOperationException("Invalid token type");
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid expression");
            }

            return stack.Pop().ToString();
        }

        private double PerformOperation(string operation, double operand1, double operand2)
        {
            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                case "^":
                    return Math.Pow(operand1, operand2);
                default:
                    throw new InvalidOperationException("Invalid operation");
            }
        }

        private double PerformFunction(string functionName, double operand)
        {
            switch (functionName.ToLower())
            {
                case "sin":
                    return Math.Sin(operand);
                case "cos":
                    return Math.Cos(operand);
                case "ln":
                    return Math.Log(operand);
                case "exp":
                    return Math.Exp(operand);
                default:
                    throw new InvalidOperationException("Invalid function");
            }
        }
    }
}
