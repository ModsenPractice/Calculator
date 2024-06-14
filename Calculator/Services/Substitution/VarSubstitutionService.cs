
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
        var variableList = (await _repostiory.GetVariablesAsync()).ToList(); 
        string pattern = @"\b(\w+)\b(?!\()"; 
        MatchCollection matches = Regex.Matches(source, pattern); 
        
        foreach(Match match in matches){ 
            string variableName = match.Groups[1].Value; 
            Variable? variable = variableList.Find(v => v.Name == variableName); 
            if(variable!=null){ 
                source = Regex.Replace(source, @"\b" + variable.Name + @"\b(?!\()", variable.Value); 
            }
        }

        return source; 
    }
}