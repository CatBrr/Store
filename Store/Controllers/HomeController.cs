using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System.Diagnostics;
using Store;
using Store.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public void database()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                keel kel = new keel();
                kel.kategoria = "A1";
                kel.nimetus = "vene keel";
                db.keelid.Add(kel);
                db.SaveChanges();
            };

        }
        public IActionResult Index()
        {
            database();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}