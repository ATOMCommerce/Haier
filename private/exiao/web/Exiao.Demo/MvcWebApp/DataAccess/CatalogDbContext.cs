// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatalogDbContext.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the CatalogDbContext type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System.Data.Entity;

    /// <summary>
    /// Defines the CatalogDbContext type.
    /// </summary>
    public sealed class CatalogDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogDbContext"/> class.
        /// </summary>
        public CatalogDbContext()
            : base("CatalogDB")
        {
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the catalog item entries.
        /// </summary>
        /// <value>
        /// The catalog item entries.
        /// </value>
        public DbSet<CatalogItemEntry> CatalogItemEntries { get; set; } 

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
            var entity = modelBuilder.Entity<CatalogItemEntry>();
            entity.HasKey(c => new { c.SupplierId, c.Sku, c.Variant }).ToTable("Catalog_CS_1");
            entity.Property(c => c.DataVersion).HasColumnName("Version");
            entity.Property(c => c.UnitPrice).HasColumnType("float");

            base.OnModelCreating(modelBuilder);
        } 

        #endregion
    }
}