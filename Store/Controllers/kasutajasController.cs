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
    public class kasutajasController : Controller
    {
        private readonly ApplicationContext _context;

        public kasutajasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: kasutajas
        public async Task<IActionResult> Index()
        {
              return View(await _context.kasutajad.ToListAsync());
        }

        // GET: kasutajas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kasutajad == null)
            {
                return NotFound();
            }

            var kasutaja = await _context.kasutajad
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kasutaja == null)
            {
                return NotFound();
            }

            return View(kasutaja);
        }

        // GET: kasutajas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: kasutajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nimi,Perenimi,telefon,epost,istenindaja,salasona")] kasutaja kasutaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kasutaja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kasutaja);
        }

        // GET: kasutajas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kasutajad == null)
            {
                return NotFound();
            }

            var kasutaja = await _context.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound();
            }
            return View(kasutaja);
        }

        // POST: kasutajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nimi,Perenimi,telefon,epost,istenindaja,salasona")] kasutaja kasutaja)
        {
            if (id != kasutaja.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kasutaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!kasutajaExists(kasutaja.ID))
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
            return View(kasutaja);
        }

        // GET: kasutajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kasutajad == null)
            {
                return NotFound();
            }

            var kasutaja = await _context.kasutajad
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kasutaja == null)
            {
                return NotFound();
            }

            return View(kasutaja);
        }

        // POST: kasutajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kasutajad == null)
            {
                return Problem("Entity set 'ApplicationContext.kasutajad'  is null.");
            }
            var kasutaja = await _context.kasutajad.FindAsync(id);
            if (kasutaja != null)
            {
                _context.kasutajad.Remove(kasutaja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool kasutajaExists(int id)
        {
          return _context.kasutajad.Any(e => e.ID == id);
        }
    }
}
