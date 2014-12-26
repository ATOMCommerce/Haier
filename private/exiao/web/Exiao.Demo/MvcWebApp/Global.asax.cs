// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the MvcApplication type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using System.Security.Claims;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Exiao.Demo.Utilities;

    /// <summary>
    /// Defines the MvcApplication type.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        /// Application start event handler.
        /// </summary>
        protected void Application_Start()
        {
            TraceHelper.TraceInformation("Application Starting...");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            TraceHelper.TraceInformation("Application Started...");
        }

        /// <summary>
        /// Application end event handler.
        /// </summary>
        protected void Application_End()
        {
            TraceHelper.TraceInformation("Application Ending...");

            TraceHelper.TraceInformation("Application Ended...");
        }

        #endregion
    }
}
