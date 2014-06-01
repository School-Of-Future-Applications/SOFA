using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SOFA.Models
{
    public class SMTPSettings
    {
        private const int DEFAULT_PORT = 25;
        private const int PORT_MAX = 65535;
        private const int PORT_MIN = 0;

        private const String SMTP_HOST_KEY = "SMTP:Host";
        private const String SMTP_PASSWORD_KEY = "SMTP:Password";
        private const String SMTP_PORT_KEY = "SMTP:Port";
        private const String SMTP_SSL_KEY = "SMTP:SSL";
        private const String SMTP_USERNAME_KEY = "SMTP:UserName";

        private String _password = "";
        private int _port = DEFAULT_PORT;

        [Compare("Password")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

        [Required]
        public String Host { get; set; }

        [DataType(DataType.Password)]
        public String Password {
            get { return _password;  }
            set {
                _password = value;
                ConfirmPassword = value;
            }
        }

        [Required]
        [DefaultValue(DEFAULT_PORT)]
        [Range(PORT_MIN, PORT_MAX)]
        public int Port {
            get { return _port;  }
            set { _port = value; }
        }

        [Required]
        [DefaultValue(false)]
        public bool SSL { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public static SMTPSettings FetchSMTPSettings()
        {
            var webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = webConfig.AppSettings.Settings;
            SMTPSettings smtpSettings = new SMTPSettings();

            if(settings[SMTP_HOST_KEY] != null)
            {
                smtpSettings.Host = settings[SMTP_HOST_KEY].Value;
                smtpSettings.Password = settings[SMTP_PASSWORD_KEY].Value;
                smtpSettings.Port = Convert.ToInt32(settings[SMTP_PORT_KEY].Value);
                smtpSettings.SSL = Convert.ToBoolean(settings[SMTP_SSL_KEY].Value);
                smtpSettings.UserName = settings[SMTP_USERNAME_KEY].Value;
            }
            return smtpSettings;
        }

        public static void SaveSMTPSettings(SMTPSettings smtpSettings)
        {
            var webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = webConfig.AppSettings.Settings;

            if(settings[SMTP_HOST_KEY] == null)
            {
                settings.Add(SMTP_HOST_KEY, smtpSettings.Host);
                settings.Add(SMTP_PASSWORD_KEY, smtpSettings.Password);
                settings.Add(SMTP_PORT_KEY, smtpSettings.Port.ToString());
                settings.Add(SMTP_SSL_KEY, smtpSettings.SSL.ToString());
                settings.Add(SMTP_USERNAME_KEY, smtpSettings.UserName);
            }
            else
            {
                settings[SMTP_HOST_KEY].Value = smtpSettings.Host;
                settings[SMTP_PASSWORD_KEY].Value = smtpSettings.Password;
                settings[SMTP_PORT_KEY].Value = smtpSettings.Port.ToString();
                settings[SMTP_SSL_KEY].Value = smtpSettings.SSL.ToString();
                settings[SMTP_USERNAME_KEY].Value = smtpSettings.UserName;
            }
            webConfig.Save();
        }
    }
}