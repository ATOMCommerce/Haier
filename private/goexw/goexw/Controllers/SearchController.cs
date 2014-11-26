using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class SearchController : Controller
    {
        //returns a collection of product models
        public ActionResult DoSearch()
        {
            return View();
        }

        public ActionResult ShippingMethods()
        {
            return View();
        }

        public ActionResult AllCatogries()
        {
            return View();
        }

        public ActionResult Promotions()
        {
            return View();
        }
    }
}