using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using System.ComponentModel.DataAnnotations;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminStaffsController : Controller
    {
        private readonly ePRJContext _context;

        public AdminStaffsController(ePRJContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminStaffs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                return View(await _context.Staffs.ToListAsync());
            }
            return RedirectToAction("Index", "AdminLogin");
        }
        public async Task<IActionResult> IndexStaff()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                return View(await _context.Staffs.ToListAsync());
            }
            return RedirectToAction("Index", "AdminLogin");
        }

        // GET: Admin/AdminStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                if (id == null || _context.Staffs == null)
                {
                    return NotFound();
                }

                var staff = await _context.Staffs
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (staff == null)
                {
                    return NotFound();
                }

                return View(staff);
            }
            return RedirectToAction("Index", "AdminLogin");
        }

        // GET: Admin/AdminStaffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,Name,Role")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Staffs.FirstOrDefaultAsync(m => m.UserName == staff.UserName);
                if (check != null)
                {
                    TempData["username"] = "Username already exists";

                    return RedirectToAction("Create");
                }
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexStaff));

            }
            return View(staff);
        }

        // GET: Admin/AdminStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Admin/AdminStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,PassWord,Name,Role")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexStaff));
            }
            return View(staff);
        }

        // GET: Admin/AdminStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Admin/AdminStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'ePRJContext.Staffs'  is null.");
            }
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexStaff));
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.Id == id);
        }
    }
}
