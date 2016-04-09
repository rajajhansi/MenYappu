using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RjamSoft.Tamil.Grammar.Parser;
using Tamil.Grammar.Prosody.Parser;

namespace Tamil.Parody.Parser.Web.Controllers
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
    }
}