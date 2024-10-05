using AirlineReservation.Migrations;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AirlineReservation.Controllers
{
    public class UserController : Controller
    {
        private Mycontext _mycontext;
        private IWebHostEnvironment _env;
        public UserController(Mycontext mycontext, IWebHostEnvironment env)
        {
            _mycontext = mycontext;
            _env = env;
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(string User_Email, string User_Password)
        {
            var user = _mycontext.Users.FirstOrDefault(u => u.EmailAddress == User_Email);
            if (user != null && user.Password == User_Password)
            {
                HttpContext.Session.SetString("UserSession",
                    user.UserId.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username Or Password";
                return View();
            }

        }
        public IActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRegister(User user)
        {
            _mycontext.Users.Add(user);
            _mycontext.SaveChanges();
            return RedirectToAction("UserLogin");
        }
        public IActionResult UserLogout()
        {
            HttpContext.Session.Remove("UserSession");
            return RedirectToAction("Index");
        }
        public IActionResult UserProfile()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                return RedirectToAction("UserLogin");
            }
            else {
                var Id = HttpContext.Session.GetString("UserSession");
                var data = _mycontext.Users.Where(u => u.UserId == int.Parse(Id)).ToList();
                return View(data);
            }
        }
        public IActionResult Index()
        {
            return View(_mycontext.Flights.ToList());
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        
    }
}
