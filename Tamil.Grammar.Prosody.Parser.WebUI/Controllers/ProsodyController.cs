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
using Tamil.Grammar.Prosody.Parser;

namespace Tamil.Parody.Parser.WebUI.Controllers
{
    [RoutePrefix("api/prosody")]
    public class ProsodyController : ApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage  Parse(TamilPaa tamilpaa)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (tamilpaa != null && !string.IsNullOrEmpty(tamilpaa.ProsodyText))
            {
                var prosodyParser = new ProsodyParser(tamilpaa.ProsodyText, tamilpaa.ShouldParseKutriyalukaram, tamilpaa.ShouldParseVilaangaaySeer);
                var prosodyPart = prosodyParser.Parse();
                response = Request.CreateResponse(HttpStatusCode.Created, prosodyPart);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", prosodyPart.MetreType));
            }

            return response;
        }

        [Route("MathiraiCount")]
        [HttpPost]
        public HttpResponseMessage MathiraiCount(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (prosodyText != null && !string.IsNullOrEmpty(prosodyText.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var mathiraiCount = prosodyParser.GetMathiraiCount();
                response = Request.CreateResponse(HttpStatusCode.Created, mathiraiCount);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", "MathiraiCount"));
            }

            return response;
        }

        [Route("ThodaiFinder")]
        [HttpPost]
        public HttpResponseMessage FindThodais(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (prosodyText != null && !string.IsNullOrEmpty(prosodyText.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var mathiraiCount = prosodyParser.GetThodais();
                response = Request.CreateResponse(HttpStatusCode.Created, mathiraiCount);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", "FindThodais"));
            }

            return response;
        }

        [Route("ThalaiFinder")]
        [HttpPost]
        public HttpResponseMessage FindThalais(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (prosodyText != null && !string.IsNullOrEmpty(prosodyText.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var metricLinkage = prosodyParser.GetMetricAndLinkage(prosodyText.InputText);
                response = Request.CreateResponse(HttpStatusCode.Created, metricLinkage);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", "FindThalais"));
            }

            return response;
        }

        [Route("AdiMeasurer")]
        [HttpPost]
        public HttpResponseMessage FindAdiClass(ProsodyText prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (prosodyText != null && !string.IsNullOrEmpty(prosodyText.InputText))
            {
                var prosodyParser = new ProsodyParser(prosodyText.InputText);
                var lineClass = prosodyParser.GetLineClass(prosodyText.InputText);
                response = Request.CreateResponse(HttpStatusCode.Created, lineClass);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", "FindAdiClass"));
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
    }
}