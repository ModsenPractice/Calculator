using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Services.Data.Interfaces;

namespace Calculator.Services.Data.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly string functionFilePath = "C:\\Users\\POOLSHOT\\Source\\Repos\\Calculator\\Calculator\\functions.json";
        public async Task<IEnumerable<Function>> GetFunctionsAsync()
        {
            string? functionsJson = await File.ReadAllTextAsync(functionFilePath);
            if (!string.IsNullOrEmpty(functionsJson))
            {
                IEnumerable<Function>? deserializedFunctions = JsonSerializer.Deserialize<IEnumerable<Function>?>(functionsJson);
                if (deserializedFunctions != null)
                {
                    IEnumerable<Function> functions = deserializedFunctions.Where(f => f != null);
                    return functions;
                }
            }

            return Enumerable.Empty<Function>();
        }

        public async Task AddFunctionAsync(Function function)
        {
            IEnumerable<Function> functions = await GetFunctionsAsync();
            var updatedFunctions = new List<Function>(functions) { function };
            string updatedFunctionsJson = JsonSerializer.Serialize(updatedFunctions);
            await File.WriteAllTextAsync(functionFilePath, updatedFunctionsJson);

        }

        public async Task DeleteFunctionAsync(string name)
        {
            IEnumerable<Function> functions = await GetFunctionsAsync();
            var updatedFunctions = new List<Function>(functions);
            updatedFunctions.RemoveAll(f => f.Name == name);
            string updatedFunctionsJson = JsonSerializer.Serialize(updatedFunctions);
            await File.WriteAllTextAsync(functionFilePath, updatedFunctionsJson);

        }
    }
}
