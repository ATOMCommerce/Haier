using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goexw.Controllers
{
    public class CartController : Controller
    {
        public ActionResult AddToCart()
        {
            return View();
        }

        public ActionResult RemoveFromCart()
        {
            return View();
        }

        public ActionResult RemoveProduct()
        {
            return View();
        }

        public ActionResult RemoveService()
        {
            return View();
        }

        public ActionResult UpdateQuantity()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult AddToFavorite()
        {
            return View();
        }
    }
}