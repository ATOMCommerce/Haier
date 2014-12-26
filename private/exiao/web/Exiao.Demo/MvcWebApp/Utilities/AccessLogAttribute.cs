// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessLogAttribute.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the AccessLogAttribute type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System.Web.Mvc;

    /// <summary>
    /// Defines the AccessLogAttribute type.
    /// </summary>
    public class AccessLogAttribute : ActionFilterAttribute
    {
        #region Methods

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TraceHelper.TraceInformation(
                "Access log with request '{0}', client ip '{1}'",
                filterContext.HttpContext.Request.Url,
                filterContext.HttpContext.Request.UserHostAddress);

            base.OnActionExecuted(filterContext);
        } 

        #endregion
    }
}