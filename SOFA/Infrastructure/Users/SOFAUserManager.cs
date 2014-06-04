using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Infrastructure.Users
{
    public class SOFAUserManager : UserManager<SOFAUser>
    {
        public SOFAUserManager(IUserStore<SOFAUser> store)
            : base(store)
        { }

        public static SOFAUserManager Create(IdentityFactoryOptions<SOFAUserManager> options
                                            ,IOwinContext context)
        {
            DBContext dbContext = context.Get<DBContext>();
            SOFAUserManager manager =
                new SOFAUserManager(new UserStore<SOFAUser>(dbContext));

            manager.EmailService = new IdentityEmail();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
                manager.UserTokenProvider = new DataProtectorTokenProvider<SOFAUser>(dataProtectionProvider.Create("ASP.NET Identity"));

            return manager;
        }
    }
}