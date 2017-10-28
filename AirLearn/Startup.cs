using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AirLearn.Startup))]
namespace AirLearn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
