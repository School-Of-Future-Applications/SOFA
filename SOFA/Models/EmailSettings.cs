/*
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace SOFA.Models
{
    public class EmailSettings
    {
        private const String USERADMINFROMEMAIL_KEY = "Email:UserAdminFrom";

        [Required]
        [DataType(DataType.EmailAddress)]
        public String UserAdminFromEmail { get; set; }

        public static EmailSettings FetchEmailSettings()
        {
            var webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = webConfig.AppSettings.Settings;
            EmailSettings emailSettings = new EmailSettings();

            if (settings[USERADMINFROMEMAIL_KEY] != null)
            {
                emailSettings.UserAdminFromEmail = settings[USERADMINFROMEMAIL_KEY].Value;
            }
            return emailSettings;
        }

        public static void SaveEmailSettings(EmailSettings emailSettings)
        {
            var webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = webConfig.AppSettings.Settings;

            if (settings[USERADMINFROMEMAIL_KEY] == null)
            {
                settings.Add(USERADMINFROMEMAIL_KEY, emailSettings.UserAdminFromEmail);
            }
            else
            {
                settings[USERADMINFROMEMAIL_KEY].Value = emailSettings.UserAdminFromEmail;
            }
            webConfig.Save();
        }

        public static MailAddress UserAdminMailAddress()
        {
            EmailSettings settings = FetchEmailSettings();
            return new MailAddress(settings.UserAdminFromEmail);
        }
    }
}