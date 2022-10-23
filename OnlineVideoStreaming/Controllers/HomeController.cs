using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineVideoStreaming.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Controllers
{
    public class HomeController : Controller
    {
       /* private readonly ILogger<HomeController> _logger;*/
        private readonly IUserRepository _userRepo;
        public HomeController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SubmitLogin(User user)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _userRepo.GetUser(user.Email, user.Password);
                if (existingUser == null)
                {
                    ViewData["Error"] = "email or password is incorrect, please enter wisely";
                    return View("Login");
                }
                
                
            }
            return View("Home");
        }

        public IActionResult Signin()
        {
            //ViewData["Error"] = "is this visible?";
            return View();
        }

        [HttpPost]
        public IActionResult SubmitSignin(User user)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _userRepo.GetExistingUser(user.Email);
                if(existingUser != null)
                {
                    ViewData["Error"] = "User with given email already registered";
                    return View("Signin");
                }
                else
                {
                    User newUser = _userRepo.Add(user);
                   
                }
            }
            return View("Home");
        }

        public IActionResult Privacy()
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
