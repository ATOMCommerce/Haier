using Goexw.Config;
using Goexw.Helper;
using Goexw.Models;
using MsStore.Mfl.Core.Models;
using MsStore.Mfl.Core.Models.Request;
using MsStore.Mfl.Core.Models.Response;
using MsStore.Mfl.Core.Utilities;
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
            var fspid = "FSPHttpConnectorV1-2";
            var ispid = "ISPHttpConnectorV1-2";

            var supplierId = model.CustPrice == 0 ? "Midea" : "HaierAgent";
            if (model.CustPrice != 0)
            {
                price = Math.Round((model.CustPrice / line.Quantity), 2);
                fspid = "FSPHttpConnectorV1-1";
                ispid = "ISPHttpConnectorV1-1";
            }

            using (var reader = new StreamReader(queryStream))
            {
                var xmlTemplate = reader.ReadToEnd();
                var postXml = xmlTemplate.Replace("[[CUST]]", SystemConfig.DefaultCust)
                    .Replace("[[SOID]]", Guid.NewGuid().ToString())
                    .Replace("[[SKU]]", line.Id)
                    .Replace("[[SUP]]", supplierId)
                    .Replace("[[PRICE]]", price.ToString())
                    .Replace("[[QTY]]", line.Quantity.ToString())
                    .Replace("[[ESOID]]", Guid.NewGuid().ToString())
                    .Replace("[[FSP]]", fspid)
                    .Replace("[[ISP]]", ispid);
                var requestModel = XmlUtility.Deserialize<SalesOrderRequestModel>(postXml);
                var item = requestModel.SalesOrder.ItemLines[0];
                for (int i = 1; i < model.Lines.Count; i++)
                {
                    var frontLine = model.Lines[i];
                    ItemLine newLine = XmlUtility.Deserialize<ItemLine>(XmlUtility.Serialize<ItemLine>(item));
                    newLine.Item.ProductId = frontLine.Id;
                    newLine.Quantity = frontLine.Quantity;
                    newLine.Item.OfferPrice = frontLine.Price;
                    newLine.TotalAmount = model.CustPrice == 0 ? line.Price * line.Quantity : model.CustPrice;
                    requestModel.SalesOrder.ItemLines.Add(newLine);
                }

                postXml = XmlUtility.Serialize<SalesOrderRequestModel>(requestModel);
                var response = AtomCommerceProxy.PostXmlData(SystemConfig.AtomComRoot + "salesorder", postXml);
                var responseModel = XmlUtility.Deserialize<SalesOrderResponseModel>(response);
                if (!responseModel.HasError)
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    return RedirectToAction("Error", new { ex = responseModel.ErrorCode });
                }
            }
        }

        public ActionResult Error()
        {
            var x = RouteData.Values["ex"];
            return View(x);
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
