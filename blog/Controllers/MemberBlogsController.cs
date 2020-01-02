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

namespace blog.Controllers
{
    [Authorize]
    public class MemberBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MemberBlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.Include(x => x.Comments).Where(x => x.Author == User.Identity.Name).OrderByDescending(x => x.LastModifiedDate).ToListAsync();
            return View(blog);
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.Include(x => x.Comments)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            ViewBag.RefererURL = HttpContext.Request.Headers["Referer"].ToString();
            return View(blog);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            var blog = new Blog
            {
                Author = User.Identity.Name,
                UserID = _context.Users.FirstOrDefault(x=>x.Email == User.Identity.Name).Id,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ContentTypeId = 2
            };

            return View(blog);
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            blog.LastModifiedDate = DateTime.Now;

            return View(blog);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Blog blog)
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

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(long? id)
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


            ViewBag.Referer = HttpContext.Request.Headers["Referer"].ToString();
            return View(blog);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(long id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
