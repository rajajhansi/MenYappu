using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RjamSoft.Tamil.Grammar.Prosody.Parser
{
    public interface ITamilAgarathi
    {
        void Seed(string agarathiFileName, int minimumSuffixLength  = 1);
        IEnumerable<string> Lookup(string word);
    }
}
