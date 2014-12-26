// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IspDbContext.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the IspDbContext type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    /// <summary>
    /// Defines the IspDbContext type.
    /// </summary>
    public sealed class IspDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IspDbContext"/> class.
        /// </summary>
        public IspDbContext()
            : base("IspDB")
        {
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the inventory entries.
        /// </summary>
        /// <value>
        /// The inventory entries.
        /// </value>
        public DbSet<InventoryEntry> InventoryEntries { get; set; }

        /// <summary>
        /// Gets or sets the inventory work item entries.
        /// </summary>
        /// <value>
        /// The inventory work item entries.
        /// </value>
        public DbSet<InventoryWorkItemEntry> InventoryWorkItemEntries { get; set; } 

        #endregion

        #region Methods

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entity =
                modelBuilder.Entity<InventoryEntry>()
                    .HasKey(i => new { i.SupplierId, i.ProductSku, i.VariantId, i.OfferId, i.WarehouseId })
                    .ToTable("Inventories");
            entity.Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<InventoryWorkItemEntry>()
                .ToTable("InventoryWorkItems")
                .HasKey(i => i.Id)
                .Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        } 

        #endregion
    }
}