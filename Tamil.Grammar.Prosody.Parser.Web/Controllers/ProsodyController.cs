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
    public class ProsodyController : ApiController
    {
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

        [HttpPost]
        public HttpResponseMessage MathiraiCount(string prosodyText)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            if (prosodyText != null && !string.IsNullOrEmpty(prosodyText))
            {
                var prosodyParser = new ProsodyParser(prosodyText);
                var mathiraiCount = prosodyParser.GetMathiraiCount();
                response = Request.CreateResponse(HttpStatusCode.Created, mathiraiCount);
                response.Headers.Location = new Uri(Request.RequestUri,
                                                    string.Format("prosody/{0}", "MathiraiCount"));
            }

            return response;
        }

    }
}