using blog.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime UserCreatedOn { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LoginDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<Blog> blogs { get; set; }
        public IEnumerable<Contact> contacts { get; set; }
    }
}
