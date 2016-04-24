using System;
using System.Collections.Generic;

namespace RjamSoft.Tamil.Grammar.Prosody.Core
{
    public static class StringExtensionMethods
    {
        public static int[] FindAll(this string matchStr, string searchedStr, int startPos)
        {
            int foundPos = -1; // -1 represents not found.
            int count = 0;
            List<int> foundItems = new List<int>();
            do
            {
                foundPos = searchedStr.IndexOf(matchStr, startPos, StringComparison.
                Ordinal);
                if (foundPos > -1)
                {
                    startPos = foundPos + 1;
                    count++;
                    foundItems.Add(foundPos);
                    //Console.WriteLine("Found item at position: " + foundPos.ToString());
                }
            } while (foundPos > -1 && startPos < searchedStr.Length);
            return foundItems.ToArray();
        }

        public static int[] FindAll(this char MatchChar, string searchedStr, int startPos)
        {
            int foundPos = -1; // -1 represents not found.
            int count = 0;
            List<int> foundItems = new List<int>();
            do
            {
                foundPos = searchedStr.IndexOf(MatchChar, startPos);
                if (foundPos > -1)
                {
                    startPos = foundPos + 1;
                    count++;
                    foundItems.Add(foundPos);
                    Console.WriteLine("Found item at position: " + foundPos.ToString());
                }
            } while (foundPos > -1 && startPos < searchedStr.Length);
            return foundItems.ToArray();
        }

        public static IEnumerable<string> SplitInGroups(this string original, int size)
        {
            var p = 0;
            var l = original.Length;
            while (l - p > size)
            {
                yield return original.Substring(p, size);
                p += size;
            }
            yield return original.Substring(p);
        }

        public static string TamilSubstr(this string tamilString, int startIndex, int length)
        {
            var latinString = Transliterator.Tamil2Latin(tamilString);
            return Transliterator.Latin2Tamil(latinString.Substring(startIndex*2, length*2));
        }

        public static int TamilLength(this string tamilString)
        {
            var latinString = Transliterator.Tamil2Latin(tamilString);
            return latinString.Length/2;
        }

        public static string ToLatinString(this string tamilString)
        {
            return Transliterator.Tamil2Latin(tamilString);
        }

        public static string ToTamilString(this string latinString)
        {
            return Transliterator.Latin2Tamil(latinString);
        }
    }
}
