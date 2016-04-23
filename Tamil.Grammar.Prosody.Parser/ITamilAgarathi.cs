using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamil.Grammar.Prosody.Parser
{
    interface ITamilAgarathi
    {
        void Seed(string agarathiFileName);
        IEnumerable<string> Lookup(string word);
    }
}
