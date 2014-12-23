using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Models
{
    public class CartModel
    {
        public string UserId { get; set; }

        public List<CartLineModel> Lines { get; set; }

        public decimal CustPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class CartLineModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal CustomPrice { get; set; }

        public string SaleType { get; set; }

        public int Quantity { get; set; }
    }
}