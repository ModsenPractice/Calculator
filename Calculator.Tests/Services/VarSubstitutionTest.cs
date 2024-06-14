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
        public async Task VariableWith2andMoreLetters(){ 
            string result = await _substitutionService.ReplaceAsync("qwe"); 
            string expectedResult = "33";

            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public async Task VariableWithNumbers(){ 
            string result = await _substitutionService.ReplaceAsync("qwe23"); 
            string expectedResult = "78";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenVariableContainsOtherVariable()
        {
            string result = await _substitutionService.ReplaceAsync("x+ax"); 
            string expectedResult = "12+58";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WithFunction()
        {
            string result = await _substitutionService.ReplaceAsync("f(x,y)"); 
            string expectedResult = "f(12,36)";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WithFunctionLikeVariable()
        {
            string result = await _substitutionService.ReplaceAsync("ax(x,y)"); 
            string expectedResult = "ax(12,36)";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WithEverything()
        {
            string result = await _substitutionService.ReplaceAsync("f(x,y)*12-ax(x,ax)+qwe23*qwe"); 
            string expectedResult = "f(12,36)*12-ax(12,58)+78*33";

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
                        Name = "qwe", 
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