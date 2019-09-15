using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class AddToRoleViewModel
    {

        public string UserId { get; set; }
        public string RoleId { get; set; }

        [NotMapped]
        public List<IdentityRole> identityRoles { get; set; }
        [NotMapped]
        public List<IdentityUser> identityUsers { get; set; }
    }
}
