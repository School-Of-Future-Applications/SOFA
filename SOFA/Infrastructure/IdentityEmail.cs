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