using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Data.Interfaces
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<Function>> GetFunctionsAsync();
        Task AddFunctionAsync(Function function);
        Task DeleteFunctionAsync(string name);
    }
}
