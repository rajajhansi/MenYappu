using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class AsaiTests
    {
        [Fact]
        public void TestNerasai()
        {
            var word = "ராஜா";
            var prosodyParser = new ProsodyParser(word);
            var metricLinkage = prosodyParser.GetMetricAndLinkage();
            foreach (var lineList in metricLinkage.ParseTree)
            {
                foreach (var wordList in lineList.Value)
                {
                    foreach (var seerValue in wordList.Value)
                    {
                        foreach (var asaiValue in seerValue.Value)
                        {
                            Console.WriteLine($"{asaiValue.Key} => {asaiValue.Value}");
                        }
                    }
                }
            }
        }
    }
}