using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.DataStructures.StringSearch;
using RjamSoft.Tamil.Grammar.Prosody.Core;
using Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class AgarathiTests
    {
        [Fact]
        public void TestFetchEhukais()
        {
            var firstWord = "அகரம்";
            var ethukaiWord = "அகராதி";

            var agarathi = new TamilAgarathi();
            var prosodyParser = new ProsodyParser();
            try
            {
                var allWords = agarathi.Lookup("க");
                var ethukaiWords = allWords.Where(w => prosodyParser.CheckEthukai(Transliterator.Tamil2Latin(firstWord), Transliterator.Tamil2Latin(w)));
                Assert.Equal(ethukaiWord, ethukaiWords.FirstOrDefault(w => w == ethukaiWord));
            }
            catch (IOException ioException) { Console.WriteLine("Error: {0}", ioException.Message); }
            catch (UnauthorizedAccessException unauthorizedAccessException) { Console.WriteLine("Error: {0}", unauthorizedAccessException.Message); }

            Console.WriteLine("-------------Press any key to quit--------------");
        }
    }
}
