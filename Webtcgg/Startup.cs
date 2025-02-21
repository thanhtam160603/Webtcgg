using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webtcgg.Startup))]
namespace Webtcgg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
