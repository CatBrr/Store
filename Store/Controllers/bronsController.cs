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
    public class bronsController : Controller
    {
        private readonly ApplicationContext _context;

        public bronsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: brons
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.bronid.Include(b => b.klient).Include(b => b.loomad).Include(b => b.master).Include(b => b.teenused);
            return View(await applicationContext.ToListAsync());
        }

        // GET: brons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bronid == null)
            {
                return NotFound();
            }

            var bron = await _context.bronid
                .Include(b => b.klient)
                .Include(b => b.loomad)
                .Include(b => b.master)
                .Include(b => b.teenused)
                .FirstOrDefaultAsync(m => m.Id == id);
            loom loom = _context.loomad.Find(bron.loomId);
            sugu sugu = _context.sugud.Find(loom.suguId);
            viilatuup viilatuup= _context.viilatuupid.Find(loom.viilatuupId);
            iseloomu iseloomu = _context.iseloomud.Find(loom.iseloomuId);
            klient klient = _context.kliendit.Find(bron.klientId);
            master master = _context.teenindajad.Find(bron.masterId);
            ViewData["Nimi"] = loom.Nimi;
            ViewData["sugu"] = sugu.nimetus;
            ViewData["viilatuup"] = viilatuup.nimetus;
            ViewData["iseloomu"] = iseloomu.nimetus;
            ViewData["suurus"] = loom.suurus+" kg";
            ViewData["tervis"] = loom.tervis+"/10";
            ViewData["vanus"] = loom.vanus + " aastat vana";
            ViewData["klient"] = klient.Nimi + " " + klient.Perenimi;
            ViewData["epost"] = klient.epost;

            if (bron == null)
            {
                return NotFound();
            }

            return View(bron);
        }
        public IActionResult Bronered(bron bron)
        {
            ApplicationContext db = new ApplicationContext();
            teenust tennust = null;
            master master= null;
            klient klient = null;
            loom loom = null;
            foreach (teenust ten in db.teenused)
            {
                if (ten.Id == bron.teenustId)
                {
                    tennust = ten;
                    foreach (master mas in db.teenindajad)
                    {
                        if (mas.Id==bron.masterId)
                        {
                            master = mas;
                            foreach (loom lo in db.loomad)
                            {
                                if (lo.Id==bron.loomId)
                                {
                                    loom = lo;
                                    foreach (klient kl in db.kliendit)
                                    {
                                        if (kl.Id==bron.klientId)
                                        {
                                            klient = kl;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ViewData["teenust"] = tennust.nimetus;
            ViewData["hind"] = tennust.hind;
            ViewData["teenindajaNimi"] = master.Nimi+" "+master.Perenimi;
            ViewData["teenindajaEmail"] = master.epost;
            ViewData["teenindajaTelephone"] = master.telefon;
            ViewData["klientNimi"] = klient.Nimi;
            ViewData["klientPerenimi"] = klient.Perenimi;
            ViewData["loomNimi"] = loom.Nimi;
            ViewData["bronAeg"] = bron.aeg;
            return View();
            
        }
            // GET: brons/Create
            public IActionResult Create()
        {
            ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
            ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
            ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
            ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
            return View();
        }

        // POST: brons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,klientId,loomId,masterId,aeg,teenustId")] bron bron)
        {

            _context.Add(bron);
            await _context.SaveChangesAsync();
            bron newbron = new bron();
            newbron.Id= bron.Id;
            newbron.teenustId = bron.teenustId;
            newbron.masterId = bron.masterId;
            newbron.klientId = bron.klientId;
            newbron.loomId = bron.loomId;
            newbron.aeg = bron.aeg;
            return RedirectToAction(nameof(Bronered), newbron);

        }

        // GET: brons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.bronid == null)
            {
                return NotFound();
            }

            var bron = await _context.bronid.FindAsync(id);
            if (bron == null)
            {
                return NotFound();
            }
            ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
            ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
            ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
            ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
            return View(bron);
        }

        // POST: brons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,klientId,loomId,masterId,aeg,teenustId")] bron bron)
        {
            if (id != bron.Id)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(bron);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!bronExists(bron.Id))
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

        // GET: brons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bronid == null)
            {
                return NotFound();
            }

            var bron = await _context.bronid
                .Include(b => b.klient)
                .Include(b => b.loomad)
                .Include(b => b.master)
                .Include(b => b.teenused)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bron == null)
            {
                return NotFound();
            }

            return View(bron);
        }

        // POST: brons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.bronid == null)
            {
                return Problem("Entity set 'ApplicationContext.bronid'  is null.");
            }
            var bron = await _context.bronid.FindAsync(id);
            if (bron != null)
            {
                _context.bronid.Remove(bron);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool bronExists(int id)
        {
          return _context.bronid.Any(e => e.Id == id);
        }
    }
}
