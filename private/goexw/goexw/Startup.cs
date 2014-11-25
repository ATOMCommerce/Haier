using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(goexw.Startup))]
namespace goexw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
