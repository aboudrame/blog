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
    [Authorize]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Blogs.Include(x=>x.Comments).Where(x=>x.Author == User.Identity.Name).OrderByDescending(x=>x.LastModifiedDate);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.Include(x=>x.Comments)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Member/Create
        public IActionResult  Create()
        {
            var blog = new Blog();
            BlogCommentViewModel blogCommentViewModel = new BlogCommentViewModel();

            blog.Author = User.Identity.Name;
            blog.CreatedDate = DateTime.Now;
            blog.LastModifiedDate = DateTime.Now;
            blog.ContentTypeId = 2;

            blogCommentViewModel.Blog = blog;
            ViewData["blog"] = blogCommentViewModel;

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


        // GET: Member/Create
        //public IActionResult Create2()
        //{
        //    var blog = new Blog();

        //    blog.Author = User.Identity.Name;
        //    blog.CreatedDate = DateTime.Now;
        //    blog.LastModifiedDate = DateTime.Now;
        //    blog.ContentTypeId = 2;

        //    return View(blog);
        //}

        //// POST: Member/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create2(Blog blog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(blog);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(blog);
        //}

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
            BlogCommentViewModel blogCommentViewModel = new BlogCommentViewModel();
            blogCommentViewModel.Blog = blog;
            ViewData["blog"] = blogCommentViewModel;

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


        //public async Task<IActionResult> CreateComment(long id)
        //{

        //    var blog = await _context.Blogs.FindAsync(id);
        //    RegisterCommentViewModel registerCommentViewModel = new RegisterCommentViewModel();

        //    registerCommentViewModel.BlogBody = blog.Body;
        //    registerCommentViewModel.BlogId = blog.BlogId;
        //    registerCommentViewModel.CreatedDate = DateTime.Now;
        //    registerCommentViewModel.LastModifiedDate = DateTime.Now;
        //    registerCommentViewModel.Commenter = User.Identity.Name;

        //    return View(registerCommentViewModel);
        //}

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> CreateComment(RegisterCommentViewModel registerCommentViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Comment comment = new Comment
        //        {
        //                 CreatedDate = registerCommentViewModel.CreatedDate,
        //            LastModifiedDate = registerCommentViewModel.LastModifiedDate,
        //                   Commenter = registerCommentViewModel.Commenter,
        //                      BlogId = registerCommentViewModel.BlogId,
        //                        Body = registerCommentViewModel.CommentBody,
                    
        //        };

        //        _context.Comments.Add(comment);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index", "Blogs");
        //    }

        //    return View(registerCommentViewModel);
        //}
    }
}
