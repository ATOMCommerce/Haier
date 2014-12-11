using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllOrders()
        {
            return View();
        }

        public ActionResult CompletedOrders()
        {
            return View();
        }

        public ActionResult InProgressOrders()
        {
            return View();
        }

        public ActionResult CanceledOrders()
        {
            return View();
        }

        public ActionResult OrderDetail()
        {
            return View();
        }

        public ActionResult CancelOrder()
        {
            return View();
        }
    }
}