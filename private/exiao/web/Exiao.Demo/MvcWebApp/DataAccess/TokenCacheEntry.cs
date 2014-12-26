// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenCacheEntry.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the TokenCacheEntry type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System;

    /// <summary>
    /// Defines the TokenCacheEntry type.
    /// </summary>
    public sealed class TokenCacheEntry
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user object identifier.
        /// </summary>
        /// <value>
        /// The user object identifier.
        /// </value>
        public string UserObjectId { get; set; }

        /// <summary>
        /// Gets or sets the cache bits.
        /// </summary>
        /// <value>
        /// The cache bits.
        /// </value>
        public byte[] CacheBits { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; } 

        #endregion
    }
}