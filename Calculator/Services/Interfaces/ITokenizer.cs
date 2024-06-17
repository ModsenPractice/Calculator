using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    /// <summary>
    /// Represent object that transforms string expression to collection of tokens
    /// </summary>
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(string source);
    }
}
