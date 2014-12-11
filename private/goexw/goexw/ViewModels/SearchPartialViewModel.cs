using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goexw.Models;

namespace Goexw.ViewModels
{
    public class SearchCategoryItemViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool IsSubItem { get; set; }
    }

    public class SearchPartialViewModel
    {
        public IEnumerable<SearchCategoryItemViewModel> Categories { get; private set; }
        public IEnumerable<SelectListItem> ShipMethods { get; private set; }
        public int Category { get; set; }
        public String Keyword { get; set; }
        public int Price { get; set; }
        public int Shipmethod { get; set; }

        public SearchPartialViewModel(
            IEnumerable<SearchCategoryItemViewModel> categories,
            IEnumerable<SelectListItem> methods,
            int category, 
            String keyword,
            int price,
            int shipmethod )
        {
            Categories = categories;
            ShipMethods = methods;

            Category = category;
            Keyword = keyword;
            Price = price;
            Shipmethod = shipmethod;
        }
    }
}