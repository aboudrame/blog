using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Authorization;
using blog.ViewModels;

namespace blog.Controllers
{
   
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index(string search)
        {
            List<Blog> blog = new List<Blog>();
            var applicationDbContext = _context.Blogs.Include(b => b.Comments);

            if (search == null)
            {
                var getappDbContext = await applicationDbContext.OrderByDescending(x => x.Posted).ToListAsync();
                foreach (var x in getappDbContext)
                {
                    blog.Add(x);
                };
            }
            else
            {
                var getappDbContext = await applicationDbContext.Where(b=>b.Body.Contains(search)).ToListAsync();

                foreach (var x in getappDbContext)
                {
                    blog.Add(x);
                }
            }

            if (!blog.Any())
            {
                return RedirectToAction("Index","Nocontent");
            }

            return View(blog);

        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }




        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {
            var x = new Blog();
            return View(x);
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Author = User.Identity.Name;
                blog.Posted = DateTime.Now;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (id == null || blog.Author != User.Identity.Name)
            {
                return NotFound();
            }
           
            if (blog == null)
            {
               
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("BlogId,Title,Author,Posted,Body,CategoryId")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);

            
            if (id == null || blog.Author != User.Identity.Name)
            {
                return NotFound();
                
            }

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(long id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
        [Authorize]
        public async Task<IActionResult> Comment(long id)
        {

            var blog = await _context.Blogs.FindAsync(id);
            RegisterCommentViewModel comment = new RegisterCommentViewModel();

            comment.BlogBody = blog.Body;
            comment.BlogHTML = blog.HTML;
            comment.BlogCSS = blog.CSS;
            comment.BlogJavaScript = blog.JavaScript;

            return View(comment);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Comment(long id, RegisterCommentViewModel registerCommentViewModel )
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    DateCommented = DateTime.Now,
                        Commenter = User.Identity.Name,
                           BlogId = id,
                             Body = registerCommentViewModel.CommentBody,
                             HTML = registerCommentViewModel.CommentHTML,
                              CSS = registerCommentViewModel.CommentCSS,
                       JavaScript = registerCommentViewModel.CommentJavaScript
                };

                _context.Comments.Add(comment);
               await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Blogs");
            }

            return View(registerCommentViewModel);
        }

    }
}
