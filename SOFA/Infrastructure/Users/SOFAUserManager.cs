using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Infrastructure.Users
{
    public class SOFAUserManager : UserManager<IdentityUser>
    {
        public SOFAUserManager(IUserStore<IdentityUser> store)
            : base(store)
        { }

        public static SOFAUserManager Create(IdentityFactoryOptions<SOFAUserManager> options
                                            ,IOwinContext context)
        {
            DBContext dbContext = context.Get<DBContext>();
            SOFAUserManager manager =
                new SOFAUserManager(new UserStore<IdentityUser>(dbContext));
            return manager;
        }
    }
}