using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goexw.Helper;
using MsStore.Mfl.Core.Models.Request;
using MsStore.Mfl.Core.Models;
using MsStore.Mfl.Core.Enumeration;

namespace goexw.Tests.Proxy
{
    [TestClass]
    public class AtomComProxyTest
    {
        [TestMethod]
        public void TestInventoryAction()
        {
            InventoryRequestModel requestModel = new InventoryRequestModel();
            requestModel.ActionCode = InventoryAction.Check;
            requestModel.ChannelId = "BZ";
            Inventory inventory = new Inventory();
            inventory.Item = new Item();
            inventory.Item.ProductId = "product1";
            inventory.Item.OfferId = "product1";
            inventory.Item.SupplierId = "product1";
            inventory.Item.VariantId = "product1";
            inventory.Item.ISPId = "product1";
            inventory.Item.ProductType = inventoryInfo.ProductInfo.ProductType;
            requestModel.Inventories.Add(inventory);

            string xml = XMLHelper.Serialize<InventoryRequestModel>(requestModel);
            Assert.IsNotNull(xml);
            var response = AtomCommerceProxy.ProcessInventoryRequest(requestModel);
        }

        [TestMethod]
        public void TestOrderAction()
        {

        }
    }
}
