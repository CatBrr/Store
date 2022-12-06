using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class loomsController : Controller
    {
        private readonly ApplicationContext _context;

        public loomsController(ApplicationContext context)
        {
            _context = context;
        }
        
        // GET: looms
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.loomad.Include(l => l.iseloomu).Include(l => l.sugu).Include(l => l.tuup).Include(l => l.viilatüüp);
            return View(await applicationContext.ToListAsync());
        }

        // GET: looms/Create
        public IActionResult Create()
        {
            ViewData["iseloomuId"] = new SelectList(_context.iseloomud, "Id", "nimetus");
            ViewData["suguId"] = new SelectList(_context.sugud, "Id", "nimetus");
            ViewData["tuupId"] = new SelectList(_context.Set<tuup>(), "Id", "nimetus");
            ViewData["viilatuupId"] = new SelectList(_context.viilatuupid, "Id", "nimetus");
            return View();
        }

        // POST: looms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nimi,suguId,tervis,suurus,viilatuupId,iseloomuId,tuupId,vanus")] loom loom)
        {
            /*if (ModelState.IsValid)
            {*/
                _context.Add(loom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            /*}
            ViewData["iseloomuId"] = new SelectList(_context.iseloomud, "Id", "nimetus", loom.iseloomuId);
            ViewData["suguId"] = new SelectList(_context.sugud, "Id", "nimetus", loom.suguId);
            ViewData["tuupId"] = new SelectList(_context.Set<tuup>(), "Id", "nimetus", loom.tuupId);
            ViewData["viilatuupId"] = new SelectList(_context.viilatuupid, "Id", "nimetus", loom.viilatuupId);
            return View(loom);*/
        }

        // GET: looms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.loomad == null)
            {
                return NotFound();
            }
            klient kl = null;
            foreach (klient kli in _context.kliendit)
            {
                if (User.Identity?.Name == kli.epost)
                {
                    kl = kli;
                }
            }
            if (kl == null || kl.loomId != id)
            {
                return RedirectToAction(nameof(Index));
            }
            var loom = await _context.loomad.FindAsync(id);
            if (loom == null)
            {
                return NotFound();
            }
            ViewData["iseloomuId"] = new SelectList(_context.iseloomud, "Id", "nimetus", loom.iseloomuId);
            ViewData["suguId"] = new SelectList(_context.sugud, "Id", "nimetus", loom.suguId);
            ViewData["tuupId"] = new SelectList(_context.Set<tuup>(), "Id", "nimetus", loom.tuupId);
            ViewData["viilatuupId"] = new SelectList(_context.viilatuupid, "Id", "nimetus", loom.viilatuupId);
            return View(loom);
        }

        // POST: looms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nimi,suguId,tervis,suurus,viilatuupId,iseloomuId,tuupId,vanus")] loom loom)
        {
            if (id != loom.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(loom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!loomExists(loom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}
            ViewData["iseloomuId"] = new SelectList(_context.iseloomud, "Id", "nimetus", loom.iseloomuId);
            ViewData["suguId"] = new SelectList(_context.sugud, "Id", "nimetus", loom.suguId);
            ViewData["tuupId"] = new SelectList(_context.Set<tuup>(), "Id", "nimetus", loom.tuupId);
            ViewData["viilatuupId"] = new SelectList(_context.viilatuupid, "Id", "nimetus", loom.viilatuupId);
            return View(loom);*/
        }

        // GET: looms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.loomad == null)
            {
                return NotFound();
            }
            klient kl = null;
            foreach (klient kli in _context.kliendit)
            {
                if (User.Identity?.Name==kli.epost)
                {
                    kl = kli;
                }
            }
            if (kl == null  || kl.loomId != id)
            {
                return RedirectToAction(nameof(Index));
            }

            var loom = await _context.loomad
                .Include(l => l.iseloomu)
                .Include(l => l.sugu)
                .Include(l => l.tuup)
                .Include(l => l.viilatüüp)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loom == null)
            {
                return NotFound();
            }

            return View(loom);
        }

        // POST: looms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.loomad == null)
            {
                return Problem("Entity set 'ApplicationContext.loomad'  is null.");
            }
            var loom = await _context.loomad.FindAsync(id);
            if (loom != null)
            {
                _context.loomad.Remove(loom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool loomExists(int id)
        {
          return _context.loomad.Any(e => e.Id == id);
        }
    }
}
