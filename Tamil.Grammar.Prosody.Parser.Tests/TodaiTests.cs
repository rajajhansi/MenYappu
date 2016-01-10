using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    [TestFixture]
    public class TodaiTests
    {
        [Test]
        public void TestIfIyaipuExists()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var firstWord = "முகிலே ";
            var secondWord = "பொழிலே ";
            var prosodyParser = new ProsodyParseTree(paa);
            prosodyParser.Parse();
            Assert.IsTrue(prosodyParser.CheckIyaipu(firstWord, secondWord));
        }

        [Test]
        public void TestIfKeezhkKathuvaiIyaipuExistsInLine1OfAPaa()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var prosodyParser = new ProsodyParseTree(paa);
            prosodyParser.Parse();
            Assert.AreEqual(prosodyParser.SeerThodaiWithinAdiWithThodaiType["_iyYpu"][0][1], "கீழ்க்கதுவாய்");
        }
    }
}
