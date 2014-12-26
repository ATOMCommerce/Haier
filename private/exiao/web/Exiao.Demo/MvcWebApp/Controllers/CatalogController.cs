// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatalogController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the CatalogController type.
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
    /// Defines the CatalogController type.
    /// </summary>
    [AuthorizeExtended]
    public class CatalogController : Controller
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
        /// GET https://{hostname}/catalog?sortOrder={sortOrder}&currentFilter={currentFilter}&searchString={searchString}&page={page}
        /// ]]>
        /// </example>
        [HttpGet]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SkuSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "sku_desc" : string.Empty;
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
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

            using (var db = new CatalogDbContext())
            {
                IQueryable<CatalogItemEntry> catalogItems;

                if (string.IsNullOrWhiteSpace(searchString))
                {
                    catalogItems = db.CatalogItemEntries.Where(c => supplierIds.Contains(c.SupplierId));
                }
                else
                {
                    catalogItems =
                        db.CatalogItemEntries.Where(
                            c =>
                            supplierIds.Contains(c.SupplierId)
                            && (c.Name.Contains(searchString) || c.Sku.Contains(searchString)));
                }

                switch (sortOrder)
                {
                    case "sku_desc":
                        catalogItems = catalogItems.OrderByDescending(c => c.Sku);
                        break;
                    case "name":
                        catalogItems = catalogItems.OrderBy(c => c.Name);
                        break;
                    case "name_desc":
                        catalogItems = catalogItems.OrderByDescending(c => c.Name);
                        break;
                    case "supplier":
                        catalogItems = catalogItems.OrderBy(c => c.SupplierId);
                        break;
                    case "supplier_desc":
                        catalogItems = catalogItems.OrderByDescending(c => c.SupplierId);
                        break;
                    case "creation_date":
                        catalogItems = catalogItems.OrderBy(c => c.CreationTime);
                        break;
                    case "creation_date_desc":
                        catalogItems = catalogItems.OrderByDescending(c => c.CreationTime);
                        break;
                    case "modified_date":
                        catalogItems = catalogItems.OrderBy(c => c.UpdateTime);
                        break;
                    case "modified_date_desc":
                        catalogItems = catalogItems.OrderByDescending(c => c.UpdateTime);
                        break;
                    default:
                        catalogItems = catalogItems.OrderBy(c => c.Sku);
                        break;
                }

                var pagedResult = catalogItems.ToPagedList(page ?? 1, PageSize);
                return this.View(pagedResult);
            }
        }

        /// <summary>
        /// The create action (get).
        /// </summary>
        /// <returns>The create view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/catalog/create
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Suppliers = SupplierMappingHelper.GetCurrentSuppliers();

            return this.View();
        }

        /// <summary>
        /// The create action (post).
        /// </summary>
        /// <param name="catalogItem">The catalog item.</param>
        /// <returns>
        /// The index view.
        /// </returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/supplier/create 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"Sku":"{Sku}","Variant":"{Variant}","SupplierId":"{SupplierId}","Name":"{Name}","Description":"{Description}","ProductType":"{ProductType}","Unit":"{Unit}","UnitPrice":"{UnitPrice}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Sku, Variant, SupplierId, Name, Description, ProductType, Unit, UnitPrice")] CatalogItemEntry catalogItem)
        {
            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(catalogItem.SupplierId))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to create catalog item for supplier: " + catalogItem.SupplierId,
                           }));
            }

            var dateTimeUtcNow = DateTime.UtcNow;
            using (var db = new CatalogDbContext())
            {
                catalogItem.DataVersion = 0;
                catalogItem.CreationTime = dateTimeUtcNow;
                catalogItem.UpdateTime = dateTimeUtcNow;
                db.CatalogItemEntries.Add(catalogItem);

                var agentCopy = new CatalogItemEntry
                {
                    SupplierId = ConfigHelper.AgentSupplier,
                    Sku = catalogItem.Sku,
                    Variant = catalogItem.Variant,
                    Name = catalogItem.Name,
                    Description = catalogItem.Description,
                    ProductType = catalogItem.ProductType,
                    Unit = catalogItem.Unit,
                    UnitPrice = catalogItem.UnitPrice,
                    CreationTime = dateTimeUtcNow,
                    UpdateTime = dateTimeUtcNow,
                };
                db.CatalogItemEntries.Add(agentCopy);

                db.SaveChanges();
            }

            using (var db = new IspDbContext())
            {
                var inventory = new InventoryEntry
                {
                    SupplierId = catalogItem.SupplierId,
                    ProductSku = catalogItem.Sku,
                    VariantId = catalogItem.Variant,
                    OfferId = string.Empty,
                    WarehouseId = string.Empty,
                    ExtraData = string.Empty,
                    AvailableQuantity = 0,
                    CreationTime = dateTimeUtcNow,
                    CreationTimeStamp = dateTimeUtcNow.Ticks,
                    LastModifiedTime = dateTimeUtcNow,
                    LastModifiedTimeStamp = dateTimeUtcNow.Ticks,
                };
                db.InventoryEntries.Add(inventory);

                var agentCopy = new InventoryEntry
                {
                    SupplierId = ConfigHelper.AgentSupplier,
                    ProductSku = catalogItem.Sku,
                    VariantId = catalogItem.Variant,
                    OfferId = string.Empty,
                    WarehouseId = string.Empty,
                    ExtraData = string.Empty,
                    AvailableQuantity = int.MaxValue,
                    CreationTime = dateTimeUtcNow,
                    CreationTimeStamp = dateTimeUtcNow.Ticks,
                    LastModifiedTime = dateTimeUtcNow,
                    LastModifiedTimeStamp = dateTimeUtcNow.Ticks,
                };
                db.InventoryEntries.Add(agentCopy);

                db.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The edit action (get).
        /// </summary>
        /// <param name="supplierid">The supplierid.</param>
        /// <param name="sku">The sku.</param>
        /// <param name="variant">The variant.</param>
        /// <returns>The edit view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/catalog/edit?supplierId={supplierId}&sku={sku}&variant={variant}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Edit(string supplierid, string sku, string variant)
        {
            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(supplierid))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to edit catalog item for supplier: " + supplierid,
                           }));
            }

            using (var db = new CatalogDbContext())
            {
                var catalogItem =
                    db.CatalogItemEntries.FirstOrDefault(
                        c => c.SupplierId.Equals(supplierid) && c.Sku.Equals(sku) && c.Variant.Equals(variant));

                return this.View(catalogItem);
            }
        }

        /// <summary>
        /// The edit action (post).
        /// </summary>
        /// <param name="catalogItem">The catalog item.</param>
        /// <returns>The details action.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/catalog/edit 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"Sku":"{Sku}","Variant":"{Variant}","SupplierId":"{SupplierId}","Name":"{Name}","Description":"{Description}","ProductType":"{ProductType}","Unit":"{Unit}","UnitPrice":"{UnitPrice}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Sku, Variant, SupplierId, Name, Description, ProductType, Unit, UnitPrice")] CatalogItemEntry catalogItem)
        {
            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

            if (!supplierIds.Contains(catalogItem.SupplierId))
            {
                return
                   new RedirectToRouteResult(
                       new RouteValueDictionary(
                           new
                           {
                               controller = "Home",
                               action = "ShowError",
                               errorMessage = "Insufficient privileges to edit catalog item for supplier: " + catalogItem.SupplierId,
                           }));
            }

            using (var db = new CatalogDbContext())
            {
                var agentSupplier = ConfigHelper.AgentSupplier;
                db.CatalogItemEntries.Where(
                    c =>
                    c.Sku.Equals(catalogItem.Sku) && c.Variant.Equals(catalogItem.Variant)
                    && (c.SupplierId.Equals(catalogItem.SupplierId) || c.SupplierId.Equals(agentSupplier)))
                    .Update(
                        c =>
                        new CatalogItemEntry
                        {
                            Name = catalogItem.Name,
                            ProductType = catalogItem.ProductType,
                            Description = catalogItem.Description,
                            Unit = catalogItem.Unit,
                            UnitPrice = catalogItem.UnitPrice,
                            DataVersion = c.DataVersion + 1,
                            UpdateTime = DateTime.UtcNow,
                        });
                db.SaveChanges();

                return this.RedirectToAction(
                    "Details",
                    new { supplierId = catalogItem.SupplierId, sku = catalogItem.Sku, variant = catalogItem.Variant });
            }
        }

        /// <summary>
        /// The details action.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="sku">The sku.</param>
        /// <param name="variant">The variant.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/catalog/details?supplierId={supplierId}&sku={sku}&variant={variant}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Details(string supplierId, string sku, string variant)
        {
            if (string.IsNullOrWhiteSpace(supplierId) || string.IsNullOrWhiteSpace(sku))
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
                               errorMessage = "Insufficient privileges to view catalog item for supplier: " + supplierId,
                           }));
            }

            using (var db = new IspDbContext())
            {
                ViewBag.HasInventory =
                    db.InventoryEntries.Any(i => i.SupplierId.Equals(supplierId) && i.ProductSku.Equals(sku));
            }

            using (var db = new CatalogDbContext())
            {
                var catalogItem =
                    db.CatalogItemEntries.FirstOrDefault(
                        c => c.SupplierId.Equals(supplierId) && c.Sku.Equals(sku) && c.Variant.Equals(variant));

                if (catalogItem == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(catalogItem);
            }
        }

        /// <summary>
        /// The delete action.
        /// </summary>
        /// <param name="supplierid">The supplierid.</param>
        /// <param name="sku">The sku.</param>
        /// <param name="variant">The variant.</param>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/catalog/delete?supplierId={supplierId}&sku={sku}&variant={variant}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Delete(string supplierid, string sku, string variant)
        {
            var agentSupplier = ConfigHelper.AgentSupplier;

            using (var db = new CatalogDbContext())
            {
                var catalogItem =
                    db.CatalogItemEntries.FirstOrDefault(
                        c => c.SupplierId.Equals(supplierid) && c.Sku.Equals(sku) && c.Variant.Equals(variant));

                if (catalogItem == null)
                {
                    return this.HttpNotFound();
                }

                var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();

                if (!supplierIds.Contains(catalogItem.SupplierId))
                {
                    return
                       new RedirectToRouteResult(
                           new RouteValueDictionary(
                               new
                               {
                                   controller = "Home",
                                   action = "ShowError",
                                   errorMessage = "Insufficient privileges to edit catalog item for supplier: " + catalogItem.SupplierId,
                               }));
                }

                db.CatalogItemEntries.Where(
                    c =>
                    c.Sku.Equals(sku) && c.Variant.Equals(variant)
                    && (c.SupplierId.Equals(supplierid) || c.SupplierId.Equals(agentSupplier))).Delete();
                db.SaveChanges();
            }

            using (var db = new IspDbContext())
            {
                db.InventoryEntries.Where(
                    i =>
                    i.ProductSku.Equals(sku) && i.VariantId.Equals(variant)
                    && (i.SupplierId.Equals(supplierid) || i.SupplierId.Equals(agentSupplier))).Delete();
                db.SaveChanges();
            }

            return this.RedirectToAction("Index");
        } 

        #endregion
    }
}