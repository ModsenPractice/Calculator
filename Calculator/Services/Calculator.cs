using Calculator.Models;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public class CalculatorService : ICalculator
    {
        private readonly IExpressionEvaluator _evaluator;
        private readonly IParser<IEnumerable<Token>> _tokensParser;
        public CalculatorService(IExpressionEvaluator evaluator,
            IParser<IEnumerable<Token>> tokensParser)
        {
            _evaluator = evaluator;
            _tokensParser = tokensParser;
        }

        public async Task<string> CalculateAsync(string expression)
        {
            var tokens = _tokensParser.Parse(expression);

            var result = _evaluator.EvaluateExpression(tokens);

            await Task.CompletedTask;

            return result;
        }
    }
}
