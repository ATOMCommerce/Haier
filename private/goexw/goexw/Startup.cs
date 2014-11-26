using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Goexw.Startup))]
namespace Goexw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
