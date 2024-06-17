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
        private readonly IEnumerable<ISubstitutionService> _substitutors;
        public CalculatorService(IExpressionEvaluator evaluator,
            IParser<IEnumerable<Token>> tokensParser,
            IEnumerable<ISubstitutionService> substitutors)
        {
            _evaluator = evaluator;
            _tokensParser = tokensParser;
            _substitutors = substitutors;
        }

        public async Task<string> CalculateAsync(string expression)
        {
            foreach(var substitutor in _substitutors)
            {
                expression = await substitutor.ReplaceAsync(expression);
            }

            var tokens = _tokensParser.Parse(expression);

            var result = _evaluator.EvaluateExpression(tokens);

            await Task.CompletedTask;

            return result;
        }
    }
}
