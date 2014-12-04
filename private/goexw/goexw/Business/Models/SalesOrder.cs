using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AtomOrder = MsStore.Mfl.Core.Models.SalesOrder;

namespace Goexw.Business.Models
{
    public class Order
    {
        public Order()
        {
            this.SalesLines = new List<SalesLine>();
        }

        public AtomOrder AtomOrder { get; set; }

        public Delivery Delivery { get; set; }

        public List<SalesLine> SalesLines { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderId { get; set; }
    }
}