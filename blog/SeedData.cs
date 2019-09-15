using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog
{
    public static class SeedData
    {

       
        public static void Initializer(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category() {Name = "JavaScript"});
                context.Categories.Add(new Category() {Name = "jQuery" });
                
                context.SaveChanges();

            }

            if (!context.Blogs.Any())
            {
                context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to place a form over a picture?",
                        Author = "seeddata@gmail.com",
                        Posted = DateTime.Now,
                        Body = "I would like to be able to place a search form. Any help would be immensely appreciated.",
                        CategoryId = 1,
                        ContentTypeId = 2
                        
                    });

                context.Blogs.Add(
                    new Blog()
                    {
                        Title = "How to hide and show Divs",
                        Author = "seeddata@gmail.com",
                        Posted = DateTime.Now,
                        Body = "The div has a title. When we click on the title the div expand. I want the div to collapse when we click on anything else not just the title. How would i do that?",
                        CategoryId = 1,
                        ContentTypeId = 2,
                    });

                context.SaveChanges();
            }

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

        }
    }
}
