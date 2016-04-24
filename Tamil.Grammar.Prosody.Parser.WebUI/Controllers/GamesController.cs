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
            ViewBag.Message = "Letter Type Finder";

            return View();
        }
        public ActionResult MathiraiCount()
        {
            ViewBag.Message = "Mathirai Finder";

            return View();
        }

        public ActionResult ThodaiFinder()
        {
            ViewBag.Message = "Thodai Finder";

            return View();
        }

        public ActionResult EthukaiMonaiFinder()
        {
            ViewBag.Message = "Ethukai Monai Finder";

            return View();
        }
        public ActionResult ThalaiFinder()
        {
            ViewBag.Message = "Thalai Finder";

            return View();
        }

        public ActionResult AsaiDivider()
        {
            ViewBag.Message = "Syllable Divider";

            return View();
        }

        public ActionResult SeerFinder()
        {
            ViewBag.Message = "Seer Finder";

            return View();
        }

        public ActionResult AdiMeasurer()
        {
            ViewBag.Message = "Adi Measurer";
            return View();
        }
    }
}