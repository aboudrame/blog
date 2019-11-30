using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace blog.Areas.Identity.Pages.Account
{
    public class NewMemberModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public NewMemberModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string userId)
        {
            var userid = await _db.Users.FindAsync(userId);
            ViewData["Fullname"] = userid.First_Name + " " + userid.Last_Name;
            ViewData["Email"] = userid.Email;
        }
    }
}
