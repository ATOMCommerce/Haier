// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FulfillmentOrderEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the FulfillmentOrderEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the FulfillmentOrderEntry type.
    /// </summary>
    public sealed class FulfillmentOrderEntry
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
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public string SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the MFL fulfillment order identifier.
        /// </summary>
        /// <value>
        /// The MFL fulfillment order identifier.
        /// </value>
        public string MflFulfillmentOrderId { get; set; }

        /// <summary>
        /// Gets or sets the backend fulfillment order identifier.
        /// </summary>
        /// <value>
        /// The backend fulfillment order identifier.
        /// </value>
        public string BackendFulfillmentOrderId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the delivery address data.
        /// </summary>
        /// <value>
        /// The delivery address data.
        /// </value>
        public string DeliveryAddressData { get; set; }

        /// <summary>
        /// Gets or sets the item lines data.
        /// </summary>
        /// <value>
        /// The item lines data.
        /// </value>
        public string ItemLinesData { get; set; }

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