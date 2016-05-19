using System.Text.RegularExpressions;

namespace RjamSoft.Tamil.Grammar.Prosody.Core
{
    public class Transliterator
    {
        public static Regex MeyRegex = new Regex(
              "([kGcJTNtnpmyrlvZLRVjSsh])X",
            RegexOptions.IgnorePatternWhitespace
            );
        public static Regex MeyRegex2 = new Regex(
            "_([kGcJTNtnpmyrlvZLRVjSsh])",
            RegexOptions.IgnorePatternWhitespace);
        public static Regex AgaramRegex = new Regex(
      "(?<!_)([kGcJTNtnpmyrlvZLRVjSsh])(?![aAiIuUeEoOYW])",
    RegexOptions.IgnorePatternWhitespace
    );
	public static Regex JoinRegex = new Regex(
      "(_[kGcJTNtnpmyrlvZLRVjSsh])(_[aAiIuUeEoOYW])",
    RegexOptions.IgnorePatternWhitespace
    );
    public static Regex AukaramRegex = new Regex(
          "(([kGcJTNtnpmyrlvZLRVjSsh]) (eLa))",
        RegexOptions.CultureInvariant
        | RegexOptions.IgnorePatternWhitespace
        | RegexOptions.Compiled
        );

        public static Regex MeyTamilRegex = new Regex(
          "([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])்",
        RegexOptions.IgnorePatternWhitespace
        );
    public static Regex MeyTamilRegex2 = new Regex(
          "_([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])",
        RegexOptions.IgnorePatternWhitespace
        );
    public static Regex AgaramTamilRegex = new Regex(
  "(?<!_)([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])(?![aAiIuUeEoOYW])",
RegexOptions.IgnorePatternWhitespace
);
    public static Regex JoinTamilRegex = new Regex(
      "(_[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])(_[aAiIuUeEoOYW])",
    RegexOptions.IgnorePatternWhitespace
    );

        public static string MeyRegexReplace = "_$1";
        public static string AgaramRegexReplace = "$1a";
        public static string AgaramTamilRegexReplace = "$1a";
		public static string JoinRegexReplace = "$1_$2";
        public static string AukaramRegexReplace = "$2W";
        public static string MeyTamilRegexReplace2 = "$1்";

        public static string Tamil2LatinTamil(string text)
        {
            var enumerator = TamilLanguageConstants.UyirEzuthukkal2LatinAgarathi.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var dictionaryItem = enumerator.Current;
                text = text.Replace(new string(dictionaryItem.Key, 1), dictionaryItem.Value);
            }

            text = MeyTamilRegex.Replace(text, MeyRegexReplace);
            text = AgaramTamilRegex.Replace(text, AgaramTamilRegexReplace);
            text = JoinTamilRegex.Replace(text, JoinRegexReplace);
            return text;
        }
        public static string Tamil2Latin(string text)
        {
            var enumerator = TamilLanguageConstants.UyirEzuthukkal2LatinEnglishAgarathi.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var dictionaryItem = enumerator.Current;
                var char2Replace = new string(dictionaryItem.Key, 1);
                text = text.Replace(char2Replace, dictionaryItem.Value);
            }

            text = MeyRegex.Replace(text, MeyRegexReplace);
            text = AgaramRegex.Replace(text, AgaramRegexReplace);
            text = JoinRegex.Replace(text, JoinRegexReplace);
            text = AukaramRegex.Replace(text, AukaramRegexReplace);
            return text;
        }

        public static string Latin2Tamil(string text)
        {
            text = MeyRegex2.Replace(text, MeyTamilRegexReplace2);
            var enumerator = TamilLanguageConstants.UyirEzuthukkal2LatinEnglishAgarathi.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var dictionaryItem = enumerator.Current;
                text = text.Replace(dictionaryItem.Value, new string(dictionaryItem.Key, 1));
            }
            text = Regex.Replace(text, "a", "");
            text = text.Replace("B", "ௌ");
            text = text.Replace("Q", "ை");
            return text;
        }

        public static string LatinTamil2Tamil(string text)
        {
            text = MeyTamilRegex2.Replace(text, MeyTamilRegexReplace2);
            var enumerator = TamilLanguageConstants.UyirEzuthukkal2LatinAgarathi.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var dictionaryItem = enumerator.Current;
                text = text.Replace(dictionaryItem.Value, new string(dictionaryItem.Key, 1));
            }
            text = Regex.Replace(text, "a", "");
            text = Regex.Replace(text, "(\b.)B", "$1ௌ");
            text = Regex.Replace(text, "(\b.)Q", "$1ை");
            return text;
        }
    }
}
