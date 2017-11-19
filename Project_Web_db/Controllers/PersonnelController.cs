using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Web_db.Data;
using Project_Web_db.Models;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Web_db.Controllers
{
    [Route("[controller]/[action]")]
    public class PersonnelController : Controller
    {
        private ApplicationDbContext _db;

        public PersonnelController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Search_userView()
        {
            return View("Search_Account");
        }

        [HttpGet]
        public ActionResult Search_user()
        {

            if(Request.Form["Search_type"] == "email")
            {
                var query_User = _db.Users.Where(u => u.email == Request.Form["Search"]);

                var query_Personnel = (from p in _db.Personnels
                                       where (p.email == Request.Form["Search"])
                                       select new User
                                       {
                                           id           = p.id,
                                           email        = p.email,
                                           name         = p.name,
                                           phone_number = p.phone_number,
                                           state        = p.state,
                                           money        = p.money,
                                           status_ban   = p.status_ban

                                       });

                if (query_User != null)
                {
                    return View("Search_Account", query_User);
                }
                else
                {
                    return View("Search_Account", query_Personnel);
                }

              
            }
            else
            {
                var query_User = _db.Users.Where(u => u.name == Request.Form["Search"]);

                var query_Personnel = (from p in _db.Personnels
                                       where (p.name == Request.Form["Search"])
                                       select new User
                                       {
                                           id           = p.id,
                                           email        = p.email,
                                           name         = p.name,
                                           phone_number = p.phone_number,
                                           state        = p.state,
                                           money        = p.money,
                                           status_ban   = p.status_ban

                                       });

                if (query_User != null)
                {
                    return View("Search_Account", query_User);
                }
                else
                {
                    return View("Search_Account", query_Personnel);
                }

            }
           
        }

        [HttpGet]
        public ActionResult get_profile(int id)
        {
            var query_user = _db.Users.Where(u => u.id == id).FirstOrDefault();

            var query_Personnel = _db.Personnels.Where(p => p.id == id).FirstOrDefault();

            if(query_user != null)
            {
                return Json(query_user);
            }
            else
            {
                return Json(query_Personnel);
            }
            
        }


        [HttpPost]
        public ActionResult Chang_ban()
        {


            var query_User = _db.Users.Where(u => u.id == int.Parse(Request.Form["parse_value_ban"])).FirstOrDefault();

            var query_Personnel = _db.Personnels.Where(p => p.id == int.Parse(Request.Form["parse_value_ban"])).FirstOrDefault();

            if (query_User != null)
            {
                if (Request.Form["submit"] == 1)
                {
                    query_User.status_ban = 1;

                    _db.Users.Update(query_User);

                    var ban = new Ban
                    {
                        id_user = query_User.id,
                        id_personnel = int.Parse(HttpContext.Session.GetString("UserID")),
                        command = 1,
                        explanation = Request.Form["explanation"],
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                    
                }
                else
                {
                    query_User.status_ban = 0;

                    _db.Users.Update(query_User);

                    var ban = new Ban
                    {
                        id_user = query_User.id,
                        id_personnel = int.Parse(HttpContext.Session.GetString("UserID")),
                        command = 0,
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                }
            }
            else
            {
                if(Request.Form["submit"] == 1)
                {
                    query_Personnel.status_ban = 1;

                    _db.Personnels.Update(query_Personnel);

                    var ban = new Ban
                    {
                        id_user = query_Personnel.id,
                        id_personnel = int.Parse(HttpContext.Session.GetString("UserID")),
                        command = 1,
                        explanation = Request.Form["explanation"],
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                }
                else
                {
                    query_Personnel.status_ban = 0;

                    _db.Personnels.Update(query_Personnel);

                    var ban = new Ban
                    {
                        id_user = query_Personnel.id,
                        id_personnel = int.Parse(HttpContext.Session.GetString("UserID")),
                        command = 0,
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                }
            }
           

            _db.SaveChanges();


            return Redirect("/Personnel/Search_Account");
        }
    }
}
