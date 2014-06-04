using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Infrastructure.Users;
using SOFA.Models;
using SOFA.Models.ViewModels;

namespace SOFA.Controllers
{
    [Authorize]
    public class LoginController : HttpsBaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectPermanent("~/Login/UserLogin");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserLogin(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(UserLoginViewModel login, string returnUrl)
        {
            ClaimsIdentity ident = null;
            SOFAUser user = null;

            if(ModelState.IsValid)
            {
                user = this.UserManager().Find(login.UserName, login.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
                else
                {
                    ident = this.UserManager().CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    this.AuthManager().SignOut();
                    this.AuthManager().SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(new UserLoginViewModel { UserName = login.UserName });
        }

        public ActionResult UserLogout()
        {
            this.AuthManager().SignOut();
            return RedirectToAction("UserLogin");
        }
	}
}