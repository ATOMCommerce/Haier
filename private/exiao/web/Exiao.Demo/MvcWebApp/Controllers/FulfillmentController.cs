// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FulfillmentController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the FulfillmentController type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EntityFramework.Extensions;

    using Exiao.Demo.DataAccess;
    using Exiao.Demo.Utilities;

    using Newtonsoft.Json;

    using PagedList;

    /// <summary>
    /// Defines the FulfillmentController type.
    /// </summary>
    [AuthorizeExtended]
    public class FulfillmentController : Controller
    {
        #region Fields

        /// <summary>
        /// The page size
        /// </summary>
        private const int PageSize = 10;

        #endregion

        #region Methods

        /// <summary>
        /// The index action.
        /// </summary>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="page">The page.</param>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/fulfillment?sortOrder={sortOrder}&currentFilter={currentFilter}&searchString={searchString}&page={page}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.OrderIdSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "order_id_desc" : string.Empty;
            ViewBag.SupplierSortParam = sortOrder == "supplier" ? "supplier_desc" : "supplier";
            ViewBag.ExternalOrderIdSortParam = sortOrder == "external_order_id" ? "external_order_id_desc" : "external_order_id";
            ViewBag.StatusSortParam = sortOrder == "status" ? "status_desc" : "status";
            ViewBag.CreationDateSortParam = sortOrder == "creation_date" ? "creation_date_desc" : "creation_date";
            ViewBag.ModifiedDateSortParam = sortOrder == "modified_date" ? "modified_date_desc" : "modified_date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            using (var db = new FspDbContext())
            {
                IQueryable<FulfillmentOrderEntry> fulfillmentOrders;

                if (string.IsNullOrWhiteSpace(searchString))
                {
                    fulfillmentOrders = db.FulfillmentOrderEntries.Where(fo => supplierIds.Contains(fo.SupplierId));
                }
                else
                {
                    fulfillmentOrders =
                        db.FulfillmentOrderEntries.Where(
                            fo =>
                            supplierIds.Contains(fo.SupplierId)
                            && (fo.BackendFulfillmentOrderId.Contains(searchString)
                                || fo.MflFulfillmentOrderId.Contains(searchString)
                                || fo.ItemLinesData.Contains(searchString)));
                }

                switch (sortOrder)
                {
                    case "order_id_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.BackendFulfillmentOrderId);
                        break;
                    case "supplier":
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.SupplierId);
                        break;
                    case "supplier_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.SupplierId);
                        break;
                    case "external_order_id":
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.MflFulfillmentOrderId);
                        break;
                    case "external_order_id_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.MflFulfillmentOrderId);
                        break;
                    case "status":
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.Status);
                        break;
                    case "status_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.Status);
                        break;
                    case "creation_date":
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.CreationTimeStamp);
                        break;
                    case "creation_date_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.CreationTimeStamp);
                        break;
                    case "modified_date":
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.LastModifiedTimeStamp);
                        break;
                    case "modified_date_desc":
                        fulfillmentOrders = fulfillmentOrders.OrderByDescending(c => c.LastModifiedTimeStamp);
                        break;
                    default:
                        fulfillmentOrders = fulfillmentOrders.OrderBy(c => c.BackendFulfillmentOrderId);
                        break;
                }

                var pagedResult = fulfillmentOrders.ToPagedList(page ?? 1, PageSize);
                return this.View(pagedResult);
            }
        }

        /// <summary>
        /// The edit action (get).
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The edit view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/fulfillment/edit?orderId={supplierId}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Edit(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new FspDbContext())
            {
                var fulfillmentOrder =
                    db.FulfillmentOrderEntries.FirstOrDefault(fo => fo.BackendFulfillmentOrderId.Equals(orderId));

                if (fulfillmentOrder == null)
                {
                    return this.HttpNotFound();
                }

                var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                if (!supplierIds.Contains(fulfillmentOrder.SupplierId))
                {
                    return
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    controller = "Home",
                                    action = "ShowError",
                                    errorMessage =
                                            "Insufficient privileges to edit fulfillment order for supplier: "
                                            + fulfillmentOrder.SupplierId,
                                }));
                }

                ViewBag.StatusList =
                    CultureHelper.AllFoStatus.TakeWhile(kvp => !kvp.Key.Equals(fulfillmentOrder.Status)).Reverse();

                return this.View(fulfillmentOrder);
            }
        }

        /// <summary>
        /// The edit action (post).
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <returns>The details action.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/fulfillment/edit 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"BackendFulfillmentOrderId":"{BackendFulfillmentOrderId}","Status":"{Status}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "BackendFulfillmentOrderId,Status")] FulfillmentOrderEntry fulfillmentOrder)
        {
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.UtcNow;
                using (var db = new FspDbContext())
                {
                    var existingEntity =
                        db.FulfillmentOrderEntries.FirstOrDefault(
                            fo => fo.BackendFulfillmentOrderId.Equals(fulfillmentOrder.BackendFulfillmentOrderId));

                    if (existingEntity == null)
                    {
                        return this.HttpNotFound();
                    }

                    var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                    if (!supplierIds.Contains(existingEntity.SupplierId))
                    {
                        return
                            new RedirectToRouteResult(
                                new RouteValueDictionary(
                                    new
                                        {
                                            controller = "Home",
                                            action = "ShowError",
                                            errorMessage =
                                                "Insufficient privileges to edit fulfillment order for supplier: "
                                                + existingEntity.SupplierId,
                                        }));
                    }

                    db.FulfillmentOrderEntries.Where(
                        fo => fo.BackendFulfillmentOrderId.Equals(fulfillmentOrder.BackendFulfillmentOrderId))
                        .Update(
                            c =>
                            new FulfillmentOrderEntry
                                {
                                    Status = fulfillmentOrder.Status,
                                    LastModifiedTime = dateTimeUtcNow,
                                    LastModifiedTimeStamp = dateTimeUtcNow.Ticks,
                                });

                    var messages = this.GenerateMessages(existingEntity, fulfillmentOrder.Status);

                    db.FulfillmentMessageEntries.AddRange(messages);

                    db.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }

            return this.View(fulfillmentOrder);
        }

        /// <summary>
        /// The details action.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/fulfillment/details?orderId={orderId}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Details(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new FspDbContext())
            {
                var fulfillmentOrder = db.FulfillmentOrderEntries.FirstOrDefault(fo => fo.BackendFulfillmentOrderId.Equals(orderId));

                if (fulfillmentOrder == null)
                {
                    return this.HttpNotFound();
                }

                var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                if (!supplierIds.Contains(fulfillmentOrder.SupplierId))
                {
                    return
                       new RedirectToRouteResult(
                           new RouteValueDictionary(
                               new
                               {
                                   controller = "Home",
                                   action = "ShowError",
                                   errorMessage = "Insufficient privileges to view fulfillment order for supplier: " + fulfillmentOrder.SupplierId,
                               }));
                }

                return this.View(fulfillmentOrder);
            }
        }

        /// <summary>
        /// Generates the common messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <param name="status">The status.</param>
        /// <returns>The fulfillment message entries.</returns>
        private static IEnumerable<FulfillmentMessageEntry> GenerateCommonMessages(FulfillmentOrderEntry fulfillmentOrder, string status)
        {
            var utcDateTime = DateTime.UtcNow;

            var itemLines = JsonConvert.DeserializeObject<dynamic[]>(fulfillmentOrder.ItemLinesData);

            foreach (var itemLine in itemLines)
            {
                itemLine.Status = status;
            }

            return new[]
                       {
                           new FulfillmentMessageEntry
                               {
                                   ReferenceGuid = Guid.NewGuid().ToString("D"),
                                   CorrelationId = fulfillmentOrder.BackendFulfillmentOrderId,
                                   MessageType = status,
                                   IsCommitted = false,
                                   CalculationTime = utcDateTime,
                                   CalculationTimeStamp = utcDateTime.Ticks,
                                   CreationTime = utcDateTime,
                                   CreationTimeStamp = utcDateTime.Ticks,
                                   LastModifiedTime = utcDateTime,
                                   LastModifiedTimeStamp = utcDateTime.Ticks,
                                   Message =
                                       JsonConvert.SerializeObject(
                                           new[]
                                               {
                                                   new
                                                       {
                                                           fulfillmentOrder.SupplierId,
                                                           fulfillmentOrder.ChannelId,
                                                           fulfillmentOrder.MflFulfillmentOrderId,
                                                           CorrelationId =
                                                       fulfillmentOrder.BackendFulfillmentOrderId,
                                                           Status = status,
                                                           DeliveryAddress =
                                                       fulfillmentOrder.DeliveryAddressData,
                                                           ItemLines = itemLines,
                                                           fulfillmentOrder.ExtraData,
                                                       }
                                               })
                               }
                       };
        }

        /// <summary>
        /// Generates the messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <param name="status">The status.</param>
        /// <returns>The fulfillment message entries.</returns>
        private IEnumerable<FulfillmentMessageEntry> GenerateMessages(FulfillmentOrderEntry fulfillmentOrder, string status)
        {
            switch (status)
            {
                case "OrderCreated":
                    return this.GenerateOrderCreatedMessages(fulfillmentOrder);
                case "InventoryConfirmed":
                    return this.GenerateInventoryConfirmedMessages(fulfillmentOrder);
                case "Shipped":
                    return this.GenerateShippedMessages(fulfillmentOrder);
                case "Delivered":
                    return this.GenerateDeliveredMessages(fulfillmentOrder);
                default:
                    return new FulfillmentMessageEntry[0];
            }
        }

        /// <summary>
        /// Generates the delivered messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <returns>The fulfillment message entries.</returns>
        private IEnumerable<FulfillmentMessageEntry> GenerateDeliveredMessages(FulfillmentOrderEntry fulfillmentOrder)
        {
            return GenerateCommonMessages(fulfillmentOrder, "Delivered");
        }

        /// <summary>
        /// Generates the shipped messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <returns>The fulfillment message entries.</returns>
        private IEnumerable<FulfillmentMessageEntry> GenerateShippedMessages(FulfillmentOrderEntry fulfillmentOrder)
        {
            return GenerateCommonMessages(fulfillmentOrder, "Shipped");
        }

        /// <summary>
        /// Generates the inventory confirmed messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <returns>The fulfillment message entries.</returns>
        private IEnumerable<FulfillmentMessageEntry> GenerateInventoryConfirmedMessages(FulfillmentOrderEntry fulfillmentOrder)
        {
            using (var db = new IspDbContext())
            {
                var utcDateTime = DateTime.UtcNow;

                db.InventoryWorkItemEntries.AddRange(
                    JsonConvert.DeserializeObject<dynamic[]>(fulfillmentOrder.ItemLinesData).Select(
                        itemLine =>
                            {
                                var tagsArray = new[]
                                                    {
                                                        fulfillmentOrder.BackendFulfillmentOrderId,
                                                        fulfillmentOrder.MflFulfillmentOrderId,
                                                        (string)itemLine.CorrelationId
                                                    };

                                return new InventoryWorkItemEntry
                                           {
                                               Type = "InventoryConfirmed",
                                               ReferenceGuid = Guid.NewGuid().ToString("D"),
                                               SupplierId = itemLine.SupplierId,
                                               ProductSku = itemLine.ProductSKU,
                                               OfferId = itemLine.OfferId,
                                               VariantId = itemLine.VariantId,
                                               WarehouseId = itemLine.WarehouseId,
                                               Quantity = itemLine.Quantity,
                                               IsProcessed = false,
                                               CalculationTime = utcDateTime,
                                               CalculationTimeStamp = utcDateTime.Ticks,
                                               CreationTime = utcDateTime,
                                               CreationTimeStamp = utcDateTime.Ticks,
                                               LastModifiedTime = utcDateTime,
                                               LastModifiedTimeStamp = utcDateTime.Ticks,
                                               Tags = string.Join(",", tagsArray),
                                           };
                            }));
                db.SaveChanges();
            }

            return GenerateCommonMessages(fulfillmentOrder, "InventoryConfirmed");
        }

        /// <summary>
        /// Generates the order created messages.
        /// </summary>
        /// <param name="fulfillmentOrder">The fulfillment order.</param>
        /// <returns>The fulfillment message entries.</returns>
        private IEnumerable<FulfillmentMessageEntry> GenerateOrderCreatedMessages(FulfillmentOrderEntry fulfillmentOrder)
        {
            return GenerateCommonMessages(fulfillmentOrder, "OrderCreated");
        }

        #endregion
    }
}