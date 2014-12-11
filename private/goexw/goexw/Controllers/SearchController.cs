﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Goexw.Models;
using System;
using System.Web.Mvc;
using Goexw.ViewModels;
using Mock.MsStore.Mfl.Core.Enumeration;
using Mock.MsStore.Mfl.Core.Models;
using Mock.MsStore.Mfl.Core.Models.Request;
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
            //MockDataProvider.GetCatalogReponse();
            var responseBody = String.Empty;


            var request = (HttpWebRequest)WebRequest.Create("http://msstorebuddy.chinacloudapp.cn/api/v1/catalog");
            request.Method = "POST";
            request.ContentType = "application/json";

            var bodyString = JsonConvert.SerializeObject(new
            {
                ActionCode = "QueryByKeyword",
                ChannelId = "Haier",
                SupplierId = "Gree",
                SearchCategory = "",
                SearchKeyword = keyword
            });
            var encoded = Encoding.UTF8.GetBytes(bodyString);

            var body = request.GetRequestStream();
            body.Write(encoded, 0, encoded.Length);
            body.Close();

            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            using (var reader = new StreamReader(dataStream, Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<CatalogResponseModel>(responseBody);

            var list = data.CatalogItems.ToList();
            int pageNo = page.GetValueOrDefault(1);


            var offset = (pageNo - 1)*QueryCatalogReportViewModel.ItemCountPerPage;
            var vm = new QueryCatalogReportViewModel
            {
                CatalogItems = list.GetRange(
                    offset,
                    Math.Min( offset + QueryCatalogReportViewModel.ItemCountPerPage , list.Count) ),
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
            var categories = new List<SearchCategoryItemViewModel>();

            foreach (var item in MockDataProvider.GetProductCategories())
            {
                categories.Add(new SearchCategoryItemViewModel
                {
                    Id = item.id,
                    Name = item.text,
                    IsSubItem = false
                });

                if (item.children == null) continue;

                categories.AddRange(
                    from subitem
                    in item.children
                    select new SearchCategoryItemViewModel
                    {
                        Id = subitem.id,
                        Name = subitem.text,
                        IsSubItem = true
                    });
            }


            var shipmethods = (
                from i in MockDataProvider.GetShipMethods()
                select new SelectListItem { Text = i.Name, Value = i.Code.ToString() }).ToList();


            var model = new SearchPartialViewModel(
                categories,
                shipmethods,
                category.GetValueOrDefault(),
                keyword,
                price.GetValueOrDefault(),
                shipmethod.GetValueOrDefault());


            return PartialView(model);
        }
    }
}

