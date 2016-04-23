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
    }
}
