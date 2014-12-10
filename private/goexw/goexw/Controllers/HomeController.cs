using Goexw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var promos = MockDataProvider.GetPromotions();
            return View(promos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.NoSearchBar = true;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.NoSearchBar = true;

            return View();
        }
    }
}