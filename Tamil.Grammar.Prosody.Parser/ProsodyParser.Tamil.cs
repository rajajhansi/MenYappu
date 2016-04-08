using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tamil.Grammar.Prosody.Parser;

namespace RjamSoft.Tamil.Grammar.Parser
{
    public partial class ProsodyParser
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> TamilParseTreeRoot;
        public List<Dictionary<string, string>> TamilWordBond;
        public List<string> TamilLineClass;
        private void TransliterateLatinTamilToTamil()
        {
            foreach (var adiKeyValue in TamilParseTreeRoot)
            {
                foreach (var seerKeyValue in adiKeyValue.Value)
                {
                    foreach (var asaiKeyValue in seerKeyValue.Value)
                    {
                        foreach (var asai in asaiKeyValue.Value)
                        {
                            //asai[asai.Key] = Transliterator.LatinTamil2Tamil(asai.Value);
                        }
                    }
                }
            }
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> GetTamilTextSyllablePattern(string tamilProsodyText)
        {
            tamilProsodyText = Regex.Replace(tamilProsodyText, @"\(.*\)", "");    // remove paranthesized words
            tamilProsodyText = RemovePunctuation(tamilProsodyText);               // remove Punctuation and reformatting the text.
            string[] lines = tamilProsodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);    // Separate lines of text
            // remove unnecessary spaces
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = Regex.Replace(lines[i], @"\s$", "");
            }
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> LineList = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
            var LineCount = 1;
            foreach (var line in lines)
            {
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> WordList = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
                int WordCount = 1;
                var words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    Dictionary<int, Dictionary<string, string>> WordSyllable = new Dictionary<int, Dictionary<string, string>>();

                    var Word = word;
                    Word = Word.Replace("W", "B").Replace("Y", "Q");    // B - aukaarakurukkam, Q - aikaarakurukkam
                    Word = Regex.Replace(Word, "(\b.)B", "$1ௌ");
                    Word = Regex.Replace(Word, "(\b.)Q", "$1ை");

                    // Get Nirai Words
                    MatchCollection matches = ProsodyGrammarConstants.AsaiRegularExpressions.TamilNiraiAsai.Matches(Word);

                    foreach (Match match in matches)
                    {
                        var asaiValue = Transliterator.LatinTamil2Tamil(match.Value);
                        WordSyllable[match.Index] = new Dictionary<string, string> { { "நிரை", asaiValue } };
                        string replaceNirais = new string('^', match.Value.Length);
                        Word = Word.Replace(match.Value, replaceNirais);
                    }

                    // Get Ner Words
                    matches = ProsodyGrammarConstants.AsaiRegularExpressions.TamilNerAsai.Matches(Word);

                    foreach (Match match in matches)
                    {
                        var asaiValue = Transliterator.LatinTamil2Tamil(match.Value);
                        WordSyllable[match.Index] = new Dictionary<string, string> { { "நேர்", asaiValue } };
                    }

                    WordSyllable = (from entry in WordSyllable orderby entry.Key ascending select entry).ToDictionary(x => x.Key, x => x.Value);

                    // Store asai values from the word
                    Dictionary<string, Dictionary<string, string>> Syllable = new Dictionary<string, Dictionary<string, string>>();
                    int SyllableCount = 1;
                    StringBuilder WordPattern = new StringBuilder();

                    foreach (var wordSyllable in WordSyllable)
                    {
                        Syllable[string.Format("அசை-{0}", SyllableCount++)] = wordSyllable.Value;

                        foreach (var asai in wordSyllable.Value)
                        {
                            WordPattern.Append(asai.Key);
                        }
                    }

                    if (WordPattern.Length > 0)
                    {
                        Syllable["சீர்வகை"] = new Dictionary<string, string> { { "சீர்வகை", ProsodyGrammarConstants.TamilWordType[WordPattern.ToString()] } };
                    }
                    else
                    {
                        WordCount--;
                    }

                    // Store the seer
                    WordList[string.Format("சீர்-{0}", WordCount++)] = Syllable;
                }
                WordList["அடிவகை"] = new Dictionary<string, Dictionary<string, string>> { { "அடிவகை", new Dictionary<string, string> { { "அடிவகை", Convert.ToString(--WordCount) } } } };
                LineList[string.Format("அடி-{0}", LineCount++)] = WordList;
            }
            this.TotalLines = --LineCount;
            return LineList;
        }
        public int GetTamilVikalpaCount()
        {
            return GetVikalpaCountHelper(CheckTamilEthukai);
        }
        public bool CheckTamilEthukai(string FirstWord, string SecondWord)
        {
            bool EthukaiLetterCheck = false;
            List<string> LongVowels = new List<string> { "ஆ", "ஈ", "ஊ", "ஏ", "ஐ", "ஓ", "ஒள" };
            List<string> ShortVowels = new List<string> { "அ", "இ", "உ", "எ", "ஒ" };

            string FirstWordInitialLetter = FirstWord.Substring(1, 1);
            string SecondWordInitialLetter = SecondWord.Substring(1, 1);

            string FirstWordSecondLetter = FirstWord.Substring(2, 2);
            string SecondWordSecondLetter = SecondWord.Substring(2, 2);

            bool InLongVowels = (LongVowels.Find(l => l == FirstWordInitialLetter) == FirstWordInitialLetter) &&
                                ((LongVowels.Find(l => l == SecondWordInitialLetter) == SecondWordInitialLetter));
            bool InShortVowels = (ShortVowels.Find(l => l == FirstWordInitialLetter) == FirstWordInitialLetter) &&
                                ((ShortVowels.Find(l => l == SecondWordInitialLetter) == SecondWordInitialLetter));
            bool VowelLengthCheck = InLongVowels || InShortVowels;

            if (FirstWordSecondLetter.Substring(0, 1) == "_" || SecondWordSecondLetter.Substring(0, 1) == "_")
            {
                EthukaiLetterCheck = (FirstWordSecondLetter == SecondWordSecondLetter);
            }
            else
            {
                EthukaiLetterCheck = (FirstWordSecondLetter.Substring(0, 1) == SecondWordSecondLetter.Substring(0, 1));
            }
            return (VowelLengthCheck && EthukaiLetterCheck);
        }
        public bool CheckTamilMonai(string FirstWord, string SecondWord)
        {
            Dictionary<int, List<string>> MonaiVowels = new Dictionary<int, List<string>>
                {
                    {1, new List<string> {"அ", "ஆ", "ஐ", "ஒள"}},
                    {2, new List<string> {"இ", "ஈ", "எ", "ஏ"}},
                    {3, new List<string> {"உ","ஊ","ஒ","ஓ"}}
                };
            Dictionary<int, List<string>> MonaiConsonants = new Dictionary<int, List<string>>
                {
                    {1, new List<string>{"ஞ","ந"}},
                    {2, new List<string>{"ம","வ"}},
                    {3, new List<string>{"த","ச"}}
                };

            var MonaiFirstLetter = false;
            var MonaiSecondLetter = false;

            if (FirstWord.Substring(0, 1) == SecondWord.Substring(0, 1))
            {
                MonaiFirstLetter = true;
            }
            else
            {
                foreach (var monaiConsonant in MonaiConsonants)
                {
                    if (monaiConsonant.Value.IndexOf(FirstWord.Substring(0, 1)) > 0 && monaiConsonant.Value.IndexOf(SecondWord.Substring(0, 1)) > 0)
                    {
                        MonaiFirstLetter = true;
                    }
                }
            }
            if (MonaiFirstLetter)
            {
                foreach (var monaiVowel in MonaiVowels)
                {
                    if (monaiVowel.Value.IndexOf(FirstWord.Substring(1, 1)) >= 0 && monaiVowel.Value.IndexOf(SecondWord.Substring(1, 1)) >= 0)
                    {
                        MonaiSecondLetter = true;
                    }
                }
            }
            return MonaiSecondLetter;
        }
        public List<Dictionary<string, string>> GetTamilWordBond(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetWordBondHelper(root, new List<string> { "நேர்", "நிரை", "சீர்வகை" }, ProsodyGrammarConstants.Thalaigal.TamilThalaiRules);
        }
        public List<string> GetTamilLineClass(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetLineClassHelper(root, "அடிவகை", ProsodyGrammarConstants.TamilLineType);
        }

        public ProsodyPart ParseInTamil()
        {
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            var tamilText = InputSourceText;
            var tamilProsodyText = Transliterator.Tamil2LatinTamil(tamilText).Trim();
            this.LetterCount = GetLetterCount(tamilText);
            MathiraiCount = CalculateMathiraiForPaa(tamilText);
            MathiraiCount.ForEach(l => TotalMathiraiCount += l.Sum(m => m.Value));
            this.TamilParseTreeRoot = GetTamilTextSyllablePattern(tamilProsodyText);
            GetThodai(tamilProsodyText, CheckTamilEthukai);
            this.AdiThodaiWithThodaiType["எதுகை"] = this.AdiThodai;
            this.MatchingAdiThodaigalWithThodaiType["எதுகை"] = this.MatchingAdiThodaigal;
            this.SeerThodaiWithinAdiWithThodaiType["எதுகை"] = this.SeerThodaiWithinAdi;
            GetThodai(tamilProsodyText, CheckTamilMonai);
            this.AdiThodaiWithThodaiType["மோனை"] = this.AdiThodai;
            this.MatchingAdiThodaigalWithThodaiType["மோனை"] = this.MatchingAdiThodaigal;
            this.SeerThodaiWithinAdiWithThodaiType["மோனை"] = this.SeerThodaiWithinAdi;
            GetThodai(ProsodyText, CheckIyaipu);
            this.AdiThodaiWithThodaiType["_iyYpu"] = new Dictionary<int, string>(AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["_iyYpu"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["_iyYpu"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            this.VikalpaCount = GetVikalpaCount();
            this.TamilWordBond = GetTamilWordBond(this.TamilParseTreeRoot);
            this.TamilLineClass = GetTamilLineClass(this.TamilParseTreeRoot);
            this.MetreType = GetMetreType();

            TransliterateLatinTamilToTamil();

            return new ProsodyPart
            {
                MetreType = this.MetreType,
                LetterCount = this.LetterCount,
                MathiraiCount = MathiraiCount,
                TotalMathiraiCount = TotalMathiraiCount,
                VenLastSyllable = VenLastSyllable,
                ParseTree = this.ParseTreeRoot,
                WordBond = this.WordBond,
                Lines = this.InputSourceText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>(),
                LineClass = this.LineClass,
                MatchingAdiEthukai = this.MatchingAdiThodaigalWithThodaiType["_etukY"],
                SeerEthukaiWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["_etukY"],
                MatchingAdiMonai = this.MatchingAdiThodaigalWithThodaiType["mOVY"],
                SeerMonaiWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["mOVY"],
                MatchingAdiIyaipu = this.MatchingAdiThodaigalWithThodaiType["_iyYpu"],
                SeerIyaipuWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["_iyYpu"],
                VenpaError = this.VenpaError
            };
        }
    }
}
