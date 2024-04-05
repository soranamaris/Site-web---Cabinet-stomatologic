using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Licenta1.Startup))]
namespace Licenta1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
