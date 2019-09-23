using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Data
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime UserCreatedOn { get; set; }
    }
}
