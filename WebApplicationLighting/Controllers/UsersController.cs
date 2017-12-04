using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationLighting.Controllers
{
    public class UsersController : Controller
    {
        private readonly LightingContext _context;

        public UsersController(LightingContext context)
        {
            //StartData.Initialize(context);
            _context = context;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // Get method for registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        //Post method for registration
        [HttpPost]
        public IActionResult Registration(string name, string surname, string email, string password)
        {
            if (_context.User.Where(u => u.Email.Equals(email)).Count() == 0)
            {
                _context.User.Add(new User { Name = name, Email = email, IsAdmin = false, Password = password });
                _context.SaveChanges();
                return View("Login");
            }
            else
            {
                ViewBag.Message = "User with this email exists, use other email or login with correct data!";
                return View("Error");
            }
        }

        //Get method for login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Method for user authentificate
        private void Authentificate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, (bool)user.IsAdmin ? "admin" : "user")
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //Post method for login
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            User user = _context.User.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                Authentificate(user);
                //HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                return RedirectToAction("Index", "StreetLightings");
            }
            else
            {
                ViewBag.Message = "No thats user. Try again or choose registration!";
                return View("Error");
            }
        }

        //Get method for login out
        [Authorize(Roles = "admin, user")]
        public IActionResult Logout()
        {
            // HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}

