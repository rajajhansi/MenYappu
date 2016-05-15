using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Tamil.Grammar.Prosody.Parser.WebUI.Controllers
{
    public class ProsodyHelpController : Controller
    {
        public ActionResult Letter()
        {
            return View();
        }
        public ActionResult Metreme()
        {
            return View();
        }

        public ActionResult MetricalFeet()
        {
            return View();
        }

        public ActionResult Linkage()
        {
            return View();
        }

        public ActionResult MetricalLine()
        {
            return View();
        }

        public ActionResult Ornamentation()
        {
            return View();
        }

        public ActionResult Sound()
        {
            return View();
        }
    }
}
