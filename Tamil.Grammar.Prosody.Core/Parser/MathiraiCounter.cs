using System.Collections.Generic;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
    public class MathiraiCounter
    {
        public List<Dictionary<string, double>> MathiraiCount { get; set; }
        public List<Dictionary<string, Dictionary<string, LetterTypeWithMathirai>>> DetailedMathiraiCount { get; set; }
        public double TotalMathiraiCount { get; set; }
        public int TotalLetterCount { get; set; }
    }
}