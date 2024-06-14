using Calculator.Services.Data.Repositories;
using Calculator.Models;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestClass]
    public class FunctionRepositoryTest
    {
        private string? testFunctionsFilePath;

        [TestInitialize]
        public void Init()
        {
            testFunctionsFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (!string.IsNullOrEmpty(testFunctionsFilePath))
            {
                File.Delete(testFunctionsFilePath);
            }
        }

        [TestMethod]
        public async Task GetFunctionsAsync_ReturnsEmptyList_WhenFileDoesNotExist()
        {
            // Arrange
            var repository = new FunctionRepository();

            // Act
            IEnumerable<Function> functions = await repository.GetFunctionsAsync();

            // Assert
            Assert.IsTrue(!functions.Any());
        }

        [TestMethod]
        public async Task AddFunctionAsync_AddsFunctionToRepository()
        {
            var repository = new FunctionRepository();
            var function = new Function()
            {
                Name = "f1",
                Params = new Dictionary<string, string>(),
                Expression = "x+x^2"
            };

            await repository.AddFunctionAsync(function);
            IEnumerable<Function> functions = await repository.GetFunctionsAsync();

            Assert.IsNotNull(functions);
            Assert.AreEqual(1, functions.Count());

            
        }

        [TestMethod]
        public async Task DeleteFunctionAsync_RemovesFunctionFromRepository()
        {
            // Arrange
            var repository = new FunctionRepository();
            var function1 = new Function
            {
                Name = "f1",
                Params = new Dictionary<string, string>(),
                Expression = "x+x^2"
            };
            var function2 = new Function
            {
                Name = "f2",
                Params = new Dictionary<string, string>(),
                Expression = "x / y"
            };
            await repository.AddFunctionAsync(function1);
            await repository.AddFunctionAsync(function2);

            // Act
            await repository.DeleteFunctionAsync("f1");
            IEnumerable<Function> functions = await repository.GetFunctionsAsync();

            // Assert
            Assert.AreEqual(1, functions.Count());
            Assert.AreEqual("f2", functions.First().Name);
            Assert.AreEqual("x / y", functions.First().Expression);
        }
    }
}