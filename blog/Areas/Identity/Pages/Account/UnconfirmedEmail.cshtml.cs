using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blog.Data;

namespace blog.Areas.Identity.Pages.Account
{
    public class UnconfirmedEmailModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public UnconfirmedEmailModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);

            ViewData["Email"] = user.Email;
        }
    }
}
