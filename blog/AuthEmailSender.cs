using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog
{
    public class AuthEmailSender: PageModel, IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var x = Url.Page("/Blogs/Index");

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Test from yahoo mail", "aboudrame@yahoo.fr"));
                mimeMessage.To.Add(new MailboxAddress("master", "aboudrame68@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress("Me", "aboudrame@yahoo.fr"));
                mimeMessage.Subject = "tesing";
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = "this is a test"

                };

                using (var client = new SmtpClient())
                {
                   
                    client.Connect("smtp.mail.yahoo.com", 465, true);
                    client.Authenticate("aboudrame@yahoo.fr", "Fatoumata_1");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(0);
        }
    }

    
}
