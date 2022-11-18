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
    public class mastersController : Controller
    {
        private readonly ApplicationContext _context;

        public mastersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: masters
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.teenindajad.Include(m => m.keelid);
            return View(await applicationContext.ToListAsync());
        }

        // GET: masters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.teenindajad == null)
            {
                return NotFound();
            }
            master kl = (master)_context.teenindajad.Find(id);
            if (User.Identity?.Name != kl.epost)
            {
                return RedirectToAction(nameof(Index));
            }
            var master = await _context.teenindajad
                .Include(m => m.keelid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // GET: masters/Create
        public IActionResult Create()
        {
            ViewData["keelId"] = new SelectList(_context.keelid, "Id", "nimetus");
            return View();
        }

        // POST: masters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nimi,Perenimi,telefon,epost,keelId")] master master)
        {
                _context.Add(master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: masters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.teenindajad == null)
            {
                return NotFound();
            }
            master kl = (master)_context.teenindajad.Find(id);
            if (User.Identity?.Name != kl.epost)
            {
                return RedirectToAction(nameof(Index));
            }
            var master = await _context.teenindajad.FindAsync(id);
            if (master == null)
            {
                return NotFound();
            }
            ViewData["keelId"] = new SelectList(_context.keelid, "Id", "nimetus", master.keelId);
            return View(master);
        }

        // POST: masters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nimi,Perenimi,telefon,epost,keelId")] master master)
        {
            if (id != master.Id)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!masterExists(master.Id))
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

        // GET: masters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            master kl = (master)_context.teenindajad.Find(id);
            if (User.Identity?.Name != kl.epost)
            {
                return RedirectToAction(nameof(Index));
            }
            if (id == null || _context.teenindajad == null)
            {
                return NotFound();
            }

            var master = await _context.teenindajad
                .Include(m => m.keelid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // POST: masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.teenindajad == null)
            {
                return Problem("Entity set 'ApplicationContext.teenindajad'  is null.");
            }
            var master = await _context.teenindajad.FindAsync(id);
            if (master != null)
            {
                _context.teenindajad.Remove(master);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool masterExists(int id)
        {
          return _context.teenindajad.Any(e => e.Id == id);
        }
    }
}
