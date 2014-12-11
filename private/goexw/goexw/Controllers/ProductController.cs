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

        public ActionResult Detail(string id, string keyword)
        {

            var catalogs = QueryCatalogReportViewModel.QueryCatalogByKeyword(keyword);
            var catalog = catalogs.FirstOrDefault(i => i.Sku == id);

            ProductDetailViewModel vm;
            if (catalog == null)
            {
                vm = new ProductDetailViewModel
                {
                    Sku = "<sku:No such product>",
                    UnitPrice = 9999999
                };
            }
            else
            {
                vm = new ProductDetailViewModel
                {
                    Sku = id,
                    UnitPrice = catalog.UnitPrice
                };
            }
            

            return View(vm);
        }
    }
}