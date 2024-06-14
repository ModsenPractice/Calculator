using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Function
    {
        public string Name { get; set; } = null!;
        public IEnumerable<string> Params { get; set; } = [];
        public string Expression { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            return obj is Function function &&
                   Name == function.Name &&
                   Params.SequenceEqual(function.Params) &&
                   Expression == function.Expression;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Params, Expression);
        }
    }
}
