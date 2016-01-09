using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RjamSoft.Tamil.Grammar.Parser
{
   
    public class Program
    {
        public static Regex MyRegex = new Regex(
      "[கஙசஞடணதநபமயரலவழளறன]?[அஆஇஈஉஊஎஏஐஒஓஒள]([கஙசஞடணதநபமயரலவழளறன])*",
    RegexOptions.IgnoreCase
    | RegexOptions.IgnorePatternWhitespace
    );



        static int vowelCount = 0;                
        
        public static void AnalyzeMeiEzuthukkal(string meiEzuthukkal)
        {
            foreach (var letter in meiEzuthukkal)
            {
                char foundLetter;
                if (TamilLanguageConstants.UyirEzuthukkalAgarathi.TryGetValue(letter, out foundLetter))
                {
                    vowelCount++;
                }
            }
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
            mathirai = kurilEzuthuCount*TamilLanguageConstants.MathiraiTable["குறில்"] +
                       nedilEzuthuCount*TamilLanguageConstants.MathiraiTable["நெடில்"] +
                       meyEzuthuCount*TamilLanguageConstants.MathiraiTable["மெய்"] +
                       aythaEzuthuCount*TamilLanguageConstants.MathiraiTable["ஆய்தம்"] +
                       aikaramInStartPosition * TamilLanguageConstants.MathiraiTable["ஐகாரம்"];
            return mathirai;
        }

        static void Main(string[] args)
        {
            //string kural = args.Count() == 0 ? "அகர முதல எழுத்தெல்லாம் ஆதி\r\nபகவன் முதற்றே உலகு" : args[0];
            //string kural = "கற்க கசடறக் கற்பவை கற்றபின்\r\nநிற்க அதற்குத் தக";
            string kural = "அறிந்தானை ஏத்தி அறிவாங் கறிந்து\r\nசெறிந்தார்க்குச் செவ்வன் உரைப்ப - செறிந்தார்\r\nசிறந்தமை ஆராய்ந்து கொண்டு";
                //"சச்சின வர்க்ரிக்கெ ட்டில்சாத னையாட்டம்\r\nநச்சென ஆடுவாரா மேல்சபையில் நல்லாட்டம்\r\nவேண்டும் அடித்தாடும் வேங்கைபோல்  துள்ளாட்டம்\r\nவேண்டாம் தடுப்பாட் டமது";
            //string[] words = kural.Split(null);
            //Dictionary<string, double> mathiraiCountOfWords = new Dictionary<string, double>();
            //foreach (var word in words)
            //{
            //    if(word.Length > 0)
            //    {
            //        mathiraiCountOfWords[Transliterator.Tamil2Latin(word)] = CalculateMathirai(word);                    
            //    }
            //}

            //foreach (var mathiraiCount in mathiraiCountOfWords)
            //{
            //    Console.WriteLine("{0} = {1}", mathiraiCount.Key, mathiraiCount.Value);
            //}
            ProsodyParseTree parodyParserTree = new ProsodyParseTree(kural);

            string InputText = "சந்தம்";
            MatchCollection ms = MyRegex.Matches(InputText);

            var text = Transliterator.Tamil2Latin(InputText);

            

              
            foreach(var ugaraMeiEzuthuVagai in TamilLanguageConstants.UgaraMeiEzuthuVagaigal)
            {
                AnalyzeMeiEzuthukkal(ugaraMeiEzuthuVagai);
            }
            foreach (var uugaraMeiEzuthuVagai in TamilLanguageConstants.UugaraMeiEzuthuVagaigal)
            {
                AnalyzeMeiEzuthukkal(uugaraMeiEzuthuVagai);
            }            
            
            int uyirEzuthuCount = kural.Count(k => k == TamilLanguageConstants.UyirEzuthukkal.Find(l => l == k));
            int uyirAndUyirMeyKurilEzuthuCount = kural.Count(k => k == TamilLanguageConstants.UyirMeyKurilEzuthukkal.Find(l => l == k));
            int uyirAndUyriMeyNedilEzuthuCount = kural.Count(k => k == TamilLanguageConstants.UyirMeyNedilEzuthukkal.Find(l => l == k));            
            int meyEzuthuCount = kural.Count(k => k == TamilLanguageConstants.MeyEzuthuPulli /* 0X0BCD - மெய்யெழுத்தில் வரும் புள்ளி. எ.கா, ’க்’ */);
            int aythaEzuthuCount = kural.Count(k => k == TamilLanguageConstants.AythaEzuthu);
            int agaraUyirMeyEzuthuCount = kural.Count(k => k == TamilLanguageConstants.AgaraUyirMeyEzuthukkal.Find(l => l == k));
            int granthaEzuthuCount = kural.Count(k => k == TamilLanguageConstants.GranthaEzuthukkal.Find(l => l == k));
            int uyirMeyEzuthuCount = (agaraUyirMeyEzuthuCount + granthaEzuthuCount) - meyEzuthuCount;
            int kurilEzuthuCount = (uyirAndUyirMeyKurilEzuthuCount + agaraUyirMeyEzuthuCount) - meyEzuthuCount - uyirAndUyriMeyNedilEzuthuCount;
            int nedilEzuthuCount = uyirAndUyriMeyNedilEzuthuCount;
            Console.WriteLine(
                "Uyir Ezuthukkal = {0}\r\nMey Ezuthukkal = {1}\r\nUyir Mey Ezuthukkal = {2}\r\nAytha Ezhuthukkal = {3}\r\nGrantha Ezuthukkal = {4}\r\nKuril Ezuthukkal = {5}\r\nNedil Ezuthukkal = {6}",
                uyirEzuthuCount, meyEzuthuCount, uyirMeyEzuthuCount, aythaEzuthuCount, granthaEzuthuCount, kurilEzuthuCount, nedilEzuthuCount);
                                
        }
    }
}
