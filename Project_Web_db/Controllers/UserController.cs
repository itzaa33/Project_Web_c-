using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Web_db.Data;
using Project_Web_db.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Web_db.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser()
        {

            if (_db.Personnel_Add_Users.Any())
            {
                _db.Personnel_Add_Users.Add(new Personnel_Add_User
                {

                    id_user = 1,
                    id_personnel = 1,
                    money = 5,
                    date = DateTime.Now,
                });

                _db.SaveChanges();

            };



            //var user = from m in dataContext.Users 
            return RedirectToAction("Index");
        }
    }
}
