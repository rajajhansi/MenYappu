using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamil.Grammar.Prosody.Parser.WebUI.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult LetterTypeFinder()
        {
            ViewBag.Message = "எழுத்து வகைபிரி";

            return View();
        }
        public ActionResult MathiraiCalculator()
        {
            ViewBag.Message = "மாத்திரை எவ்வளவு?";

            return View();
        }

        public ActionResult ThodaiFinder()
        {
            ViewBag.Message = "Thodai Finder";

            return View();
        }

        public ActionResult EthukaiMonaiFinder()
        {
            ViewBag.Message = "எதுகை மோனை எழுது";

            return View();
        }
        public ActionResult ThalaiFinder()
        {
            ViewBag.Message = "தளை தட்டாதே";

            return View();
        }

        public ActionResult AsaiDivider()
        {
            ViewBag.Message = "அசை பிரி";

            return View();
        }

        public ActionResult SeerFinder()
        {
            ViewBag.Message = "சீர் செய்";

            return View();
        }

        public ActionResult AdiMeasurer()
        {
            ViewBag.Message = "அடி அளவிடு";
            return View();
        }
    }
}