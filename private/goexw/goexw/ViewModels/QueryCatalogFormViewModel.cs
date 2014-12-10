using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.ViewModels
{
    public class QueryCatalogFormViewModel
    {
        public int Category { get; set; }
        public String Keyword { get; set; }
        public int Price { get; set; }
        public int Shipmethod { get; set; }
        public int Page { get; set; }
    }

}