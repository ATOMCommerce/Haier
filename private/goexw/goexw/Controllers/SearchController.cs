using System.Collections.Generic;
using System.Linq;
using Goexw.Models;
using System;
using System.Web.Mvc;
using Goexw.ViewModels;
using Mock.MsStore.Mfl.Core.Models;
using Mock.MsStore.Mfl.Core.Models.Response;
using Newtonsoft.Json;

namespace Goexw.Controllers
{
    public class SearchController : Controller
    {

        //Mock Search Model
        public class SearchResult 
        {
            public String Title { get; set; }
            public String Description { get; set; }
        }
        public ActionResult Index(int? category, String keyword, int? price, int? shipmethod, int? page)
        {
            var responseBody = MockDataProvider.GetCatalogReponse();
            var data = JsonConvert.DeserializeObject<CatalogResponseModel>(responseBody);

            var list = data.CatalogItems.ToList();
            int pageNo = page.GetValueOrDefault(1);

            var vm = new QueryCatalogReportViewModel
            {
                CatalogItems = list.GetRange( 
                    (pageNo-1) * QueryCatalogReportViewModel.ItemCountPerPage , 
                    QueryCatalogReportViewModel.ItemCountPerPage),
                Page = pageNo,
                HasMoreItem = pageNo * QueryCatalogReportViewModel.ItemCountPerPage < list.Count,
                Parameters = new QueryCatalogFormViewModel
                {
                    Category = category.GetValueOrDefault(),
                    Keyword = keyword,
                    Price = price.GetValueOrDefault(),
                    Shipmethod = shipmethod.GetValueOrDefault(),
                    Page = page.GetValueOrDefault(1)
                }
            };


            return View(vm);
        }


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
            var mockData = MockDataProvider.GetProductCategories();
            return Json(mockData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Promotions()
        {
            return View();
        }

        public ActionResult SearchPartial(int? category, String keyword, int? price, int? shipmethod)
        {
            var model = new SearchPartialViewModel(
                MockDataProvider.GetProductCategories(),
                MockDataProvider.GetShipMethods(), 
                new QueryCatalogFormViewModel
                {
                    Category = category.GetValueOrDefault(),
                    Keyword = keyword,
                    Price = price.GetValueOrDefault(),
                    Shipmethod = shipmethod.GetValueOrDefault()
                });


            return PartialView(model);
        }
    }
}

