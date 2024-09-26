﻿using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    public class AdminController : Controller
    {
        private Mycontext _mycontext;
        private IWebHostEnvironment _env;
        public AdminController(Mycontext mycontext, IWebHostEnvironment env)
        {
            _env = env;
            _mycontext = mycontext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string adminEmail,string adminPassword)
        {
            var row = _mycontext.tbl_admin.FirstOrDefault(a => a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.admin_id.ToString());
                return View("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return View();
            } 
        }
        public IActionResult Index()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("login");
        }
        public IActionResult Profile()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                var adm = HttpContext.Session.GetString("admin_session");
                var data = _mycontext.tbl_admin.Where(a => a.admin_id == int.Parse(adm)).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("login");
            }

        }
        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _mycontext.tbl_admin.Update(admin);
            _mycontext.SaveChanges();
            return RedirectToAction("profile");
        }
        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", admin_image.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                admin_image.CopyTo(fs);
            }
            admin.admin_image = admin_image.FileName;
            _mycontext.tbl_admin.Update(admin);
            _mycontext.SaveChanges();
            return RedirectToAction("profile");
        }
        public IActionResult FetchUser()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.Users.ToList());

            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult DetailUser(int id)
        {
            return View(_mycontext.Users.FirstOrDefault(c => c.UserId == id));
        }
        public IActionResult UpdateUser(int id)
        {
            return View(_mycontext.Users.Find(id));
        }
        [HttpPost]
        public IActionResult UpdateUser(User user, IFormFile user_image)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "UserImages", user_image.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                user_image.CopyTo(fs);
            }
            user.UserImage = user_image.FileName;
            _mycontext.Users.Update(user);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchUser");
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _mycontext.Users.Find(id);
            _mycontext.Users.Remove(user);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchUser");
        }
    }
}