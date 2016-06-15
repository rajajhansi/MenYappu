using System.Collections.Generic;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
    public class MetricLinkage
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTree { get; set; }
        public List<Dictionary<string, string>> WordBond { get; set; }
    }
}