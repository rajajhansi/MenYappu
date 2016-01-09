using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tamil.Grammar.Prosody.Parser;

namespace RjamSoft.Tamil.Grammar.Parser
{
    public class ProsodyParseTree
    {        
        /**
	     * The Root of the Parse Tree
	     * @var Iterative String Array
	     */
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTreeRoot;
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> TamilParseTreeRoot; 
		
	    /**
	     * The Input Source Text
	     * @var String
	     */
	    public string InputSourceText;

        public string ProsodyText;
	    // Variables for Metrical Information
	
	    /**
	     * The Metre type of the Prosody Text
	     * @var String
	     */
	    public string MetreType; 
	    /**
	     * The Total Lines in the Text
	     * @var Integer
	     */
	    public int TotalLines; 	
	    /**
	     * Contains the Talai Informatoin
	     * @var String Array
	     */
        public List<Dictionary<string, string>> WordBond;
        public List<Dictionary<string, string>> TamilWordBond; 
	
	    public string VenLastSyllable; 
	
	    public Dictionary<string, int> LetterCount;
        public List<Dictionary<string, double>> MathiraiCount;
        public double TotalMathiraiCount;
	    public List<string> LineClass;
        public List<string> TamilLineClass;
	
	    public int VikalpaCount;
        public Dictionary<int, string> AdiThodai = new Dictionary<int, string>();
        public List<Dictionary<int, string>> MatchingAdiThodaigal = new List<Dictionary<int, string>>();
        public Dictionary<int, List<string>> SeerThodaiWithinAdi = new Dictionary<int, List<string>>();

        public Dictionary<string, Dictionary<int, string>> AdiThodaiWithThodaiType = new Dictionary<string, Dictionary<int, string>>();
        public Dictionary<string, List<Dictionary<int, string>>> MatchingAdiThodaigalWithThodaiType = new Dictionary<string, List<Dictionary<int, string>>>();
        public Dictionary<string, Dictionary<int, List<string>>> SeerThodaiWithinAdiWithThodaiType = new Dictionary<string, Dictionary<int, List<string>>>();
	    public string Lang;

        public List<string> TamilLineType = new List<string> { "", "தனிச்சொல்", "குறளடி", "சிந்தடி", "அளவடி", "நெடிலடி", "அறுசீர்க் கழிநெடிலடி", "எழுசீர்க் கழிநெடிலடி", "எண்சீர்க் கழிநெடிலடி", "ஒன்பதின்சீர்க் கழிநெடிலடி", "பதின்சீர்க் கழிநெடிலடி", "பதினொறு சீர்க் கழிநெடிலடி", "பன்னிரு சீர்க் கழிநெடிலடி", "பதின்மூன்று சீர்க் கழிநெடிலடி", "பதினான்கு சீர்க் கழிநெடிலடி", "பதினைந்து சீர்க் கழிநெடிலடி", "பதினாறு சீர்க் கழிநெடிலடி", "பதினேழு சீர்க் கழிநெடிலடி", "பதினெட்டு சீர்க் கழிநெடிலடி", "பத்தொன்பது சீர்க் கழிநெடிலடி", "இருபது சீர்க் கழிநெடிலடி", "இருபத்தோரு சீர்க் கழிநெடிலடி", "இருபத்தி இரண்டு சீர்க் கழிநெடிலடி", "இருபத்து மூன்று சீர்க் கழிநெடிலடி", "இருபத்து நான்கு சீர்க் கழிநெடிலடி" };
        public List<string> LineType = new List<string> { "", "taVi_cco_l", "kuRaLaTi", "ci_ntaTi", "_aLavaTi", "neTilaTi", "_aRucI_r_k kaZineTilaTi", "_eZucI_r_k kaZineTilaTi", "_e_NcI_r_k kaZineTilaTi", "_o_Vpati_ncI_r_k kaZineTilaTi", "pati_VcI_r_k kaZineTilaTi", "patiVoru cI_r_k kaZineTilaTi", "pa_VViru cI_r_k kaZineTilaTi", "patimU_VRu cI_r_k kaZineTilaTi", "patiVA_Vku cI_r_k kaZineTilaTi", "patiVY_ntu cI_r_k kaZineTilaTi", "patiVARu cI_r_k kaZineTilaTi", "patiVEZu cI_r_k kaZineTilaTi", "patiVe_TTu cI_r_k kaZineTilaTi", "pa_tto_Vpatu cI_r_k kaZineTilaTi", "_irupatu cI_r_k kaZineTilaTi", "_irupa_ttoru cI_r_k kaZineTilaTi", "_irupa_ttu _ira_NTu cI_r_k kaZineTilaTi", "_irupa_ttu mU_VRu cI_r_k kaZineTilaTi", "_irupa_ttu nA_Vku cI_r_k kaZineTilaTi" };

        public List<string> SyllableTypes = new List<string> { "nE_r", "nirY" };
        public List<string> TamilSyllableTypes = new List<string> { "நேர்", "நிரை" };
      
        public Func<string, string, bool>[] ThodaiCalculators; 

     /**
	 * Reference Array for getting the Word Type
	 * @var String Array
	 */
	    public Dictionary<string, string> WordType = new Dictionary<string,string> 
        {

	        // Two Asais

            { "nE_rnE_r" , "tEmA"},
            {"nirYnE_r" , "puLimA"},
            {"nE_rnirY" , "kUviLa_m"},
            {"nirYnirY" , "karuviLa_m"},

	        // Three Asais - Kay seers

            {"nE_rnE_rnE_r" , "tEmA_GkA_y"},
            {"nirYnE_rnE_r" , "puLimA_GkA_y"},
            {"nE_rnirYnE_r" , "kUviLa_GkA_y"},
            {"nirYnirYnE_r" , "karuviLa_GkA_y"},

	        // Three Asais - Kani seers

            {"nE_rnE_rnirY" , "tEmA_GkaVi"},
            {"nirYnE_rnirY" , "puLimA_GkaVi"},
            {"nE_rnirYnirY" , "kUviLa_GkaVi"},
            {"nirYnirYnirY" , "karuviLa_GkaVi"},

	        // Four Asais - Tanpuu seers

            {"nE_rnE_rnE_rnE_r" , "tEmA_nta_NpU"},
            {"nirYnE_rnE_rnE_r" , "puLimA_nta_NpU"},
            {"nE_rnirYnE_rnE_r" , "kUviLa_nta_NpU"},
            {"nirYnirYnE_rnE_r" , "karuviLa_nta_NpU"},
    
            // Four Asais - naRumpU seers

            {"nE_rnE_rnirYnE_r" , "tEmAnaRu_mpU"},
            {"nirYnE_rnirYnE_r" , "puLimAnaRu_mpU"},
            {"nE_rnirYnirYnE_r" , "kUviLanaRu_mpU"},
            {"nirYnirYnirYnE_r" , "karuviLanaRu_mpU"},
    
	        // Four Asais - naRunizhal

            {"nE_rnE_rnirYnirY" , "tEmAnaRuniZa_l"},
            {"nirYnE_rnirYnirY" , "puLimAnaRuniZa_l"},
            {"nE_rnirYnirYnirY" , "kUviLanaRuniZa_l"},
            {"nirYnirYnirYnirY" , "karuviLanaRuniZa_l"},

	        // Four Asais- Tannizhal

            {"nE_rnE_rnE_rnirY" , "tEmA_nta_NNiZa_l"},
            {"nirYnE_rnE_rnirY" , "puLimA_nta_NNiZa_l"},
            {"nE_rnirYnE_rnirY" , "kUviLa_nta_NNiZa_l"},
            {"nirYnirYnE_rnirY" , "karuviLa_nta_NNiZa_l"},

	        // Singla ASai - Exceptions

            {"nE_r" , "mA"},
            {"nirY" , "viLa_m"}            
        };

        public Dictionary<string, string> VenpaWordClass = new Dictionary<string, string>
        {
            {"nE_r" , "nAL"},
            {"nirY" , "malar"},
            {"nirYpu", "kAcu"},
            {"nE_rpu", "piRa_ppu"}
        };

        public Dictionary<string, string> TamilWordType = new Dictionary<string, string> 
        {

	        // ஈரசைச்சீர்

            {"நேர்நேர்" , "தேமா"},
            {"நிரைநேர்" , "புளிமா"},
            {"நேர்நிரை" , "கூவிளம்"},
            {"நிரைநிரை" , "கருவிளம்"},

	        // மூவசைச் சீர்  - காய்ச்சீர்

            {"நேர்நேர்நேர்" , "தேமாங்காய்"},
            {"நிரைநேர்நேர்" , "புளிமாங்காய்"},
            {"நேர்நிரைநேர்" , "கூவிளங்காய்"},
            {"நிரைநிரைநேர்" , "கருவிளங்காய்"},

	        // மூவசைச்சீர் - கனிச்சீர்

            {"நேர்நேர்நிரை" , "தேமாங்கனி"},
            {"நிரைநேர்நிரை" , "புளிமாங்கனி"},
            {"நேர்நிரைநிரை" , "கூவிளங்கனி"},
            {"நிரைநிரைநிரை" , "கருவிளங்கனி"},

	        // நாலசைச்சீர் - பூச்சீர் - தண்பூ

            {"நேர்நேர்நேர்நேர்" , "தேமாந்தண்பூ"},
            {"நிரைநேர்நேர்நேர்" , "புளிமாந்தண்பூ"},
            {"நேர்நிரைநேர்நேர்" , "கூவிளந்தண்பூ"},
            {"நிரைநிரைநேர்நேர்" , "கருவிளந்தண்பூ"},
    
            // நாலசைச்சீர் - பூச்சீர் - நறும்பூச்சீர்

            {"நேர்நேர்நிரைநேர்" , "தேமாநறும்பூ"},
            {"நிரைநேர்நிரைநேர்" , "புளிமாநறும்பூ"},
            {"நேர்நிரைநிரைநேர்" , "கூவிளநறும்பூ"},
            {"நிரைநிரைநிரைநேர்" , "கருவிளநறும்பூ"},
    
	        // நாலசைச்சீர் - நிழல் சீர் - நறுநிழல்

            {"நேர்நேர்நிரைநிரை" , "தேமாநறுநிழல்"},
            {"நிரைநேர்நிரைநிரை" , "புளிமாநறுநிழல்"},
            {"நேர்நிரைநிரைநிரை" , "கூவிளநறுநிழல்"},
            {"நிரைநிரைநிரைநிரை" , "கருவிளநறுநிழல்"},

	        // நாலசைச்சீர் - நிழல் சீர் - தண்ணிழல்

            {"நேர்நேர்நேர்நிரை" , "தேமாந்தண்ணிழல்"},
            {"நிரைநேர்நேர்நிரை" , "புளிமாந்தண்ணிழல்"},
            {"நேர்நிரைநேர்நிரை" , "கூவிளந்தண்ணிழல்"},
            {"நிரைநிரைநேர்நிரை" , "கருவிளந்தண்ணிழல்"},

	        // ஓரசைச்சீர்
            {"நேர்" , "மா"},
            {"நிரை" , "விளம்"}
        };

        public Dictionary<string, string> VenpaTamilWordClass = new Dictionary<string, string>
        {
            {"நேர்", "நாள்"},
            {"நிரை", "மலர்"},
            {"நேர்பு", "காசு"},
            {"நிரைபு", "பிறப்பு"}
        };

        private bool WordBondClassCheck;
        private bool FinalSyllableClassCheck;
        private bool ThaniCholExists;

        public ProsodyParseTree(string prosodyText)
        {            
            this.InputSourceText = prosodyText;
            this.ThodaiCalculators = new Func<string, string, bool>[] {CheckMonai, CheckEthukai};
            ;
        }

        public ProsodyPart Parse()
        {
            ProsodyText = Transliterator.Tamil2Latin(InputSourceText).Trim();
            var tamilText = InputSourceText;
            var tamilProsodyText = Transliterator.Tamil2LatinTamil(tamilText).Trim();
            this.LetterCount = GetLetterCount(tamilText);
            MathiraiCount = CalculateMathiraiForPaa(tamilText);
            MathiraiCount.ForEach(l => TotalMathiraiCount += l.Sum(m => m.Value));
            this.ParseTreeRoot = GetTextSyllablePattern(ProsodyText);;
            //this.TamilParseTreeRoot = GetTamilTextSyllablePattern(tamilProsodyText);
            GetThodai(ProsodyText, CheckEthukai);
            this.AdiThodaiWithThodaiType["_etukY"] = this.AdiThodai;
            this.MatchingAdiThodaigalWithThodaiType["_etukY"] = this.MatchingAdiThodaigal;
            this.SeerThodaiWithinAdiWithThodaiType["_etukY"] = this.SeerThodaiWithinAdi;            
            //GetThodai(tamilProsodyText, CheckTamilEthukai);
            //this.AdiThodaiWithThodaiType["எதுகை"] = this.AdiThodai;
            //this.MatchingAdiThodaigalWithThodaiType["எதுகை"] = this.MatchingAdiThodaigal;
            //this.SeerThodaiWithinAdiWithThodaiType["எதுகை"] = this.SeerThodaiWithinAdi;
            GetThodai(ProsodyText, CheckMonai);
            this.AdiThodaiWithThodaiType["mOVY"] = this.AdiThodai;
            this.MatchingAdiThodaigalWithThodaiType["mOVY"] = this.MatchingAdiThodaigal;
            this.SeerThodaiWithinAdiWithThodaiType["mOVY"] = this.SeerThodaiWithinAdi;
            //GetThodai(tamilProsodyText, CheckTamilMonai);
            //this.AdiThodaiWithThodaiType["மோனை"] = this.AdiThodai;
            //this.MatchingAdiThodaigalWithThodaiType["மோனை"] = this.MatchingAdiThodaigal;
            //this.SeerThodaiWithinAdiWithThodaiType["மோனை"] = this.SeerThodaiWithinAdi;
            this.VikalpaCount = GetVikalpaCount();
            this.WordBond = GetWordBond(this.ParseTreeRoot);
            //this.TamilWordBond = GetTamilWordBond(this.TamilParseTreeRoot);
            this.LineClass = GetLineClass(this.ParseTreeRoot);
            //this.TamilLineClass = GetTamilLineClass(this.TamilParseTreeRoot);
            this.MetreType = GetMetreType();

            //TransliterateLatinTamilToTamil();

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
                    SeerMonaiWithinAdi = this.SeerThodaiWithinAdiWithThodaiType["mOVY"]
                };
        }

        private void TransliterateLatinTamilToTamil()
        {
            foreach(var adiKeyValue in TamilParseTreeRoot)
            {
                foreach(var seerKeyValue in adiKeyValue.Value)
                {
                    foreach(var asaiKeyValue in seerKeyValue.Value)
                    {
                        foreach(var asai in asaiKeyValue.Value)
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
                        Syllable["சீர்வகை"] = new Dictionary<string, string> { { "சீர்வகை", TamilWordType[WordPattern.ToString()] } };
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
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> LineList =
                new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
            var LineCount = 1;
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
                        Syllable[string.Format("acY-{0}", SyllableCount++)] = wordSyllable.Value;                        

                        foreach (var asai in wordSyllable.Value)
                        {
                            WordPattern.Append(asai.Key);
                        }
                    }

                    if (WordPattern.Length > 0)
                    {
                        Syllable["meta"] = new Dictionary<string, string>{ {"meta", TamilWordType[WordPattern.ToString()] }};
                    }
                    else
                    {
                        WordCount--;
                    }

                    // Store the seer 
                    WordList[string.Format("cI_r-{0}", WordCount++)] = Syllable;
                }
                WordList["smeta"] = new Dictionary<string, Dictionary<string, string>> { { "smeta", new Dictionary<string, string> { { "smeta", Convert.ToString(--WordCount) } } } };
                LineList[string.Format("aTi-{0}", LineCount++)] = WordList;                
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
        
        /**
         * Calculates the Thodai of a prosody text
         **/
        public void GetThodai(string prosodyText, int thodaiVagai)
        {
            var CheckEthukaiOrMonaiFunc = this.ThodaiCalculators[thodaiVagai];
            //Dictionary<int, string> AdiThodai = new Dictionary<int, string>();
            //Dictionary<int, List<string>> SeerThodaiWithinAdi = new Dictionary<int, List<string>>();
            AdiThodai.Clear();
            SeerThodaiWithinAdi.Clear();
            MatchingAdiThodaigal.Clear();
            prosodyText = RemovePunctuation(prosodyText);
            var adigal = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            var mudhalSeerInMudhalAdi = adigal[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            //AdiThodai[0] = mudhalSeerInMudhalAdi;
            for (var adiIndex = 0; adiIndex < adigal.Count; adiIndex++)
            {
                int seerThodaiInAdiMask = 1;
                var seerThodaiInAdi = new StringBuilder();
                var seergal = adigal[adiIndex].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                seerThodaiInAdi.Append("1 ").Append(Transliterator.Latin2Tamil(seergal[0])).Append(' ');
                for (var seerIndex = 1; seerIndex < seergal.Length; seerIndex++)
                {
                    if (CheckEthukaiOrMonaiFunc(seergal[0], seergal[seerIndex]))
                    {
                        seerThodaiInAdiMask |= (1 << seerIndex);
                        seerThodaiInAdi.Append(seerIndex + 1).Append(' ').Append(Transliterator.Latin2Tamil(seergal[seerIndex])).Append(' ');
                    }
                }
                SeerThodaiWithinAdi[adiIndex] = new List<string>
                                                    {
                                                        Convert.ToString(seerThodaiInAdiMask, 2),
                                                        TamilLanguageConstants.TodaiVagaigalTamil[Convert.ToString(seerThodaiInAdiMask, 2)],                                                        
                                                        seerThodaiInAdi.ToString()
                                                    };
            }
            CalculateAdiThodai(adigal, 0, CheckEthukaiOrMonaiFunc);
            //var nextEthugai = mudhalSeerInMudhalAdi;            
            //for (var adiIndex = 1; adiIndex < adigal.Count - 1; adiIndex++)
            //{
            //    var mudhalSeerInNextAdi = adigal[adiIndex + 1].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            //    if (CheckTamilEthukai(mudhalSeerInMudhalAdi, mudhalSeerInNextAdi))
            //    {
            //        AdiThodai[adiIndex + 1] = mudhalSeerInNextAdi;
            //        adigal[adiIndex] = string.Empty;
            //    }
            //    else
            //    {
            //        nextEthugai = mudhalSeerInNextAdi;
            //    }
            //}
        }
        public void GetThodai(string prosodyText, Func<string, string, bool> CheckEthukaiOrMonaiFunc )
        {
            //Dictionary<int, string> AdiThodai = new Dictionary<int, string>();
            //Dictionary<int, List<string>> SeerThodaiWithinAdi = new Dictionary<int, List<string>>();
            AdiThodai.Clear();
            SeerThodaiWithinAdi.Clear();
            MatchingAdiThodaigal.Clear();
            prosodyText = RemovePunctuation(prosodyText);
            var adigal = prosodyText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            var mudhalSeerInMudhalAdi = adigal[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            //AdiThodai[0] = mudhalSeerInMudhalAdi;
            for (var adiIndex = 0; adiIndex < adigal.Count; adiIndex++)
            {
                int seerThodaiInAdiMask = 1;
                var seerThodaiInAdi = new StringBuilder();                
                var seergal = adigal[adiIndex].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                seerThodaiInAdi.Append("1 ").Append(Transliterator.Latin2Tamil(seergal[0])).Append(' ');
                for (var seerIndex = 1; seerIndex < seergal.Length; seerIndex++)
                {
                    if (CheckEthukaiOrMonaiFunc(seergal[0], seergal[seerIndex]))
                    {
                        seerThodaiInAdiMask |= ( 1 << seerIndex);
                        seerThodaiInAdi.Append(seerIndex +1).Append(' ').Append(Transliterator.Latin2Tamil(seergal[seerIndex])).Append(' ');
                    }
                }
                SeerThodaiWithinAdi[adiIndex] = new List<string>
                                                    {
                                                        Convert.ToString(seerThodaiInAdiMask, 2),
                                                        TamilLanguageConstants.TodaiVagaigalTamil[Convert.ToString(seerThodaiInAdiMask, 2)],                                                        
                                                        seerThodaiInAdi.ToString()
                                                    };
            }
            CalculateAdiThodai(adigal, 0, CheckEthukaiOrMonaiFunc);
            //var nextEthugai = mudhalSeerInMudhalAdi;            
            //for (var adiIndex = 1; adiIndex < adigal.Count - 1; adiIndex++)
            //{
            //    var mudhalSeerInNextAdi = adigal[adiIndex + 1].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            //    if (CheckTamilEthukai(mudhalSeerInMudhalAdi, mudhalSeerInNextAdi))
            //    {
            //        AdiThodai[adiIndex + 1] = mudhalSeerInNextAdi;
            //        adigal[adiIndex] = string.Empty;
            //    }
            //    else
            //    {
            //        nextEthugai = mudhalSeerInNextAdi;
            //    }
            //}
        }

        private bool CheckIfAllAdigalAreVisited(List<string> adigal)
        {
            return adigal.Where(a => !string.IsNullOrEmpty(a)).ToArray().Length == 0;
        }
        private void CalculateAdiThodai(List<string> adigal, int startIndex, Func<string, string, bool> CheckEthukaiOrMonaiFunc)
        {
            if (CheckIfAllAdigalAreVisited(adigal))
                return;
            var mudhalSeerInMudhalAdi = adigal[startIndex].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            AdiThodai[startIndex] = Transliterator.Latin2Tamil(mudhalSeerInMudhalAdi);
            adigal[startIndex] = string.Empty;
            bool foundNewEthukaiOrMonai = false;
            for (var adiIndex = startIndex + 1; adiIndex < adigal.Count; adiIndex++)
            {
                // Check to make sure that the adi is not already visited and cleared out by the previous ethukai/monai match
                if (!string.IsNullOrEmpty(adigal[adiIndex]))
                {
                    var mudhalSeerInNextAdi =
                        adigal[adiIndex].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    if (CheckEthukaiOrMonaiFunc(mudhalSeerInMudhalAdi, mudhalSeerInNextAdi))
                    {
                        AdiThodai[adiIndex] = Transliterator.Latin2Tamil(mudhalSeerInNextAdi);
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
            CalculateAdiThodai(adigal, startIndex, CheckEthukaiOrMonaiFunc);
        }
        /**
         * Calculates the number of Vikalpa (Adi Etukai) in the Text
         * @return number
         */

        public int GetVikalpaCount()
        {
            return GetVikalpaCountHelper(CheckEthukai);
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

        public string GetMetreType()
        {
            var Venpaa = CheckVenpaa();

            MetreType = Venpaa;

            return MetreType;
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
                    if (monaiConsonant.Value.IndexOf(FirstWord.Substring(0, 1)) > 0 && monaiConsonant.Value.IndexOf(SecondWord.Substring(0, 1)) > 0)
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

        public bool CheckThaniChol(string SourceText, int LineIndex, bool RhymeCheck)
        {
            SourceText = SourceText.Replace("--", "-");
            SourceText = SourceText.Replace("–", "-");

            var Lines = SourceText.Trim().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
        public List<Dictionary<string, string>> GetTamilWordBond(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetWordBondHelper(root, new List<string> { "நேர்", "நிரை", "சீர்வகை" }, ProsodyGrammarConstants.Thalaigal.TamilThalaiRules);
        }
        public List<string> GetLineClass(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetLineClassHelper(root, "smeta", TamilLineType);
        }
        public List<string> GetTamilLineClass(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> root)
        {
            return GetLineClassHelper(root, "அடிவகை", TamilLineType);
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

        private string CheckVenpaa()
        {
            var root = this.ParseTreeRoot;

            bool LineClassCheck = true;
            bool WordClassCheck = true;
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> LastLine =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            // Check for allowed Seers, Line Count

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
                                // Check for Allowed Seers
                                if (
                                    ProsodyGrammarConstants.TamilPaIlakkanaSeergal.Venpaa["AllowedSeergal"].Find(
                                        a => a == asai.Value) != asai.Value)
                                {
                                    WordClassCheck = false;
                                }
                            }
                        }
                    }
                }
                // Check for Allowed Line Classes
                if (line.Key != string.Format("aTi-{0}", this.TotalLines) &&
                    int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 4)
                {
                    LineClassCheck = false;
                }
                if (line.Key == string.Format("aTi-{0}", this.TotalLines) &&
                    int.Parse(line.Value["smeta"]["smeta"]["smeta"]) != 3)
                {
                    LineClassCheck = false;
                }
                if (line.Key == string.Format("aTi-{0}", this.TotalLines))
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
            FinalSyllableClassCheck = (LastWord.Count == 2 || LastWord.Count == 3);

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
                LastWord["meta"].Add("meta", VenpaTamilWordClass[VenLastSyllable]);
                FinalSyllableClassCheck = true;
            }

            // Check for Metre Specific Thalais 
            WordBondClassCheck = true;
            foreach (var bond in WordBond)
            {
                if (!bond["bond"].Contains("வெண்டளை"))
                {
                    WordBondClassCheck = false;
                    break;
                }
            }

            // Classify the Metre
            if (WordBondClassCheck && FinalSyllableClassCheck && LineClassCheck && WordClassCheck)
            {
                ThaniCholExists = CheckThaniChol(ProsodyText, 2, true);
            }
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
            if(TotalLines <5 )
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

            return MetreType;
        }
        /*
        private string RemovePunctuation(string text)
        {
            //var punctuations = new string[] {"-", "!", ";", "?", ".", "(", ",", "'", ":", "\"", "_", ")", "[", "]"};

            Regex regex = new Regex(
               "[\\p{P}]",
             RegexOptions.IgnoreCase
             | RegexOptions.CultureInvariant
             | RegexOptions.IgnorePatternWhitespace
             | RegexOptions.Compiled
             );

            text = regex.Replace(text, "");
            text = Regex.Replace(text, "  *", "  ");

            return text;
        }
         * */
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
            int kurilEzuthuCount = (uyirKurilEzhuthuCount + agaraUyirMeyEzuthuCount + aikaraKurukkamCount) - meyEzuthuCount - uyirMeyNedilEzuthuCount;
            int nedilEzuthuCount = (uyirNedilEzhuthuCount + uyirMeyNedilEzuthuCount) - aikaraKurukkamCount - aikaramInStartPosition;
            //Console.WriteLine(
            //    "Uyir Ezuthukkal = {0}\r\nMey Ezuthukkal = {1}\r\nUyir Mey Ezuthukkal = {2}\r\nAytha Ezhuthukkal = {3}\r\nGrantha Ezuthukkal = {4}\r\nKuril Ezuthukkal = {5}\r\nNedil Ezuthukkal = {6}",
            //    uyirEzuthuCount, meyEzuthuCount, uyirMeyEzuthuCount, aythaEzuthuCount, granthaEzuthuCount, kurilEzuthuCount, nedilEzuthuCount);
            mathirai = kurilEzuthuCount * TamilLanguageConstants.MathiraiTable["குறில்"] +
                       nedilEzuthuCount * TamilLanguageConstants.MathiraiTable["நெடில்"] +
                       meyEzuthuCount * TamilLanguageConstants.MathiraiTable["மெய்"] +
                       aythaEzuthuCount * TamilLanguageConstants.MathiraiTable["ஆய்தம்"] +
                       aikaramInStartPosition * TamilLanguageConstants.MathiraiTable["ஐகாரம்"];
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
}
