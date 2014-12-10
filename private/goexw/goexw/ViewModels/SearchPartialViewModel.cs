using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Goexw.Models;

namespace Goexw.ViewModels
{
    public class SearchPartialViewModel
    {
        public List<MockProductCategory> ProductCategories { get; private set; }


        public List<MockShipMethod> ShipMethods { get; private set; }



        public SearchPartialViewModel(
            List<MockProductCategory> categories, 
            List<MockShipMethod> methods)
        {
            ProductCategories = categories;
            ShipMethods = methods;
        }
    }
}