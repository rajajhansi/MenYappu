using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RjamSoft.Tamil.Grammar.Prosody.Core.Parser
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
            public static Regex KutriyalukaramRegex = new Regex("(([kcTtpR])(u))(\\s*)((_)([aAiIuUeEoO]))",
                RegexOptions.Multiline | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            public static Regex KutriyalukaramAtEOLRegex = new Regex("(([kcTtpR])(u))(\\s*\\n)((_)([aAiIuUeEoO]))",
                RegexOptions.Multiline | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            public static Regex KutriyalikaramPunarchiRegex = new Regex("(([kcTtpR])(u)\\b)(\\s*)((y[A]))",
                RegexOptions.Multiline | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            public static string KutriyalukaramRegexReplacementString = "$4$2$7";//"($1)$2$3";
            public static string KutriyalikaramPunarchiRegexReplacementString = "$4($2i)$6";
            public static Regex KutriyalikaramRegex = new Regex("(([kcTtpR])(i))(\\s*)((y[A]))",
                RegexOptions.Multiline | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled
                );

            public static string KutriyalikaramRegexReplacementString = "($1)$4$5";
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
                            {"DisAllowedSeergal", new List<string> {}}
                        };
            public static Dictionary<string, List<string>> Asiriyipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"tEmA","puLimA","kUviLa_m","karuviLa_m"}},
                            {"DisAllowedSeergal", new List<string> {"karuviLa_GkaVi","kUviLa_GkaVi"}}
                        };
            public static Dictionary<string, List<string>> Kalipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"tEmA_GkA_y","puLimA_GkA_y","kUviLa_GkA_y","karuviLa_GkA_y"}},
                            {"DisAllowedSeergal", new List<string> {"karuviLa_GkaVi","kUviLa_GkaVi","tEmA","puLimA"}}
                        };
            public static Dictionary<string, List<string>> VenKalipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"tEmA_GkA_y","puLimA_GkA_y","kUviLa_GkA_y","karuviLa_GkA_y"}},
                            {"DisAllowedSeergal", new List<string> {"karuviLa_GkaVi","kUviLa_GkaVi","tEmA","puLimA"}},
                            {"NonFinalWordClass", new List<string> {"karuviLa_GkaVi", "kUviLa_GkaVi" }}
                        };
        }

        public class TamilPaIlakkanaSeergal
        {
            public static Dictionary<string, List<string>> Venpaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> { "தேமா","புளிமா","கூவிளம்","கருவிளம்","தேமாங்காய்","புளிமாங்காய்","கூவிளங்காய்","கருவிளங்காய்" /*,"மா","விளம்" */}},
                            {"DisAllowedSeergal", new List<string> {}}
                        };
            public static Dictionary<string, List<string>> Asiriyipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"தேமா","புளிமா","கூவிளம்","கருவிளம்"}},
                            {"DisAllowedSeergal", new List<string> {"கருவிளங்கனி","கூவிளங்கனி"}},
                            {"AllowedMonoFinalLetters", new List<string> { "E", "O", "I", "Y", "Q"}}, // ஏ ஓ ஐ ஈ Q
                            {"AllowedTriFinalLetters", new List<string> { "A_y", "e_V"}} // "ஆய்" "என்"
                        };
            public static Dictionary<string, List<string>> Kalipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"தேமாங்காய்","புளிமாங்காய்","கூவிளங்காய்","கருவிளங்காய்"}},
                            {"DisAllowedSeergal", new List<string> {"கருவிளங்கனி","கூவிளங்கனி","தேமா","புளிமா"}}
                        };
            public static Dictionary<string, List<string>> VenKalipaa = new Dictionary<string, List<string>>
            {
                            {"AllowedSeergal", new List<string> {"தேமாங்காய்","புளிமாங்காய்","கூவிளங்காய்","கருவிளங்காய்"}},
                            {"DisAllowedSeergal", new List<string> {"கருவிளங்கனி","கூவிளங்கனி","தேமா","புளிமா"}},
                            {"NonFinalWordClass", new List<string> {"கருவிளங்கனி","கூவிளங்கனி" }}
                        };
        }

        public static List<string> TamilLineType = new List<string> { "", "தனிச்சொல்", "குறளடி", "சிந்தடி", "அளவடி", "நெடிலடி", "அறுசீர்க் கழிநெடிலடி", "எழுசீர்க் கழிநெடிலடி", "எண்சீர்க் கழிநெடிலடி", "ஒன்பதின்சீர்க் கழிநெடிலடி", "பதின்சீர்க் கழிநெடிலடி", "பதினொறு சீர்க் கழிநெடிலடி", "பன்னிரு சீர்க் கழிநெடிலடி", "பதின்மூன்று சீர்க் கழிநெடிலடி", "பதினான்கு சீர்க் கழிநெடிலடி", "பதினைந்து சீர்க் கழிநெடிலடி", "பதினாறு சீர்க் கழிநெடிலடி", "பதினேழு சீர்க் கழிநெடிலடி", "பதினெட்டு சீர்க் கழிநெடிலடி", "பத்தொன்பது சீர்க் கழிநெடிலடி", "இருபது சீர்க் கழிநெடிலடி", "இருபத்தோரு சீர்க் கழிநெடிலடி", "இருபத்தி இரண்டு சீர்க் கழிநெடிலடி", "இருபத்து மூன்று சீர்க் கழிநெடிலடி", "இருபத்து நான்கு சீர்க் கழிநெடிலடி" };
        public static List<string> LineType = new List<string> { "", "taVi_cco_l", "kuRaLaTi", "ci_ntaTi", "_aLavaTi", "neTilaTi", "_aRucI_r_k kaZineTilaTi", "_eZucI_r_k kaZineTilaTi", "_e_NcI_r_k kaZineTilaTi", "_o_Vpati_ncI_r_k kaZineTilaTi", "pati_VcI_r_k kaZineTilaTi", "patiVoru cI_r_k kaZineTilaTi", "pa_VViru cI_r_k kaZineTilaTi", "patimU_VRu cI_r_k kaZineTilaTi", "patiVA_Vku cI_r_k kaZineTilaTi", "patiVY_ntu cI_r_k kaZineTilaTi", "patiVARu cI_r_k kaZineTilaTi", "patiVEZu cI_r_k kaZineTilaTi", "patiVe_TTu cI_r_k kaZineTilaTi", "pa_tto_Vpatu cI_r_k kaZineTilaTi", "_irupatu cI_r_k kaZineTilaTi", "_irupa_ttoru cI_r_k kaZineTilaTi", "_irupa_ttu _ira_NTu cI_r_k kaZineTilaTi", "_irupa_ttu mU_VRu cI_r_k kaZineTilaTi", "_irupa_ttu nA_Vku cI_r_k kaZineTilaTi" };

        public static List<string> SyllableTypes = new List<string> { "nE_r", "nirY" };
        public static List<string> TamilSyllableTypes = new List<string> { "நேர்", "நிரை" };

        public static Dictionary<string, double> MathiraiTable = new Dictionary<string, double>
        {
            {"குறில்", 1.0 },
            {"நெடில்", 2.0},
            {"மெய்", 0.5},
            {"ஆய்தம்", 0.5},
            {"ஐகாரம்", 1.5}
        };

        public static Dictionary<string, string> TodaiVagaigalTamil = new Dictionary<string, string>
        {
            { "1000", "இல்லை"},
            { "1100", "இணை"},
            { "1010", "பொழிப்பு"},
            { "1001", "ஒரூஉ"},
            { "1110", "கூழை"},
            { "1101", "கீழ்க்கதுவாய்"},
            { "1011", "மேற்கதுவாய்"},
            { "1111", "முற்று"},
            { "0011", "கடையிணை"},
            { "0101", "பின்"},
            { "0111", "கடைக்கூழை"},
            { "0110", "இடைப்புணர்"}
        };

        /**
        * Reference Array for getting the Word Type
        * @var String Array
        */
        public static Dictionary<string, string> WordType = new Dictionary<string, string>
        {

	        // Two Asais

            { "nE_rnE_r" , "tEmA"},
            {"nirYnE_r" , "puLimA"},
            {"nE_rnirY" , "kUviLa_m"},
            {"nirYnirY" , "karuviLa_m"},

	        // Three Asais - Kay seers

            {"nE_rnE_rnE_r" , "tEmA_GkA_y"},
            {"nirYnE_rnE_r" , "puLimA_GkA_y"},
            {"nE_rnirYnE_r" , "kUviLa_GkA_y"},
            {"nirYnirYnE_r" , "karuviLa_GkA_y"},
            {"nE_rnirY2nE_r" , "kUviLA_GkA_y"},
            {"nirYnirY2nE_r" , "karuviLA_GkA_y"},

	        // Three Asais - Kani seers

            {"nE_rnE_rnirY" , "tEmA_GkaVi"},
            {"nirYnE_rnirY" , "puLimA_GkaVi"},
            {"nE_rnirYnirY" , "kUviLa_GkaVi"},
            {"nirYnirYnirY" , "karuviLa_GkaVi"},

	        // Four Asais - Tanpuu seers

            {"nE_rnE_rnE_rnE_r" , "tEmA_nta_NpU"},
            {"nirYnE_rnE_rnE_r" , "puLimA_nta_NpU"},
            {"nE_rnirYnE_rnE_r" , "kUviLa_nta_NpU"},
            {"nirYnirYnE_rnE_r" , "karuviLa_nta_NpU"},

            // Four Asais - naRumpU seers

            {"nE_rnE_rnirYnE_r" , "tEmAnaRu_mpU"},
            {"nirYnE_rnirYnE_r" , "puLimAnaRu_mpU"},
            {"nE_rnirYnirYnE_r" , "kUviLanaRu_mpU"},
            {"nirYnirYnirYnE_r" , "karuviLanaRu_mpU"},

	        // Four Asais - naRunizhal

            {"nE_rnE_rnirYnirY" , "tEmAnaRuniZa_l"},
            {"nirYnE_rnirYnirY" , "puLimAnaRuniZa_l"},
            {"nE_rnirYnirYnirY" , "kUviLanaRuniZa_l"},
            {"nirYnirYnirYnirY" , "karuviLanaRuniZa_l"},

	        // Four Asais- Tannizhal

            {"nE_rnE_rnE_rnirY" , "tEmA_nta_NNiZa_l"},
            {"nirYnE_rnE_rnirY" , "puLimA_nta_NNiZa_l"},
            {"nE_rnirYnE_rnirY" , "kUviLa_nta_NNiZa_l"},
            {"nirYnirYnE_rnirY" , "karuviLa_nta_NNiZa_l"},

	        // Singla ASai - Exceptions

            {"nE_r" , "mA"},
            {"nirY" , "viLa_m"}
        };

        public static Dictionary<string, string> VenpaWordClass = new Dictionary<string, string>
        {
            {"nE_r" , "nAL"},
            {"nirY" , "malar"},
            {"nirYpu", "kAcu"},
            {"nE_rpu", "piRa_ppu"}
        };

        public static Dictionary<string, string> TamilWordType = new Dictionary<string, string>
        {

	        // ஈரசைச்சீர்

            {"நேர்நேர்" , "தேமா"},
            {"நிரைநேர்" , "புளிமா"},
            {"நேர்நிரை" , "கூவிளம்"},
            {"நிரைநிரை" , "கருவிளம்"},

	        // மூவசைச் சீர்  - காய்ச்சீர்

            {"நேர்நேர்நேர்" , "தேமாங்காய்"},
            {"நிரைநேர்நேர்" , "புளிமாங்காய்"},
            {"நேர்நிரைநேர்" , "கூவிளங்காய்"},
            {"நிரைநிரைநேர்" , "கருவிளங்காய்"},
            {"நேர்நிரை2நேர்" , "கூவிளாங்காய்"},
            {"நிரைநிரை2நேர்" , "கருவிளாங்காய்"},

	        // மூவசைச்சீர் - கனிச்சீர்

            {"நேர்நேர்நிரை" , "தேமாங்கனி"},
            {"நிரைநேர்நிரை" , "புளிமாங்கனி"},
            {"நேர்நிரைநிரை" , "கூவிளங்கனி"},
            {"நிரைநிரைநிரை" , "கருவிளங்கனி"},

	        // நாலசைச்சீர் - பூச்சீர் - தண்பூ

            {"நேர்நேர்நேர்நேர்" , "தேமாந்தண்பூ"},
            {"நிரைநேர்நேர்நேர்" , "புளிமாந்தண்பூ"},
            {"நேர்நிரைநேர்நேர்" , "கூவிளந்தண்பூ"},
            {"நிரைநிரைநேர்நேர்" , "கருவிளந்தண்பூ"},

            // நாலசைச்சீர் - பூச்சீர் - நறும்பூச்சீர்

            {"நேர்நேர்நிரைநேர்" , "தேமாநறும்பூ"},
            {"நிரைநேர்நிரைநேர்" , "புளிமாநறும்பூ"},
            {"நேர்நிரைநிரைநேர்" , "கூவிளநறும்பூ"},
            {"நிரைநிரைநிரைநேர்" , "கருவிளநறும்பூ"},

	        // நாலசைச்சீர் - நிழல் சீர் - நறுநிழல்

            {"நேர்நேர்நிரைநிரை" , "தேமாநறுநிழல்"},
            {"நிரைநேர்நிரைநிரை" , "புளிமாநறுநிழல்"},
            {"நேர்நிரைநிரைநிரை" , "கூவிளநறுநிழல்"},
            {"நிரைநிரைநிரைநிரை" , "கருவிளநறுநிழல்"},

	        // நாலசைச்சீர் - நிழல் சீர் - தண்ணிழல்

            {"நேர்நேர்நேர்நிரை" , "தேமாந்தண்ணிழல்"},
            {"நிரைநேர்நேர்நிரை" , "புளிமாந்தண்ணிழல்"},
            {"நேர்நிரைநேர்நிரை" , "கூவிளந்தண்ணிழல்"},
            {"நிரைநிரைநேர்நிரை" , "கருவிளந்தண்ணிழல்"},

	        // ஓரசைச்சீர்
            {"நேர்" , "மா"},
            {"நிரை" , "விளம்"}
        };

        public static Dictionary<string, string> VenpaTamilWordClass = new Dictionary<string, string>
        {
            {"நேர்", "நாள்"},
            {"நிரை", "மலர்"},
            {"நேர்பு", "காசு"},
            {"நிரைபு", "பிறப்பு"}
        };
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

        public static Dictionary<string, int> ThodaiTypeDictionary = new Dictionary<string, int>
        {
                {"CheckMonai", 0},
                {"CheckEthukai", 1},
                {"CheckIyaipu", 2}
            };
    }
}