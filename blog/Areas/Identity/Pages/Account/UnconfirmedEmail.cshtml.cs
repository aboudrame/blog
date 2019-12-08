using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace blog.Areas.Identity.Pages.Account
{
    public class UnconfirmedEmailModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender; 
        public UnconfirmedEmailModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public string ConfirmEmailAddress { get; set; }
        

        public async Task OnGetAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);

            ViewData["Email"] = user.Email;
            ViewData["userId"] = userId;
        }

        public async Task<IActionResult> OnPostAsync(string ConfirmEmailAddress, string RegisteredEmailAddress)
        {
            var confirmEmailAddress = ConfirmEmailAddress;

            if (string.IsNullOrEmpty(confirmEmailAddress))
            {
                ViewData["Email"] = RegisteredEmailAddress;
                return Page();
            }

            if (RegisteredEmailAddress != confirmEmailAddress)
            {
                ViewData["Email"] = RegisteredEmailAddress;
                return Page();
            }

            var user = _db.Users.FirstOrDefault(x => x.Email == RegisteredEmailAddress);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Resending the Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Redirect(Url.Page("/Account/CodeResend",null,null));
        }
    }
}
