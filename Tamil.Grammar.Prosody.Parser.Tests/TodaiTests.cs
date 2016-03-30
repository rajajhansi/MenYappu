using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
   
    public class TodaiTests
    {
        [Fact]
        public void TestIfIyaipuExists()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var firstWord = "முகிலே ";
            var secondWord = "பொழிலே ";
            var prosodyParser = new ProsodyParser(paa);
            prosodyParser.Parse();
            Assert.True(prosodyParser.CheckIyaipu(firstWord, secondWord));
        }

        [Fact]
        public void TestIfKeezhkKathuvaiIyaipuExistsInLine1OfAPaa()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var prosodyParser = new ProsodyParser(paa);
            var prosodyPart = prosodyParser.Parse();
            Assert.Equal(TamilLanguageConstants.TodaiVagaigalTamil[prosodyPart.SeerIyaipuWithinAdi[0][0][0]], "கீழ்க்கதுவாய்");
        }
    }
}
