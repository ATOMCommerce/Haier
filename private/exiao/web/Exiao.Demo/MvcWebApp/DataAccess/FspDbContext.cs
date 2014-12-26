// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FspDbContext.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the FspDbContext type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.DataAccess
{
    using System.Data.Entity;

    /// <summary>
    /// Defines the FspDbContext type.
    /// </summary>
    public sealed class FspDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FspDbContext"/> class.
        /// </summary>
        public FspDbContext()
            : base("FspDB")
        {
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the fulfillment order entries.
        /// </summary>
        /// <value>
        /// The fulfillment order entries.
        /// </value>
        public DbSet<FulfillmentOrderEntry> FulfillmentOrderEntries { get; set; }

        /// <summary>
        /// Gets or sets the fulfillment message entries.
        /// </summary>
        /// <value>
        /// The fulfillment message entries.
        /// </value>
        public DbSet<FulfillmentMessageEntry> FulfillmentMessageEntries { get; set; } 

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
            modelBuilder.Entity<FulfillmentMessageEntry>().HasKey(m => m.Id).ToTable("FulfillmentMessages");

            var fulfillmentOrderEntity = modelBuilder.Entity<FulfillmentOrderEntry>();
            fulfillmentOrderEntity.HasKey(e => e.Id).ToTable("FulfillmentOrders");
            fulfillmentOrderEntity.Property(e => e.MflFulfillmentOrderId).HasColumnName("MFLFulfillmentOrderId");
            fulfillmentOrderEntity.Property(e => e.BackendFulfillmentOrderId).HasColumnName("BEFulfillmentOrderId");
        } 

        #endregion
    }
}