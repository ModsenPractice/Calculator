
namespace Calculator.Tests
{
    [TestClass]
    public class VarSubstitutionTest
    {
        private ISubstitutionService _substitutionService; 
        private IVariableRepository _rep; 

        public VarSubstitutionTest(){
            _substitutionService = new VarSubstitutionService(new TestRepository()); 
        }

        [TestMethod]
        public async void ChangeSymbols()
        {
            string result = await _substitutionService.ReplaceAsync("x+y"); 
            string expectedResult = "12+36";
        }

        private class TestRepository : IVariableRepository{
            public async Task<IEnumerable<Variable>> GetVariablesAsync(){
                return new IEnumerable<Variable>(){ 
                    new (){
                        Name = "x", 
                        Value="12"
                    },
                    new (){
                        Name = "y", 
                        Value="36"
                    },
                    new (){
                        Name = "ax", 
                        Value="58"
                    },
                    new (){
                        Name = "qwe23", 
                        Value="78"
                    },
                    new (){
                        Name = "qw", 
                        Value="33"
                    }
                };
            }
            public Task AddVariableAsync(Variable function){
                throw new NotImplementedException(); 
            }        
            Task DeleteVariableAsync(string name){
                throw new NotImplementedException(); 
            }
        }
    }



}