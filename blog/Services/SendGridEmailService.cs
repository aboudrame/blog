using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace blog.Services
{
    public class SendGridEmailService
    {
    }

    public class UserSecret
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; } //This will hold the SendGrid API Key
    }

    public class EmailSender :  IEmailSender
    {
        private readonly IHostingEnvironment _env;
        public EmailSender(IOptions<UserSecret> optionsAccessor, IHostingEnvironment env)
        {
            Options = optionsAccessor.Value;
            _env = env;
        }

        public UserSecret Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var SendGridKey = "";

            if (_env.IsDevelopment())
            {
                //On development environment, read from the secret store
                SendGridKey = Options.SendGridKey;
            }
            else
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SendGridAPIKey"))) {
                    //On production, read from the environment variable
                    SendGridKey = Environment.GetEnvironmentVariable("SendGridAPIKey");
                }
            }

            if (!string.IsNullOrEmpty(SendGridKey))
            {

                return Execute(SendGridKey, subject, message, email);
            }

            return null;
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            if (string.IsNullOrEmpty(apiKey) || 
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Admin")) ||
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SendGridAPIUser")))
            {
                return null; //If the email cannot be sent due to the a missing required input, go to the error page
            }
            
            
            //Read values from the environment variables
             SendGridClient client = new SendGridClient(apiKey);
                      string Admin = Environment.GetEnvironmentVariable("Admin");
            string SendGridAPIUser = Environment.GetEnvironmentVariable("SendGridAPIUser");
               SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(Admin, SendGridAPIUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            
            //Send email to the user
            return client.SendEmailAsync(msg);
        }
    }
}
