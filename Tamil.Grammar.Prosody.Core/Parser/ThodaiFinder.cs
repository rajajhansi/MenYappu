using System.Collections.Generic;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
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
}