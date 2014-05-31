using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Infrastructure
{
    public abstract class DashBoardBaseController : HttpsBaseController, INavProvider
    {
        public abstract DashboardNavTerms NavProviderTerm();
	}
}