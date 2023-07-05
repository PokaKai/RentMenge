using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rent.Startup))]
namespace rent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
