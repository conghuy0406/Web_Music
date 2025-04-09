using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    public class CaSisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaSisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CaSis.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caSi = await _context.CaSis
                .FirstOrDefaultAsync(m => m.MaCaSi == id);
            if (caSi == null)
            {
                return NotFound();
            }

            return View(caSi);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCaSi,TenCaSi,AnhDaiDien,TieuSu")] CaSi caSi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caSi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caSi);
        }

        // GET: CaSis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caSi = await _context.CaSis.FindAsync(id);
            if (caSi == null)
            {
                return NotFound();
            }
            return View(caSi);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCaSi,TenCaSi,AnhDaiDien,TieuSu")] CaSi caSi)
        {
            if (id != caSi.MaCaSi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caSi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaSiExists(caSi.MaCaSi))
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
            return View(caSi);
        }

        // GET: CaSis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caSi = await _context.CaSis
                .FirstOrDefaultAsync(m => m.MaCaSi == id);
            if (caSi == null)
            {
                return NotFound();
            }

            return View(caSi);
        }

        // POST: CaSis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caSi = await _context.CaSis.FindAsync(id);
            if (caSi != null)
            {
                _context.CaSis.Remove(caSi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaSiExists(int id)
        {
            return _context.CaSis.Any(e => e.MaCaSi == id);
        }
    }
}
