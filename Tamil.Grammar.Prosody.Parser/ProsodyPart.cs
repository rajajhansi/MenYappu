using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamil.Grammar.Prosody.Parser
{
    public class ProsodyPart
    {
        public string MetreType { get; set; }
        public Dictionary<string, int> LetterCount { get; set; }
        public List<Dictionary<string, double>> MathiraiCount { get; set; }
        public double TotalMathiraiCount { get; set; }
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTree { get; set; }
        public List<Dictionary<string, string>> WordBond { get; set; }
        public List<string> Lines { get; set; }
        public List<string> LineClass { get; set; }
        public List<Dictionary<int, string>> MatchingAdiEthukai { get; set; }
        public Dictionary<int, List<string>> SeerEthukaiWithinAdi { get; set; }
        public List<Dictionary<int, string>> MatchingAdiMonai { get; set; }
        public Dictionary<int, List<string>> SeerMonaiWithinAdi { get; set; }
    }
}
