using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

using SOFA.Models;

namespace SOFA.Infrastructure
{
    public static class DBContextUtil
    {
        public static DBContext DBCon(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().Get<DBContext>();
        }
    }
}