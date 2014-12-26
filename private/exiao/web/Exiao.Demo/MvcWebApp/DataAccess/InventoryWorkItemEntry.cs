// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryWorkItemEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the InventoryWorkItemEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the InventoryWorkItemEntry type.
    /// </summary>
    public sealed class InventoryWorkItemEntry
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
        /// Gets or sets the reference unique identifier.
        /// </summary>
        /// <value>
        /// The reference unique identifier.
        /// </value>
        public string ReferenceGuid { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

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
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the calculation time.
        /// </summary>
        /// <value>
        /// The calculation time.
        /// </value>
        public DateTime CalculationTime { get; set; }

        /// <summary>
        /// Gets or sets the calculation time stamp.
        /// </summary>
        /// <value>
        /// The calculation time stamp.
        /// </value>
        public long CalculationTimeStamp { get; set; }

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

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string Tags { get; set; }

        #endregion
    }
}