// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the InventoryEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the InventoryEntry type.
    /// </summary>
    public sealed class InventoryEntry
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public string SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the product sku.
        /// </summary>
        /// <value>
        /// The product sku.
        /// </value>
        public string ProductSku { get; set; }

        /// <summary>
        /// Gets or sets the offer identifier.
        /// </summary>
        /// <value>
        /// The offer identifier.
        /// </value>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets the variant identifier.
        /// </summary>
        /// <value>
        /// The variant identifier.
        /// </value>
        public string VariantId { get; set; }

        /// <summary>
        /// Gets or sets the warehouse identifier.
        /// </summary>
        /// <value>
        /// The warehouse identifier.
        /// </value>
        public string WarehouseId { get; set; }

        /// <summary>
        /// Gets or sets the available quantity.
        /// </summary>
        /// <value>
        /// The available quantity.
        /// </value>
        public double AvailableQuantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unlimited.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is unlimited; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnlimited { get; set; }

        /// <summary>
        /// Gets or sets the extra data.
        /// </summary>
        /// <value>
        /// The extra data.
        /// </value>
        public string ExtraData { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the creation time stamp.
        /// </summary>
        /// <value>
        /// The creation time stamp.
        /// </value>
        public long CreationTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified time stamp.
        /// </summary>
        /// <value>
        /// The last modified time stamp.
        /// </value>
        public long LastModifiedTimeStamp { get; set; }

        #endregion
    }
}