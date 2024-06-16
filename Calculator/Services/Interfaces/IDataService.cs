using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    public interface IDataService<T>
    {
        Task<IEnumerable<string>> GetAsync();
        Task AddAsync(string expression);
        Task DeleteAsync(string expression);
    }
}
