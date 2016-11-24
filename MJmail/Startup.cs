using Owin;
using Microsoft.Owin;


[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseSession();
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Adds a default in-memory implementation of IDistributedCache
        //    services.AddCaching();

        //    services.AddSession();
        //    //// This Method may contain other code as well
        //}
    }
}