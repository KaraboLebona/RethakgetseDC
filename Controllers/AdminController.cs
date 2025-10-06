using DayCareProject.Data;
using DayCareProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayCareProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly DayCareContext _context;
        private readonly PasswordHasher<Admin> _passwordHasher;
        private readonly IWebHostEnvironment _env;

        public AdminController(DayCareContext context, IWebHostEnvironment env)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = new PasswordHasher<Admin>();
            _env = env;
        }

        // Existing Register, Login, Dashboard, Logout code here...
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "All fields are required!";
                return View();
            }

            // Check if username or email already exists
            if (await _context.Admins.AnyAsync(a => a.Username == username || a.Email == email))
            {
                ViewBag.Error = "Username or Email already exists!";
                return View();
            }

            // Create new admin and hash password
            var admin = new Admin
            {
                Username = username,
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password)
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Admin registered successfully! You can now login.";
            return RedirectToAction("Login");
        }
        // GET: Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and Password are required!";
                return View();
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);
            if (admin == null)
            {
                ViewBag.Error = "Invalid username or password!";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                HttpContext.Session.SetString("AdminUsername", admin.Username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password!";
            return View();
        }


        // GET: Admin/UploadEvent
        [HttpGet]
        public IActionResult UploadEvent()
        {
            if (!HttpContext.Session.Keys.Contains("AdminId"))
                return RedirectToAction("Login");

            return View();
        }

        // GET: Admin/UploadEvent
        //[HttpGet]
        //public IActionResult UploadEvent()
        //{
        //    if (!HttpContext.Session.Keys.Contains("AdminId"))
        //        return RedirectToAction("Login");

        //    return View();
        //}

        // POST: Admin/UploadEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadEvent(IFormFile image)
        {
            if (!HttpContext.Session.Keys.Contains("AdminId"))
                return RedirectToAction("Login");

            if (image == null || image.Length == 0)
            {
                ViewBag.Error = "Please select an image to upload!";
                return View();
            }

            var uploads = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            var newEvent = new Event
            {
                PhotoPath = "/images/" + fileName
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Event photo uploaded successfully!";
            return RedirectToAction("Dashboard");
        }
        //public IActionResult Dashboard()
        //{
        //    if (!HttpContext.Session.Keys.Contains("AdminId"))
        //        return RedirectToAction("Login");

        //    ViewBag.AdminUsername = HttpContext.Session.GetString("AdminUsername");
        //    var events = _context.Events.OrderByDescending(e => e.DateCreated).ToList();
        //    return View(events); // Pass list of events to the view
        //}
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new DashboardViewModel
            {
                Events = await _context.Events
                                       .OrderByDescending(e => e.DateCreated)
                                       .ToListAsync(),
                Applications = await _context.ChildApplications
                                             .OrderByDescending(a => a.DateSubmitted)
                                             .ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();

            // ✅ Redirect to About page (HomeController -> About action)
            return RedirectToAction("About", "Home");
        }




    }
}
