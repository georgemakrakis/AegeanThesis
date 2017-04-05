using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AegeanThesis.Startup))]
namespace AegeanThesis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
