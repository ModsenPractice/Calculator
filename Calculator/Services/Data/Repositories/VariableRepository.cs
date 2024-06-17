using Calculator.Models;
using Calculator.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calculator.Services.Data.Repositories
{
    public class VariableRepository : IVariableRepository
    {
        private readonly string variableFilePath = "variables.json";
        public async Task<IEnumerable<Variable>> GetVariablesAsync()
        {
            string variablesJson = await File.ReadAllTextAsync(variableFilePath);
            if (!string.IsNullOrEmpty(variablesJson))
            {
                IEnumerable<Variable>? deserializedVariables = JsonSerializer.Deserialize<IEnumerable<Variable>>(variablesJson);
                if (deserializedVariables != null)
                {
                    IEnumerable<Variable> variables = deserializedVariables.Where(v => v != null);
                    return variables;
                }
            }

            return Enumerable.Empty<Variable>();
        }

        public async Task AddVariableAsync(Variable variable)
        {
            IEnumerable<Variable> variables = await GetVariablesAsync();
            var updatedVariables = new List<Variable>(variables) { variable };
            string updatedVariableJson = JsonSerializer.Serialize(updatedVariables);
            await File.WriteAllTextAsync(variableFilePath, updatedVariableJson);

        }

        public async Task DeleteVariableAsync(string name)
        {
            IEnumerable<Variable> variables = await GetVariablesAsync();
            var updatedVariables = new List<Variable>(variables);
            updatedVariables.RemoveAll(f => f.Name == name);
            string updatedVariablesJson = JsonSerializer.Serialize(updatedVariables);
            await File.WriteAllTextAsync(variableFilePath, updatedVariablesJson);

        }
    }
}
