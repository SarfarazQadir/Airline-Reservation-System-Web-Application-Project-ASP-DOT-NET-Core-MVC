using AirlineReservation.Models;
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
                return View(_mycontext.tbl_admin.ToList());
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
        public IActionResult UpdateUser(User user, IFormFile UserImage)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "UserImages", UserImage.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                UserImage.CopyTo(fs);
            } 
            user.UserImage = UserImage.FileName;
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
        public IActionResult FetchFlight()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.Flights.ToList());

            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult AddFlight()
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
        [HttpPost]

        public IActionResult AddFlight(Flight flight)
        {
            _mycontext.Flights.Add(flight);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchFlight");

        }

        public IActionResult UpdateFlight(int id)
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.Flights.Find(id));
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        public IActionResult UpdateFlight(Flight flight)
        {
            _mycontext.Flights.Update(flight);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchFlight");
        }

        public IActionResult DeleteFlight(int id)
        {
            var flight = _mycontext.Flights.Find(id);
            _mycontext.Flights.Remove(flight);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchFlight");
        }
        public IActionResult FetchReservation()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.Reservations.ToList());

            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult AddReservation()
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
        [HttpPost]

        public IActionResult AddReservation(Reservation reservation)
        {
            _mycontext.Reservations.Add(reservation);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchReservation");

        }

        public IActionResult UpdateReservation(int id)
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.Reservations.Find(id));
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        public IActionResult UpdateReservation(Reservation reservation)
        {
            _mycontext.Reservations.Update(reservation);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchReservation");
        }

        public IActionResult DeleteReservation(int id)
        {
            var reservation = _mycontext.Reservations.Find(id);
            _mycontext.Reservations.Remove(reservation);
            _mycontext.SaveChanges();
            return RedirectToAction("FetchReservation");
        }
        public IActionResult FetchTicket()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycontext.TicketStatuses.ToList());

            }
            else
            {
                return RedirectToAction("login");
            }
        }
    }
}
