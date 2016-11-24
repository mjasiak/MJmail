using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MJmail.App_Start;
using MJMail.Data.Models;
using MJMail.Data.Models.AccountHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MJmail.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                ApplicationUser user = userManager.Find(login.Email, login.Password);
                if (user != null) //Tutaj co się dzieje po logowaniu
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Inbox", "Messages");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if(string.IsNullOrEmpty(model.UserName))
            {
                model.UserName = model.Email;
            }
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) //Tutaj co sie dzieje po zarejestrowaniu
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}