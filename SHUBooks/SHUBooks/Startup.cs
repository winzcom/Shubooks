using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SHUBooks.Startup))]
namespace SHUBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
