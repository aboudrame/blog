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

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;
        public EmailSender(IOptions<UserSecret> optionsAccessor, IConfiguration config, IHostingEnvironment env)
        {
            Options = optionsAccessor.Value;
            _config = config;
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
                //On production, read from the environment variable
                SendGridKey = Environment.GetEnvironmentVariable("SendGridAPIKey");
            }

            return Execute(SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var Admin = Environment.GetEnvironmentVariable("Admin");
            var SendGridAPIUser = Environment.GetEnvironmentVariable("SendGridAPIUser");
            var msg = new SendGridMessage()
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

            return client.SendEmailAsync(msg);
        }
    }
}
