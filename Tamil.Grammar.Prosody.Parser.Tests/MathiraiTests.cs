using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
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

        [Fact]
        public void TestEachWordsMathiraiCount()
        {
            var sentence = "தமிழ் வாழ்க";
            var mathirais = new Dictionary<string, double> {
            {
               "தமிழ்", 2.5
            },
            {
                "வாழ்க", 3.5
            }};
            var prosodyParser = new ProsodyParser(sentence);
            var MathiraiCount = prosodyParser.GetMathiraiCount();

            foreach (var wordDictionary in MathiraiCount.DetailedMathiraiCount)
            {
                foreach (var wordList in wordDictionary)
                {
                    double mathiraiCountForWord = 0;
                    foreach (var word in wordList.Value)
                    {
                        //mathiraiCountForWord = mathiraiCountForWord + word.Value;
                    }
                    Assert.Equal(mathirais[wordList.Key], mathiraiCountForWord);
                }
            }

            Assert.True(MathiraiCount.TotalMathiraiCount == 6);
        }
        [Fact]
        public void TestTotalMathiraiCountWithGranthaLetters()
        {
            var sentence = "மஹத்தி ராஜா";
            var prosodyParser = new ProsodyParser(sentence);
            var MathiraiCount = prosodyParser.GetMathiraiCount();

            Assert.True(MathiraiCount.TotalMathiraiCount == 7.5);
        }
    }
}
