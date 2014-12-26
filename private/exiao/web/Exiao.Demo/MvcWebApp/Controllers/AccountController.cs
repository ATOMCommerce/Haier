// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Exiao.Demo.DataAccess;
    using Exiao.Demo.Utilities;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;

    /// <summary>
    /// Defines the AccountController type.
    /// </summary>
    public class AccountController : Controller
    {
        #region Methods

        /// <summary>
        /// The sign in action.
        /// </summary>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/account/signIn?redirectUri={redirectUri}
        /// ]]>
        /// </example>
        [HttpGet]
        public void SignIn(string redirectUri)
        {
            if (redirectUri == null)
            {
                redirectUri = "/";
            }

            HttpContext.GetOwinContext()
                .Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = redirectUri },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }

        /// <summary>
        /// The sign out action.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// GET https://{hostname}/account/signOut
        /// ]]>
        /// </example>
        [HttpGet]
        public void SignOut()
        {
            if (!this.Request.IsAuthenticated)
            {
                return;
            }

            var userObjectId = ClaimsPrincipal.Current.FindFirst(ClaimExtendedTypes.ObjectIdentifier).Value;

            var authContext = new AuthenticationContext(ConfigHelper.Authority, new TokenDbCache(userObjectId));
            authContext.TokenCache.Clear();

            this.HttpContext.GetOwinContext()
                .Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
        }

        /// <summary>
        /// The account profile action.
        /// </summary>
        /// <returns>The account profile view.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AccountProfile()
        {
            ViewBag.Groups = await GraphHelper.GetGroups(ClaimsPrincipal.Current);

            var supplierIds = SupplierMappingHelper.GetCurrentSuppliers();
            using (var db = new ExiaoDbContext())
            {
                ViewBag.Suppliers = db.SupplierProfileEntries.Where(s => supplierIds.Contains(s.SupplierId)).ToList();
            }

            return this.View();
        } 

        #endregion
    }
}