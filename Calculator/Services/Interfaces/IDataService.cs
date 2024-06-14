using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<T>> GetAsync<T>();
        Task AddAsync<T>(string expression);
        Task DeleteAsync<T>(string name);
    }
}
