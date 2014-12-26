// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FulfillmentMessageEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the FulfillmentMessageEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the FulfillmentMessageEntry type.
    /// </summary>
    public sealed class FulfillmentMessageEntry
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
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>
        /// The correlation identifier.
        /// </value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public string MessageType { get; set; }

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
        /// Gets or sets a value indicating whether this instance is committed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is committed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCommitted { get; set; }

        #endregion
    }
}