using Calculator.Models;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Data.Services
{
    public class FunctionService : IDataService<Function>
    {
        private readonly IFunctionRepository _repository;
        private readonly IParser<Function> _parser;

        public FunctionService(IFunctionRepository repository,
            IParser<Function> parser)
        {
            _repository = repository;
            _parser = parser;
        }

        public async Task AddAsync(string expression)
        {
            var function = _parser.Parse(expression);

            await _repository.AddFunctionAsync(function);
        }

        public async Task DeleteAsync(string expression)
        {
            var function = _parser.Parse(expression);

            await _repository.DeleteFunctionAsync(function.Name);
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            return (await _repository.GetFunctionsAsync()).Select(f => f.ToString());
        }
    }
}
