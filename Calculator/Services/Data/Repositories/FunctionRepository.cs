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
        private readonly string _functionFileName = "functions.json";
        private readonly string _targetFile;

        public FunctionRepository()
        {
            _targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, _functionFileName);

            
            Task.Run(() => EnsureCreated()).Wait();
        }

        private async Task EnsureCreated()
        {

            if (!File.Exists(_targetFile))
            {
                using Stream inputStream =
                    await FileSystem.Current.OpenAppPackageFileAsync(_functionFileName);
                using FileStream outputStream = File.Create(_targetFile);
                await inputStream.CopyToAsync(outputStream);
            }

        }

        public async Task<IEnumerable<Function>> GetFunctionsAsync()
        {
            string? functionsJson = await File.ReadAllTextAsync(_targetFile);

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
            await File.WriteAllTextAsync(_targetFile, updatedFunctionsJson);

        }

        public async Task DeleteFunctionAsync(string name)
        {
            IEnumerable<Function> functions = await GetFunctionsAsync();
            var updatedFunctions = new List<Function>(functions);
            updatedFunctions.RemoveAll(f => f.Name == name);
            string updatedFunctionsJson = JsonSerializer.Serialize(updatedFunctions);
            await File.WriteAllTextAsync(_targetFile, updatedFunctionsJson);

        }
    }
}
