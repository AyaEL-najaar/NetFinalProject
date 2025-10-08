using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetFinalProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username)
        {
            var validAdmins = new List<string> { "admin1", "admin2" };

            if (validAdmins.Contains(username))
            {
                HttpContext.Session.SetString("LoggedIn", username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoggedIn");
            return RedirectToAction("Login");
        }
    }
}
