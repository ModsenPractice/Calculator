using Calculator.Models;
using Calculator.Services.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests.Services.Parsing
{
    [TestClass]
    public class VariableParserUnitTest
    {
        [TestMethod]
        public void ParseSingleCharVariable()
        {
            var parser = new VariableParser();

            var res = parser.Parse("x=51");

            var expected = new Variable()
            {
                Name = "x",
                Value = "51"
            };

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ParseMultipleCharVariable()
        {
            var parser = new VariableParser();

            var res = parser.Parse("axc=51");

            var expected = new Variable()
            {
                Name = "axc",
                Value = "51"
            };

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ParseFloatValueVariable()
        {
            var parser = new VariableParser();

            var res = parser.Parse("x=51.5125");

            var expected = new Variable()
            {
                Name = "x",
                Value = "51.5125"
            };

            Assert.AreEqual(expected, res);
        }
    }
}
