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