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
            ViewBag.Message = "An introduction to MenYappu";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About MenYappu";

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
    }
}
