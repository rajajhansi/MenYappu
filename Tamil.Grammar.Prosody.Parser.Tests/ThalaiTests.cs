using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class ThalaiTests
    {
        [Fact]
        public void TestIyarSeerVendalai()
        {
            var lines = new []{ "அன்பும் அறிவும்", "ஆடலும் பாடலும்" };
            var prosodyParser = new ProsodyParser();
            foreach (var line in lines)
            {
                var metricLinkage = prosodyParser.GetMetricAndLinkage(line);
                foreach (Dictionary<string, string> wordBond in metricLinkage.WordBond)
                {
                    Console.WriteLine($"{wordBond["word1"]} - {wordBond["word1"]}");
                    Console.WriteLine($"{wordBond["class1"]} - {wordBond["class1"]}");
                    Assert.Equal(wordBond["bond"], "இயற்சீர் வெண்டளை");
                }
            }
        }
        [Fact]
        public void TestVenSeerVendalai()
        {
            var lines = new[] { "அழகான தோட்டம்", "தோட்டத்தில் காய்கள்" };
            var prosodyParser = new ProsodyParser();
            foreach (var line in lines)
            {
                var metricLinkage = prosodyParser.GetMetricAndLinkage(line);
                foreach (Dictionary<string, string> wordBond in metricLinkage.WordBond)
                {
                    Console.WriteLine($"{wordBond["word1"]} - {wordBond["word1"]}");
                    Console.WriteLine($"{wordBond["class1"]} - {wordBond["class1"]}");
                    Assert.Equal(wordBond["bond"], "வெண்சீர் வெண்டளை");
                }
            }
        }
    }
}
