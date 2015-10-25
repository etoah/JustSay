using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustSay.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A404()
        {
            return View("404");
        }

        public ActionResult Denied()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

    }
}
