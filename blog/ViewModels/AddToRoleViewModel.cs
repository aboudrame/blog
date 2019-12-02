using Microsoft.AspNetCore.Identity;
using blog.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class AddToRoleViewModel
    {

        public string UserId { get; set; }
        public string RoleId { get; set; }

        [NotMapped]
        public List<IdentityRole> identityRoles { get; set; }
        [NotMapped]
        public List<ApplicationUser> identityUsers { get; set; }
    }
}
