// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="atom-commerce">
//   Copyright @  2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the BundleConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using System.Web.Optimization;

    using Exiao.Demo.Utilities;

    /// <summary>
    /// Defines the BundleConfig type.
    /// </summary>
    internal static class BundleConfig
    {
        #region Methods

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-2.1.1.min.js",
                    "~/Scripts/jquery-ui-1.11.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"));

            TraceHelper.TraceInformation("All bundles configured.");
        } 

        #endregion
    }
}