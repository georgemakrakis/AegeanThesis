using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AegeanThesis.Startup))]
namespace AegeanThesis
{
    public partial class Startup
    {
        //These two variables were used to deny or allow access to some feautures
        public static string curr_role = "";
        public static string curr_user = ""; //this variable maybe used later

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
