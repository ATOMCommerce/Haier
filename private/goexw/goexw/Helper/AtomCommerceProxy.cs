using Goexw.Config;
using MsStore.Mfl.Core.Models.Request;
using MsStore.Mfl.Core.Models.Response;
using MsStore.Mfl.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Goexw.Helper
{
    public class AtomCommerceProxy
    {
        public static SalesOrderResponseModel ProcessSalesOrderRequest(SalesOrderRequestModel request)
        {
            var postdata = XmlUtility.Serialize<SalesOrderRequestModel>(requestModel);
            var url = AtomComConfig.AtomComRoot + "salesorder";
            var response = HttpReqeustHelper.PostData(url, postdata);
            if (!string.IsNullOrEmpty(response))
            {
                return XMLHelper.Deserialize<SalesOrderResponseModel>(response);
            }

            return null;
        }

        public static InventoryResponseModel ProcessInventoryRequest(InventoryRequestModel request)
        {
            var postdata = XmlUtility.Serialize<InventoryRequestModel>(requestModel);
            var url = AtomComConfig.AtomComRoot + "inventory";
            var response = HttpReqeustHelper.PostData(url, postdata);
            if (!string.IsNullOrEmpty(response))
            {
                return XMLHelper.Deserialize<InventoryResponseModel>(response);
            }

            return null;
        }

        private static string PostXmlData(string url, string data)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                var content = new StringContent(postdata, System.Text.Encoding.UTF8, "text/xml");
                response = client.PostAsync(uri, content).Result;
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //@@ TODO With Retry
                //@@ TODO With Log
                return string.Empty;
            }
        }
    }
}