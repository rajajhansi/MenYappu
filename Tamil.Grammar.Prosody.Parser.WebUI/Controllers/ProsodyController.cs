using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using RjamSoft.Tamil.Grammar.Parser;
using RjamSoft.Tamil.Grammar.Prosody.Core;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract;
using Tamil.Grammar.Prosody.Parser;

namespace Tamil.Parody.Parser.WebUI.Controllers
{
    public class ThodaiFunctionsAndMessages
    {
        public Func<string, int> ThodaiIndexCalculatorFunc { get; set; }
        public Func<string, string, bool> ThodaiCheckFunc { get; set; }
        public Dictionary<bool, string> ExplanationMessages { get; set; }
    }
    [RoutePrefix("api/prosody")]
    public class ProsodyController : ApiController
    {
        private readonly ITamilAgarathi _tamilAgarathi;
        private readonly ISharedResourceManager _sharedResourceManager;
        private readonly ProsodyParser _prosodyParser;

        private static readonly int Monai = 0;
        private static readonly int Ethukai = 1;
        private static readonly int Iyaipu = 2;
        private static readonly string MonaiCorrectExplanation = "இரண்டு சொற்களிலும் உள்ள முதல் எழுத்தான ‘{0}’ ஒன்றி வந்திருப்பதால் அவைகளுக்குள் மோனை இருக்கிறது.";
        private static readonly string MonaiIncorrectExplanation =
            "இரண்டு சொற்களிலும் உள்ள முதல் எழுத்துகள் ‘{0}' '{1}’ ஒன்றி வராததால் அவைகளுக்குள் மோனை இல்லை.";
        private static readonly string EthukaiCorrectExplanation = "இரண்டு சொற்களிலும் உள்ள இரண்டாம் எழுத்தான ‘{0}’ ஒன்றி வந்திருப்பதாலும் முதல் எழுத்துகளின் மாத்திரைகள் ஒன்றி இருப்பதாலும் அவைகளுக்குள் எதுகை இருக்கிறது.";
        private static readonly string EthukaiIncorrectExplanation =
            "இரண்டு சொற்களிலும் உள்ள இரண்டாம் எழுத்துகள் ‘{0}' '{1}’ ஒன்றி வராததாலோ (அ) முதல் எழுத்துகளின் மாத்திரைகள் ஒன்றி வராததாலோ அவைகளுக்குள் எதுகை இல்லை.";
        private static readonly string IyaipuCorrectExplanation = "இரண்டு சொற்களிலும் உள்ள கடைசி எழுத்தான ‘{0}’ ஒன்றி வந்திருப்பதால் அவைகளுக்குள் மோனை இருக்கிறது.";
        private static readonly string IyaipuIncorrectExplanation =
            "இரண்டு சொற்களிலும் உள்ள கடைசி 	எழுத்துகள் ‘{0}' '{1}’ ஒன்றி வராததால் அவைகளுக்குள் மோனை இல்லை.";
        private readonly Dictionary<int, ThodaiFunctionsAndMessages> _thodaiFunctionsAndMessagesDictionary;


        private readonly Dictionary<int, Func<string, int>> _thodaiIndexCalculatorFuncDictionary = new Dictionary
            <int, Func<string, int>>
        {
            {Monai, (s) => 0},
            {Ethukai, (s) => 1 },
            {Iyaipu, (s) => s.TamilLength() - 1}
        };

        private readonly Dictionary<int, Func<string, string, bool>> _thodaiCheckFuncDictionary;

        #region Helper Methods
        private IEnumerable<string> GetThodaiWords(Func<string, string, bool> checkThodaiFunc, int funcIndex)
        {
            var random = new Random();
            var letterIndex = random.Next(0, TamilLanguageConstants.PossibleFirstLettersInAWord.Count);
            var letter = TamilLanguageConstants.PossibleFirstLettersInAWord[letterIndex];
            var words = _tamilAgarathi.Lookup(letter).Where(w => (w.TamilLength() > 1 && w.TamilSubstr(_thodaiIndexCalculatorFuncDictionary[funcIndex](w), 1) == letter)).ToList();
            letterIndex = random.Next(0, words.Count);
            var latinFirstWord = words[letterIndex];
            return words.Where(w => checkThodaiFunc(latinFirstWord, Transliterator.Tamil2Latin(w)));
        }
        #endregion //Helper Methods

        //public ProsodyController()
        //{

        //}
        public ProsodyController(ITamilAgarathi tamilAgarathi, ISharedResourceManager sharedResourceManager, ProsodyParser prosodyParser)
        {
            _tamilAgarathi = tamilAgarathi;
            _sharedResourceManager = sharedResourceManager;
            _prosodyParser = prosodyParser;
            _thodaiFunctionsAndMessagesDictionary =
                new Dictionary<int, ThodaiFunctionsAndMessages>
                {
                    {Monai, new ThodaiFunctionsAndMessages
                            {
                                ThodaiIndexCalculatorFunc = (s) => 0, ThodaiCheckFunc = _prosodyParser.CheckMonai,
                                ExplanationMessages = new Dictionary<bool, string>
                                {
                                    { true, MonaiCorrectExplanation },
                                    { false, MonaiIncorrectExplanation }
                                }
                            }
                    },
                    {Ethukai, new ThodaiFunctionsAndMessages
                            {
                                ThodaiIndexCalculatorFunc = (s) => 1, ThodaiCheckFunc = _prosodyParser.CheckEthukai,
                                ExplanationMessages = new Dictionary<bool, string>
                                {
                                    { true, EthukaiCorrectExplanation },
                                    { false, EthukaiIncorrectExplanation }
                                }
                            }
                    },
                    {Iyaipu, new ThodaiFunctionsAndMessages
                            {
                                ThodaiIndexCalculatorFunc = (s) => s.TamilLength() - 1, ThodaiCheckFunc = _prosodyParser.CheckIyaipu,
                                ExplanationMessages = new Dictionary<bool, string>
                                {
                                    { true, IyaipuCorrectExplanation },
                                    { false, IyaipuIncorrectExplanation }
                                }
                            }
                    },
                };
        }

        [Route("StringResources")]
        [HttpPost]
        public IHttpActionResult StringResources(StringResource stringResource)
        {
            return Ok(_sharedResourceManager.GetAllStrings(stringResource.Language));
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Parse(TamilPaa tamilpaa)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(tamilpaa?.ProsodyText))
            {
                var prosodyParser = new ProsodyParser(tamilpaa.ProsodyText, tamilpaa.ShouldParseKutriyalukaram,
                    tamilpaa.ShouldParseVilaangaaySeer);
                var prosodyPart = prosodyParser.Parse();
                response = Request.CreateResponse(HttpStatusCode.Created, prosodyPart);
                response.Headers.Location = new Uri(Request.RequestUri, $"prosody /{prosodyPart.MetreType}");
            }

            return response;
        }

        [Route("MathiraiCount")]
        [HttpPost]
        public HttpResponseMessage MathiraiCount(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(prosodyText?.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var mathiraiCount = prosodyParser.GetMathiraiCount();
                response = Request.CreateResponse(HttpStatusCode.Created, mathiraiCount);
                response.Headers.Location = new Uri(Request.RequestUri, $"prosody/{"MathiraiCount"}");
            }

            return response;
        }

        [Route("ThodaiChecker")]
        [HttpPost]
        public HttpResponseMessage CheckTodais(ThodaiPairsText thodaiPairsText)
        {
            var thodaiResults = new List<ThodaiResult>(3);
            for (var thodaiIndex = Monai; thodaiIndex <= Iyaipu; thodaiIndex++)
            {
                var firstWord = thodaiPairsText.ThodaiPairs[thodaiIndex].InputText?.ToLatinString();
                var secondWord = thodaiPairsText.ThodaiPairs[thodaiIndex].ThodaiText?.ToLatinString();
                var thodaiResult = new ThodaiResult();
                var letterInFirstWordIndex = _thodaiFunctionsAndMessagesDictionary[thodaiIndex].ThodaiIndexCalculatorFunc(firstWord);
                var letterInSecondWordIndex = _thodaiFunctionsAndMessagesDictionary[thodaiIndex].ThodaiIndexCalculatorFunc(secondWord);
                var letterInFirstWord = firstWord.TamilSubstr(letterInFirstWordIndex, 1);
                var letterInSecondWord = secondWord.TamilSubstr(letterInSecondWordIndex, 1);
                thodaiResult.DoesThodaiExist = _thodaiFunctionsAndMessagesDictionary[thodaiIndex].ThodaiCheckFunc(firstWord, secondWord);
                thodaiResult.Explanation =
                    (thodaiResult.DoesThodaiExist) ?
                    string.Format(_thodaiFunctionsAndMessagesDictionary[thodaiIndex].ExplanationMessages[thodaiResult.DoesThodaiExist],
                        letterInFirstWord) :
                    string.Format(_thodaiFunctionsAndMessagesDictionary[thodaiIndex].ExplanationMessages[thodaiResult.DoesThodaiExist],
                        letterInFirstWord, letterInSecondWord);

                thodaiResults.Add(thodaiResult);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, thodaiResults);

            return response;
        }

        [Route("ThodaiFinder")]
        [HttpPost]
        public HttpResponseMessage FindThodais(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(prosodyText?.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var mathiraiCount = prosodyParser.GetThodais();
                response = Request.CreateResponse(HttpStatusCode.Created, mathiraiCount);
                response.Headers.Location = new Uri(Request.RequestUri, $"prosody/{"FindThodais"}");
            }

            return response;
        }

        [Route("ThalaiFinder")]
        [HttpPost]
        public HttpResponseMessage FindThalais(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(prosodyText?.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var metricLinkage = prosodyParser.GetMetricAndLinkage(prosodyText.InputText);
                response = Request.CreateResponse(HttpStatusCode.Created, metricLinkage);
                response.Headers.Location = new Uri(Request.RequestUri, $"prosody/{"FindThalais"}");
            }

            return response;
        }

        [Route("AdiMeasurer")]
        [HttpPost]
        public HttpResponseMessage FindAdiClass(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(prosodyText?.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var lineClass = prosodyParser.GetLineClass(prosodyText.InputText);
                response = Request.CreateResponse(HttpStatusCode.Created, lineClass);
                response.Headers.Location = new Uri(Request.RequestUri, $"prosody/{"FindAdiClass"}");
            }

            return response;
        }

        private HttpResponseMessage ReadJsonFile(string fileName)
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath(fileName));

            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [Route("Monaigal")]
        [HttpPost]
        public HttpResponseMessage Monaigal()
        {
            var monaiWords = GetThodaiWords(_prosodyParser.CheckMonai, Monai);

            var response = Request.CreateResponse(HttpStatusCode.Created, monaiWords);
            return response;
        }

        [Route("Ethukaigal")]
        [HttpPost]
        public HttpResponseMessage Ethukaigal()
        {
            var ethukaiWords = GetThodaiWords(_prosodyParser.CheckEthukai, Ethukai);

            var response = Request.CreateResponse(HttpStatusCode.Created, ethukaiWords);
            return response;
        }

        [Route("Iyaipugal")]
        [HttpPost]
        public HttpResponseMessage Iyaipugal()
        {
            var iyaipuWords = GetThodaiWords(_prosodyParser.CheckIyaipu, Iyaipu);

            var response = Request.CreateResponse(HttpStatusCode.Created, iyaipuWords);
            return response;
        }
        [Route("ThalaiSentences")]
        [HttpPost]
        public HttpResponseMessage ThalaiSentences()
        {
            return ReadJsonFile(@"~/App_Data/ThalaiSentences.json");
        }

        [Route("Seergal")]
        [HttpPost]
        public HttpResponseMessage Seergal()
        {
            return ReadJsonFile(@"~/App_Data/Seergal.json");
        }

        [Route("Thodaigal")]
        [HttpPost]
        public HttpResponseMessage Thodaigal()
        {
            return ReadJsonFile(@"~/App_Data/Thodaigal.json");
        }

        [Route("Adigal")]
        [HttpPost]
        public HttpResponseMessage Adigal()
        {
            return ReadJsonFile(@"~/App_Data/Adigal.json");
        }
    }
}