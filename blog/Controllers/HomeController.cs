using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using blog.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
            var Recents = _db.Blogs.OrderByDescending(x => x.Posted).Take(5).ToList();
            
            return View(Recents);
        }

        public IActionResult Blog(long id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            var Category = _db.Categories.FirstOrDefault(x => x.CategoryId == blog.CategoryId);
                       
            blog.Category.Name = Category.Name;
            return View(blog);
        }

        [Authorize]
        public IActionResult Create()
        {
            var myblog = new Blog();
            var mylist = new SelectList(_db.Categories.ToList(), "CategoryId", "Name");

            ViewBag.CategoryId = mylist;
           

            return View(myblog);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            blog.Posted = DateTime.Now;
            blog.Author = User.Identity.Name;  

            //UserName can also be get using this method
            //blog.Author =_db.Users.FirstOrDefault().UserName; 

            _db.Blogs.Add(blog);
            _db.SaveChanges();

            return RedirectToAction("Blog", "Home", new
            {
                id = blog.BlogId
            });
        }


        [Authorize]
        public IActionResult Edit (long id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", blog.BlogId);

            return View(blog);
        }

        [HttpPost][Authorize]
        public IActionResult Edit (Blog blog)
        {
            //var _blog = _db.Blogs.OrderBy(x => x.BlogId == id);

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
