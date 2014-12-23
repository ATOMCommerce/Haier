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
            var requestModelList = BuildSalesOrderRequestList(model);
            foreach (var requestModel in requestModelList)
            {
                var postXml = XmlUtility.Serialize<SalesOrderRequestModel>(requestModel);
                var response = AtomCommerceProxy.PostXmlData(SystemConfig.AtomComRoot + "salesorder", postXml);
                var responseModel = XmlUtility.Deserialize<SalesOrderResponseModel>(response);
                if (responseModel.HasError)
                {
                    return RedirectToAction("Error", new { ex = responseModel.ErrorCode });
                }
            }

            return RedirectToAction("Index", "Order");
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

        private List<SalesOrderRequestModel> BuildSalesOrderRequestList(CartModel cart)
        {
            var normalLines = cart.Lines.Where(i => i.SaleType == "Sample").ToList();
            var custLines = cart.Lines.Where(i => i.SaleType != "Sample").ToList();

            var orderRequestList = new List<SalesOrderRequestModel>();
            if (normalLines.Count > 0)
            {
                orderRequestList.Add(BuildOrderRequestModel(normalLines));
            }

            foreach (var line in custLines)
            {
                var lineModel = new List<CartLineModel>() { line };
                orderRequestList.Add(BuildOrderRequestModel(lineModel));
            }

            return orderRequestList;
        }

        private SalesOrderRequestModel BuildOrderRequestModel(List<CartLineModel> lineModels)
        {
            var salesOrderRequest = LoadRequestModel();
            var lineTemplate = salesOrderRequest.SalesOrder.ItemLines[0];
            salesOrderRequest.SalesOrder.ItemLines.Clear();
            salesOrderRequest.SalesOrder.AuthUserId = SystemConfig.DefaultCust;
            salesOrderRequest.SalesOrder.SalesOrderId = Guid.NewGuid().ToString();
            salesOrderRequest.SalesOrder.ExternalSalesOrderId = Guid.NewGuid().ToString();

            foreach (var line in lineModels)
            {
                ItemLine newLine = XmlUtility.Deserialize<ItemLine>(XmlUtility.Serialize<ItemLine>(lineTemplate));
                newLine.ExternalItemLineId = Guid.NewGuid().ToString();
                newLine.Item.ProductId = line.Id;
                newLine.Quantity = line.Quantity;
                var totalPrice = line.Quantity * line.UnitPrice;
                newLine.TotalAmount = line.SaleType == "WholeSale" ? line.CustomPrice : totalPrice;
                newLine.Item.SupplierId = GetSupplierId(line.SaleType);
                newLine.Item.FSPId = GetFSPId(line.SaleType);
                newLine.Item.ISPId = GetISPId(line.SaleType);
                newLine.Item.OfferPrice = line.UnitPrice;
                salesOrderRequest.SalesOrder.ItemLines.Add(newLine);
            }

            salesOrderRequest.SalesOrder.TotalAmount = salesOrderRequest.SalesOrder.ItemLines.Sum(i => i.TotalAmount);
            return salesOrderRequest;
        }


        private SalesOrderRequestModel LoadRequestModel()
        {
            var queryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Goexw.Business.Content.Order.txt");
            SalesOrderRequestModel requestModel = null;

            using (var reader = new StreamReader(queryStream))
            {
                var xmlTemplate = reader.ReadToEnd();
                requestModel = XmlUtility.Deserialize<SalesOrderRequestModel>(xmlTemplate);
            }

            return requestModel;
        }

        private string GetFSPId(string saleType)
        {
            return saleType == "Sample" ? "FSPHttpConnectorV1-2" : "FSPHttpConnectorV1-1";
        }

        private string GetISPId(string saleType)
        {
            return saleType == "Sample" ? "ISPHttpConnectorV1-2" : "ISPHttpConnectorV1-1";
        }

        private string GetSupplierId(string saleType)
        {
            return saleType == "Sample" ? "Midea" : "HaierAgent";
        }
    }
}
