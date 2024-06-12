using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Exceptions
{
    public class UnexpectedCharacterException : Exception
    {
        public UnexpectedCharacterException(char character, int position) :
            base($"Unexpected character faced during string parsing: {character}, position: {position}") { }
    }
}
