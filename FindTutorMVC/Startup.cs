using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindTutorMVC.Startup))]
namespace FindTutorMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
