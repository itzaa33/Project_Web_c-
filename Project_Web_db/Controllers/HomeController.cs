using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Web_db.Models;
using Project_Web_db.Data;
using Microsoft.AspNetCore.Identity;
using Project_Web_db.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Project_Web_db.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
                           



























        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            if(Request.Cookies["UserID"] != null)
            {
                string name = Request.Cookies["Username"];
                string state = Request.Cookies["Userstate"];
                string email = Request.Cookies["Useremail"];

                var queryUser = _db.Users.Where(u => u.name == name && u.state == state && u.email == email).FirstOrDefault();

                var queryPersonnel = _db.Personnels.Where(u => u.name == name && u.state == state).FirstOrDefault();

                if(queryUser != null)
                {
                    HttpContext.Session.SetString("UserID", queryUser.id.ToString());
                    HttpContext.Session.SetString("Username", queryUser.name.ToString());
                    HttpContext.Session.SetString("Userstate", queryUser.state.ToString());
                    HttpContext.Session.SetString("Usermoney", queryUser.money.ToString());
                }
                else if(queryPersonnel != null)
                {
                    HttpContext.Session.SetString("UserID", queryPersonnel.id.ToString());
                    HttpContext.Session.SetString("Username", queryPersonnel.name.ToString());
                    HttpContext.Session.SetString("Userstate", queryPersonnel.state.ToString());
                    HttpContext.Session.SetString("Usermoney", queryPersonnel.money.ToString());
                }
                
                
                return View();
            }
            else
            {
                return View();
            }

            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
