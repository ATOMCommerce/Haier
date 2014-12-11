using Goexw.Config;
using Goexw.Helper;
using Goexw.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class CartController : Controller
    {
        [HttpPost]
        public ActionResult View()
        {
            var jsonStr = Request.Form["commitOrder"];
            var model = JsonConvert.DeserializeObject<CartModel>(jsonStr);
            var queryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Goexw.Business.Content.Order.txt");
            var line = model.Lines[0];
            var price = line.Price;

            var supplierId =  model.CustPrice ==0? "Midea" : "HaierAgent";
            if(model.CustPrice != 0)
            {
                price = Math.Round((model.CustPrice / line.Quantity),2);
            }

            using (var reader = new StreamReader(queryStream))
            {
                var xmlTemplate = reader.ReadToEnd();
                var postXml = xmlTemplate.Replace("[[CUST]]", model.CustomerID)
                    .Replace("[[SOID]]", Guid.NewGuid().ToString())
                    .Replace("[[SKU]]", line.Id)
                    .Replace("[[SUP]]", supplierId)
                    .Replace("[[PRICE]]", price.ToString())
                    .Replace("[[QTY]]", line.Quantity.ToString());
                AtomCommerceProxy.PostXmlData(SystemConfig.AtomComRoot, postXml);
            }

           
            return RedirectToAction("Index", "Order");
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
