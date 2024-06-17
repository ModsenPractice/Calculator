using Calculator.Models;
using Calculator.Services;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;

namespace Calculator.Tests.Services
{
    [TestClass]
    public class FunctionSubstitutionTest
    {
        private ISubstitutionService _substitutionService;

        public FunctionSubstitutionTest()
        {
            _substitutionService = new FunctionSubstitutionService(new TestRepository());
        }

        [TestMethod]
        public async Task SimpleFunction()
        {
            string result = await _substitutionService.ReplaceAsync("f(2)");
            string expectedResult = "((2)+1)";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task ManyParams()
        {
            string result = await _substitutionService.ReplaceAsync("ff(2,4)");
            string expectedResult = "((2)+1+(4)+2*(2))";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task Manyfunctions()
        {
            string result = await _substitutionService.ReplaceAsync("ff(2,4)+f(8)");
            string expectedResult = "((2)+1+(4)+2*(2))+((8)+1)";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task FuntionInFuntion()
        {
            string result = await _substitutionService.ReplaceAsync("ff(f(8),4)");
            string expectedResult = "((((8)+1))+1+(4)+2*(((8)+1)))";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task ParamContainsNameOfOther()
        {
            string result = await _substitutionService.ReplaceAsync("ff(x,ax)");
            string expectedResult = "((x)+1+(ax)+2*(x))";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task RepeatedFuntions()
        {
            string result = await _substitutionService.ReplaceAsync("f(2)+f(4)");
            string expectedResult = "((2)+1)+((4)+1)";
            Assert.AreEqual(expectedResult, result);
        }

        class TestRepository : IFunctionRepository
        {
            public Task AddFunctionAsync(Function function)
            {
                throw new NotImplementedException();
            }

            public Task DeleteFunctionAsync(string name)
            {
                throw new NotImplementedException();
            }

            public async Task<IEnumerable<Function>> GetFunctionsAsync()
            {
                return new List<Function>(){
                new Function(){
                    Name = "f", Expression = "x+1", Params = new List<string>(){"x"}
                },
                new Function(){
                    Name = "ff", Expression = "x+1+y+2*x", Params = new List<string>(){"x", "y"}
                }
            };
            }
        }
    }
}