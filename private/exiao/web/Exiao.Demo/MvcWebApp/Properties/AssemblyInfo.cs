// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   General Information about this assembly.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.InteropServices;

using Exiao.Demo;

using Microsoft.Owin;

[assembly: AssemblyTitle("Exiao.Demo.MvcWebApp")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("atom-commerce")]
[assembly: AssemblyProduct("Exiao.Demo")]
[assembly: AssemblyCopyright("Copyright © atom-commerce 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("bc6510f5-d2eb-433d-8a8a-ae14a00959fd")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: OwinStartup(typeof(Startup))]