using Calculator.Models;
using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    public class ValidatorManager(IEnumerable<IValidator> validators) : IValidatorManager
    {
        public Result Validate(string source)
        {
            var rootValidator = validators.FirstOrDefault();

            validators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            var result = new Result();

            rootValidator?.Validate(source, result);

            return result;
        }
    }
}
