using Calculator.Models;
using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    public class ValidatorManager(IEnumerable<IExpressionValidator> expressionValidators, IEnumerable<IFunctionValidator> functionValidators, IEnumerable<IVariableValidator> variableValidators) : IValidatorManager
    {
        public Result ValidateExpression(string source)
        {
            var rootValidator = expressionValidators.FirstOrDefault();

            expressionValidators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            var result = new Result();

            rootValidator?.Validate(source, result);

            return result;
        }

        public Result ValidateFunction(string source)
        {
            var rootExpressionValidator = expressionValidators.FirstOrDefault();
            var rootFunctionValidator = functionValidators.FirstOrDefault();

            functionValidators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            expressionValidators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            var result = new Result();

            rootFunctionValidator?.Validate(source, result);
            rootExpressionValidator?.Validate(source, result);

            return result;
        }

        public Result ValidateVariable(string source)
        {
            var rootExpressionValidator = expressionValidators.FirstOrDefault();
            var rootVariableValidator = variableValidators.FirstOrDefault();

            expressionValidators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            variableValidators.Aggregate((prev, curr) => { prev.SetNextValidator(curr); return curr; });
            var result = new Result();

            rootVariableValidator?.Validate(source, result);
            rootExpressionValidator?.Validate(source, result);

            return result;
        }
    }
}
