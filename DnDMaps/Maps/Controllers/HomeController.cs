using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maps.Controllers
{

    //default controller for root requests...
    public class HomeController : Controller
    {
        //index page hit, redirect to wherever we want to land by default
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Map");
        }

    }
}
