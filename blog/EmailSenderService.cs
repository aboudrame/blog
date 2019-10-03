using blog.Data;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace blog
{

    public class EmailSenderService
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly ApplicationDbContext _db;
        public EmailSenderService(IHostingEnvironment env, IConfiguration config, IHttpContextAccessor HttpContextAccessor, ApplicationDbContext db)
        {
            _env = env;
            _config = config;
            _HttpContextAccessor = HttpContextAccessor;
            _db = db;            
        }
        public void Sender(string ToEmailAddress)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("aboudrame", "aboudrame@yahoo.fr"));
            message.To.Add(new MailboxAddress(ToEmailAddress, ToEmailAddress));
            message.Bcc.Add(new MailboxAddress("aboudrame@yahoo.fr", "aboudrame@yahoo.fr"));
            message.Subject = "Confirmation from aboudrame.com";

            var pathToFile = _env.WebRootPath
                    + Path.DirectorySeparatorChar.ToString()
                    + "contact.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            MimeEntity banner;
            MimeEntity profile;
            string getTheRoot;
            
            if (_config.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                getTheRoot = string.Format(@"C:\Users\aboubacar\source\repos\blog\blog\wwwroot");
            }
            else
            {
                getTheRoot = string.Format(@"h:\root\home\aboudrame-002\www\root\wwwroot");
            }

            banner  = builder.LinkedResources.Add(getTheRoot + @"\images\emailcontact.jpeg");
            profile = builder.LinkedResources.Add(getTheRoot + @"\images\about-profile.png");

            banner.ContentId = MimeUtils.GenerateMessageId();
            profile.ContentId = MimeUtils.GenerateMessageId();

            var LogginUserId = _HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var LogginUser = _db.Users.FirstOrDefault(x => x.Id == LogginUserId);
            var Full_Name = LogginUser.First_Name + ' ' + LogginUser.Last_Name;
        
            builder.HtmlBody = string.Format(builder.HtmlBody, banner.ContentId, profile.ContentId, Full_Name);
            message.Body = builder.ToMessageBody();



            using (var client = new SmtpClient())
            {
                client.Connect("smtp.mail.yahoo.com", 465, true);
                client.Authenticate("aboudrame@yahoo.fr", "Fatoumata_1");
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
