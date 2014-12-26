// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizeExtendedAttribute.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the AuthorizeExtendedAttribute type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Defines the AuthorizeExtendedAttribute type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeExtendedAttribute : AuthorizeAttribute
    {
        #region Methods

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Home",
                                action = "ShowError",
                                errorMessage = "You do not have sufficient privileges to view this page.",
                                signIn = filterContext.HttpContext.Request.IsAuthenticated,
                            }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        } 

        #endregion
    }
}