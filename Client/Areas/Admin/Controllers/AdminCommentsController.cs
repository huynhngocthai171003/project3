using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using PagedList.Core;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentsController : Controller
    {
        private readonly ePRJContext _context;

        public AdminCommentsController(ePRJContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminComments
        public async Task<IActionResult> IndexComment(int page = 1)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var pageNumber = page;
                var pageSize = 5;

                List<Comment> IsProducts = new List<Comment>();

                IsProducts = _context.Comments.Include(c => c.Customer).Include(c => c.Product).ToList();
                PagedList<Comment> models = new PagedList<Comment>(IsProducts.AsQueryable(), pageNumber, pageSize);

                return View(models);
            }
            return RedirectToAction("Index", "AdminLogin");
        }

        // GET: Admin/AdminComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        // GET: Admin/AdminComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Admin/AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'ePRJContext.Comments'  is null.");
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexComment));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
