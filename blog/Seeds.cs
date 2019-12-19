using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using blog.Data;
using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace blog
{
    public class Seeds
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Seeds(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void Initializer()
        {
           
            _context.Database.EnsureCreated();
            if (!_context.ContentTypes.Any())
            {
                _context.ContentTypes.Add(
                    new ContentType()
                    {
                        Type = "Course"
                    });

                _context.ContentTypes.Add(
                    new ContentType()
                    {
                        Type = "Article"
                    });


                _context.SaveChanges();
            }

            if (!_context.Blogs.Any())
            {
                _context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to place a form over a picture?",
                        Author = "seeddata@gmail.com",
                        LastModifiedDate = DateTime.Now,
                        Body = "I would like to be able to place a search form. Any help would be immensely appreciated.",
                        ContentTypeId = 2

                    });

                _context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to hide and show Divs",
                        Author = "seeddata@gmail.com",
                        LastModifiedDate = DateTime.Now,
                        Body = "The div has a title. When we click on the title the div expand. I want the div to collapse when we click on anything else not just the title. How would i do that?",
                        ContentTypeId = 2,
                    });

                _context.SaveChanges();
            }

            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new IdentityRole() { Name = "Admin" });

                _context.SaveChanges();
            }


            if (!_context.Users.Any())
            {
                var passwordHassher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser()
                {
                    Email = "aboudrame@yahoo.fr",
                    UserName = "aboudrame@yahoo.fr",
                    EmailConfirmed = true,
                    First_Name = "Aboubacar",
                    Last_Name = "Drame",
                    UserCreatedOn = DateTime.Now,
                    Status = "1"
                };

                var result =  _userManager.CreateAsync(user, "Fatoumata_1").Result;

            }

            if (!_context.UserRoles.Any())
            {
                var roleId = _context.Roles.FirstOrDefault(x => x.Name == "Admin").Id;
                var userId = _context.Users.FirstOrDefault(x => x.Email == "aboudrame@yahoo.fr").Id;
                _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = userId });

                _context.SaveChanges();

            }

            if (!_context.Statuses.Any())
            {
                _context.Statuses.Add(
                    new StatusModel()
                    {
                        Status = "Active"
                    });

                _context.Statuses.Add(
                    new StatusModel()
                    {
                        Status = "Blocked"
                    });

                _context.SaveChanges();
            }


        }
    }
}


