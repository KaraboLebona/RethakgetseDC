using DayCareProject.Data;
using DayCareProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using DayCareProject.Services;

namespace DayCareProject.Controllers
{
    public class ParentController : Controller
    {
        private readonly DayCareContext _context;

        public ParentController(DayCareContext context)
        {
            _context = context;
        }

        // Default Parent page (optional)
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Parent/Apply
        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        // POST: /Parent/Apply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(ChildApplication application)
        {
            if (ModelState.IsValid)
            {
                // Save to DB
                _context.ChildApplications.Add(application);
                _context.SaveChanges();

                // Confirmation message
                TempData["Success"] = "Application submitted successfully!";
                return RedirectToAction("Apply");
            }

            return View(application);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitApplication(ChildApplication model)
        {
            if (ModelState.IsValid)
            {
                _context.ChildApplications.Add(model);
                await _context.SaveChangesAsync();

                //var emailService = new EmailService(); // create instance
                //await emailService.SendApplicationEmailAsync(model); // call the method

                return RedirectToAction("Success");
            }
            return View(model);
        }

    }
}
