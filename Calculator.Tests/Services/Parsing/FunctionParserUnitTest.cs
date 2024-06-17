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
    public class FunctionParserUnitTest
    {
        [TestMethod]
        public void ParseSingleCharNameFunction()
        {
            var parser = new FunctionParser();

            var res = parser.Parse("f(x)=x^2");

            var expected = new Function()
            {
                Name = "f",
                Expression = "x^2",
                Params = [ "x" ]
            };

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ParseFunctionWithNumberInName()
        {
            var parser = new FunctionParser();

            var res = parser.Parse("fr2(x)=x^2");

            var expected = new Function()
            {
                Name = "fr2",
                Expression = "x^2",
                Params = ["x"]
            };

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ParseFunctionWithMultipleParams()
        {
            var parser = new FunctionParser();

            var res = parser.Parse("f(x,y,z)=x^2+y-z/5");

            var expected = new Function()
            {
                Name = "f",
                Expression = "x^2+y-z/5",
                Params = ["x", "y", "z"],
            };

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ParseFunctionWithMultipleCharNameParam()
        {
            var parser = new FunctionParser();

            var res = parser.Parse("f(xyz)=xyz+51");

            var expected = new Function()
            {
                Name = "f",
                Expression = "xyz+51",
                Params = ["xyz"],
            };

            Assert.AreEqual(expected, res);
        }

    }
}
