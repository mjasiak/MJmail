using Owin;
using Microsoft.Owin;
using MJmail.App_Start;

[assembly: OwinStartup("Startup",typeof(MJmail.Startup))]
namespace MJmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureIdentity(app);
           ConfigureSignalR(app);
        }
    }
}