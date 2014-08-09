﻿/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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