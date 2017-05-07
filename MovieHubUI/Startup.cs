using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieHubUI.Startup))]
namespace MovieHubUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
