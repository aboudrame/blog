﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using blog.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        
        public IActionResult Index()
        {
             var Course = _db.Blogs.Include(x=>x.Comments).Where(x=>x.ContentTypeId==1).OrderByDescending(x =>  x.LastModifiedDate ).Take(5).ToList();
            return View(Course);
        }

        [Authorize]
        public IActionResult Create()
        {
            var myCourse = new Blog();
            return View(myCourse);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid) {
                blog.LastModifiedDate = DateTime.Now;
                blog.CreatedDate = DateTime.Now;
                blog.Author = User.Identity.Name;
                blog.ContentTypeId = 1;

                _db.Blogs.Add(blog);
                _db.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = blog.BlogId });
            }

            return View(blog);
        }


        [Authorize]
        public IActionResult Edit (long id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);

            return View(blog);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit (Blog blog)
        {
            _db.Blogs.Update(blog);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Delete (long id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);

           return View(blog);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete (Blog blog)
        {
            _db.Blogs.Remove(blog);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
