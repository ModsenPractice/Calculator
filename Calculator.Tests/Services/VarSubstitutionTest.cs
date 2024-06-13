using Calculator.Models;
using Calculator.Services;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;

namespace Calculator.Tests
{
    [TestClass]
    public class VarSubstitutionTest
    {
        private ISubstitutionService _substitutionService; 

        public VarSubstitutionTest()
        {
            _substitutionService = new VarSubstitutionService(new TestRepository());
        }

        [TestMethod]
        public async Task ChangeSymbols()
        {
            string result = await _substitutionService.ReplaceAsync("x+y"); 
            string expectedResult = "12+36";

            Assert.AreEqual(expectedResult, result);
        }

        private class TestRepository : IVariableRepository
        {
            public async Task<IEnumerable<Variable>> GetVariablesAsync(){
                await Task.CompletedTask;
                return new List<Variable>(){ 
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

            public Task DeleteVariableAsync(string source)
            {
                throw new NotImplementedException();
            }
        }
    }



}