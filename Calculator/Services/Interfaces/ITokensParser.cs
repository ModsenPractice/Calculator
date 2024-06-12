using Calculator.Models;
using Calculator.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    /// <summary>
    /// Represent object that parse collection of tokens in infix notation to reverse polish notation
    /// </summary>
    public interface ITokensParser
    {
        IEnumerable<Token> Parse(IEnumerable<Token> source);
    }
}
