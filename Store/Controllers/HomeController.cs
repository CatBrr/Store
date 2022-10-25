using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System.Diagnostics;
using Store;
using Store.Data;

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
                kasutaja kas = new kasutaja();
                kas.Nimi = "test";
                kas.telefon = "+372534653";
                kas.salasona = "test";
                kas.istenindaja = false;
                kas.Perenimi = "test";
                kas.epost = "test@gmail.com";
                db.kasutajad.Add(kas);
                db.SaveChanges();
            };

        }
        public IActionResult Index()
        {
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