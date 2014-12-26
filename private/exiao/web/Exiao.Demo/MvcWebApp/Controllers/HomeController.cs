// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Controllers
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the HomeController type.
    /// </summary>
    public class HomeController : Controller
    {
        #region Methods

        /// <summary>
        /// The index action.
        /// </summary>
        /// <returns>
        /// The index view.
        /// </returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The show error action
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="signIn">The sign in.</param>
        /// <returns>The show error view.</returns>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/showError?errorMessage={errorMessage}&signIn={true|false}
        /// ]]>
        /// </example>
        [HttpGet]
        public ActionResult ShowError(string errorMessage, string signIn)
        {
            ViewBag.SignIn = Convert.ToBoolean(signIn);
            ViewBag.ErrorMessage = errorMessage;
            return this.View();
        }

        #endregion
    }
}