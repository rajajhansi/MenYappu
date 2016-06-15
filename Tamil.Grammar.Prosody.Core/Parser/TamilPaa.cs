using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
    public class TamilPaa
    {
        public string ProsodyText { get; set; }
        public bool ShouldParseKutriyalukaram { get; set; }
        public bool ShouldParseVilaangaaySeer { get; set; }
        public bool ShouldCompareVenpaRules { get; set; }
    }
}
