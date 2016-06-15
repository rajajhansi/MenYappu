using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{

    public class ThodaiTests
    {
        [Fact]
        public void TestIfSeerMonaiExists()
        {
            var paa = @"பாரதி தன்கவியால் பாரதில் தீந்தமிழை
ஊரணியாய்த் தந்த கவி";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.NotEqual(thodaiFinder.SeerMonaiWithinAdi[0][0][0], "1000");
            Assert.Equal(thodaiFinder.SeerMonaiWithinAdi[0][1][0], "பொழிப்பு");
        }

        [Fact]
        public void TestIfAdiMonaiExists()
        {
            var paa = @"கடவுள் மறுப்பாளர் கூடஆ சானைக்
கடவுளாய்ப் போற்றல் வியப்பு";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.True(thodaiFinder.MatchingAdiMonai[0].Count > 1);
        }
        [Fact]
        public void TestIfSeerEthukaiExists()
        {
            var paa = @"பாரதி தன்கவியால் பாரதில் தீந்தமிழை
ஊரணியாய்த் தந்த கவி";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.NotEqual(thodaiFinder.SeerEthukaiWithinAdi[0][0][0], "1000");
            Assert.Equal(thodaiFinder.SeerEthukaiWithinAdi[0][1][0], "பொழிப்பு");
        }

        [Fact]
        public void TestIfAdiEthukaiExists()
        {
            var paa = @"கடவுள் மறுப்பாளர் கூடஆ சானைக்
கடவுளாய்ப் போற்றல் வியப்பு";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.True(thodaiFinder.MatchingAdiEthukai[0].Count > 1);
        }
        [Fact]
        public void TestIfSeerIyaipuExists()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.NotEqual(thodaiFinder.SeerIyaipuWithinAdi[0][0][0], "1000");
            Assert.Equal(thodaiFinder.SeerIyaipuWithinAdi[0][1][0], "மேற்கதுவாய்");
        }

        [Fact]
        public void TestIfAdiIyaipuExists()
        {
            var paa = @"வில்லே நுதலே வேற்கண் கயலே
பல்லே தளவம் பாலே சொல்லே
புயலே குழலே மயிலே";
            var prosodyParser = new ProsodyParser(paa);
            var thodaiFinder = prosodyParser.GetThodais();
            Assert.True(thodaiFinder.MatchingAdiIyaipu[0].Count > 1);
        }
    }
}
