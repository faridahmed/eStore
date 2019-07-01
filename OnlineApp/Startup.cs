using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineApp.Startup))]
namespace OnlineApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
