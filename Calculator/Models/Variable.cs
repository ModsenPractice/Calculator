using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Variable
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}
