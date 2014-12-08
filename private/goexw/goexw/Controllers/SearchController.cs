using Goexw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Goexw.ViewModels;

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
        public ActionResult Index(int category, String keyword, int price, int shipmethod)
        {
            

            var webClient = new WebClient();
            Object res = null;
            using (webClient)
            {
                var json = webClient.DownloadString("http://bingsearchproxy.azurewebsites.net/api/Search?query=" + keyword);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                res = serializer.Deserialize(json, typeof(List<SearchResult>));
            }

            return View(res);
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

        public ActionResult SearchPartial()
        {
            var model = new SearchPartialViewModel(
                MockDataProvider.GetProductCategories(),
                MockDataProvider.GetShipMethods());


            return PartialView(model);
        }
    }
}

