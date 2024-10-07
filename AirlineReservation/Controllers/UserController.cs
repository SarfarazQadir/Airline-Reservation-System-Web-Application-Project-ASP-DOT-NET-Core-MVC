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
                ViewBag.checkSession = HttpContext.Session.GetString("UserSession");
                return View(data);
            }
        }
        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile UserImage, User user)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "UserImages", UserImage.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                UserImage.CopyTo(fs);
            }
            user.UserImage = UserImage.FileName;
            _mycontext.Users.Update(user);
            _mycontext.SaveChanges();
            return RedirectToAction("UserProfile");
        }
        [HttpPost]
        public IActionResult UpdateProfile(User user)
        {
            _mycontext.Users.Update(user);
            _mycontext.SaveChanges();
            return RedirectToAction("UserProfile");
        }
        public IActionResult Index()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("UserSession");
            return View(_mycontext.Flights.ToList());
        }
        public IActionResult Contact()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("UserSession");
            return View();
        }
        public IActionResult About()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("UserSession");
            return View();
        }
        
    }
}
