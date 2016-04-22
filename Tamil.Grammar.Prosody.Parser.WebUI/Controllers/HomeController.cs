using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamil.Parody.Parser.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Venpas()
        {
            ViewBag.Message = "வெண்பாக்கள்";

            return View();
        }
        public ActionResult VenpaConversations()
        {
            ViewBag.Message = "Venpa Conversations";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

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
