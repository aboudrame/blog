﻿using System;
using System.Collections.Generic;
using System.Text;
using blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public  DbSet<Blog> Blogs { get; set; }
        public  DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
