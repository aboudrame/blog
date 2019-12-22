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

namespace blog.Services
{
    public static class SeedsTest
    {

        public static async void Initializer(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.ContentTypes.Any())
            {
                context.ContentTypes.Add(
                    new ContentType()
                    {
                        Type = "Course"
                    });

                context.ContentTypes.Add(
                    new ContentType()
                    {
                        Type = "Article"
                    });


                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to place a form over a picture?",
                        Author = "seeddata@gmail.com",
                        LastModifiedDate = DateTime.Now,
                        Body = "I would like to be able to place a search form. Any help would be immensely appreciated.",
                        ContentTypeId = 2

                    });

                context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to hide and show Divs",
                        Author = "seeddata@gmail.com",
                        LastModifiedDate = DateTime.Now,
                        Body = "The div has a title. When we click on the title the div expand. I want the div to collapse when we click on anything else not just the title. How would i do that?",
                        ContentTypeId = 2,
                    });

                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole() { Name = "Admin" });

                context.SaveChanges();
            }


            if (!context.Users.Any())
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

                var result =  userManager.CreateAsync(user, "Fatoumata_1").Result;

            }

            if (!context.UserRoles.Any())
            {
                var roleId = context.Roles.FirstOrDefault(x => x.Name == "Admin").Id;
                var userId = context.Users.FirstOrDefault(x => x.Email == "aboudrame@yahoo.fr").Id;
                context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = userId });

                context.SaveChanges();

            }

            if (!context.Statuses.Any())
            {
                context.Statuses.Add(
                    new StatusModel()
                    {
                        Status = "Active"
                    });

                context.Statuses.Add(
                    new StatusModel()
                    {
                        Status = "Blocked"
                    });

                context.SaveChanges();
            }


        }
    }
}


