using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Interfaces
{
    public interface ITokensParser
    {
        IEnumerable<Token> Parse(IEnumerable<Token> source);
    }
}
