using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    [TestFixture]
    public class PunarchiPaguPaduTests
    {
        [Test]
        public void TestIfPunarchiPaguPaduIsUyirMuthalUyirIru()
        {
            var words = new List<string> { "அன்பு", "அமிழ்து", "உணவு" };

            foreach(var word in words)
            {
                var wordInLatin = Transliterator.Tamil2Latin(word).Trim();
               int  firstValue = (TamilLanguageConstants.UyirEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 2)) >= 0) ? 0 :
                    (TamilLanguageConstants.AgaraUyirMeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 1)) >= 0 ? 1 : -1);

                int lastValue = (TamilLanguageConstants.MeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 2, 2)) >= 0) ? 1 :
                    (TamilLanguageConstants.UyirEzhuthukkalInUyirMeiInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 1, 1)) >= 0 ? 0 : -1);

                if(firstValue == -1 || lastValue == -1)
                {
                    throw new Exception("Parse Error");
                }
                int finalValue = (firstValue << 1) | lastValue;
                Assert.AreEqual(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')], "உயிர் முதல் உயிர் ஈறு");
                Console.WriteLine(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')]);                
            }
            
        }

        [Test]
        public void TestIfPunarchiPaguPaduIsUyirMuthalMeiIru()
        {
            var words = new List<string> { "உயிர்", "உடல்", "ஊண்" };

            foreach (var word in words)
            {
                var wordInLatin = Transliterator.Tamil2Latin(word).Trim();
                int firstValue = (TamilLanguageConstants.UyirEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 2)) >= 0) ? 0 :
                     (TamilLanguageConstants.AgaraUyirMeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 1)) >= 0 ? 1 : -1);

                int lastValue = (TamilLanguageConstants.MeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 2, 2)) >= 0) ? 1 :
                    (TamilLanguageConstants.UyirEzhuthukkalInUyirMeiInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 1, 1)) >= 0 ? 0 : -1);

                if (firstValue == -1 || lastValue == -1)
                {
                    throw new Exception("Parse Error");
                }
                int finalValue = (firstValue << 1) | lastValue;
                Assert.AreEqual(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')], "உயிர் முதல் மெய் ஈறு");
                Console.WriteLine(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')]);
            }

        }

        [Test]
        public void TestIfPunarchiPaguPaduIsMeiMuthalUyiIru()
        {
            var words = new List<string> { "குலவை", "பறவை", "செவிலி" };

            foreach (var word in words)
            {
                var wordInLatin = Transliterator.Tamil2Latin(word).Trim();
                int firstValue = (TamilLanguageConstants.UyirEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 2)) >= 0) ? 0 :
                     (TamilLanguageConstants.AgaraUyirMeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 1)) >= 0 ? 1 : -1);

                int lastValue = (TamilLanguageConstants.MeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 2, 2)) >= 0) ? 1 :
                    (TamilLanguageConstants.UyirEzhuthukkalInUyirMeiInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 1, 1)) >= 0 ? 0 : -1);

                if (firstValue == -1 || lastValue == -1)
                {
                    throw new Exception("Parse Error");
                }
                int finalValue = (firstValue << 1) | lastValue;
                Assert.AreEqual(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')], "மெய் முதல் உயிர் ஈறு");
                Console.WriteLine(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')]);
            }

        }

        [Test]
        public void TestIfPunarchiPaguPaduIsMeiMuthalMeiIru()
        {
            var words = new List<string> { "குரல்", "சொந்தம்", "குயில்" };

            foreach (var word in words)
            {
                var wordInLatin = Transliterator.Tamil2Latin(word).Trim();
                int firstValue = (TamilLanguageConstants.UyirEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 2)) >= 0) ? 0 :
                     (TamilLanguageConstants.AgaraUyirMeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(0, 1)) >= 0 ? 1 : -1);

                int lastValue = (TamilLanguageConstants.MeyEzhuthukkalInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 2, 2)) >= 0) ? 1 :
                    (TamilLanguageConstants.UyirEzhuthukkalInUyirMeiInLatin.IndexOf(wordInLatin.Substring(wordInLatin.Length - 1, 1)) >= 0 ? 0 : -1);

                if (firstValue == -1 || lastValue == -1)
                {
                    throw new Exception("Parse Error");
                }
                int finalValue = (firstValue << 1) | lastValue;
                Assert.AreEqual(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')], "மெய் முதல் மெய் ஈறு");
                Console.WriteLine(TamilLanguageConstants.PunarchiPaguPadugal[Convert.ToString(finalValue, 2).PadLeft(2, '0')]);
            }

        }
    }
}
