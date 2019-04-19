using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DojoActivities.Models;

namespace DojoActivities.Controllers {
    public class HomeController : Controller {

        private DojoActivityContext dbContext;

        public HomeController (DojoActivityContext context) {
            dbContext = context;
        }

        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }

        [HttpGet]
        [Route ("register")]
        public IActionResult Register () {
            return View ("Register");
        }

        [HttpPost]
        [Route ("processregistration")]
        public IActionResult ProcessRegistration (User newUser) {
            if (ModelState.IsValid) {
                if (dbContext.Users.Any (u => u.Email == newUser.Email)) {
                    ModelState.AddModelError ("Email",
                        "Email already in use. Please log in.");
                    return View ("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                newUser.Password = Hasher.HashPassword (newUser, newUser.Password);
                dbContext.Users.Add (newUser);
                dbContext.SaveChanges ();
                User loggedUser = dbContext.Users.FirstOrDefault ((u => u.Email == newUser.Email));
                HttpContext.Session.SetInt32 ("logged", loggedUser.UserId);
                return RedirectToAction ("Dashboard");
            } else {
                return View ("Register");
            }
        }

        [HttpGet]
        [Route ("dashboard")]
        public IActionResult Dashboard () {
            int flag = CheckLogged();
            if (flag == 0) {
                return View ("Index");
            }
            User loggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("logged"));
            PopulateBag ();
            return View ("Dashboard", loggedUser);
        }

        [HttpGet]
        [Route ("login")]
        public IActionResult Login () {
            return View ("Login");
        }

        [HttpPost]
        [Route ("processlogin")]
        public IActionResult ProcessLogin (LogIn userSubmission) {
            if (ModelState.IsValid) {
                var userInDb = dbContext.Users.FirstOrDefault (u => u.Email == userSubmission.Email);
                if (userInDb == null) {
                    ModelState.AddModelError ("Email", "Invalid Email");
                    return View ("Login");
                }

                var hasher = new PasswordHasher<LogIn> ();
                var result = hasher.VerifyHashedPassword (userSubmission, userInDb.Password, userSubmission.Password);
                if (result == 0) {
                    ModelState.AddModelError ("Password", "Invalid Password");
                    return View ("Login");
                }
                User loggedUser = userInDb;
                HttpContext.Session.SetInt32 ("logged", loggedUser.UserId);
                return RedirectToAction ("Dashboard");
            } else {
                return View ("Login");
            }
        }

        [HttpGet]
        [Route ("newactiv")]
        public IActionResult NewActivity () {
            int flag = CheckLogged();
            if (flag == 0) {
                return View ("Index");
            }
            return View ("newactiv");
        }

        [HttpPost]
        [Route ("processactiv")]
        public IActionResult ProcessActiv (Activ NewActivity) {
            if (ModelState.IsValid) {
                if (NewActivity.ActivDate < DateTime.Now) {
                    TempData["alertMessage"] = "<p style='color:red;'>Date of Activ must be in the future.</p>";
                    return RedirectToAction ("NewActivity");
                }
                User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("logged"));
                NewActivity.Creator = loggedUser;
                NewActivity.UserId = loggedUser.UserId;
                dbContext.Activs.Add (NewActivity);
                dbContext.SaveChanges ();
                return RedirectToAction ("ViewActiv", new { ActivId = NewActivity.ActivId });
            }
            return View ("NewActivity");
        }

        [HttpGet]
        [Route ("viewActiv/{ActivId}")]
        public IActionResult ViewActiv (int ActivId) {
            int flag = CheckLogged();
            if (flag == 0) {
                return View ("Index");
            }
            Activ retrievedActiv = dbContext.Activs.FirstOrDefault (w => w.ActivId == ActivId);
            GetActivJoiners(ActivId);
            return View ("ViewActiv", retrievedActiv);
        }

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return View ("Index");
        }

        [HttpGet]
        [Route ("delete/{ActivId}")]
        public IActionResult DeleteActiv (int ActivId) {
            Activ retrievedActiv = dbContext.Activs.FirstOrDefault (w => w.ActivId == ActivId);
            dbContext.Activs.Remove (retrievedActiv);
            dbContext.SaveChanges ();
            PopulateBag ();
            return RedirectToAction ("Dashboard");
        }

        [HttpGet]
        [Route ("Join/{ActivId}")]
        public IActionResult JoinToActiv (int ActivId) {
            Activ retrievedActiv = dbContext.Activs.FirstOrDefault (w => w.ActivId == ActivId);
            User loggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("logged"));
            Join newJoin = new Join() {
                UserId = loggedUser.UserId,
                ActivId = retrievedActiv.ActivId,
                User = loggedUser,
                Activ = retrievedActiv,
            };
            dbContext.Joins.Add(newJoin);
            dbContext.SaveChanges ();
            PopulateBag ();
            return RedirectToAction ("Dashboard");
        }

        [HttpGet]
        [Route ("unJoin/{ActivId}")]
        public IActionResult UnJoinToActiv (int ActivId) {
            Activ retrievedActiv = dbContext.Activs.FirstOrDefault (w => w.ActivId == ActivId);
            User loggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("logged"));
            List<Join> retrievedJoins = dbContext.Joins
                .Where(r => r.ActivId == retrievedActiv.ActivId).ToList();
            Join retrievedJoin = retrievedJoins.FirstOrDefault(r => r.UserId == loggedUser.UserId);
            dbContext.Remove(retrievedJoin);
            dbContext.SaveChanges ();
            PopulateBag ();
            return RedirectToAction ("Dashboard");
        }

        public void PopulateBag () {
            User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("logged"));
            List<Activ> ActivsWithJoinersAndUsers = dbContext.Activs
                .Include (w => w.Joiners)
                .ThenInclude (g => g.User)
                .ToList ();
            List<Join> usersJoins = dbContext.Joins.Where (r => r.User.Equals (loggedUser)).ToList ();
            ViewBag.LoggedUserId = HttpContext.Session.GetInt32 ("logged");
            ViewBag.ActivsWithJoinersAndUsers = ActivsWithJoinersAndUsers;
            ViewBag.LoggedUser = loggedUser;
            ViewBag.UsersJoins = usersJoins;
        }

        public void GetActivJoiners (int ActivId) {
            List<Join> ActivJoiners = dbContext.Joins
                .Where(r => r.ActivId == ActivId)
                .Include(r => r.User)
                .ToList();
            ViewBag.ActivJoiners = ActivJoiners;
        }

        public int CheckLogged (){
            int flag = 1;
            if (HttpContext.Session.GetInt32 ("logged") == null) {
                flag = 0;
                TempData["alertMessage"] = "<p style='color:red;'>Please login or register.</p>";
            }
            return flag;
        }
    }
}