// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatalogItemEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the CatalogItemEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the CatalogItemEntry type.
    /// </summary>
    public sealed class CatalogItemEntry
    {
        #region Properties

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the variant.
        /// </summary>
        /// <value>
        /// The variant.
        /// </value>
        public string Variant { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public string SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the update time.
        /// </summary>
        /// <value>
        /// The update time.
        /// </value>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the data version.
        /// </summary>
        /// <value>
        /// The data version.
        /// </value>
        public long DataVersion { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public double? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the product category identifier list.
        /// </summary>
        /// <value>
        /// The product category identifier list.
        /// </value>
        public string ProductCategoryIdList { get; set; }

        /// <summary>
        /// Gets or sets the supported category identifier list.
        /// </summary>
        /// <value>
        /// The supported category identifier list.
        /// </value>
        public string SupportedCategoryIdList { get; set; } 

        #endregion
    }
}