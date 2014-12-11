using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Mock.MsStore.Mfl.Core.Models;
using Mock.MsStore.Mfl.Core.Models.Request;
using Mock.MsStore.Mfl.Core.Models.Response;
using MsStore.Mfl.Core.Enumeration;
using Newtonsoft.Json;
using CatalogAction = Mock.MsStore.Mfl.Core.Enumeration.CatalogAction;

namespace Goexw.ViewModels
{

    public class QueryCatalogReportViewModel
    {
        public List<CatalogItem> CatalogItems { get; set; }
        public int Page { get; set; }
        public bool HasMoreItem { get; set; }
        public QueryCatalogFormViewModel Parameters { get; set; }


        public static readonly int ItemCountPerPage = 10;


        public static List<CatalogItem> QueryCatalogByKeyword(String keyword)
        {

            var request = (HttpWebRequest)WebRequest.Create("http://msstorebuddy.chinacloudapp.cn/api/v1/catalog");
            request.Method = "POST";
            request.ContentType = "application/json";

            var bodyString = JsonConvert.SerializeObject(new
            {
                ActionCode = "QueryByKeyword",
                ChannelId = "Haier",
                SupplierId = "Midea",
                SearchCategory = "",
                SearchKeyword = String.IsNullOrEmpty(keyword)? " ": keyword
            });
            var encoded = Encoding.UTF8.GetBytes(bodyString);

            var body = request.GetRequestStream();
            body.Write(encoded, 0, encoded.Length);
            body.Close();

            try
            {
                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                using (var reader = new StreamReader(dataStream, Encoding.UTF8))
                {
                    var responseBody = reader.ReadToEnd();
                    var desBody = JsonConvert.DeserializeObject<CatalogResponseModel>(responseBody);
                    return desBody.CatalogItems.ToList();
                }
            }
            catch (Exception e)
            {
                return new List<CatalogItem>();
            }



        }
    }
}