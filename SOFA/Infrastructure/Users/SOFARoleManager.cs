using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

using SOFA.Models;

namespace SOFA.Infrastructure.Users
{
    public class SOFARoleManager : RoleManager<SOFARole>, IDisposable
    {
        public SOFARoleManager(RoleStore<SOFARole> store)
            : base(store)
        { }

        public static SOFARoleManager Create(IdentityFactoryOptions<SOFARoleManager> options
                                            ,IOwinContext context)
        {
            return new SOFARoleManager(new RoleStore<SOFARole>(context.Get<DBContext>()));
        }
    }
}