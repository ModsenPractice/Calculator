using Calculator.Models;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Data.Services
{
    public class VariableService : IDataService<Variable>
    {
        private readonly IVariableRepository _repository;
        private readonly IParser<Variable> _parser;

        public VariableService(IVariableRepository repository,
            IParser<Variable> parser)
        {
            _repository = repository;
            _parser = parser;
        }

        public async Task AddAsync(string expression)
        {
            var variable = _parser.Parse(expression);

            await _repository.AddVariableAsync(variable);
        }

        public async Task DeleteAsync(string expression)
        {
            var variable = _parser.Parse(expression);

            await _repository.DeleteVariableAsync(variable.Name);
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            return (await _repository.GetVariablesAsync()).Select(v => v.ToString());
        }
    }
}
