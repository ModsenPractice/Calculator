using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Exceptions
{
    public class UnrecognizedBuiltInFunctionException : Exception
    {
        public UnrecognizedBuiltInFunctionException(string functionName) :
            base($"Unrecognized built-in function name: {functionName}") { }
    }
}
