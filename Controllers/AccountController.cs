using DayCareProject.Models;
using DayCareProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace DayCareProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly DayCareContext _context;
        private readonly PasswordHasher<Admin> _passwordHasher;

        public AccountController(DayCareContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Admin>();
        }

        // GET: Admin Registration
        public IActionResult Register()
        {
            return View();
        }

        // POST: Admin Registration
        [HttpPost]
        public IActionResult Register(Admin admin, string password)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                var existingAdmin = _context.Admins.FirstOrDefault(a => a.Username == admin.Username);
                if (existingAdmin != null)
                {
                    ViewBag.Error = "Username already exists!";
                    return View();
                }

                // Hash the password
                admin.PasswordHash = _passwordHasher.HashPassword(admin, password);

                _context.Admins.Add(admin);
                _context.SaveChanges();
                ViewBag.Message = "Registration successful! You can now login.";
                return RedirectToAction("Login");
            }
            return View(admin);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username);
            if (admin != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                    HttpContext.Session.SetString("AdminUsername", admin.Username);
                    return RedirectToAction("Index", "Admin");
                }
            }
            ViewBag.Error = "Invalid username or password!";
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
