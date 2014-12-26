// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoleController type.
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

    using Exiao.Demo.Utilities;

    using Microsoft.Azure.ActiveDirectory.GraphClient;

    /// <summary>
    /// Defines the RoleController type.
    /// </summary>
    [AuthorizeExtended(Roles = "Admin")]
    public class RoleController : Controller
    {
        #region Methods

        /// <summary>
        /// The index action.
        /// </summary>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/role/
        /// ]]>
        /// </example>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var appRoles = await GraphHelper.GetAppRoles();
            return this.View(appRoles);
        }

        /// <summary>
        /// The create action (get).
        /// </summary>
        /// <returns>The create view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/role/create
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
        /// <param name="appRole">The application role.</param>
        /// <returns>
        /// The index view.
        /// </returns>
        /// <example>
        /// <![CDATA[
        /// POST https://{hostname}/role/create 
        /// Content-Type: application/json; charset=utf-8
        /// Request-Body: {"DisplayName":"{displayName}","Description":"{description}"}
        /// ]]>
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DisplayName,Description")]AppRole appRole)
        {
            try
            {
                var currentApplication = await GraphHelper.GetCurrentApplication();

                if (currentApplication.AppRoles.Any(role => role.DisplayName.Equals(appRole.DisplayName)))
                {
                    ModelState.AddModelError(string.Empty, @"AppRole already exist.");
                    return this.View(appRole);
                }

                var client = GraphHelper.GetGraphClient();
                var applicationEntity =
                    await client.Applications.GetByObjectId(currentApplication.ObjectId).ExecuteAsync();

                appRole.AllowedMemberTypes.Add("User");
                appRole.Id = Guid.NewGuid();
                appRole.IsEnabled = true;
                appRole.Value = appRole.DisplayName;

                applicationEntity.AppRoles.Add(appRole);

                client.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "ShowError", errorMessage = ex.Message, }));
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The details action.
        /// </summary>
        /// <param name="objectid">The objectid.</param>
        /// <returns>The details view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/role/details?objectId={objectId}
        /// ]]>
        /// </example>
        [HttpGet]
        public async Task<ActionResult> Details(Guid objectid)
        {
            var currentApplication = await GraphHelper.GetCurrentApplication();
            var appRole = currentApplication.AppRoles.FirstOrDefault(role => role.Id.Equals(objectid));

            if (appRole == null)
            {
                return
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "ShowError", errorMessage = "AppRole not exist.", }));
            }

            return this.View(appRole);
        }

        /// <summary>
        /// The delete action.
        /// </summary>
        /// <param name="objectid">The objectid.</param>
        /// <returns>The index view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/role/delete?objectId={objectId}
        /// ]]>
        /// </example>
        [HttpGet]
        public async Task<ActionResult> Delete(Guid objectid)
        {
            try
            {
                var currentApplication = await GraphHelper.GetCurrentApplication();

                var client = GraphHelper.GetGraphClient();
                var applicationEntity =
                    await client.Applications.GetByObjectId(currentApplication.ObjectId).ExecuteAsync();

                var appRole = applicationEntity.AppRoles.FirstOrDefault(role => role.Id.Equals(objectid));

                if (appRole == null)
                {
                    return
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Home", action = "ShowError", errorMessage = "AppRole not exist.", }));
                }

                appRole.IsEnabled = false;
                client.Context.SaveChanges();

                applicationEntity.AppRoles.Remove(appRole);

                client.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "ShowError", errorMessage = ex.Message, }));
            }

            return this.RedirectToAction("Index");
        } 

        #endregion
    }
}