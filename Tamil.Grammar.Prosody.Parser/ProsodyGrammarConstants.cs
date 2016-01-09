using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RjamSoft.Tamil.Grammar.Parser
{
    // யாப்பிலக்கணம் - யாப்பிலக்கணத்தை பகுப்பதற்கு தேவையான நிலைகள்
    public class ProsodyGrammarConstants
    {
        public class AsaiRegularExpressions
        {                                               
            public static Regex NiraiAsai = new Regex("([kGcJTNtnpmyrlvZLRVjSsh]?_?[aiueoBQ])([kGcJTNtnpmyrlvZLRVjSsh][aAiIuUeEoOYWBQ])(_[KkGcJTNtnpmyrlvZLRVjSsh])*",
                                                        RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            public static Regex NerAsai = new Regex("[kGcJTNtnpmyrlvZLRVjSsh]?_?[aAiIuUeEoOQYBW](_[KkGcJTNtnpmyrlvZLRVjSsh])*",
                                                        RegexOptions.IgnorePatternWhitespace);
            public static Regex NiraipuAsai = new Regex("([kGcJTNtnpmyrlvZLRVjSsh]?_?[aiueoBQ])([kGcJTNtnpmyrlvZLRVjSsh][aAiIuUeEoOYWBQ])(_[KkGcJTNtnpmyrlvZLRVjSsh])*([kGcJTNtnpmyrlvZLRVjSsh]u)",
                                                        RegexOptions.IgnorePatternWhitespace);
            public static Regex NerpuAsai = new Regex("[kGcJTNtnpmyrlvZLRVjSsh]?_?[AIUEOQYBW](_[KkGcJTNtnpmyrlvZLRVjSsh])*([kGcJTNtnpmyrlvZLRVjSsh]u)",
                                                        RegexOptions.IgnorePatternWhitespace);

            public static Regex TamilNiraiAsai = new Regex("([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[aiueoBQ])([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ][அஆஇஈஉஊஎஏஐஒளBQ])(_[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*",
                                                        RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            public static Regex TamilNerAsai = new Regex("[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[aAiIuUeEoOQYBW](_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*",
                                                        RegexOptions.IgnorePatternWhitespace);
            public static Regex TamilNiraipuAsai = new Regex("([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[aiueoBQ])([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ][அஆஇஈஉஊஎஏஐஒளBQ])(_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]u)",
                                                        RegexOptions.IgnorePatternWhitespace);
            public static Regex TamilNerpuAsai = new Regex("[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[AIUEOQYBW](_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]u)",
                                                        RegexOptions.IgnorePatternWhitespace);

            //public static Regex TamilNiraiAsai = new Regex("([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[அஇஉஎஒBQ])([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ][அஆஇஈஉஊஎஏஐஒளBQ])(_[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*",
            //                                            RegexOptions.IgnorePatternWhitespace);
            //public static Regex TamilNerAsai = new Regex("[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[அஆஇஈஉஊஎஏஐஒளBQ](_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*",
            //                                            RegexOptions.IgnorePatternWhitespace);
            //public static Regex TamilNiraipuAsai = new Regex("([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[அஇஉஎஒBQ])([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ][அஆஇஈஉஊஎஏஐஒளBQ])(_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]உ)",
            //                                            RegexOptions.IgnorePatternWhitespace);
            //public static Regex TamilNerpuAsai = new Regex("[கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]?_?[ஆஊஊஏஓஐஒளBW](_[ஃகஙசஞடணதநபமயரலவழளறனஜஶஷஸ])*([கஙசஞடணதநபமயரலவழளறனஜஶஷஸ]உ)",
            //                                            RegexOptions.IgnorePatternWhitespace);

        }

        public class PaIlakkanaSeergal
        {
            public static Dictionary<string, List<string>> Venpaa = new Dictionary<string, List<string>> 
                        { 
                            {"AllowedSeergal", new List<string> { "tEmA","puLimA","kUviLa_m","karuviLa_m","tEmA_GkA_y","puLimA_GkA_y","kUviLa_GkA_y","karuviLa_GkA_y","mA","viLa_m"}},
                            {"DisAllowedSeergal", new List<string>{}}
                        };
            public static Dictionary<string, List<string>> Asiriyipaa = new Dictionary<string, List<string>>
                        {
                            {"AllowedSeergal", new List<string>{"tEmA","puLimA","kUviLa_m","karuviLa_m"}},
                            {"DisAllowedSeergal", new List<string>{"karuviLa_GkaVi","kUviLa_GkaVi"}}
                        };
            public static Dictionary<string, List<string>> Kalipaa = new Dictionary<string, List<string>>
                        {
                            {"AllowedSeergal", new List<string>{"tEmA_GkA_y","puLimA_GkA_y","kUviLa_GkA_y","karuviLa_GkA_y"}},
                            {"DisAllowedSeergal", new List<string>{"karuviLa_GkaVi","kUviLa_GkaVi","tEmA","puLimA"}}
                        };

        }

        public class TamilPaIlakkanaSeergal
        {
            public static Dictionary<string, List<string>> Venpaa = new Dictionary<string, List<string>> 
                        { 
                            {"AllowedSeergal", new List<string> { "தேமா","புளிமா","கூவிளம்","கருவிளம்","தேமாங்காய்","புளிமாங்காய்","கூவிளங்காய்","கருவிளங்காய்","மா","விளம்"}},
                            {"DisAllowedSeergal", new List<string>{}}
                        };
            public static Dictionary<string, List<string>> Asiriyipaa = new Dictionary<string, List<string>>
                        {
                            {"AllowedSeergal", new List<string>{"தேமா","புளிமா","கூவிளம்","கருவிளம்"}},
                            {"DisAllowedSeergal", new List<string>{"கருவிளங்கனி","கூவிளங்கனி"}}
                        };
            public static Dictionary<string, List<string>> Kalipaa = new Dictionary<string, List<string>>
                        {
                            {"AllowedSeergal", new List<string>{"தேமாங்காய்","புளிமாங்காய்","கூவிளங்காய்","கருவிளங்காய்"}},
                            {"DisAllowedSeergal", new List<string>{"கருவிளங்கனி","கூவிளங்கனி","தேமா","புளிமா"}}
                        };

        } 

        public class Thalaigal
        {

            public static Dictionary<string, Dictionary<KeyValuePair<int, string>, string>> ThalaiRules = new Dictionary<string, Dictionary<KeyValuePair<int, string>, string>> 
            {
                { "நேரொன்றாசிரியத்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {                                                        
                                                        { new KeyValuePair<int, string>(2, "mA"), "nE_r" }
                                                         
                                                    }
                },
                { "நிரையொன்றாசிரியத்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(6, "viLa_m"), "nirY" } 
                                                    }
                },
                {
                  "இயற்சீர் வெண்டளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(2, "mA"), "nirY" },
                                                        { new KeyValuePair<int, string>(6, "viLa_m"), "nE_r" } 
                                                    }
                },
                {
                  "வெண்சீர் வெண்டளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(4, "kA_y"), "nE_r" },
                                                        { new KeyValuePair<int, string>(2, "pU"), "nE_r" } 
                                                    }
                },
                {
                  "ஒன்றிய வஞ்சித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(4, "kaVi"), "nirY" },
                                                        { new KeyValuePair<int, string>(6, "niZa_l"), "nirY" } 
                                                    }
                },
                {
                  "ஒன்றா வஞ்சித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(4, "kaVi"), "nE_r" },
                                                        { new KeyValuePair<int, string>(6, "niZa_l"), "nE_r" } 
                                                    }
                },
                {
                  "கலித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>(4, "kA_y"), "nirY" },
                                                        { new KeyValuePair<int, string>(2, "pU"), "nirY" } 
                                                    }
                }
            };

            public static Dictionary<string, Dictionary<KeyValuePair<int, string>, string>> TamilThalaiRules = new Dictionary<string, Dictionary<KeyValuePair<int, string>, string>> 
            {
                { "நேரொன்றாசிரியத்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {                                                        
                                                        { new KeyValuePair<int, string>("மா".Length, "மா"), "நேர்" }
                                                         
                                                    }
                },
                { "நிரையொன்றாசிரியத்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("விளம்".Length, "விளம்"), "நிரை" } 
                                                    }
                },
                {
                  "இயற்சீர் வெண்டளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("மா".Length, "மா"), "நிரை" },
                                                        { new KeyValuePair<int, string>("விளம்".Length, "விளம்"), "நேர்" } 
                                                    }
                },
                {
                  "வெண்சீர் வெண்டளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("காய்".Length, "காய்"), "நேர்" },
                                                        { new KeyValuePair<int, string>("பூ".Length, "பூ"), "நேர்" } 
                                                    }
                },
                {
                  "ஒன்றிய வஞ்சித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("கனி".Length, "கனி"), "நிரை" },
                                                        { new KeyValuePair<int, string>("நிழல்".Length, "நிழல்"), "நிரை" } 
                                                    }
                },
                {
                  "ஒன்றா வஞ்சித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("கனி".Length, "கனி"), "நேர்" },
                                                        { new KeyValuePair<int, string>("நிழல்".Length, "நிழல்"), "நேர்" } 
                                                    }
                },
                {
                  "கலித்தளை", new Dictionary<KeyValuePair<int, string>, string> 
                                                    {
                                                        { new KeyValuePair<int, string>("காய்".Length, "காய்"), "நிரை" },
                                                        { new KeyValuePair<int, string>("பூ".Length, "பூ"), "நிரை" } 
                                                    }
                }
            };
            //public static List<Dictionary<string, string>> NerondrasiriyaThalai = new List<Dictionary<string, string>>()
            //{
            //    new Dictionary<string, string> { { "mA", "nE_r"} }
            //};
            //public static List<Dictionary<string, string>> NiraiyondrasiriyaThalai = new List<Dictionary<string, string>>()
            //{
            //    new Dictionary<string, string> { { "viLa_m", "nirY"} }
            //};
            public static int TodaiMonai = 0;
            public static int TodaiEthukai = 1;
        }
    }
}
