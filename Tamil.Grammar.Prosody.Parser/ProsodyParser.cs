using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Tamil.Grammar.Prosody.Parser;

namespace RjamSoft.Tamil.Grammar.Parser
{
    public partial class ProsodyParser
    {
        // The Root of the Parse Tree
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTreeRoot;
        // The Input Source Text
	    public string InputSourceText;

        public string ProsodyText;
	    // Variables for Metrical Information
        // The Metre type of the Prosody Text
	    public string MetreType;
	    // The Total Lines in the Text
	    public int TotalLines;
	    // Contains the Talai Informatoin
	    public List<Dictionary<string, string>> WordBond;
        public string VenLastSyllable;

	    public Dictionary<string, int> LetterCount;
        public List<Dictionary<string, double>> MathiraiCount;
        public double TotalMathiraiCount;
	    public List<string> LineClass;

	    public int VikalpaCount;
        public Dictionary<int, string> AdiThodai = new Dictionary<int, string>();
        public List<Dictionary<int, string>> MatchingAdiThodaigal = new List<Dictionary<int, string>>();
        public Dictionary<int, List<List<string>>> SeerThodaiWithinAdi = new Dictionary<int, List<List<string>>>();

        public Dictionary<string, Dictionary<int, string>> AdiThodaiWithThodaiType = new Dictionary<string, Dictionary<int, string>>();
        public Dictionary<string, List<Dictionary<int, string>>> MatchingAdiThodaigalWithThodaiType = new Dictionary<string, List<Dictionary<int, string>>>();
        public Dictionary<string, Dictionary<int, List<List<string>>>> SeerThodaiWithinAdiWithThodaiType = new Dictionary<string, Dictionary<int, List<List<string>>>>();
	    public string Lang;

        public Func<string, string, bool>[] ThodaiCalculators;

        private bool WordBondClassCheck;
        private bool FinalSyllableClassCheck;
        private bool ThaniCholExists;

        private bool ShouldParseKutriyalukaram = false;
        private bool ShouldParseVilaangaaySeer = false;
        private bool ShouldCompareVenpaRules = false;
        public Dictionary<string, List<string>> VenpaError = new Dictionary<string, List<string>>
        {
            { "word", new List<string> { "ஈற்றடியின் ஈற்றுச்சீரைத் தவிர்த்து ஈரசைச்சீர்களும் காய்ச்சீர்களும் மட்டுமே பயின்று வருதல் வேண்டும்" } },
            { "bond", new List<string> { "வெண்டளைகள் மட்டுமே பயின்று வருதல் வேண்டும்" } },
            { "line", new List<string> { "ஈற்றடி மூன்று சீர்களும் ஏனைய அடிகள் நான்கு சீர்களும் கொண்டிருத்தல் வேண்டும்" } },
            { "final", new List<string> { "ஈற்றடியின் ஈற்றுச்சீர் நாள், மலர், காசு, பிறப்பு ஆகியவற்றுள் இருத்தல் வேண்டும்" } }
        };
        public ProsodyParser(string prosodyText= "தமிழ்", bool shouldParseKutriyalukaram = false,
                             bool shouldParseVilaangaaySeer = false, bool shouldCompareVenpaRules = false)
        {
            this.InputSourceText = prosodyText;
            ShouldParseKutriyalukaram = shouldParseKutriyalukaram;
            ShouldParseVilaangaaySeer = shouldParseVilaangaaySeer;
            ShouldCompareVenpaRules = shouldCompareVenpaRules;
            this.ThodaiCalculators = new Func<string, string, bool>[] {CheckMonai, CheckEthukai};
        }

        public MathiraiCounter GetMathiraiCount(string prosodyText="")
        {
            InputSourceText = (prosodyText.Length > 0) ? prosodyText : InputSourceText;
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            var tamilText = InputSourceText;
            this.LetterCount = GetLetterCount(tamilText);
            MathiraiCount = CalculateMathiraiForPaa(tamilText);
            MathiraiCount.ForEach(l => TotalMathiraiCount += l.Sum(m => m.Value));

            return new MathiraiCounter
            {
                MathiraiCount = MathiraiCount,
                TotalMathiraiCount = TotalMathiraiCount
            };
        }

        public ThodaiFinder GetThodais(string prosodyText="")
        {
            InputSourceText = (prosodyText.Length > 0) ? prosodyText : InputSourceText;
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            GetThodai(ProsodyText, CheckEthukai);
            this.AdiThodaiWithThodaiType["_etukY"] = new Dictionary<int, string>(this.AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["_etukY"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["_etukY"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            GetThodai(ProsodyText, CheckMonai);
            this.AdiThodaiWithThodaiType["mOVY"] = new Dictionary<int, string>(AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["mOVY"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["mOVY"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            GetThodai(ProsodyText, CheckIyaipu);
            this.AdiThodaiWithThodaiType["_iyYpu"] = new Dictionary<int, string>(AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["_iyYpu"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["_iyYpu"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);

            return new ThodaiFinder
            {
                Lines = this.InputSourceText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>(),
                MatchingAdiEthukai = this.MatchingAdiThodaigalWithThodaiType["_etukY"],
                SeerEthukaiWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["_etukY"],
                MatchingAdiMonai = this.MatchingAdiThodaigalWithThodaiType["mOVY"],
                SeerMonaiWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["mOVY"],
                MatchingAdiIyaipu = this.MatchingAdiThodaigalWithThodaiType["_iyYpu"],
                SeerIyaipuWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["_iyYpu"]
            };
        }

        public MetricLinkage GetMetricAndLinkage(string prosodyText="")
        {
            InputSourceText = (prosodyText.Length > 0) ? prosodyText : InputSourceText;
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            this.ParseTreeRoot = GetTextSyllablePattern(ProsodyText);
            this.WordBond = GetWordBond(this.ParseTreeRoot);
            return new MetricLinkage
            {
                ParseTree = ParseTreeRoot,
                WordBond = WordBond
            };
        }
        public ProsodyPart Parse(string prosodyText="")
        {
            InputSourceText = (prosodyText.Length > 0) ? prosodyText : InputSourceText;
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            var tamilText = InputSourceText;
            this.LetterCount = GetLetterCount(tamilText);
            MathiraiCount = CalculateMathiraiForPaa(tamilText);
            MathiraiCount.ForEach(l => TotalMathiraiCount += l.Sum(m => m.Value));
            this.ParseTreeRoot = GetTextSyllablePattern(ProsodyText);
            GetThodai(ProsodyText, CheckEthukai);
            this.AdiThodaiWithThodaiType["_etukY"] = new Dictionary<int, string>(this.AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["_etukY"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["_etukY"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            GetThodai(ProsodyText, CheckMonai);
            this.AdiThodaiWithThodaiType["mOVY"] = new Dictionary<int, string>(AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["mOVY"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["mOVY"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            GetThodai(ProsodyText, CheckIyaipu);
            this.AdiThodaiWithThodaiType["_iyYpu"] = new Dictionary<int, string>(AdiThodai);
            this.MatchingAdiThodaigalWithThodaiType["_iyYpu"] = new List<Dictionary<int, string>>(this.MatchingAdiThodaigal);
            this.SeerThodaiWithinAdiWithThodaiType["_iyYpu"] = new Dictionary<int, List<List<string>>>(this.SeerThodaiWithinAdi);
            this.VikalpaCount = GetVikalpaCount();
            this.WordBond = GetWordBond(this.ParseTreeRoot);
            this.LineClass = GetLineClass(this.ParseTreeRoot);
            this.MetreType = GetMetreType();
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
        public List<Dictionary<string, double>> CalculateMathiraiForPaa(string prosodyText)
        {
            List<Dictionary<string, double>> mathiraiCountForEachSeerInAllLines = new List<Dictionary<string, double>>();
            prosodyText = Regex.Replace(prosodyText, @"\(.*\)", ""); // remove paranthesized words
            prosodyText = RemovePunctuation(prosodyText); // remove Punctuation and reformatting the text.
            string[] lines = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                // Separate lines of text
            // remove unnecessary spaces
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = Regex.Replace(lines[i], @"\s$", "");
            }

            foreach (var line in lines)
            {
                Dictionary<string, double> mathiraiCountForEachSeer = new Dictionary<string, double>();
                var words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    double mathiraiCount = CalculateMathirai(word, false);
                    mathiraiCountForEachSeer[word] = mathiraiCount;
                }
                mathiraiCountForEachSeerInAllLines.Add(mathiraiCountForEachSeer);
            }
            return mathiraiCountForEachSeerInAllLines;
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> GetTextSyllablePattern(string prosodyText)
        {
            prosodyText = Regex.Replace(prosodyText, @"\(.*\)", "");    // remove paranthesized words
            prosodyText = RemovePunctuation(prosodyText);               // remove Punctuation and reformatting the text.
            string[] lines = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);    // Separate lines of text
            // remove unnecessary spaces
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = Regex.Replace(lines[i], @"\s$", "");
            }
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> LineList = new Dictionary<string, Dictionary<string,Dictionary<string,Dictionary<string,string>>>>();
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
                    Word = Regex.Replace(Word, @"(\b.)B", "$1W");
                    Word = Regex.Replace(Word, @"(\b.)Q", "$1Y");

                    // Get Nirai Words
                    MatchCollection matches = ProsodyGrammarConstants.AsaiRegularExpressions.NiraiAsai.Matches(Word);

                    foreach (Match match in matches)
                    {
                        var asaiValue = Transliterator.Latin2Tamil(match.Value);
                        //WordSyllable[match.Index] = new Dictionary<string, string> { { "nirY", asaiValue } };
                        WordSyllable[match.Index] = new Dictionary<string, string> { { "நிரை", asaiValue } };
                        string replaceNirais = new string('^', match.Value.Length);
                        Word = Word.Replace(match.Value, replaceNirais);
                    }

                    // Get Ner Words
                    matches = ProsodyGrammarConstants.AsaiRegularExpressions.NerAsai.Matches(Word);

                    foreach (Match match in matches)
                    {
                        var asaiValue = Transliterator.Latin2Tamil(match.Value);
                        //WordSyllable[match.Index] = new Dictionary<string, string> { { "nE_r", asaiValue } };
                        WordSyllable[match.Index] = new Dictionary<string, string> { { "நேர்", asaiValue } };
                    }

                    WordSyllable = (from entry in WordSyllable orderby entry.Key ascending select entry).ToDictionary(x => x.Key, x => x.Value);

                    // Store asai values from the word
                    Dictionary<string, Dictionary<string, string>> Syllable = new Dictionary<string, Dictionary<string, string>>();
                    int SyllableCount = 1;
                    StringBuilder WordPattern = new StringBuilder();

                    foreach(var wordSyllable in WordSyllable)
                    {
                        Syllable[$"acY-{SyllableCount++}"] = wordSyllable.Value;

                        foreach (var asai in wordSyllable.Value)
                        {
                            WordPattern.Append(asai.Key);
                        }
                    }

                    if (WordPattern.Length > 0)
                    {
                        var wordPattern = WordPattern.ToString();
                        if ( ShouldParseVilaangaaySeer &&
                            (Syllable.Count == 3) &&
                            (Syllable["acY-2"].Keys.ElementAt(0) == "நிரை") &&
                            (Transliterator.Tamil2Latin(Syllable["acY-2"]["நிரை"]).Substring(3, 1).IndexOfAny("AIUEOQYBW".ToCharArray()) == 0))
                        {
                            wordPattern = wordPattern.Insert(8, "2");
                        }
                        Syllable["meta"] = new Dictionary<string, string>{ {"meta", ProsodyGrammarConstants.TamilWordType[wordPattern] }};
                    }
                    else
                    {
                        WordCount--;
                    }

                    // Store the seer
                    WordList[$"cI_r-{WordCount++}"] = Syllable;
                }
                WordList["smeta"] = new Dictionary<string, Dictionary<string, string>> { { "smeta", new Dictionary<string, string> { { "smeta", Convert.ToString(--WordCount) } } } };
                LineList[$"aTi-{LineCount++}"] = WordList;
            }
            this.TotalLines = --LineCount;

            return LineList;
        }

        private int GetVikalpaCountHelper(Func<string, string, bool> CheckEthukaiFunc)
        {
            var prosodyText = RemovePunctuation(ProsodyText);
            string[] lines = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);    // Separate lines of text
            int VikalpaCount = 1;
            List<string> FeetWords = new List<string>();

            foreach (var line in lines)
            {
                var Words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                FeetWords.Add(Words[0]);
            }
            for (var WordIndex = 0; WordIndex < FeetWords.Count - 1; WordIndex++)
            {
                if (!CheckEthukaiFunc(FeetWords[WordIndex], FeetWords[WordIndex + 1]))
                {
                    VikalpaCount++;
                }
            }
            return VikalpaCount;
        }

        public void GetThodai(string prosodyText, Func<string, string, bool> CheckThodaiFunc )
        {
            //Dictionary<int, string> AdiThodai = new Dictionary<int, string>();
            //Dictionary<int, List<string>> SeerThodaiWithinAdi = new Dictionary<int, List<string>>();
            AdiThodai.Clear();
            SeerThodaiWithinAdi.Clear();
            MatchingAdiThodaigal.Clear();
            prosodyText = RemovePunctuation(prosodyText);
            var adigal = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            var mudhalSeerInMudhalAdi = adigal[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];

            for (var adiIndex = 0; adiIndex < adigal.Count; adiIndex++)
            {
                var seerThodaiInAdiMask = 0;
                var seerThodaiInAdi = new StringBuilder();
                var seergal = adigal[adiIndex].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var hasFoundThodaiInAdi = false;
                for (var startingSeerIndex = 0; startingSeerIndex < seergal.Length; startingSeerIndex++)
                {
                    if (hasFoundThodaiInAdi)
                    {
                        break;
                    }
                    seerThodaiInAdiMask = 8 >> startingSeerIndex;
                    seerThodaiInAdi = new StringBuilder();
                    seerThodaiInAdi.Append(startingSeerIndex + 1)
                                .Append(' ')
                                .Append(/*Transliterator.Latin2Tamil(*/seergal[startingSeerIndex]/*)*/)
                                .Append(' ');
                    //seerThodaiInAdi.Append("1 ").Append(Transliterator.Latin2Tamil(seergal[0])).Append(' ');
                    for (var seerIndex = startingSeerIndex + 1; seerIndex < seergal.Length; seerIndex++)
                    {
                        if (CheckThodaiFunc(seergal[startingSeerIndex], seergal[seerIndex]))
                        {
                            hasFoundThodaiInAdi = true;
                            seerThodaiInAdiMask |= (8 >> seerIndex);
                            seerThodaiInAdi.Append(seerIndex + 1)
                                .Append(' ')
                                .Append(/*Transliterator.Latin2Tamil(*/seergal[seerIndex]/*)*/)
                                .Append(' ');
                        }
                    }
                }
                seerThodaiInAdiMask = (hasFoundThodaiInAdi) ? seerThodaiInAdiMask : 8;
                var seerThodaiInAdiMaskString =
                    ThodaiProcessor.CalcSeerThodaiInAdiMaskFuncDictionary[CheckThodaiFunc.Method.Name](seerThodaiInAdiMask);
                SeerThodaiWithinAdi[adiIndex] = new List<List<string>>
                                                    {
                                                        new List<string> { seerThodaiInAdiMaskString },
                                                        new List<string>{ ProsodyGrammarConstants.TodaiVagaigalTamil[seerThodaiInAdiMaskString]},
                                                        ThodaiProcessor.GetThodaiForDisplay(seerThodaiInAdi.ToString(), ProsodyGrammarConstants.ThodaiTypeDictionary[CheckThodaiFunc.Method.Name])
                                                    };
            }
            CalculateAdiThodai(adigal, 0, CheckThodaiFunc);
        }

        private bool CheckIfAllAdigalAreVisited(List<string> adigal)
        {
            return adigal.Where(a => !string.IsNullOrEmpty(a)).ToArray().Length == 0;
        }
        private void CalculateAdiThodai(List<string> adigal, int startIndex, Func<string, string, bool> CheckThodaiFunc)
        {
            if (CheckIfAllAdigalAreVisited(adigal))
                return;
            var seerInMudhalAdi = ThodaiProcessor.SeerWithinAdiFuncDictionary[CheckThodaiFunc.Method.Name](adigal[startIndex]);
            AdiThodai[startIndex] = Transliterator.Latin2Tamil(seerInMudhalAdi);
            adigal[startIndex] = string.Empty;
            bool foundNewEthukaiOrMonai = false;
            for (var adiIndex = startIndex + 1; adiIndex < adigal.Count; adiIndex++)
            {
                // Check to make sure that the adi is not already visited and cleared out by the previous ethukai/monai match
                if (!string.IsNullOrEmpty(adigal[adiIndex]))
                {
                    var seerInNextAdi =
                        ThodaiProcessor.SeerWithinAdiFuncDictionary[CheckThodaiFunc.Method.Name](adigal[adiIndex]);
                    if (CheckThodaiFunc(seerInMudhalAdi, seerInNextAdi))
                    {
                        AdiThodai[adiIndex] = Transliterator.Latin2Tamil(seerInNextAdi);
                        adigal[adiIndex] = string.Empty;
                    }
                    else
                    {
                        if (!foundNewEthukaiOrMonai)
                        {
                            startIndex = adiIndex;
                            foundNewEthukaiOrMonai = true;
                        }
                    }
                }
            }
            MatchingAdiThodaigal.Add(AdiThodai);
            AdiThodai = new Dictionary<int, string>();
            CalculateAdiThodai(adigal, startIndex, CheckThodaiFunc);
        }
        /**
         * Calculates the number of Vikalpa (Adi Etukai) in the Text
         * @return number
         */

        public int GetVikalpaCount()
        {
            return GetVikalpaCountHelper(CheckEthukai);
        }

        public string GetMetreType()
        {
            var Venpaa = CheckVenpaa();
            var Asiriyappaa = CheckAsiriyapaa();
            var Kalippaa = CheckKalippaa();
            var VenKalippaa = CheckVenKalippaa();

            if(Venpaa != null)
                MetreType = Venpaa;
            else if (Asiriyappaa != null)
                MetreType = Asiriyappaa;
            else if (Kalippaa != null)
                MetreType = Kalippaa;
            else if (VenKalippaa != null)
                MetreType = VenKalippaa;
            return (MetreType ?? "எந்த பா வகையும் பொருந்தவில்லை");
        }

        public bool CheckMonai(string FirstWord, string SecondWord)
        {
            Dictionary<int, List<string>> MonaiVowels = new Dictionary<int, List<string>>
                {
                    {1, new List<string> {"a", "A", "Y", "W"}},
                    {2, new List<string> {"i", "I", "e", "E"}},
                    {3, new List<string> {"u","U","o","O"}}
                };
            Dictionary<int, List<string>> MonaiConsonants = new Dictionary<int, List<string>>
                {
                    {1, new List<string>{"J","n"}},
                    {2, new List<string>{"m","v"}},
                    {3, new List<string>{"t","c"}}
                };

            var MonaiFirstLetter = false;
            var MonaiSecondLetter = false;

            if(FirstWord.Substring(0,1) == SecondWord.Substring(0,1))
            {
                MonaiFirstLetter = true;
            }
            else
            {
                foreach (var monaiConsonant in MonaiConsonants)
                {
                    if (monaiConsonant.Value.IndexOf(FirstWord.Substring(0, 1)) >= 0 && monaiConsonant.Value.IndexOf(SecondWord.Substring(0, 1)) >= 0)
                    {
                        MonaiFirstLetter = true;
                    }
                }
            }
            if(MonaiFirstLetter)
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

        public bool CheckEthukai(string FirstWord, string SecondWord)
        {
            bool EthukaiLetterCheck = false;
            List<string> LongVowels = new List<string> { "A", "I", "U", "E", "O", "W", "Y" };
            List<string> ShortVowels = new List<string> { "a", "i", "u", "e", "o" };

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

        public bool CheckIyaipu(string FirstWord, string SecondWord)
        {
            return (
                (FirstWord == SecondWord) ||
                (FirstWord.Substring(FirstWord.Length - 2) == SecondWord.Substring(SecondWord.Length - 2))
           );
        }
        public bool CheckThaniChol(string SourceText, int LineIndex, bool RhymeCheck)
        {
            SourceText = SourceText.Replace("--", "-");
            SourceText = SourceText.Replace("–", "-");

            var Lines = SourceText.Trim().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length < 2)
                return false;
            var Words = Lines[LineIndex - 1].Split("-".ToCharArray());

            var TaniCholExists = true;
            var TaniCholVikalpaExists = true;

            if(Words.Length != 2)
            {
                TaniCholExists = false;
            }
            else if(!CheckEthukai(Words[0].Trim(), Words[1].Trim()))
            {
                TaniCholVikalpaExists = false;
            }
            return TaniCholExists && TaniCholVikalpaExists;
        }
        private List<Dictionary<string, string>> GetWordBondHelper(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root, List<string> asaiNames,
            Dictionary<string, Dictionary<KeyValuePair<int, string>, string>> thalaiRules)
        {
            bool FirstSyllable = true;
            StringBuilder Syllable = new StringBuilder();
            List<string> SyllableClass = new List<string>();
            List<string> WordClass = new List<string>();
            List<string> Word = new List<string>();

            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    foreach (var asais in seer.Value)
                    {
                        foreach (var asai in asais.Value)
                        {
                            if (asai.Key == asaiNames[0] || asai.Key == asaiNames[1])
                            {
                                Syllable.Append(asai.Value).Append('/');
                                if (FirstSyllable)
                                {
                                    SyllableClass.Add(asai.Key);
                                    FirstSyllable = false;
                                }
                            }
                            if (asai.Key == asaiNames[2])
                            {
                                WordClass.Add(asai.Value);
                                Word.Add(Syllable.ToString());
                                Syllable.Clear();
                                FirstSyllable = true;
                            }
                        }
                    }
                }
            }

            Dictionary<string, string> Bond = new Dictionary<string, string>();
            List<Dictionary<string, string>> SyllableBond = new List<Dictionary<string, string>>();
            // Compare the Word Class & Syllable Class to calculate the TaLai
            for (int i = 0; i < Word.Count - 1; i++)
            {
                Bond = new Dictionary<string, string>();
                Bond["word1"] = Word[i];
                Bond["word2"] = Word[i + 1];
                Bond["class1"] = WordClass[i];
                Bond["class2"] = SyllableClass[i + 1];

                foreach (var thalaiRule in thalaiRules)
                {
                    foreach (var rule in thalaiRule.Value)
                    {
                        if ((WordClass[i].Length - rule.Key.Key >= 0) && WordClass[i].Substring(WordClass[i].Length - rule.Key.Key) == rule.Key.Value &&
                            SyllableClass[i + 1] == rule.Value)
                        {
                            Bond["bond"] = thalaiRule.Key;
                            break;
                        }
                    }

                    string bond;
                    if (Bond.TryGetValue("bond", out bond))
                    {
                        SyllableBond.Add(Bond);
                        break;
                    }
                }
            }


            return SyllableBond;
        }
        public List<Dictionary<string, string>> GetWordBond(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            //return GetWordBondHelper(root, new List<string> { "nE_r", "nirY", "meta" }, ProsodyGrammarConstants.Thalaigal.ThalaiRules);
            return GetWordBondHelper(root, new List<string> { "நேர்", "நிரை", "meta" }, ProsodyGrammarConstants.Thalaigal.TamilThalaiRules);
        }
        public List<string> GetLineClass(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetLineClassHelper(root, "smeta", ProsodyGrammarConstants.TamilLineType);
        }
        private List<string> GetLineClassHelper(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root, string key, List<string> lineTypes)
        {
            List<string> LineClass = new List<string>();
            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    if (seer.Key == key)
                    {
                       LineClass.Add(lineTypes[int.Parse(seer.Value[seer.Key][seer.Key])]);
                    }
                }
            }
            return LineClass;
        }

        private List<int> GetLineWordCountHelper(
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root, string key)
        {
            List<int> LineWordCount = new List<int>();
            foreach (var line in root)
            {
                LineWordCount.Add(int.Parse(line.Value[key][key][key]));
            }
            return LineWordCount;
        }
        private string CheckVenpaa()
        {
            var root = this.ParseTreeRoot;

            bool LineClassCheck = true;
            bool WordClassCheck = true;
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> LastLine =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            // Check for allowed Seers, Line Count
            HashSet<string> wrongWordClassList = new HashSet<string>();
            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    foreach (var asais in seer.Value)
                    {
                        foreach (var asai in asais.Value)
                        {

                            if (asai.Key == "meta" && line.Key != $"aTi-{this.TotalLines}")
                            {
                                // Check for Allowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Venpaa["AllowedSeergal"].Find(
                                        a => a == asai.Value) != asai.Value)
                                {
                                    WordClassCheck = false;
                                    wrongWordClassList.Add(asai.Value);
                                }
                            }
                        }
                    }
                }
                // Check for Allowed Line Classes
                if (line.Key != $"aTi-{this.TotalLines}" &&
                    int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 4)
                {
                    LineClassCheck = false;
                }
                if (line.Key == $"aTi-{this.TotalLines}" &&
                    int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 3)
                {
                    LineClassCheck = false;
                }
                if (line.Key == $"aTi-{this.TotalLines}")
                {
                    LastLine = line.Value;
                }
            }
            Dictionary<string, Dictionary<string, string>> LastWord =
                new Dictionary<string, Dictionary<string, string>>();
            foreach (var seer in LastLine)
            {
                if (seer.Key != "smeta")
                {
                    LastWord = seer.Value;
                }
            }
            FinalSyllableClassCheck = (LastWord.Count == 2);

            // Check for nErpu and nirYpu
            if (LastWord.Count > 2 && LastWord["acY-2"].Values.ElementAt(0).Length == 2 &&
                TamilLanguageConstants.VallinaKutriyalugaraEzhuthukal.IndexOf(
                LastWord["acY-2"].Values.ElementAt(0).Substring(LastWord["acY-2"].Values.ElementAt(0).Length - 2)) >= 0)
            {
                VenLastSyllable = LastWord["acY-1"].Keys.ElementAt(0) + "பு";

                // Preserve the penultimate and last syllable before changing it to "pu" in "Nerpu"
                var penultimateAsai = LastWord["acY-1"].Values.ElementAt(0);
                var asai = LastWord["acY-2"].Values.ElementAt(0);
                LastWord["acY-1"].Remove(LastWord["acY-1"].Keys.ElementAt(0));
                LastWord["acY-2"].Remove(LastWord["acY-2"].Keys.ElementAt(0));
                LastWord["acY-2"].Add(VenLastSyllable, penultimateAsai);
                LastWord["acY-2"].Add("பு", asai);
                LastWord["meta"].Remove("meta");
                LastWord["meta"].Add("meta", ProsodyGrammarConstants.VenpaTamilWordClass[VenLastSyllable]);
                FinalSyllableClassCheck = true;
            }

            HashSet<string> wrongBondsList = new HashSet<string>();
            // Check for Metre Specific Thalais
            WordBondClassCheck = true;
            foreach (var bond in WordBond)
            {
                if (!bond["bond"].Contains("வெண்டளை"))
                {
                    WordBondClassCheck = false;
                    wrongBondsList.Add(bond["bond"]);
                }
            }

            MetreType = null;

            // Classify and log the venpa errors
            foreach (var key in VenpaError.Keys)
            {
                if (VenpaError[key].Count > 1)
                {
                    for (var i = 1; i < VenpaError[key].Count; i++)
                    {
                        VenpaError[key][i] = string.Empty;
                    }
                }
            }

            if (!WordClassCheck)
            {
                var wrongWordClasses = string.Join(", ", wrongWordClassList);
                VenpaError["word"].Add(wrongWordClasses + " ஆகிய வாய்பாடு(கள்) பயின்றுள்ளது(ன)");
            }
            if (!WordBondClassCheck)
            {
                var wrongBonds = string.Join(", ", wrongBondsList);
                VenpaError["bond"].Add("பாவினுள் வெண்டளை அல்லாத " + wrongBonds +
                    " பயின்று வந்துள்ளது(ன)");
            }
            if (!LineClassCheck)
            {
                VenpaError["line"].Add("பொருந்தவில்லை");
            }
            if (!FinalSyllableClassCheck)
            {
                VenpaError["final"].Add("பொருந்தவில்லை");
            }
            // Classify the Metre
            if (WordBondClassCheck && FinalSyllableClassCheck && LineClassCheck && WordClassCheck)
            {
                ThaniCholExists = CheckThaniChol(ProsodyText, 2, true);
                var MetreTypeTemplate = "{0} விகற்ப {1} வெண்பா";
                var VikalpaType = "";
                var AdiOrOsaiType = (ThaniCholExists ? "நேரிசை" : "இன்னிசை");
                switch (VikalpaCount)
                {
                    case 1:
                        VikalpaType = "ஒரு";
                        break;
                    case 2:
                        VikalpaType = "இரு";
                        break;
                    default:
                        VikalpaType = "பல";
                        break;

                }
                switch (TotalLines)
                {
                    case 2:
                        AdiOrOsaiType = "குறள்";
                        break;
                    case 3:
                        AdiOrOsaiType += "ச் சிந்தியல்";
                        break;
                }
                if (TotalLines < 5)
                {
                    MetreType = string.Format(MetreTypeTemplate, VikalpaType, AdiOrOsaiType);
                }
                else if (TotalLines > 4 && TotalLines <= 12)
                {
                    MetreType = string.Format(MetreTypeTemplate, VikalpaType, "பஃறொடை");
                }
                else if (TotalLines > 12)
                {
                    MetreType = string.Format(MetreTypeTemplate, VikalpaType, "கலி");
                }

            }

            return MetreType;
        }

        private string CheckAsiriyapaa()
        {
            var root = this.ParseTreeRoot;

            bool LineClassCheck = true;
            bool WordClassCheck = true;
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> LastLine =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            // Check for allowed Seers, Line Count
            HashSet<string> wrongWordClassList = new HashSet<string>();
            int AllowedWordClassCount = 0;
            int TotalWordCount = 0;
            List<int> LineWordCount = new List<int>();
            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    foreach (var asais in seer.Value)
                    {
                        foreach (var asai in asais.Value)
                        {

                            if (asai.Key == "meta")
                            {
                                TotalWordCount++;
                                // Check for DisAllowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Asiriyipaa["DisAllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    WordClassCheck = false;
                                    wrongWordClassList.Add(asai.Value);
                                }
                                // Check for Allowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Asiriyipaa["AllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    AllowedWordClassCount++;
                                }
                            }
                        }
                    }
                }
                // Check for Allowed Line Classes
                if (line.Key != $"aTi-{this.TotalLines}")
                {
                    LineWordCount.Add(int.Parse(line.Value["smeta"]["smeta"]["smeta"]));
                }
                if (line.Key == $"aTi-{this.TotalLines}")
                {
                    LineWordCount.Add(int.Parse(line.Value["smeta"]["smeta"]["smeta"]));
                }
                if (line.Key == $"aTi-{this.TotalLines}")
                {
                    LastLine = line.Value;
                }
            }
            var FinalLettersMono = ProsodyText.Substring(ProsodyText.Length - 1);
            var FinalLettersTri = ProsodyText.Substring(ProsodyText.Length - 3);
            var FinalLastSyllableClassCheck = (ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Asiriyipaa["AllowedMonoFinalLetters"].
                Find(m => m == FinalLettersMono) == FinalLettersMono) ||
                (ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Asiriyipaa["AllowedTriFinalLetters"].
                Find(m => m == FinalLettersTri) == FinalLettersTri);
            LineClassCheck = (TotalLines > 2);
            // Check if Agavarseers are Majority
            var WordSyllableCheck = (AllowedWordClassCount/(double)TotalWordCount) > 0.45;
            var AsiriyappaaCheck = (WordClassCheck && FinalLastSyllableClassCheck && LineClassCheck && WordSyllableCheck);

            MetreType = null;
            // Find the type of Asiriyappaa
            var AlavadiClass = true;
            var NonAlavadiClassCount = new List<int>();
            var VikalpaCount = GetVikalpaCount();

            // Validate the count of words in each line, to decide the type of Asiriyappaa
            for (var LineIndex = 0; LineIndex < LineWordCount.Count; LineIndex++)
            {
                if (LineWordCount[LineIndex] < 4)
                {
                    AlavadiClass = false;
                    NonAlavadiClassCount.Add(LineIndex + 1);

                    if (LineIndex == 0 || LineIndex == LineWordCount.Count - 1)
                    {
                        AsiriyappaaCheck = false;
                    }
                }
                if (LineWordCount[LineIndex] > 4)
                {
                    AsiriyappaaCheck = false;
                }
            }

            if (AsiriyappaaCheck)
            {
                // If Vikalpa == 1 then it must be considered as Kali Vritta rather than Asiriyappaa
                if (AlavadiClass)
                {
                    if (VikalpaCount != 1)
                    {
                        MetreType = "நிலைமண்டில ஆசிரியப்பா";
                    }
                    else
                    {
                        MetreType = null;
                    }
                }
                else
                {
                    if (NonAlavadiClassCount.Count == 1 && NonAlavadiClassCount[0] == TotalLines - 1)
                    {
                        MetreType = "நேரிசை ஆசிரியப்பா";
                    }
                    else
                    {
                        MetreType = "இணைக்குறள் ஆசிரியப்பா";
                    }
                }
            }
            return MetreType;
        }

        private string CheckKalippaa()
        {
            var root = this.ParseTreeRoot;

            bool LineClassCheck = true;
            bool WordClassCheck = true;
            bool WordSyllableClassCheck = false;
            bool LineCountCheck = false;

            Dictionary<string, Dictionary<string, Dictionary<string, string>>> LastLine =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            // Check for allowed Seers, Line Count
            HashSet<string> wrongWordClassList = new HashSet<string>();
            int AllowedWordClassCount = 0;
            int TotalWordCount = 0;
            List<int> LineWordCount = new List<int>();

            // Check for line count
            if (TotalLines > 3)
            {
                LineCountCheck = true;
            }
            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    foreach (var asais in seer.Value)
                    {
                        foreach (var asai in asais.Value)
                        {

                            if (asai.Key == "meta")
                            {
                                TotalWordCount++;
                                // Check for DisAllowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Kalipaa["DisAllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    WordClassCheck = false;
                                    wrongWordClassList.Add(asai.Value);
                                }
                                // Check for Allowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Kalipaa["AllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    AllowedWordClassCount++;
                                }
                            }
                        }
                    }
                }
                // Check for Allowed Line Classes
                if (int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 4)
                {
                    LineClassCheck = false;
                }
                if (line.Key == $"aTi-{this.TotalLines}")
                {
                    LastLine = line.Value;
                }
            }

            // Check for Allowed Word Syllable Types is Majority
            WordSyllableClassCheck = (AllowedWordClassCount / (double)TotalWordCount) > 0.45;

            MetreType = null;
            if (WordClassCheck && LineClassCheck && WordSyllableClassCheck && LineCountCheck)
            {
                MetreType = "தரவு கொச்சகக் கலிப்பா";
            }
            return MetreType;
        }

        private string CheckVenKalippaa()
        {
            var root = this.ParseTreeRoot;

            bool LineClassCheck = true;
            bool WordClassCheck = true;
            bool WordSyllableClassCheck = false;
            bool VenKalippaEndCheck = false;
            bool LineCountCheck = false;

            Dictionary<string, Dictionary<string, Dictionary<string, string>>> LastLine =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            // Check for allowed Seers, Line Count
            HashSet<string> wrongWordClassList = new HashSet<string>();
            int AllowedWordClassCount = 0;
            int TotalWordCount = 0;
            List<int> LineWordCount = new List<int>();
            List<string> FinalWords = new List<string>();
            int FinalWordsCount = 0;
            // Check for line count
            if (TotalLines > 3)
            {
                LineCountCheck = true;
            }
            var LineIndex = 0;
            foreach (var line in root)
            {
                foreach (var seer in line.Value)
                {
                    foreach (var asais in seer.Value)
                    {
                        foreach (var asai in asais.Value)
                        {

                            if (asai.Key == "meta" && LineIndex != TotalLines - 1)
                            {
                                TotalWordCount++;
                                // Check for DisAllowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["DisAllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    WordClassCheck = false;
                                    wrongWordClassList.Add(asai.Value);
                                }
                                // Check for Allowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["AllowedSeergal"].Find(
                                        a => a == asai.Value) == asai.Value)
                                {
                                    AllowedWordClassCount++;
                                }
                            }

                        }
                    }
                }
                LineIndex++;
                // Check for Allowed Line Classes
                if (line.Key != $"aTi-{this.TotalLines}" &&
                    int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 4 )
                {
                    LineClassCheck = false;
                }
                if (line.Key == $"aTi-{this.TotalLines}")
                {
                    LastLine = line.Value;
                    FinalWordsCount = int.Parse(line.Value["smeta"]["smeta"]["smeta"]);
                }

            }
            Dictionary<string, Dictionary<string, string>> LastWord =
               new Dictionary<string, Dictionary<string, string>>();
            foreach (var seer in LastLine)
            {
                foreach (var asais in seer.Value)
                {
                    foreach (var asai in asais.Value)
                    {
                        if (asai.Key == "meta" && LineIndex == TotalLines)
                        {
                            FinalWords.Add(asai.Value);
                            if (
                                ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["AllowedSeergal"].Find(
                                    a => a == asai.Value) == asai.Value)
                            {
                                AllowedWordClassCount++;
                            }
                        }
                    }
                }
            }
            // Check if Final Words Count is 3 and presence of DisallowedWordClass
            if (FinalWordsCount == 3 && (
                ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["DisAllowedSeergal"].Find(
                    a => a == FinalWords[0]) != FinalWords[0]) &&
                (
                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["DisAllowedSeergal"].Find(
                        a => a == FinalWords[1]) != FinalWords[1]) &&
                (
                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.VenKalipaa["DisAllowedSeergal"].Find(
                        a => a == FinalWords[2]) != FinalWords[2]))
            {
                VenKalippaEndCheck = true;
            }
            // Check for Allowed Word Syllable Types is Majority
            WordSyllableClassCheck = (AllowedWordClassCount / (double)TotalWordCount) > 0.45;

            MetreType = null;
            if (WordClassCheck && LineClassCheck && WordSyllableClassCheck && VenKalippaEndCheck && LineCountCheck)
            {
                MetreType = "வெண்கலிப்பா";
            }
            return MetreType;
        }
        private string  RemovePunctuation(string text)
        {
            List<string> punctuations = new List<string> {"-","!",";","?",".","(",",","\'",":","\"","–",")","[","]"};
            punctuations.ForEach(p => text = text.Replace(p, ""));
            text = Regex.Replace(text, "  *", "  ");
            return text;
        }

        public static double CalculateMathirai(string word, bool shouldUseOneAndHalfMathiraiForAikaramInStartPosition = false)
        {
            double mathirai = 0.0;
            int uyirEzuthuCount = word.Count(k => k == TamilLanguageConstants.UyirEzuthukkal.Find(l => l == k));
            int uyirMeyKurilEzuthuCount = word.Count(k => k == TamilLanguageConstants.UyirMeyKurilEzuthukkal.Find(l => l == k));
            int uyirMeyNedilEzuthuCount = word.Count(k => k == TamilLanguageConstants.UyirMeyNedilEzuthukkal.Find(l => l == k));
            int uyirKurilEzhuthuCount = word.Count(k => k == TamilLanguageConstants.UyirKurilEzuthukkal.Find(l => l == k));
            int uyirNedilEzhuthuCount = word.Count(k => k == TamilLanguageConstants.UyirNedilEzuthukkal.Find(l => l == k));
            int meyEzuthuCount = word.Count(k => k == TamilLanguageConstants.MeyEzuthuPulli /* 0X0BCD - மெய்யெழுத்தில் வரும் புள்ளி. எ.கா, ’க்’ */);
            int aythaEzuthuCount = word.Count(k => k == TamilLanguageConstants.AythaEzuthu);
            int agaraUyirMeyEzuthuCount = word.Count(k => k == TamilLanguageConstants.AgaraUyirMeyEzuthukkal.Find(l => l == k));
            int granthaEzuthuCount = word.Count(k => k == TamilLanguageConstants.GranthaEzuthukkal.Find(l => l == k));
            int[] aikaramPositions = "ை".FindAll(word, 0);
            int[] uyirAikaramPositions = "ஐ".FindAll(word, 0);
            aikaramPositions.Concat(uyirAikaramPositions);
            int aikaramInStartPosition = 0;
            if (shouldUseOneAndHalfMathiraiForAikaramInStartPosition)
            {
                var position = aikaramPositions.Select(a => a == 1).ToList();
                aikaramInStartPosition = position[0] ? 1 : 0;
            }
            int aikaraKurukkamCount = "ை".FindAll(word, 0).Count();
            aikaraKurukkamCount = (aikaraKurukkamCount > 0) ? aikaraKurukkamCount - aikaramInStartPosition : aikaramInStartPosition;
            int uyirMeyEzuthuCount = (agaraUyirMeyEzuthuCount + granthaEzuthuCount) - meyEzuthuCount;
            int kurilEzuthuCount = (uyirKurilEzhuthuCount + agaraUyirMeyEzuthuCount + granthaEzuthuCount + aikaraKurukkamCount) - meyEzuthuCount - uyirMeyNedilEzuthuCount;
            int nedilEzuthuCount = (uyirNedilEzhuthuCount + uyirMeyNedilEzuthuCount) - aikaraKurukkamCount - aikaramInStartPosition;
            //Console.WriteLine(
            //    "Uyir Ezuthukkal = {0}\r\nMey Ezuthukkal = {1}\r\nUyir Mey Ezuthukkal = {2}\r\nAytha Ezhuthukkal = {3}\r\nGrantha Ezuthukkal = {4}\r\nKuril Ezuthukkal = {5}\r\nNedil Ezuthukkal = {6}",
            //    uyirEzuthuCount, meyEzuthuCount, uyirMeyEzuthuCount, aythaEzuthuCount, granthaEzuthuCount, kurilEzuthuCount, nedilEzuthuCount);
            mathirai = kurilEzuthuCount * ProsodyGrammarConstants.MathiraiTable["குறில்"] +
                       nedilEzuthuCount * ProsodyGrammarConstants.MathiraiTable["நெடில்"] +
                       meyEzuthuCount * ProsodyGrammarConstants.MathiraiTable["மெய்"] +
                       aythaEzuthuCount * ProsodyGrammarConstants.MathiraiTable["ஆய்தம்"] +
                       aikaramInStartPosition * ProsodyGrammarConstants.MathiraiTable["ஐகாரம்"];
            return mathirai;
        }

        private Dictionary<string, int> GetLetterCount(string tamilText)
        {
            int uyirEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.UyirEzuthukkal.Find(l => l == k));
            int uyirAndUyirMeyKurilEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.UyirMeyKurilEzuthukkal.Find(l => l == k));
            int uyirAndUyriMeyNedilEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.UyirMeyNedilEzuthukkal.Find(l => l == k));
            int meyEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.MeyEzuthuPulli /* 0X0BCD - மெய்யெழுத்தில் வரும் புள்ளி. எ.கா, ’க்’ */);
            int aythaEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.AythaEzuthu);
            int agaraUyirMeyEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.AgaraUyirMeyEzuthukkal.Find(l => l == k));
            int granthaEzuthuCount = tamilText.Count(k => k == TamilLanguageConstants.GranthaEzuthukkal.Find(l => l == k));
            int uyirMeyEzuthuCount = (agaraUyirMeyEzuthuCount + granthaEzuthuCount) - meyEzuthuCount;
            int kurilEzuthuCount = (uyirAndUyirMeyKurilEzuthuCount + agaraUyirMeyEzuthuCount) - meyEzuthuCount - uyirAndUyriMeyNedilEzuthuCount;
            int nedilEzuthuCount = uyirAndUyriMeyNedilEzuthuCount;
            Console.WriteLine(
                "Uyir Ezuthukkal = {0}\r\nMey Ezuthukkal = {1}\r\nUyir Mey Ezuthukkal = {2}\r\nAytha Ezhuthukkal = {3}\r\nGrantha Ezuthukkal = {4}\r\nKuril Ezuthukkal = {5}\r\nNedil Ezuthukkal = {6}",
                uyirEzuthuCount, meyEzuthuCount, uyirMeyEzuthuCount, aythaEzuthuCount, granthaEzuthuCount, kurilEzuthuCount, nedilEzuthuCount);

            return new Dictionary<string, int> {
                {TamilLanguageConstants.Vowel, uyirEzuthuCount},
                {TamilLanguageConstants.Consonant, meyEzuthuCount},
                {TamilLanguageConstants.ConsonantVowel, uyirMeyEzuthuCount},
                {TamilLanguageConstants.Aytham, aythaEzuthuCount},
                {TamilLanguageConstants.Kuril,  kurilEzuthuCount},
                {TamilLanguageConstants.Nedil, nedilEzuthuCount }
            };
        }
    }

    public class MetricLinkage
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTree { get; set; }
        public List<Dictionary<string, string>> WordBond { get; set; }
    }

    public class ThodaiFinder
    {
        public List<string> Lines { get; set; }
        public List<Dictionary<int, string>> MatchingAdiEthukai { get; set; }
        public Dictionary<int, List<List<string>>> SeerEthukaiWithinAdi { get; set; }
        public List<Dictionary<int, string>> MatchingAdiMonai { get; set; }
        public Dictionary<int, List<List<string>>> SeerMonaiWithinAdi { get; set; }
        public List<Dictionary<int, string>> MatchingAdiIyaipu { get; set; }
        public Dictionary<int, List<List<string>>> SeerIyaipuWithinAdi { get; set; }
    }

    public class ProsodyText
    {
        public string InputText { get; set; }
    }
    public class MathiraiCounter
    {
        public List<Dictionary<string, double>> MathiraiCount { get; set; }
        public double TotalMathiraiCount { get; set; }
    }
}
