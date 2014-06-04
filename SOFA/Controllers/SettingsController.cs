using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    [Authorize(Roles = SOFARole.AUTH_SYSADMIN)]
    public class SettingsController : DashBoardBaseController
    {
        //
        // GET: /Settings/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditEmailSettings()
        {
            return View(EmailSettings.FetchEmailSettings());
        }

        [HttpPost]
        public ActionResult EditEmailSettings(EmailSettings emailSettings)
        {
            if (ModelState.IsValid)
            {
                EmailSettings.SaveEmailSettings(emailSettings);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult EditSMTPSettings()
        {
            return View(SMTPSettings.FetchSMTPSettings());
        }

        [HttpPost]
        public ActionResult EditSMTPSettings(SMTPSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                SMTPSettings.SaveSMTPSettings(smtpSettings);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.SystemConfig;
        }
	}
}