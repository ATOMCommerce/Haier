// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the RouteConfig type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Exiao.Demo.Utilities;

    /// <summary>
    /// Defines the RouteConfig type.
    /// </summary>
    internal static class RouteConfig
    {
        #region Methods

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            TraceHelper.TraceInformation("All routes configured.");
        } 

        #endregion
    }
}
