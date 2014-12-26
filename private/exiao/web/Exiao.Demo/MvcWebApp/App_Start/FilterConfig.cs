// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the FilterConfig type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using System.Web.Mvc;

    using Exiao.Demo.Utilities;

    /// <summary>
    /// Defines the FilterConfig type.
    /// </summary>
    internal static class FilterConfig
    {
        #region Methods

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new AccessLogAttribute());

            TraceHelper.TraceInformation("All global filters configured.");
        } 

        #endregion
    }
}