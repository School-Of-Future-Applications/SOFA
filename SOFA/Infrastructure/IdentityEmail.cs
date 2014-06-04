using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

using SOFA.Models;

namespace SOFA.Infrastructure
{
    public class IdentityEmail : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage imessage)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = SMTPSettings.SmtpClientFromSettings();

            msg.IsBodyHtml = true;
            msg.From = EmailSettings.UserAdminMailAddress();
            msg.To.Add(imessage.Destination);
            msg.Subject = imessage.Subject;
            msg.Body = imessage.Body;

            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
            };

            return client.SendMailAsync(msg);
        }
    }
}