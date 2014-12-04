using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Business.Models
{
    public class SalesLine
    {
        public int Id { get; set; }

        public ProductItem Item { get; set; }

        public string Isp { get; set; }

        public string WarehouseId { get; set; }

        public string Fsp { get; set; }

        public string DeliveryInfo { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}