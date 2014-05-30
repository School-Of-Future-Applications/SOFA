using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SOFA.Helpers
{
    public class Helper
    {
        public static void SendEmail(string toAddress, string subject, string body)
        {
            var email = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP:Username");
            var password = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP:Password");
            var smtpaddr = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP:Host");
            var smtpport = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("SMTP:Port"));

            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(email);
            msg.To.Add(toAddress);
            msg.Subject = subject;
            msg.Body = body; 

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(email, password);
            client.Host = smtpaddr;
            client.Port = smtpport;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            client.Send(msg);
        }
    }
}