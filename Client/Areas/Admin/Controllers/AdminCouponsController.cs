using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCouponsController : Controller
    {
        private readonly ePRJContext _context;

        public AdminCouponsController(ePRJContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCoupons
        public async Task<IActionResult> IndexCoupon()
        {
              return View(await _context.Coupons.ToListAsync());
        }

        // GET: Admin/AdminCoupons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // GET: Admin/AdminCoupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCoupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Discount,Code,Limit")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexCoupon));
            }
            return View(coupon);
        }

        // GET: Admin/AdminCoupons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        // POST: Admin/AdminCoupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Discount,Code,Limit")] Coupon coupon)
        {
            if (id != coupon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(coupon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexCoupon));
            }
            return View(coupon);
        }

        // GET: Admin/AdminCoupons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // POST: Admin/AdminCoupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coupons == null)
            {
                return Problem("Entity set 'ePRJContext.Coupons'  is null.");
            }
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexCoupon));
        }

        private bool CouponExists(int id)
        {
          return _context.Coupons.Any(e => e.Id == id);
        }
    }
}
