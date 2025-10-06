using DayCareProject.Data;
using DayCareProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DayCareProject.Controllers
{
    public class FacilityController : Controller
    {
        private readonly DayCareContext _context;
        private readonly IWebHostEnvironment _env;

        public FacilityController(DayCareContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // List Facilities
        public IActionResult Index()
        {
            var facilities = _context.Facilities.ToList();
            return View(facilities);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Facility facility, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uploads = Path.Combine(_env.WebRootPath, "images/facilities");
                    Directory.CreateDirectory(uploads);
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    facility.ImagePath = "/images/facilities/" + fileName;
                }

                _context.Facilities.Add(facility);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var facility = _context.Facilities.Find(id);
            if (facility == null) return NotFound();
            return View(facility);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var facility = _context.Facilities.Find(id);
            if (facility != null)
            {
                _context.Facilities.Remove(facility);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
