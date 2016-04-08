using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class MathiraiTests
    {
        [Fact]
        public void TestTotalMathiraiCount()
        {
            var sentence = "தமிழ் வாழ்க";
            var prosodyParser = new ProsodyParser(sentence);
            var MathiraiCount = prosodyParser.GetMathiraiCount();

            Assert.True(MathiraiCount.TotalMathiraiCount == 6);
        }
    }
}
