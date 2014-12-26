// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SupplierController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the SupplierController type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EntityFramework.Extensions;

    using Exiao.Demo.DataAccess;
    using Exiao.Demo.Utilities;

    using PagedList;

    /// <summary>
    /// Defines the SupplierController type.
    /// </summary>
    [AuthorizeExtended(Roles = "Admin")]
    public class SupplierController : Controller
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
        /// GET https://{hostname}/supplier?sortOrder={sortOrder}&currentFilter={currentFilter}&searchString={searchString}&page={page}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SupplierSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "supplier_desc" : string.Empty;
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            using (var db = new ExiaoDbContext())
            {
                IQueryable<SupplierProfileEntry> suppliers;

                if (string.IsNullOrWhiteSpace(searchString))
                {
                    suppliers = db.SupplierProfileEntries;
                }
                else
                {
                    suppliers =
                        db.SupplierProfileEntries.Where(
                            s => s.SupplierId.Contains(searchString) || s.SupplierName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name":
                        suppliers = suppliers.OrderBy(c => c.SupplierName);
                        break;
                    case "name_desc":
                        suppliers = suppliers.OrderByDescending(c => c.SupplierName);
                        break;
                    case "supplier_desc":
                        suppliers = suppliers.OrderByDescending(c => c.SupplierId);
                        break;
                    default:
                        suppliers = suppliers.OrderBy(c => c.SupplierId);
                        break;
                }

                var pagedResult = suppliers.ToPagedList(page ?? 1, PageSize);
                return this.View(pagedResult);
            }
        }

        /// <summary>
        /// The create action (get).
        /// </summary>
        /// <returns>The create view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/supplier/create
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// The create action (post).
        /// </summary>
        /// <param name="supplierProfile">The supplier profile.</param>
        /// <returns>
        /// The index view.
        /// </returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/supplier/create 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"SupplierId":"{supplierId}","SupplierName":"{supplierName}","Description":"{description}","Profile":"{profile}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "SupplierId, SupplierName, Description, Profile")] SupplierProfileEntry supplierProfile)
        {
            var groupName = "Supplier" + supplierProfile.SupplierId;
            var groupDescription = supplierProfile.SupplierId + " supplier";

            var group = await GraphHelper.CreateGroupIfNotExist(groupName, groupDescription);

            using (var db = new ExiaoDbContext())
            {
                db.SupplierProfileEntries.Add(supplierProfile);

                db.SupplierMappingEntries.Add(
                    new SupplierMappingEntry
                    {
                        SupplierId = supplierProfile.SupplierId,
                        SupplierName = supplierProfile.SupplierName,
                        GroupId = group.ObjectId,
                        GroupName = groupName,
                    });

                db.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The details action.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/supplier/details?supplierId={supplierId}
        /// ]]>
        /// </example>
        [HttpGet]
        public async Task<ActionResult> Details(string supplierId)
        {
            using (var db = new ExiaoDbContext())
            {
                var supplier = db.SupplierProfileEntries.FirstOrDefault(s => s.SupplierId.Equals(supplierId));

                if (supplier == null)
                {
                    return
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Home", action = "ShowError", errorMessage = "Supplier not exist", }));
                }

                ViewBag.Groups = await GraphHelper.GetAllGroups();

                ViewBag.MappingGroups = db.SupplierMappingEntries.Where(m => m.SupplierId.Equals(supplierId)).ToList();

                return this.View(supplier);
            }
        }

        /// <summary>
        /// The delete action.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/supplier/delete?supplierId={supplierId}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Delete(string supplierId)
        {
            using (var db = new ExiaoDbContext())
            {
                db.SupplierProfileEntries.Where(s => s.SupplierId.Equals(supplierId)).Delete();

                db.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The assign group action.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/supplier/assignGroup 
        /// Content-Type: application/x-www-form-urlencoded;charset=UTF-8
        /// Request-Body: SupplierId={supplierId}&SupplierName={supplierName}&group={group}&groupText={groupText}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignGroup(FormCollection formCollection)
        {
            var supplierId = formCollection["SupplierId"];
            var supplierName = formCollection["SupplierName"];
            var groupId = formCollection["group"];
            var groupName = formCollection["groupText"];

            using (var db = new ExiaoDbContext())
            {
                db.SupplierMappingEntries.Add(
                    new SupplierMappingEntry
                    {
                        SupplierId = supplierId,
                        SupplierName = supplierName,
                        GroupId = groupId,
                        GroupName = groupName,
                    });
                db.SaveChanges();
            }

            return this.RedirectToAction("Details", new { supplierId });
        }

        /// <summary>
        /// The remove group action.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/supplier/removeGroup 
        /// Content-Type: application/x-www-form-urlencoded;charset=UTF-8
        /// Request-Body: {mappingId1}=delete&{mappingId2}=delete
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveGroup(FormCollection formCollection)
        {
            using (var db = new ExiaoDbContext())
            {
                foreach (var mappingId in
                    formCollection.AllKeys.Where(key => formCollection[key].Equals("delete"))
                        .Select(key => Convert.ToInt32(key)))
                {
                    var id = mappingId;
                    db.SupplierMappingEntries.Where(m => m.Id == id).Delete();
                }

                db.SaveChanges();
            }

            return this.RedirectToAction("Details", new { supplierId = formCollection["SupplierId"] });
        }

        /// <summary>
        /// The edit action (get).
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>The edit view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/supplier/edit?supplierId={supplierId}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Edit(string supplierId)
        {
            using (var db = new ExiaoDbContext())
            {
                var supplier = db.SupplierProfileEntries.FirstOrDefault(s => s.SupplierId.Equals(supplierId));

                if (supplier == null)
                {
                    return
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Home", action = "ShowError", errorMessage = "Supplier not exist", }));
                }

                return this.View(supplier);
            }
        }

        /// <summary>
        /// The edit action (post).
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns>The details action.</returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/supplier/edit 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"SupplierId":"{supplierId}","SupplierName":"{supplierName}","Description":"{description}","Profile":"{profile}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "SupplierId, SupplierName, Description, Profile")] SupplierProfileEntry supplier)
        {
            using (var db = new ExiaoDbContext())
            {
                db.SupplierProfileEntries.Where(s => s.SupplierId.Equals(supplier.SupplierId))
                    .Update(
                        s =>
                        new SupplierProfileEntry
                            {
                                SupplierName = supplier.SupplierName,
                                Description = supplier.Description,
                                Profile = supplier.Profile,
                            });
                db.SaveChanges();

                return this.RedirectToAction("Details", new { supplierId = supplier.SupplierId });
            }
        }

        #endregion
    }
}