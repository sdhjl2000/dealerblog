using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DealerBlog.Web.Startup))]
namespace DealerBlog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           

        }
    }
}
