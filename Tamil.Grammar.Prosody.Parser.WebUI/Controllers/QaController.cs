using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Tamil.Grammar.Prosody.Parser.WebUI.Controllers
{
    public class QuestionType
    {
        public string ProsodyType { get; set; }
    }
    [RoutePrefix("api/qa")]
    public class QaController : ApiController
    {
        private HttpResponseMessage ReadJsonFile(string fileName)
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath(fileName));

            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [Route("Questions")]
        [HttpPost]
        public HttpResponseMessage GetQuestions(QuestionType questionTypes)
        {
            return ReadJsonFile(@"~/App_Data/qa.json");
        }
    }
}
