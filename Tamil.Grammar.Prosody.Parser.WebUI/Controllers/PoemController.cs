using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Tamil.Grammar.Prosody.Parser.WebUI.Controllers
{
    public class PoemController : Controller
    {
        // GET: Poem
        public ActionResult Index()
        {
            var tamilpaa = new TamilPaa();
            return View(tamilpaa);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(TamilPaa tamilpaa)
        {
            return View(tamilpaa);
        }
    }
}