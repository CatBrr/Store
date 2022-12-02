using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using EASendMail;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Data;
using Store.Models;
using Attachment = System.Net.Mail.Attachment;
using MailAddress = System.Net.Mail.MailAddress;
using SmtpClient = System.Net.Mail.SmtpClient;

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
            public async Task<IActionResult> Service(int? id)
        {
            if (id == null || _context.bronid == null) { return NotFound(); };

            bron current_bron = await _context.bronid
                .Include(b => b.klient)
                .Include(b => b.loomad)
                .Include(b => b.master)
                .Include(b => b.teenused)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (current_bron.aeg.Date!=DateTime.Now && current_bron.aeg.Hour != DateTime.Now.Hour)
            {
                return RedirectToAction(nameof(Index));
            }
            master master = _context.teenindajad.Find(current_bron.masterId);
            klient klient = _context.kliendit.Find(current_bron.klientId);
            loom loom = _context.loomad.Find(current_bron.loomId);
            Random rnd = new Random();
            int rndnum = rnd.Next(1, 2);
            rndnum = rnd.Next(1, 3);
            rndnum = rnd.Next(1, 3);
            if (current_bron.teenustId==2)
            {
                ViewData["image2"] = "cut_cat.gif";
            }
            else if (current_bron.teenustId == 1)
            {
                ViewData["image2"] = "cut_bog.gif";
            }
            else if (current_bron.teenustId == 3)
            {
                ViewData["image2"] = "cut_claws.gif";
            }
            else if (current_bron.teenustId == 10)
            {
                if (loom.tuupId == 2)
                {
                    ViewData["image2"] = "spa_dog.gif";
                }
                else if (loom.tuupId == 1)
                {
                    ViewData["image2"] = "cat_spa.gif";
                }
            }
            else
            {
                if (loom.tuupId == 2)
                {
                    ViewData["image2"] = "cleaning_dog.gif";
                }
                else if (loom.tuupId == 1)
                {
                    ViewData["image2"] = "cleaning_cat.gif";
                }
            }
            if (loom.tuupId==2)
            {
                if (loom.iseloomuId ==6 && rndnum == 1)
                {
                    ViewData["image"] = "dog_bad.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli väga halb kutsikas:(";
                    ViewData["hind"] = current_bron.teenused.hind+12;
                }
                else if (loom.iseloomuId == 6 && rndnum == 2)
                {
                    ViewData["image"] = "neutral_dog.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna halb, kuid saime sellega hakkama";
                    ViewData["hind"] = current_bron.teenused.hind + 3;
                }
                else if (loom.iseloomuId == 5 && rndnum == 1)
                {
                    ViewData["image"] = "neutral_dog.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid saime sellega hakkama";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 5 && rndnum == 2)
                {
                    ViewData["image"] = "dog_bad.gif";
                    ViewData["Result"] = "Teie lemmikloom oli väga närvis ja tegi peremehele haiget";
                    ViewData["hind"] = current_bron.teenused.hind+10;
                }
                else if (loom.iseloomuId == 4 && rndnum == 1)
                {
                    ViewData["image"] = "neutral_dog.gif";
                    ViewData["Result"] = "Your pet was pretty nervouse but we coped with it";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 4 && rndnum == 2)
                {
                    ViewData["image"] = "dog_bad.gif";
                    ViewData["Result"] = "Teie lemmikloom oli tõesti närvis, kuid midagi ei juhtu";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 3 && rndnum == 1)
                {
                    ViewData["image"] = "neutral_dog.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid rahunege siis maha";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 3 && rndnum == 2)
                {
                    ViewData["image"] = "good_puppy.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli hea kutsikas :)";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 2 && rndnum == 1)
                {
                    ViewData["image"] = "good_puppy.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli hea kutsikas :)";
                    ViewData["hind"] = current_bron.teenused.hind;

                }
                else if (loom.iseloomuId == 2 && rndnum == 2)
                {
                    ViewData["image"] = "neutral_dog.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid rahunege siis maha";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 1)
                {
                    ViewData["image"] = "good_puppy.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli hea kutsikas :)";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                

            }
            else if (loom.tuupId == 1)
            {
                if (loom.iseloomuId == 6 && rndnum == 1)
                {
                    ViewData["image"] = "bad_cat.gif";
                    ViewData["Result"] = "Su lemmikloom oli väga halb kiisu:(";
                    ViewData["hind"] = current_bron.teenused.hind + 12;
                }
                else if (loom.iseloomuId == 6 && rndnum == 2)
                {
                    ViewData["image"] = "neutral_kitty.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna vihane, kuid saime sellega hakkama";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 5 && rndnum == 1)
                {
                    ViewData["image"] = "neutral_kitty.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid midagi ei juhtu";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 5 && rndnum == 2)
                {
                    ViewData["image"] = "bad_cat.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli väga halb kiisu:( ja lõhkus midagi salongis";
                    ViewData["hind"] = current_bron.teenused.hind + 10;
                }
                else if (loom.iseloomuId == 4 && rndnum == 1)
                {
                    ViewData["image"] = "neutral_kitty.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid rahunege siis maha";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 4 && rndnum == 2)
                {
                    ViewData["image"] = "bad_cat.gif";
                    ViewData["Result"] = "Sinu lemmikloom oli väga halb kiisu:( ja lõhkus midagi salongis";
                    ViewData["hind"] = current_bron.teenused.hind + 10;
                }
                else if (loom.iseloomuId == 3 && rndnum == 1)
                {
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid ta sai sellega hakkama!";
                    ViewData["hind"] = current_bron.teenused.hind;
                    ViewData["image"] = "neutral_kitty.gif";
                }
                else if (loom.iseloomuId == 3 && rndnum == 2)
                {
                    ViewData["image"] = "good_kitty.gif";
                    ViewData["Result"] = "Su lemmikloom oli tubli kiisu :)";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 2 && rndnum == 1)
                {
                    ViewData["image"] = "good_kitty.gif";
                    ViewData["Result"] = "Su lemmikloom oli tubli kiisu :)";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 2 && rndnum == 2)
                {
                    ViewData["image"] = "neutral_kitty.gif";
                    ViewData["Result"] = "Teie lemmikloom oli üsna närvis, kuid saime sellega hakkama";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
                else if (loom.iseloomuId == 1)
                {
                    ViewData["image"] = "good_kitty.gif";
                    ViewData["Result"] = "Su lemmikloom oli tubli kiisu :)";
                    ViewData["hind"] = current_bron.teenused.hind;
                }
            }
            if (loom.tervis <= 5 && rnd.Next(1, 4) == 3)
            {
                ViewData["image"] = "furr_bugs.gif";
                ViewData["Result"] = "Teie lemmikloomal on kirbud! Teie broneering on tühistatud";
                ViewData["hind"] = 0;
            }
            if (User.Identity?.Name == klient.epost)
            {
                return View(current_bron);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: brons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bronid == null)
            {
                return NotFound();
            }
            bron bronm = (bron)_context.bronid.Find(id);
            klient klient = (klient)_context.kliendit.Find(bronm.klientId);
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
            master master = _context.teenindajad.Find(bron.masterId);
            ViewData["Nimi"] = loom.Nimi;
            ViewData["sugu"] = sugu.nimetus;
            ViewData["viilatuup"] = viilatuup.nimetus;
            ViewData["iseloomu"] = iseloomu.nimetus;
            ViewData["suurus"] = loom.suurus+" kg";
            ViewData["tervis"] = loom.tervis + "/10";
            ViewData["vanus"] = loom.vanus + " aastat vana";
            ViewData["klient"] = klient.Nimi + " " + klient.Perenimi;
            ViewData["epost"] = klient.epost;

            if (bron == null)
            {
                return NotFound();
            }
            if (User.Identity?.Name == master.epost)
            {
                return View(bron);

            }
            else if (User.Identity?.Name == klient.epost)
            {
                return View(bron);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
        public int CheckHours(teenust tennust)
        {
            int hours_to_do = 0;
            if (tennust.nimetus == "soengkass" || tennust.nimetus == "soengkoer")
            {
                hours_to_do = 2;
            }
            else if (tennust.nimetus == "küüniste lõikamine")
            {
                hours_to_do = 1;
            }
            else if (tennust.nimetus == "küüniste lõikamine")
            {
                hours_to_do = 1;
            }
            else if (tennust.nimetus == "korrastamine")
            {
                hours_to_do = 2;
            }
            else if (tennust.nimetus == "villa värvimine")
            {
                hours_to_do = 1;
            }
            else if (tennust.nimetus == "puntrate välja kammimine")
            {
                hours_to_do = 2;
            }
            else if (tennust.nimetus == "soengkass,küüniste lõikamine,puntrate välja kammimine")
            {
                hours_to_do = 5;
            }
            else if (tennust.nimetus == "soengkoer,küüniste lõikamine,puntrate välja kammimine")
            {
                hours_to_do = 5;
            }
            else if (tennust.nimetus == "soengkoer,küüniste lõikamine,puntrate välja kammimine")
            {
                hours_to_do = 5;
            }
            else if (tennust.nimetus == "täielik korrastamine")
            {
                hours_to_do = 3;
            }
            else if (tennust.nimetus == "SPA protseduurid")
            {
                hours_to_do = 3;
            }
            return hours_to_do;
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
            
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = klient.epost;
            mailRequest.Body = $"Sinu bronnerida aeg on:{bron.aeg} \nteenindaja: {master.Nimi} {master.Perenimi}\nteenindaja kontacktid:\n{master.epost} {master.telefon}\n teenust:{tennust.nimetus} hind: {tennust.hind} €\nTäname meie teenuste ostmise eest! Head päeva! \n\nⒸPetCare";
            mailRequest.Subject = "Broonerida teenust";
            int hours_to_do = CheckHours(tennust);
            var calendar = new Calendar();

            var icalEvent = new CalendarEvent
            {
                Class = "PUBLIC",
                Summary = mailRequest.Subject,
                Description = mailRequest.Body,
                // 15th of march 2021 12 o'clock.
                Start = new CalDateTime(bron.aeg),
                End = new CalDateTime(bron.aeg.AddHours(hours_to_do)),
                Created = new CalDateTime(DateTime.Now),
                Organizer=new Organizer("petcare_12@hotmail.com")
            };

            calendar.Events.Add(icalEvent);

            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);
            var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
            MemoryStream ms = new MemoryStream(bytesCalendar);
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(ms, "bron.ics", "text/calendar");
            MailMessage message = new MailMessage();
            message.To.Add(mailRequest.ToEmail);
            message.From = new MailAddress("petcare_12@hotmail.com", "Pet Care");
            message.Subject = mailRequest.Subject;
            message.Body = mailRequest.Body;
            message.Attachments.Add(attachment);
            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("petcare_12@hotmail.com", "Testcat__123"),
                EnableSsl = true,
            };
            smtpClient.SendMailAsync(message);

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
            klient klient = _context.kliendit.Find(bron.klientId);

            if (User.Identity?.Name != klient.epost)
            {
                ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
                ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
                ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
                ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
                return View(bron);
            }
            else if (klient.loomId != bron.loomId)
            {
                ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
                ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
                ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
                ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
                return View(bron);
            }
            else
            {
                teenust tennust = _context.teenused.Find(bron.teenustId);
                int hours_to_do = CheckHours(tennust);
                foreach (var br in _context.bronid)
                {
                    if (bron.aeg.AddHours(hours_to_do) == br.aeg)
                    {
                        ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
                        ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
                        ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
                        ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
                        return View(bron);
                    }
                }     
                
                _context.Add(bron);
                await _context.SaveChangesAsync();
                bron newbron = new bron();
                newbron.Id = bron.Id;
                newbron.teenustId = bron.teenustId;
                newbron.masterId = bron.masterId;
                newbron.klientId = bron.klientId;
                newbron.loomId = bron.loomId;
                newbron.aeg = bron.aeg;
                return RedirectToAction(nameof(Bronered), newbron);
            }
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
            bron bronm = (bron)_context.bronid.Find(id);
            klient klient= (klient)_context.kliendit.Find(bronm.klientId);
            master master = (master)_context.teenindajad.Find(bronm.masterId);  
            ViewData["klientId"] = new SelectList(_context.kliendit, "Id", "Nimi");
            ViewData["loomId"] = new SelectList(_context.loomad, "Id", "Nimi");
            ViewData["masterId"] = new SelectList(_context.teenindajad, "Id", "Nimi");
            ViewData["teenustId"] = new SelectList(_context.teenused, "Id", "nimetus");
            if (User.Identity?.Name == master.epost)
            {
                return View(bron);

            }
            else if (User.Identity?.Name == klient.epost)
            {
                return View(bron);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
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
                klient klient = (klient)_context.kliendit.Find(bron.klientId);
                master master = (master)_context.teenindajad.Find(bron.masterId);
                teenust tennust = (teenust)_context.teenused.Find(bron.teenustId);
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = klient.epost;
                mailRequest.Body = $"Sinu bronnerida aeg on:{bron.aeg} \nteenindaja: {master.Nimi} {master.Perenimi}\nteenindaja kontacktid:\n{master.epost} {master.telefon}\n teenust:{tennust.nimetus} hind: {tennust.hind} €\n Head päeva! \n\nⒸPetCare";
                mailRequest.Subject = "Sinu Broonerida on muutatud";
                int hours_to_do = CheckHours(tennust);
                var calendar = new Calendar();

                var icalEvent = new CalendarEvent
                {
                    Class = "PUBLIC",
                    Summary = mailRequest.Subject,
                    Description = mailRequest.Body,
                    // 15th of march 2021 12 o'clock.
                    Start = new CalDateTime(bron.aeg),
                    End = new CalDateTime(bron.aeg.AddHours(hours_to_do)),
                    Created = new CalDateTime(DateTime.Now),
                    Organizer = new Organizer("petcare_12@hotmail.com")
                };

                calendar.Events.Add(icalEvent);

                var serializer = new CalendarSerializer(new SerializationContext());
                var serializedCalendar = serializer.SerializeToString(calendar);
                var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
                MemoryStream ms = new MemoryStream(bytesCalendar);
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(ms, "bron.ics", "text/calendar");
                MailMessage message = new MailMessage();
                message.To.Add(mailRequest.ToEmail);
                message.From = new MailAddress("petcare_12@hotmail.com", "Pet Care");
                message.Subject = mailRequest.Subject;
                message.Body = mailRequest.Body;
                message.Attachments.Add(attachment);
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("petcare_12@hotmail.com", "Testcat__123"),
                    EnableSsl = true,
                };
                smtpClient.SendMailAsync(message);
                return RedirectToAction(nameof(Index));

        }

        // GET: brons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bronid == null)
            {
                return NotFound();
            }
            bron bronm = (bron)_context.bronid.Find(id);
            klient klient = (klient)_context.kliendit.Find(bronm.klientId);
            master master = (master)_context.teenindajad.Find(bronm.masterId);
            
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
            if (User.Identity?.Name == master.epost)
            {
                return View(bron);
                
            }
            else if (User.Identity?.Name == klient.epost)
            {
                return View(bron);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
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
            klient klient = (klient)_context.kliendit.Find(bron.klientId);
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = klient.epost;
            mailRequest.Body = $"Sinu bronnerida aeg on kustatud\n Head päeva! \n\nⒸPetCare";
            mailRequest.Subject = "Sinu Broonerida on kustatud";
            MailMessage message = new MailMessage();
            message.To.Add(mailRequest.ToEmail);
            message.From = new MailAddress("petcare_12@hotmail.com", "Pet Care");
            message.Subject = mailRequest.Subject;
            message.Body = mailRequest.Body;
            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("petcare_12@hotmail.com", "Testcat__123"),
                EnableSsl = true,
            };
            smtpClient.SendMailAsync(message);
            return RedirectToAction(nameof(Index));
        }

        private bool bronExists(int id)
        {
          return _context.bronid.Any(e => e.Id == id);
        }
    }
}
