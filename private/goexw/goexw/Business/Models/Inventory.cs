using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Business.Models
{
    public class Inventory
    {
        ProductItem Item { get; set; }

        public string IspId { get; set; }

        public string WarehouseId { get; set; }

        public long AvailableQuantity { get; set; }
    }
}