// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo
{
    using Owin;

    /// <summary>
    /// Defines the Startup type.
    /// </summary>
    public class Startup
    {
        #region Methods

        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            OwinConfig.ConfigureAuth(app);
        } 

        #endregion
    }
}