using System.Collections.Generic;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
    public class ThodaiRule
    {
        public int RuleNumber { get; set; }
        public Dictionary<bool, string> RuleExplanations { get; set; }

        public ThodaiRule()
        {
            RuleNumber = 0;
            RuleExplanations = new Dictionary<bool, string>();
        }
    }
}