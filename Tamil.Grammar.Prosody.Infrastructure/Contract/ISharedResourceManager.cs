using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract
{
    public interface ISharedResourceManager
    {
        Dictionary<string, string> GetAllStrings(string language);
        string Get(string key, string language);
    }
}
