using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Mock.MsStore.Mfl.Core.Models;
using MsStore.Mfl.Core.Enumeration;

namespace Goexw.ViewModels
{
    public class QueryParameter
    {
        public int Category { get; set; }
        public String Keyword { get; set; }
        public int Price { get; set; }
        public int Shipmethod { get; set; }
        public int Page { get; set; }
    }

    public class QueryCatalogViewModel
    {
        public List<CatalogItem> CatalogItems { get; set; }
        public int Page { get; set; }
        public bool HasMoreItem { get; set; }
        public QueryParameter Parameters { get; set; }


        public static readonly int ItemCountPerPage = 10;
    }
}