using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace blog
{

    public class EmailSenderService
    {
        private readonly IHostingEnvironment _env;
        public EmailSenderService(IHostingEnvironment env)
        {
            _env = env;
        }
        public void Sender(string ToEmailAddress)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("aboudrame", "aboudrame@yahoo.fr"));
            message.To.Add(new MailboxAddress(ToEmailAddress, ToEmailAddress));
            //  message.To.Add(new MailboxAddress("Me", "aboudrame@yahoo.fr"));
            message.Subject = "tesing";
            //message.Body = new TextPart("plain")
            //{
            //   Text = "this is a test"

            //};


            //var builder = new BodyBuilder();
            //var page = builder.LinkedResources.Add(@"C:\Users\aboubacar\source\repos\blog\blog\Views\About\Index.cshtml");
            ////var page = builder.LinkedResources.Add(@"C:\Users\aboubacar\source\repos\blog\blog\wwwroot\htmlpage.html");
            //page.ContentId = MimeUtils.GenerateMessageId();

            //builder.HtmlBody = string.Format(@"this is a test {0}", page.ContentId);
            //message.Body = builder.ToMessageBody();




            var webRoot = _env.WebRootPath; //get wwwroot Folder  

            //Get TemplateFile located at wwwroot/Templates/EmailTemplate/Register_EmailTemplate.html
            //C:\Users\aboubacar\source\repos\blog\blog\Views\Blogs\Index.cshtml
            //C:\Users\aboubacar\source\repos\blog\blog\wwwroot\Blogs\Index.cshtml
            //https://localhost:44347/
            //C:\Users\aboubacar\source\repos\blog\blog\wwwroot\htmlpage.html




            var pathToFile = _env.WebRootPath
                    + Path.DirectorySeparatorChar.ToString()
                    + "contact.html";

            var subject = "Confirm Account Registration";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            //{0} : Subject  
            //{1} : DateTime  
            //{2} : Email  
            //{3} : Username  
            //{4} : Password  
            //{5} : Message  
            //{6} : callbackURL  

            //string messageBody = string.Format(builder.HtmlBody,
            //                                                    subject
            //    //String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
            //    //model.Email,
            //    //model.Email,
            //    //model.Password,
            //    //Message,
            //    //callbackUrl
            //    );

            //string messageBody = builder.HtmlBody;

            var banner = builder.LinkedResources.Add(@"C:\Users\aboubacar\source\repos\blog\blog\wwwroot\images\emailcontact.jpeg");
            var profile = builder.LinkedResources.Add(@"C:\Users\aboubacar\source\repos\blog\blog\wwwroot\images\about-profile.png");
            banner.ContentId = MimeUtils.GenerateMessageId();
            profile.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody = string.Format(builder.HtmlBody, banner.ContentId, profile.ContentId);
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
