using Microsoft.AspNetCore.Mvc;
using DayCareProject.Data;   // ?? Add this so it knows about DayCareContext
using System.Linq;           // ?? Add this for .ToList()

namespace DayCareSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DayCareContext _context;

        // ?? Constructor to inject the database context
        public HomeController(DayCareContext context)
        {
            _context = context;
        }

        // Landing / About page
        public IActionResult About()
        {
            return View();
        }

        // Facilities page
        public IActionResult Facilities()
        {
            var facilities = _context.Facilities.ToList();
            return View(facilities);
        }
    }
}
