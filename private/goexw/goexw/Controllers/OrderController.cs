using Goexw.Business.Models;
using Goexw.Config;
using Goexw.Helper;
using Goexw.Models;
using MsStore.Mfl.Core.Models.Response;
using MsStore.Mfl.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            var orders = new List<Order>();
            var model = new CartModel();
            var queryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Goexw.Business.Content.Query.txt");
            using (var reader = new StreamReader(queryStream))
            {
                var xmlTemplate = reader.ReadToEnd();
                var postXml = xmlTemplate.Replace("[[CUST]]", SystemConfig.DefaultCust);
                var response = AtomCommerceProxy.PostXmlData(SystemConfig.AtomComRoot + "salesorder", postXml);
                var responseModel = XmlUtility.Deserialize<SalesOrderResponseModel>(response);
                foreach (var so in responseModel.SalesOrders)
                {
                    orders.Add(ModelConvert.ConvertFromAtomOrder(so));
                }
            }
            return View(orders);
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