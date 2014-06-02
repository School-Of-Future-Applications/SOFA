using System;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.Infrastructure
{
    [Authorize]
    public abstract class DashBoardBaseController : HttpsBaseController, INavProvider
    {
        [NonAction]
        public abstract Enum NavProviderTerm();
	}
}