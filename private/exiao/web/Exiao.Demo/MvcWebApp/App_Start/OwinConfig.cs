// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwinConfig.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the OwinConfig type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    using Exiao.Demo.Utilities;

    using Microsoft.Azure.ActiveDirectory.GraphClient;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.IdentityModel.Protocols;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Notifications;
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    /// <summary>
    /// Defines the OwinConfig type.
    /// </summary>
    internal static class OwinConfig
    {
        #region Methods

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = ConfigHelper.ClientId,
                    Authority = ConfigHelper.Authority,
                    PostLogoutRedirectUri = ConfigHelper.PostLogoutRedirectUri,
                    Notifications = GetOpenIdConnectAuthenticationNotifications(),
                });
        }

        /// <summary>
        /// Gets the open identifier connect authentication notifications.
        /// </summary>
        /// <returns>
        /// The OpenId connect authentication notifications.
        /// </returns>
        private static OpenIdConnectAuthenticationNotifications GetOpenIdConnectAuthenticationNotifications()
        {
            return new OpenIdConnectAuthenticationNotifications
            {
                AuthorizationCodeReceived = AuthorizationCodeReceived,
                SecurityTokenValidated = SecurityTokenValidated,
            };
        }

        /// <summary>
        /// Securities the token validated.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>An empty task instance.</returns>
        private static Task SecurityTokenValidated(
            SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
        {
            var groupIds = context.AuthenticationTicket.Identity.FindAll("groups").Select(c => c.Value).ToList();
            var suppliers = SupplierMappingHelper.GetMappingSuppliers(groupIds);

            foreach (var supplier in suppliers)
            {
                context.AuthenticationTicket.Identity.AddClaim(
                    new Claim("suppliers", supplier, ClaimValueTypes.String, "Exiao"));
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Authorizations the code received.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>An asynchronous task instance.</returns>
        private static async Task AuthorizationCodeReceived(AuthorizationCodeReceivedNotification context)
        {
            try
            {
                var code = context.Code;

                var credential = new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey);
                var userObjectId =
                    context.AuthenticationTicket.Identity.FindFirst(ClaimExtendedTypes.ObjectIdentifier).Value;

                var authContext = new AuthenticationContext(ConfigHelper.Authority, new TokenDbCache(userObjectId));

                var result = authContext.AcquireTokenByAuthorizationCode(
                    code,
                    new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)),
                    credential,
                    ConfigHelper.GraphResourceId);

                var graphClient = new ActiveDirectoryClient(
                    new Uri(ConfigHelper.GraphServiceRoot),
                    () => Task.FromResult(result.AccessToken));

                var tenantApps =
                    await graphClient.Applications.Where(a => a.AppId.Equals(ConfigHelper.ClientId)).ExecuteAsync();

                var appFetcher = (IApplicationFetcher)tenantApps.CurrentPage[0];
                var appOwners = await appFetcher.Owners.ExecuteAsync();
                do
                {
                    if (appOwners.CurrentPage.ToList().Any(owner => owner.ObjectId == userObjectId))
                    {
                        context.AuthenticationTicket.Identity.AddClaim(
                            new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, "Exiao"));
                    }

                    appOwners = await appOwners.GetNextPageAsync();
                }
                while (appOwners != null);
            }
            catch (Exception ex)
            {
                TraceHelper.TraceError(
                    "Error occurred after authorizations code received, exception detail: '{0}'",
                    ex.GetDetail());

                context.HandleResponse();
                context.Response.Redirect("/Home/ShowError?errorMessage=" + ex.Message + "&signIn=true");
            }
        }

        #endregion
    }
}