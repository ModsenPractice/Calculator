using Calculator.Services.Data.Repositories;
using Calculator.Models;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestClass]
    public class VariableRepositoryTest
    {
        private string? testVariablesFilePath;

        [TestInitialize]
        public void Init()
        {
            testVariablesFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (!string.IsNullOrEmpty(testVariablesFilePath))
            {
                File.Delete(testVariablesFilePath);
            }
        }

        [TestMethod]
        public async Task GetVariablesAsync_ReturnsEmptyList_WhenFileDoesNotExist()
        {
            // Arrange
            var repository = new VariableRepository();

            // Act
            IEnumerable<Variable> variables = await repository.GetVariablesAsync();

            // Assert
            Assert.IsTrue(!variables.Any());
        }

        [TestMethod]
        public async Task AddVariableAsync_AddsFunctionToRepository()
        {
            var repository = new VariableRepository();
            var variable = new Variable()
            {
                Name = "x1",
                Value = "1"
            };

            await repository.AddVariableAsync(variable);
            IEnumerable<Variable> variables = await repository.GetVariablesAsync();

            Assert.IsNotNull(variables);
            Assert.AreEqual(1, variables.Count());

        }

        [TestMethod]
        public async Task DeleteVariableAsync_RemovesFunctionFromRepository()
        {
            // Arrange
            var repository = new VariableRepository();
            var variable1 = new Variable
            {
                Name = "x1",
                Value = "3"
            };
            var variable2 = new Variable
            {
                Name = "x2",
                Value = "4"
            };
            await repository.AddVariableAsync(variable1);
            await repository.AddVariableAsync(variable2);

            // Act
            await repository.DeleteVariableAsync("x1");
            IEnumerable<Variable> variables = await repository.GetVariablesAsync();

            // Assert
            Assert.AreEqual(1, variables.Count());
            Assert.AreEqual("x2", variables.First().Name);
            Assert.AreEqual("4", variables.First().Value);
        }
    }
}