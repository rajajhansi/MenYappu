using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.DataStructures.StringSearch;
using RjamSoft.Tamil.Grammar.Prosody.Core;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class AgarathiFixture : IDisposable
    {
        public ITamilAgarathi TamilAgarathi { get; set; }
        public ProsodyParser ProsodyParser { get; set; }
        public AgarathiFixture()
        {
            TamilAgarathi = new TamilAgarathi();
            ProsodyParser = new ProsodyParser();
        }
        public void Dispose()
        {

        }
    }
    public class AgarathiTests : IClassFixture<AgarathiFixture>
    {
        AgarathiFixture _agarathiFixture;

        public AgarathiTests(AgarathiFixture agarathiFixture)
        {
            this._agarathiFixture = agarathiFixture;
        }


        [Fact]
        public void TestFetchEhukais()
        {
            var firstWord = "அகரம்";
            var ethukaiWord = "அகராதி";

            try
            {
                var secondLetter = firstWord.TamilSubstr(1, 1);
                var allWords = _agarathiFixture.TamilAgarathi.Lookup(secondLetter).Where(w => (w.TamilLength() > 1 && w.TamilSubstr(1, 1) == secondLetter));
                var latinFirstWord = Transliterator.Tamil2Latin(firstWord);
                var ethukaiWords = allWords.Where(w => _agarathiFixture.ProsodyParser.CheckEthukai(latinFirstWord, Transliterator.Tamil2Latin(w)));
                Assert.Equal(ethukaiWord, ethukaiWords.FirstOrDefault(w => w == ethukaiWord));
            }
            catch (IOException ioException) { Console.WriteLine("Error: {0}", ioException.Message); }
            catch (UnauthorizedAccessException unauthorizedAccessException) { Console.WriteLine("Error: {0}", unauthorizedAccessException.Message); }

            Console.WriteLine("-------------Press any key to quit--------------");
        }

        [Fact]
        public void TestFetchMonais()
        {
            var firstWord = "தமிழ்";
            var monaiWord = "தஞ்சாவூர்";

            try
            {
                var firstLetter = firstWord.TamilSubstr(0, 1);
                var allWords =
                    _agarathiFixture.TamilAgarathi.Lookup(firstLetter).Where(w => (w.TamilLength() > 1 &&  w.TamilSubstr(0, 1) == firstLetter));
                var latinFirstWord = Transliterator.Tamil2Latin(firstWord);
                var monaiWords = allWords.Where(w => _agarathiFixture.ProsodyParser.CheckMonai(latinFirstWord, Transliterator.Tamil2Latin(w)));
                Assert.Equal(monaiWord, monaiWords.FirstOrDefault(w => w == monaiWord));
            }
            catch (IOException ioException) { Console.WriteLine("Error: {0}", ioException.Message); }
            catch (UnauthorizedAccessException unauthorizedAccessException) { Console.WriteLine("Error: {0}", unauthorizedAccessException.Message); }

            Console.WriteLine("-------------Press any key to quit--------------");
        }

        [Fact]
        public void TestFetchIyaipus()
        {
            var firstWord = "அகம்";
            var iyaipuWord = "புறம்";

            try
            {
                var lastLetter = firstWord.TamilSubstr(firstWord.TamilLength() - 1, 1);
                var allWords = _agarathiFixture.TamilAgarathi.Lookup(lastLetter).
                    Where(w => (w.TamilLength() > 1 && w.TamilSubstr(w.TamilLength() - 1, 1) == lastLetter));
                var latinFirstWord = Transliterator.Tamil2Latin(firstWord);
                var iyaipuWords = allWords.Where(w => _agarathiFixture.ProsodyParser.CheckIyaipu(latinFirstWord, Transliterator.Tamil2Latin(w))).ToList();
                Assert.Equal(iyaipuWord, iyaipuWords.FirstOrDefault(w => w == iyaipuWord));
            }
            catch (IOException ioException) { Console.WriteLine("Error: {0}", ioException.Message); }
            catch (UnauthorizedAccessException unauthorizedAccessException) { Console.WriteLine("Error: {0}", unauthorizedAccessException.Message); }

            Console.WriteLine("-------------Press any key to quit--------------");
        }
    }
}
