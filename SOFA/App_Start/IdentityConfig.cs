using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DBContext>(DBContext.Create);
            app.CreatePerOwinContext<SOFAUserManager>(SOFAUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
                   ,LoginPath = new PathString("/Login/UserLogin")
                });
        }
    }
}