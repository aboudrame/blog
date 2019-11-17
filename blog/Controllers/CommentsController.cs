using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;
using blog.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace blog.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var comment = await _context.Comments.Include(c => c.blog).Where(x=>x.Commenter == User.Identity.Name).OrderByDescending(x=>x.LastModifiedDate).ToListAsync();
            //BlogCommentViewModel blogCommentViewModel = new BlogCommentViewModel();

            //foreach (var c in comment) {
            //    blogCommentViewModel.Comment = comment;
            //}
            return View(comment);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public async Task<IActionResult> Create(long id)
        {
            RegisterCommentViewModel registerCommentViewModel = new RegisterCommentViewModel();
            Blog blog = await _context.Blogs.FindAsync(id);

            BlogCommentViewModel blogCommentViewModel = new BlogCommentViewModel();
            Comment comment = new Comment();

            registerCommentViewModel.BlogTitle = blog.Title;
            registerCommentViewModel.BlogBody = blog.Body;
            registerCommentViewModel.BlogId = blog.BlogId;

            blogCommentViewModel.Comment = comment;
            ViewData["comment"] = blogCommentViewModel;

            return View(registerCommentViewModel);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long id, RegisterCommentViewModel registerCommentViewModel)
        {
            if (id != registerCommentViewModel.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Comment comment = new Comment() {
                    BlogId = registerCommentViewModel.BlogId,
                    Body = registerCommentViewModel.CommentBody,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Commenter = User.Identity.Name
                };

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Blogs");
            }

            return View(registerCommentViewModel);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }


            ViewData["BlogId"] = new SelectList(_context.Blogs, "BlogId", "Author", comment.BlogId);

            BlogCommentViewModel blogCommentViewModel = new BlogCommentViewModel();
            blogCommentViewModel.Comment = comment;
            ViewData["comment"] = blogCommentViewModel; 

            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CommentId,Body,Commenter,CreatedDate,LastModifiedDate,BlogId")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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
            ViewData["BlogId"] = new SelectList(_context.Blogs, "BlogId", "Author", comment.BlogId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(long id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
