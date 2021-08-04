using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DekanatFPM.Startup))]
namespace DekanatFPM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
