using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goexw.ViewModels;

namespace Goexw.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Detail(string id)
        {
            var vm = new ProductDetailViewModel
            {
                Sku = id
            };
            return View(vm);
        }
    }
}