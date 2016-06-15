using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class AdiTests
    {
        [Fact]
        public void TestAlavadi()
        {
            var paa = @"கடவுள் மறுப்பாளர் கூடஆ சானைக்
கடவுளாய்ப் போற்றல் வியப்பு";
            var prosodyParser = new ProsodyParser(paa);
            var lineMeasure = prosodyParser.GetLineClass(paa);
            Assert.True(lineMeasure.LineClass[0] == "அளவடி" && lineMeasure.LineClass[1] == "சிந்தடி");
        }
    }
}
