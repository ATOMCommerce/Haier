// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureHelper.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the CultureHelper type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System.Collections.Generic;
    using System.Globalization;

    using Exiao.Demo.Resources;

    /// <summary>
    /// Defines the CultureHelper type.
    /// </summary>
    public static class CultureHelper
    {
        #region Fields

        /// <summary>
        /// The default ui culture.
        /// </summary>
        private static readonly CultureInfo DefaultUiCulture = CultureInfo.GetCultureInfo("en-US");

        /// <summary>
        /// The current ui culture value.
        /// </summary>
        private static readonly CultureInfo CurrentUiCultureValue;

        /// <summary>
        /// All fulfillment order status value
        /// </summary>
        private static readonly IDictionary<string, string> AllFoStatusValue = new Dictionary<string, string>
                                                                                 {
                                                                                     {
                                                                                         "Delivered",
                                                                                         "FoStatusDelivered"
                                                                                     },
                                                                                     {
                                                                                         "Shipped",
                                                                                         "FoStatusShipped"
                                                                                     },
                                                                                     {
                                                                                         "InventoryConfirmed",
                                                                                         "FoStatusInventoryConfirmed"
                                                                                     },
                                                                                     {
                                                                                         "OrderCreated",
                                                                                         "FoStatusOrderCreated"
                                                                                     },
                                                                                     {
                                                                                         "New",
                                                                                         "FoStatusNew"
                                                                                     }
                                                                                 };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="CultureHelper"/> class.
        /// </summary>
        static CultureHelper()
        {
            if (string.IsNullOrWhiteSpace(ConfigHelper.CurrentUiCulture))
            {
                return;
            }

            try
            {
                CurrentUiCultureValue = CultureInfo.GetCultureInfo(ConfigHelper.CurrentUiCulture);
            }
            catch (CultureNotFoundException)
            {
            }
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
        public static CultureInfo CurrentUiCulture
        {
            get
            {
                return CurrentUiCultureValue ?? DefaultUiCulture;
            }
        }

        /// <summary>
        /// Gets all fulfillment order status.
        /// </summary>
        /// <value>
        /// All fulfillment order status.
        /// </value>
        public static IDictionary<string, string> AllFoStatus
        {
            get
            {
                return AllFoStatusValue;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the view text.
        /// </summary>
        /// <param name="textName">Name of the text.</param>
        /// <returns>
        /// The view text string in current UI culture.
        /// </returns>
        public static string GetViewText(string textName)
        {
            return ViewTexts.ResourceManager.GetString(textName, CurrentUiCulture) ?? textName;
        } 

        #endregion
    }
}