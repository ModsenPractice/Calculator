
using System.Text.RegularExpressions;
using Calculator.Models;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;

namespace Calculator.Services;

public class VarSubstitutionService : ISubstitutionService
{
    private IVariableRepository _repostiory;

    public VarSubstitutionService(IVariableRepository repository){
        _repostiory = repository; 
    }

    public async Task<string> ReplaceAsync(string source)
    {
        List<Variable> variableList = (await _repostiory.GetVariablesAsync()).ToList(); 
        string pattern = @"(\w+)"; 
        MatchCollection matches = Regex.Matches(source, pattern); 

        foreach(Match match in matches){ 
            string variableName = match.Groups[1].Value; 
            Variable? variable = variableList.Find(v => v.Name == variableName); 
            if(variable!=null){ 
                source = source.Replace(variableName, variable.Value); 
            }
            else{
                Console.WriteLine($"Cannot find {variableName}"); 
            }
        }

        return source; 
    }
}