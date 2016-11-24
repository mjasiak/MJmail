using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MJmail.Data;
using MJMail.Data.Models;
using Owin;

namespace MJmail.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new MaildbContext());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<RoleManager<ApplicationRole>>((options, context) =>
                new RoleManager<ApplicationRole>(
                    new RoleStore<ApplicationRole>(context.Get<MaildbContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(MaildbContext context)
            : base(context)
        {

        }

    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser>(context.Get<MaildbContext>()));

            return manager;
        }
    }
}