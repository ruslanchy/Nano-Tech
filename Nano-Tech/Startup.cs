using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nano_Tech.Startup))]
namespace Nano_Tech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
