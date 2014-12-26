// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigHelper.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConfigHelper type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System;
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// Defines the ConfigHelper type.
    /// </summary>
    internal static class ConfigHelper
    {
        #region Fields

        /// <summary>
        /// The Azure Active Directory instance value
        /// </summary>
        private static readonly string AadInstanceValue = ConfigurationManager.AppSettings["ida:AADInstance"];

        /// <summary>
        /// The client identifier value
        /// </summary>
        private static readonly string ClientIdValue = ConfigurationManager.AppSettings["ida:ClientId"];

        /// <summary>
        /// The application key value
        /// </summary>
        private static readonly string AppKeyValue = ConfigurationManager.AppSettings["ida:AppKey"];

        /// <summary>
        /// The graph resource identifier value
        /// </summary>
        private static readonly string GraphResourceIdValue = ConfigurationManager.AppSettings["ida:GraphUrl"];

        /////// <summary>
        /////// The graph API version value
        /////// </summary>
        ////private static readonly string GraphApiVersionValue = ConfigurationManager.AppSettings["ida:GraphApiVersion"];

        /// <summary>
        /// The tenant value
        /// </summary>
        private static readonly string TenantValue = ConfigurationManager.AppSettings["ida:Tenant"];

        /// <summary>
        /// The post logout redirect URI value
        /// </summary>
        private static readonly string PostLogoutRedirectUriValue =
            ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

        /// <summary>
        /// The authority value
        /// </summary>
        private static readonly string AuthorityValue = string.Format(
            CultureInfo.InvariantCulture,
            AadInstanceValue,
            TenantValue);

        /// <summary>
        /// The graph service root value
        /// </summary>
        private static readonly string GraphServiceRootValue = string.Format(
            CultureInfo.InvariantCulture,
            @"{0}/{1}",
            GraphResourceIdValue,
            TenantValue);

        /// <summary>
        /// The agent supplier value
        /// </summary>
        private static readonly string AgentSupplierValue = ConfigurationManager.AppSettings["exiao:AgentSupplier"];

        /// <summary>
        /// The current UI culture value
        /// </summary>
        private static readonly string CurrentUiCultureValue =
            ConfigurationManager.AppSettings["exiao:CurrentUiCulture"];

        /// <summary>
        /// The write trace log to database value
        /// </summary>
        private static readonly bool WriteTraceLogToDbValue =
            Convert.ToBoolean(ConfigurationManager.AppSettings["exiao:WriteTraceLogToDb"]);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public static string ClientId
        {
            get
            {
                return ClientIdValue;
            }
        }

        /// <summary>
        /// Gets the application key.
        /// </summary>
        /// <value>
        /// The application key.
        /// </value>
        public static string AppKey
        {
            get
            {
                return AppKeyValue;
            }
        }

        /// <summary>
        /// Gets the graph resource identifier.
        /// </summary>
        /// <value>
        /// The graph resource identifier.
        /// </value>
        public static string GraphResourceId
        {
            get
            {
                return GraphResourceIdValue;
            }
        }

        /// <summary>
        /// Gets the authority.
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public static string Authority
        {
            get
            {
                return AuthorityValue;
            }
        }

        /// <summary>
        /// Gets the post logout redirect URI.
        /// </summary>
        /// <value>
        /// The post logout redirect URI.
        /// </value>
        public static string PostLogoutRedirectUri
        {
            get
            {
                return PostLogoutRedirectUriValue;
            }
        }

        /// <summary>
        /// Gets the graph service root.
        /// </summary>
        /// <value>
        /// The graph service root.
        /// </value>
        public static string GraphServiceRoot
        {
            get
            {
                return GraphServiceRootValue;
            }
        }

        /// <summary>
        /// Gets the agent supplier.
        /// </summary>
        /// <value>
        /// The agent supplier.
        /// </value>
        public static string AgentSupplier
        {
            get
            {
                return AgentSupplierValue;
            }
        }

        /// <summary>
        /// Gets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
        public static string CurrentUiCulture
        {
            get
            {
                return CurrentUiCultureValue;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [write trace log to database].
        /// </summary>
        /// <value>
        /// <c>true</c> if [write trace log to database]; otherwise, <c>false</c>.
        /// </value>
        public static bool WriteTraceLogToDb
        {
            get
            {
                return WriteTraceLogToDbValue;
            }
        }

        #endregion
    }
}