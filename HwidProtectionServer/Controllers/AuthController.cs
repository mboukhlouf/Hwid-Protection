using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HwidProtectionServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using HwidProtectionServer.Models;
using HwidProtectionServer.ViewModels;

namespace HwidProtectionServer.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: Login
        public ActionResult Login()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new BaseViewModel { User = null });
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            User userFound =
                UsersRepository.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (userFound != null)
            {
                HttpContext.Session.SetObject("user", userFound);
                RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }

        // GET: Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
    }
}
