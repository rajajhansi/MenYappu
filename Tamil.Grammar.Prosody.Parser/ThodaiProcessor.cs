using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Parser;
using RjamSoft.Tamil.Grammar.Prosody.Core;

namespace Tamil.Grammar.Prosody.Parser
{
    public class ThodaiProcessor
    {
        private static List<string> GetThodaiForDisplayOfMonai(string adiWithThodai, string thodaiInAdi, int position)
        {
            var thodaisInTamil = new List<string>();
            thodaisInTamil.Add(String.Concat(adiWithThodai, " "));
            thodaisInTamil.Add(String.Concat("~", Transliterator.Latin2Tamil(thodaiInAdi.Substring(position * 2, 2))));
            thodaisInTamil.Add(Transliterator.Latin2Tamil(thodaiInAdi.Substring((position * 2) + 2)));

            return thodaisInTamil;
        }

        private static List<string> GetThodaiForDisplayOfEthukai(string adiWithThodai, string thodaiInAdi, int position)
        {
            var thodaisInTamil = new List<string>();
            thodaisInTamil.Add(String.Concat(adiWithThodai, " "));
            thodaisInTamil.Add(Transliterator.Latin2Tamil(thodaiInAdi.Substring(0, 2)));
            if (thodaiInAdi.Length > 2)
            {
                thodaisInTamil.Add(String.Concat("~", Transliterator.Latin2Tamil(thodaiInAdi.Substring(position * 2, 2))));
                thodaisInTamil.Add(Transliterator.Latin2Tamil(thodaiInAdi.Substring((position * 2) + 2)));
            }
            return thodaisInTamil;
        }

        private static List<string> GetThodaiForDisplayOfIyaipu(string adiWithThodai, string thodaiInAdi, int position)
        {
            var thodaisInTamil = new List<string>();
            thodaisInTamil.Add(String.Concat(adiWithThodai, " "));
            thodaisInTamil.Add(Transliterator.Latin2Tamil(thodaiInAdi.Substring(0, thodaiInAdi.Length - 2)));
            thodaisInTamil.Add(String.Concat("~", Transliterator.Latin2Tamil(thodaiInAdi.Substring(thodaiInAdi.Length - 2))));
            return thodaisInTamil;
        }

        private static readonly Dictionary<int, Func<string, string, int, List<string>>> GetThodaiForDisplayFuncDictionary = new Dictionary
            <int, Func<string, string, int, List<string>>>
        {
            {0, GetThodaiForDisplayOfMonai},
            {1, GetThodaiForDisplayOfEthukai},
            {2, GetThodaiForDisplayOfIyaipu}
        };
        public static List<string> GetThodaiForDisplay(string thodaiInAdi, int position)
        {
            var thodais = thodaiInAdi.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var thodaisInTamil = new List<string>();
            for (var index = 1; index < thodais.Length; index += 2)
            {
                thodaisInTamil.AddRange(GetThodaiForDisplayFuncDictionary[position](thodais[index - 1], thodais[index], position));
            }
            return thodaisInTamil;
        }

        private int CalcIndex(int i, int startFrom = 0)
        {
            return (startFrom == 0 ? i : startFrom - 1 - i);
        }

        public static readonly Dictionary<string, Func<int, string>> CalcSeerThodaiInAdiMaskFuncDictionary = new Dictionary
            <string, Func<int, string>>
        {
            {"CheckMonai", CalcSeerThodaiInAdiMask},
            {"CheckEthukai", CalcSeerThodaiInAdiMask},
            {"CheckIyaipu", CalcSeerThodaiInAdiMaskForIyaipu}
        };
        private static string CalcSeerThodaiInAdiMask(int seerThodaiInAdiMask)
        {
            return Convert.ToString(seerThodaiInAdiMask, 2).PadLeft(4, '0');
        }

        private static string CalcSeerThodaiInAdiMaskForIyaipu(int seerThodaiInAdiMask)
        {
            return ((seerThodaiInAdiMask == 8) ?
                CalcSeerThodaiInAdiMask(seerThodaiInAdiMask) :
                new String(CalcSeerThodaiInAdiMask(seerThodaiInAdiMask).Reverse().ToArray())
                );
        }

        public static readonly Dictionary<string, Func<string, string>> SeerWithinAdiFuncDictionary = new Dictionary
            <string, Func<string, string>>
        {
            {"CheckMonai", GetSeerWithinAdi},
            {"CheckEthukai", GetSeerWithinAdi},
            {"CheckIyaipu", GetSeerWithinAdiForIyaipu}
        };

        private static string GetSeerWithinAdi(string adi)
        {
            return adi.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private static string GetSeerWithinAdiForIyaipu(string adi)
        {
            var seergal = adi.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return seergal[seergal.Length - 1];
        }
    }
}
