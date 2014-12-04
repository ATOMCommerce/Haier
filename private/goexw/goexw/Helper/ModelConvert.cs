using Goexw.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AtomItemLine = MsStore.Mfl.Core.Models.ItemLine;
using AtomItem = MsStore.Mfl.Core.Models.Item;
using AtomOrder = MsStore.Mfl.Core.Models.SalesOrder;
using AtomPayment = MsStore.Mfl.Core.Models.Payment;
using AtomDelivery =  MsStore.Mfl.Core.Models.Delivery;

namespace Goexw.Helper
{
    public class ModelConvert
    {
        public static AtomOrder ConvertToAtomOrder(Order order)
        {
            Random rnd = new Random();
            AtomOrder so = new AtomOrder();
            so.SalesOrderId = order.OrderId;
            so.Delivery = ConvertToAtomDelivery(order.Delivery);
            foreach (var sl in order.SalesLines)
            {
                AtomItemLine line = new AtomItemLine();
                line.ExternalItemLineId = Guid.NewGuid().ToString();
                line.Quantity = sl.Quantity;
                //use payment memo to work around this
                line.Payments.Add(new AtomPayment { CurrencyAmount = sl.Item.Price });
                line.Item = new AtomItem();
                line.Item.SupplierId = sl.Item.SupplierId;
                line.Item.ProductId = sl.Item.Sku;
                line.Item.VariantId = sl.Item.Variant;
                line.Item.ISPId = sl.Isp;
                line.Item.Name = sl.Item.Name;
                line.Item.OfferId = sl.Item.OfferId;
                line.Item.FSPId = sl.Fsp;
                line.Item.WarehouseId = sl.WarehouseId;
                line.Item.SetFrontId(sl.Item.Id);
                
                /// TODO use version to workaround.
                /// @@TODO need use optional fields
                so.ItemLines.Add(line);
            }

            return so;
        }

        public static Order ConvertFromAtomOrder(AtomOrder input)
        {
            Order output = new Order();
            output.OrderId = input.SalesOrderId;
            var idList = input.ItemLines.Select(i => i.Version).ToList();
            var items = ProductItemLocator.FindItems(idList);

            foreach (var sl in input.ItemLines)
            {
                SalesLine line = new SalesLine();
                line.Quantity = (int)sl.Quantity;
                line.Isp = sl.Item.ISPId;
                line.Fsp = sl.Item.FSPId;
                //@@TODO need use optional fields
                line.Item = ProductItemLocator.FindItem(sl.Item.GetFrontId());
                output.SalesLines.Add(line);
            }

            return output;
        }

        private static AtomDelivery ConvertToAtomDelivery(Delivery deliveryInfo)
        {
            var delivery = new MsStore.Mfl.Core.Models.Delivery();
            var address = new MsStore.Mfl.Core.Models.Address();
            address.AddressType = MsStore.Mfl.Core.Enumeration.AddressType.Shipping;
            address.Country = "China";
            address.State = "Beijing";
            address.City = "Beijing";
            address.District = "Haidian";
            address.Street = "BJW";
            address.FirstName = "T";
            address.Fullname = "HAHA";
            address.MobilePhone = "11011011110";
            address.EmailAddress = "xcv@msss.com";
            delivery.Addresses.Add(address);
            return delivery;
        }
    }
}