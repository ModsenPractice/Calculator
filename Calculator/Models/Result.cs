using Calculator.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Result
    {
        public ResultStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
