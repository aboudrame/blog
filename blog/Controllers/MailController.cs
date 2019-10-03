using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using EASendMail;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Utils;
//using SmtpClient = EASendMail.SmtpClient;

namespace blog.Controllers
{
    public class MailController : Controller
    {
        private readonly IHostingEnvironment _env;
        public MailController(IHostingEnvironment HostingEnvironment)
        {
            _env = HostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test from yahoo mail", "aboudrame@yahoo.fr"));
            message.To.Add(new MailboxAddress("master", "aboudrame@yahoo.fr"));
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
                    + "htmlpage.html";

           // var subject = "Confirm Account Registration";

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

            var image1 = builder.LinkedResources.Add(@"C:\Users\aboubacar\source\repos\blog\blog\wwwroot\images\bkg5.jpg");
            image1.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody = string.Format(builder.HtmlBody, image1.ContentId);
            message.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Connect("smtp.mail.yahoo.com", 465, true);
                client.Authenticate("aboudrame@yahoo.fr", "Fatoumata_1");
                await client.SendAsync(message);
                client.Disconnect(true);
            }



            //SmtpMail oMail = new SmtpMail("TryIt");

            //// Your yahoo email address
            //oMail.From = "aboudrame@yahoo.fr";

            //// Set recipient email address
            //oMail.To = "aboudrame68@gmail.com";

            //// Set email subject
            //oMail.Subject = "test email from yahoo account";

            //// Set email body
            //oMail.TextBody = "this is a test email sent from c# with yahoo.";

            //// Yahoo SMTP server address
            //SmtpServer oServer = new SmtpServer("smtp.mail.yahoo.com");

            //// For example: your email is "myid@yahoo.com", then the user should be "myid@yahoo.com"
            //oServer.User = "aboudrame@yahoo.fr";
            //oServer.Password = "Fatoumata_1";

            //// Because yahoo deploys SMTP server on 465 port with direct SSL connection.
            //// So we should change the port to 465. you can also use 25 or 587
            //oServer.Port = 465;

            //// detect SSL type automatically
            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //Console.WriteLine("start to send email over SSL ...");

            //SmtpClient oSmtp = new SmtpClient();
            //oSmtp.SendMail(oServer, oMail);

            //Console.WriteLine("email was sent successfully!");
            return View();
        }
    }
}