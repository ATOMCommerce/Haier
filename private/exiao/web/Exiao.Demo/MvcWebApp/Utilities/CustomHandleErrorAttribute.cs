// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomHandleErrorAttribute.cs" company="atom-commerce">
//   Copyright @  2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the CustomHandleErrorAttribute type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Defines the CustomHandleErrorAttribute type.
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        #region Methods

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            TraceHelper.TraceError(
                "Error occurred for request: '{0}', exception detail: '{1}'",
                filterContext.HttpContext.Request.RawUrl,
                filterContext.Exception.GetDetail());

            filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "ShowError",
                            errorMessage = filterContext.Exception.Message,
                        }));

            filterContext.ExceptionHandled = true;
        } 

        #endregion
    }
}