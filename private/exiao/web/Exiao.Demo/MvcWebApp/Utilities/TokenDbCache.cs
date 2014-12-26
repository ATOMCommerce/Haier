// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenDbCache.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the TokenDbCache type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Linq;

    using EntityFramework.Extensions;

    using Exiao.Demo.DataAccess;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    /// <summary>
    /// Defines the TokenDbCache type.
    /// </summary>
    public class TokenDbCache : TokenCache
    {
        #region Fields

        /// <summary>
        /// The user object identifier
        /// </summary>
        private readonly string userObjectId;

        /// <summary>
        /// The cache entry
        /// </summary>
        private TokenCacheEntry cacheEntry; 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDbCache"/> class.
        /// </summary>
        /// <param name="userObjectId">The user object identifier.</param>
        public TokenDbCache(string userObjectId)
        {
            this.userObjectId = userObjectId;

            this.AfterAccess = this.AfterAccessNotification;
            this.BeforeAccess = this.BeforeAccessNotification;
            this.BeforeWrite = this.BeforeWriteNotification;

            this.cacheEntry = this.GetCacheEntry();
            this.DeserializeCacheEntry();
        } 

        #endregion

        #region Methods

        /// <summary>
        /// Clears the cache by deleting all the items. Note that if the cache is the default shared cache, clearing it would
        /// impact all the instances of <see cref="T:Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext" /> which share that cache.
        /// </summary>
        public override void Clear()
        {
            using (var db = new ExiaoDbContext())
            {
                try
                {
                    db.TokenCacheEntries.Where(c => c.UserObjectId == this.userObjectId).Delete();
                }
                catch (OptimisticConcurrencyException)
                {
                    TraceHelper.TraceWarning("Optimistic concurrency exception occurred while clearing token caches");
                }
                catch (Exception ex)
                {
                    TraceHelper.TraceError("Clear token caches failed, exception detail: '{0}'", ex.GetDetail());
                }
            }

            base.Clear();
        }

        /// <summary>
        /// Gets the cache entry.
        /// </summary>
        /// <returns>The token cache entry.</returns>
        private TokenCacheEntry GetCacheEntry()
        {
            using (var db = new ExiaoDbContext())
            {
                return db.TokenCacheEntries.FirstOrDefault(c => c.UserObjectId == this.userObjectId);
            }
        }

        /// <summary>
        /// Deserializes the cache entry.
        /// </summary>
        private void DeserializeCacheEntry()
        {
            this.Deserialize((this.cacheEntry == null) ? null : this.cacheEntry.CacheBits);
        }

        /// <summary>
        /// Befores the access notification.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            var cache = this.GetCacheEntry();

            if (this.cacheEntry == null || (cache != null && cache.LastModified > this.cacheEntry.LastModified))
            {
                this.cacheEntry = cache;
            }

            this.DeserializeCacheEntry();
        }

        /// <summary>
        /// Afters the access notification.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            if (!this.HasStateChanged)
            {
                return;
            }

            var cache = this.cacheEntry ?? new TokenCacheEntry { UserObjectId = this.userObjectId };
            cache.CacheBits = this.Serialize();
            cache.LastModified = DateTime.Now;

            this.cacheEntry = cache;
            using (var db = new ExiaoDbContext())
            {
                try
                {
                    db.Entry(this.cacheEntry).State = this.cacheEntry.Id == 0 ? EntityState.Added : EntityState.Modified;
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    TraceHelper.TraceWarning("Optimistic concurrency exception occurred while saving token cache.");
                }
                catch (Exception ex)
                {
                    TraceHelper.TraceError("Save token caches failed, exception detail: '{0}'", ex.GetDetail());
                }
            }

            this.HasStateChanged = false;
        }

        /// <summary>
        /// Befores the write notification.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private void BeforeWriteNotification(TokenCacheNotificationArgs args)
        {
            //// if you want to ensure that no concurrent write take place, use this notification to place a lock on the entry
        } 

        #endregion
    }
}