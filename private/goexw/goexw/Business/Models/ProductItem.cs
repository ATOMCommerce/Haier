using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Business.Models
{
    public class ProductItem
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Variant { get; set; }

        public string Name { get; set; }

        public string OfferId { get; set; }

        public string SupplierId { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Tag { get; set; }

        public string ProductType { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}