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
    public class klientsController : Controller
    {
        private readonly ApplicationContext _context;

        public klientsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: klients
        public async Task<IActionResult> Index()
        {
              return View(await _context.kliendit.ToListAsync());
        }

        // GET: klients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kliendit == null)
            {
                return NotFound();
            }

            var klient = await _context.kliendit
                .FirstOrDefaultAsync(m => m.ID == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: klients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: klients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nimi,Perenimi,telefon,epost,aeg")] klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: klients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kliendit == null)
            {
                return NotFound();
            }

            var klient = await _context.kliendit.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            return View(klient);
        }

        // POST: klients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nimi,Perenimi,telefon,epost,aeg")] klient klient)
        {
            if (id != klient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!klientExists(klient.ID))
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
            return View(klient);
        }

        // GET: klients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kliendit == null)
            {
                return NotFound();
            }

            var klient = await _context.kliendit
                .FirstOrDefaultAsync(m => m.ID == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kliendit == null)
            {
                return Problem("Entity set 'ApplicationContext.kliendit'  is null.");
            }
            var klient = await _context.kliendit.FindAsync(id);
            if (klient != null)
            {
                _context.kliendit.Remove(klient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool klientExists(int id)
        {
          return _context.kliendit.Any(e => e.ID == id);
        }
    }
}
