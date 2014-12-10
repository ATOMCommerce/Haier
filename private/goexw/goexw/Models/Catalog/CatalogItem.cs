using System;
using MsStore.Mfl.Core.Models;

namespace Mock.MsStore.Mfl.Core.Models
{
    public class CatalogItem : EntityModelBase
    {
        public string Sku { get; set; }

        public string Variant { get; set; }

        public string SupplierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }

        public string ProductCategoryIdList { get; set; }

        public string SupportedCategoryIdList { get; set; }

        public string Unit { get; set; }

        public double UnitPrice { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public long DataVersion { get; set; }
    }
}