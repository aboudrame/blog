using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministratorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._context = context;
        }

        [HttpGet]
        public IActionResult CreateRole ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName                                 
                };
                
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
             


            return View(model);
        }


        public IActionResult AddToRole ()
        {
            List<IdentityUser> user = _context.Users.ToList();
            List<IdentityRole> Role = _context.Roles.ToList();

            AddToRoleViewModel model = new AddToRoleViewModel
            {
                identityUsers = user,
                identityRoles = Role
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddToRoleViewModel model)
        {
            if (ModelState.IsValid) {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = model.UserId
                };

                
                IdentityUser getUser  = await userManager.FindByIdAsync(identityUser.UserName);
                IdentityRole getRole = await roleManager.FindByIdAsync(model.RoleId);

                IdentityResult result = await userManager.AddToRoleAsync(getUser, getRole.ToString());

                if (result.Succeeded)
                {
                    ViewBag.S = string.Format("SUCCESS: {0} has been added to the {1} role", getUser.UserName, getRole);
                }
                else
                {
                    ViewBag.S = string.Format("Failed: {0} has not been added to the {1} role", getUser.UserName, getRole);
                }

                List<IdentityUser> user = _context.Users.ToList();
                List<IdentityRole> role = _context.Roles.ToList();

                model.identityUsers = user;
                model.identityRoles = role;
            }

            return View(model);
        }

    }
}