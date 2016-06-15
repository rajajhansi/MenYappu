using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
{
    public class LineMeasure
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> ParseTree { get; set; }
        public List<string> Lines { get; set; }
        public List<string> LineClass { get; set; }
    }
}