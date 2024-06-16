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
    }
}
