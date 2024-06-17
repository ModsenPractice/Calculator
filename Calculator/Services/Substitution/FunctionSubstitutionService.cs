using System.Text;
using System.Text.RegularExpressions;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;

public class FunctionSubstitutionService : ISubstitutionService
{
    private IFunctionRepository _repository; 

    public FunctionSubstitutionService(IFunctionRepository repository){
        _repository = repository; 
    }

    public async Task<string> ReplaceAsync(string source)
    {
        var functionList = (await _repository.GetFunctionsAsync()).ToList();
        string pattern = @"(\w+)\(([^()]*(?:\((?:[^()]*(?:\([^()]*\))?[^()]*)*\))?[^()]*)\)";
        
        while (true)
        {
            var matches = Regex.Matches(source, pattern);
            if (matches.Count == 0) break;

            foreach (Match match in matches)
            {
                var functionName = match.Groups[1].Value;
                var arguments = match.Groups[2].Value;
                var function = functionList.Find(f => f.Name == functionName);

                if (function != null)
                {
                    var resultExpression = function.Expression;
                    var funcArgs = function.Params.ToArray();
                    var argValues = SplitArguments(arguments);

                    for (int i = 0; i < funcArgs.Length; i++)
                    {
                        resultExpression = Regex.Replace(resultExpression, @"\b" + funcArgs[i] + @"\b", argValues[i]);
                    }

                    source = source.Replace(match.Value, "(" + resultExpression + ")");
                }
            }
        }

        return source;
    }

    private List<string> SplitArguments(string arguments)
    {
        var argList = new List<string>();
        int parenthesesDepth = 0;
        StringBuilder currentArg = new StringBuilder();

        foreach (char c in arguments)
        {
            if (c == ',' && parenthesesDepth == 0)
            {
                argList.Add(currentArg.ToString().Trim());
                currentArg.Clear();
            }
            else
            {
                if (c == '(') parenthesesDepth++;
                if (c == ')') parenthesesDepth--;
                currentArg.Append(c);
            }
        }

        if (currentArg.Length > 0)
        {
            argList.Add(currentArg.ToString().Trim());
        }

        return argList;
    }
}