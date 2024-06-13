using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Exceptions
{
    public class UnexpectedCharacterException : Exception
    {
        public UnexpectedCharacterException(string source, int position) :
            base($"Unexpected character faced during string parsing. string: {source}, position: {position}") { }
    }
}
