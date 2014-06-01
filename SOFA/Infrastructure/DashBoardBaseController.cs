using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.Infrastructure
{
    public abstract class DashBoardBaseController : HttpsBaseController, INavProvider
    {
        [NonAction]
        public abstract Enum NavProviderTerm();

        protected DBContext DBCon
        {
            get
            {
                return HttpContext.GetOwinContext().Get<DBContext>();
            }
        }

        protected SOFAUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext()
                      .GetUserManager<SOFAUserManager>();
            }
        }
	}
}