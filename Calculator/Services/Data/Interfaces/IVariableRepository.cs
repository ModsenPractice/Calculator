using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Data.Interfaces
{
    public interface IVariableRepository
    {
        Task<IEnumerable<Variable>> GetVariablesAsync();
        Task AddVariableAsync(Variable function);
        Task DeleteVariableAsync(string name);
    }
}
