// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SupplierMappingHelper.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the SupplierMappingHelper type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using Exiao.Demo.DataAccess;

    /// <summary>
    /// Defines the SupplierMappingHelper type.
    /// </summary>
    public static class SupplierMappingHelper
    {
        #region Methods

        /// <summary>
        /// Gets the mapping entries.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>The supplier mapping entries.</returns>
        public static IEnumerable<SupplierMappingEntry> GetMappingEntries(string supplierId)
        {
            using (var db = new ExiaoDbContext())
            {
                return db.SupplierMappingEntries.Where(m => m.SupplierId.Equals(supplierId)).ToList();
            }
        }

        /// <summary>
        /// Gets the mapping suppliers.
        /// </summary>
        /// <param name="groupIds">The group ids.</param>
        /// <returns>The supplier id list.</returns>
        public static IList<string> GetMappingSuppliers(IList<string> groupIds)
        {
            using (var db = new ExiaoDbContext())
            {
                return
                    db.SupplierMappingEntries.Where(m => groupIds.Contains(m.GroupId))
                        .Select(m => m.SupplierId)
                        .ToList();
            }
        }

        /// <summary>
        /// Gets the current suppliers.
        /// </summary>
        /// <returns>The supplier id list.</returns>
        public static IList<string> GetCurrentSuppliers()
        {
            return ClaimsPrincipal.Current.FindAll("suppliers").Select(c => c.Value).ToList();
        } 

        #endregion
    }
}