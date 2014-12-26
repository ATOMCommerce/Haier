// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the InventoryController type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EntityFramework.Extensions;

    using Exiao.Demo.DataAccess;
    using Exiao.Demo.Utilities;

    using PagedList;

    /// <summary>
    /// Defines the InventoryController type.
    /// </summary>
    [AuthorizeExtended]
    public class InventoryController : Controller
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
        /// GET https://{hostname}/inventory?sortOrder={sortOrder}&currentFilter={currentFilter}&searchString={searchString}&page={page}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SkuSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "sku_desc" : string.Empty;
            ViewBag.SupplierSortParam = sortOrder == "supplier" ? "supplier_desc" : "supplier";
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

            using (var db = new IspDbContext())
            {
                IQueryable<InventoryEntry> inventories;

                if (string.IsNullOrWhiteSpace(searchString))
                {
                    inventories = db.InventoryEntries.Where(i => supplierIds.Contains(i.SupplierId));
                }
                else
                {
                    inventories =
                        db.InventoryEntries.Where(
                            i => supplierIds.Contains(i.SupplierId) && i.ProductSku.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "sku_desc":
                        inventories = inventories.OrderByDescending(c => c.ProductSku);
                        break;
                    case "supplier":
                        inventories = inventories.OrderBy(c => c.SupplierId);
                        break;
                    case "supplier_desc":
                        inventories = inventories.OrderByDescending(c => c.SupplierId);
                        break;
                    case "creation_date":
                        inventories = inventories.OrderBy(c => c.CreationTimeStamp);
                        break;
                    case "creation_date_desc":
                        inventories = inventories.OrderByDescending(c => c.CreationTimeStamp);
                        break;
                    case "modified_date":
                        inventories = inventories.OrderBy(c => c.LastModifiedTimeStamp);
                        break;
                    case "modified_date_desc":
                        inventories = inventories.OrderByDescending(c => c.LastModifiedTimeStamp);
                        break;
                    default:
                        inventories = inventories.OrderBy(c => c.ProductSku);
                        break;
                }

                var pagedResult = inventories.ToPagedList(page ?? 1, PageSize);
                return this.View(pagedResult);
            }
        }

        /// <summary>
        /// The details action.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="productSku">The product sku.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/inventory/details?supplierId={supplierId}&productSku={productSku}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Details(string supplierId, string productSku)
        {
            if (string.IsNullOrWhiteSpace(supplierId) || string.IsNullOrWhiteSpace(productSku))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(supplierId))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to view inventory for supplier: " + supplierId,
                           }));
            }

            using (var db = new IspDbContext())
            {
                var inventory =
                    db.InventoryEntries.FirstOrDefault(
                        i => i.SupplierId.Equals(supplierId) && i.ProductSku.Equals(productSku));

                if (inventory == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(inventory);
            }
        }

        /// <summary>
        /// The edit action (get).
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="productSku">The product sku.</param>
        /// <returns>The edit view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/inventory/edit?supplierId={supplierId}&productSku={productSku}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Edit(string supplierId, string productSku)
        {
            if (string.IsNullOrWhiteSpace(supplierId) || string.IsNullOrWhiteSpace(productSku))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(supplierId))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to edit inventory for supplier: " + supplierId,
                           }));
            }

            using (var db = new IspDbContext())
            {
                var inventory =
                    db.InventoryEntries.FirstOrDefault(
                        i => i.SupplierId.Equals(supplierId) && i.ProductSku.Equals(productSku));

                if (inventory == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(inventory);
            }
        }

        /// <summary>
        /// The edit action (post).
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        /// <returns>The index action.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/inventory/edit 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"SupplierId":"{supplierId}","ProductSku":"{productSku}","IsUnlimited":"{isUnlimited}","AvailableQuantity":"{availableQuantity}","ExtraData":"{extraData}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "SupplierId,ProductSku,IsUnlimited,AvailableQuantity,ExtraData")] InventoryEntry inventory)
        {
            if (ModelState.IsValid)
            {
                var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                if (!supplierIds.Contains(inventory.SupplierId))
                {
                    return
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                    {
                                        controller = "Home",
                                        action = "ShowError",
                                        errorMessage =
                                            "Insufficient privileges to edit inventory for supplier: "
                                            + inventory.SupplierId,
                                    }));
                }

                var dateTimeUtcNow = DateTime.UtcNow;
                using (var db = new IspDbContext())
                {
                    db.InventoryEntries.Where(
                        i => i.SupplierId.Equals(inventory.SupplierId) && i.ProductSku.Equals(inventory.ProductSku))
                        .Update(
                            c =>
                            new InventoryEntry
                                {
                                    IsUnlimited = inventory.IsUnlimited,
                                    AvailableQuantity = inventory.AvailableQuantity,
                                    ExtraData = inventory.ExtraData ?? string.Empty,
                                    LastModifiedTime = dateTimeUtcNow,
                                    LastModifiedTimeStamp = dateTimeUtcNow.Ticks,
                                });
                    db.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }

            return this.View(inventory);
        }

        /// <summary>
        /// The create action (get).
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="productSku">The product sku.</param>
        /// <returns>The create view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/inventory/create?supplierId={supplierId}&productSku={productSku}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Create(string supplierId, string productSku)
        {
            if (string.IsNullOrWhiteSpace(supplierId) || string.IsNullOrWhiteSpace(productSku))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(supplierId))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to create inventory for supplier: " + supplierId,
                           }));
            }

            ViewBag.SupplierId = supplierId;
            ViewBag.ProductSku = productSku;

            return this.View();
        }

        /// <summary>
        /// The create action (post).
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/inventory/edit 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"SupplierId":"{supplierId}","ProductSku":"{productSku}","IsUnlimited":"{isUnlimited}","AvailableQuantity":"{availableQuantity}","ExtraData":"{extraData}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,ProductSku,IsUnlimited,AvailableQuantity,ExtraData")] InventoryEntry inventory)
        {
            if (ModelState.IsValid)
            {
                var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                if (!supplierIds.Contains(inventory.SupplierId))
                {
                    return
                       new RedirectToRouteResult(
                           new RouteValueDictionary(
                               new
                               {
                                   controller = "Home",
                                   action = "ShowError",
                                   errorMessage = "Insufficient privileges to create inventory for supplier: " + inventory.SupplierId,
                               }));
                }

                var dateTimeUtcNow = DateTime.UtcNow;

                using (var db = new IspDbContext())
                {
                    inventory.OfferId = string.Empty;
                    inventory.VariantId = string.Empty;
                    inventory.WarehouseId = string.Empty;
                    inventory.ExtraData = string.Empty;
                    inventory.CreationTime = dateTimeUtcNow;
                    inventory.CreationTimeStamp = dateTimeUtcNow.Ticks;
                    inventory.LastModifiedTime = dateTimeUtcNow;
                    inventory.LastModifiedTimeStamp = dateTimeUtcNow.Ticks;

                    db.InventoryEntries.Add(inventory);
                    db.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }

            return this.View(inventory);
        }

        #endregion
    }
}