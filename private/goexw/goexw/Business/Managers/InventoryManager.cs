using Goexw.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Business.Managers
{
    public class InventoryManager
    {
        public IList<Inventory> CheckInventory(ProductItem item)
        {
            return null;
        }

        public IDictionary<ProductItem, IList<Inventory>> CheckInventory(IList<ProductItem> productItems)
        {
            return null;
        }
    }
}