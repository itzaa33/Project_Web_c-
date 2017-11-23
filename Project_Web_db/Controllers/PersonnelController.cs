using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Web_db.Data;
using Project_Web_db.Models;
using Microsoft.AspNetCore.Http;
using Project_Web_db.Models.ViewModel;


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
            var query = _db.Users.ToList();

            return View("Search_Account", query);
        }

        [HttpPost]
        public ActionResult Search_user()
        {

            if(Request.Form["Search_ptable"] == "user")
            {

                if (Request.Form["Search_type"] == "email")
                {
                    var query_User = _db.Users.Where(u => u.email == Request.Form["Search"]).ToList();

                        return View("Search_Account", query_User);

                }
                else
                {
                    var query_User = _db.Users.Where(u => u.name == Request.Form["Search"]).ToList();

                    return View("Search_Account", query_User);
                }

            }
            else if(Request.Form["Search_table"] == "personnel")
            {
                if (Request.Form["Search_type"] == "email")
                {
                    var query_Personnel = (from p in _db.Personnels
                                           where (p.email == Request.Form["Search"])
                                           select new User
                                           {
                                               id = p.id,
                                               email = p.email,
                                               name = p.name,
                                               phone_number = p.phone_number,
                                               state = p.state,
                                               money = p.money,
                                               status_ban = p.status_ban

                                           }).ToList();
                    return View("Search_Account", query_Personnel);
                }
                else
                {
                    var query_Personnel = (from p in _db.Personnels
                                           where (p.name == Request.Form["Search"])
                                           select new User
                                           {
                                               id = p.id,
                                               email = p.email,
                                               name = p.name,
                                               phone_number = p.phone_number,
                                               state = p.state,
                                               money = p.money,
                                               status_ban = p.status_ban

                                           }).ToList();
                    return View("Search_Account", query_Personnel);
                }

                
            }
            else
            {
                var query_Personnel = _db.Users.ToList();
                return View("Search_Account", query_Personnel);
            }

        }

        [Route("{id}")]
        public ActionResult Redirect_Search_user(int id)
        {

            
                var query_User = _db.Users.Where(u => u.id == id).ToList();

                var query_Personnel = (from p in _db.Personnels
                                       where (p.id == id)
                                       select new User
                                       {
                                           id = p.id,
                                           email = p.email,
                                           name = p.name,
                                           phone_number = p.phone_number,
                                           state = p.state,
                                           money = p.money,
                                           status_ban = p.status_ban

                                       }).ToList();

                if (query_User != null)
                {
                    return View("Search_Account", query_User);
                }
                else
                {
                    return View("Search_Account", query_Personnel);
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
        public ActionResult Change_ban()
        {
            int id_user = int.Parse(Request.Form["user_id"]);
            int status_user = int.Parse(Request.Form["parse_value_ban"]);


            var query_User = _db.Users.Where(u => u.status_ban == status_user && u.id == id_user).FirstOrDefault();

            var query_Personnel = _db.Personnels.Where(p => p.status_ban == status_user && p.id == id_user).FirstOrDefault();

            if (query_User != null)
            {
                if (Request.Form["submit"] == "1")
                {
                    query_User.status_ban = 1;

                    _db.Users.Update(query_User);

                    var ban = new Ban
                    {
                        email_user = query_User.email,
                        email_personnel = HttpContext.Session.GetString("Useremail"),
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
                        email_user = query_User.email,
                        email_personnel = HttpContext.Session.GetString("Useremail"),
                        command = 0,
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                }
            }
            else
            {
                if(Request.Form["submit"] == "1")
                {
                    query_Personnel.status_ban = 1;

                    _db.Personnels.Update(query_Personnel);

                    var ban = new Ban
                    {
                        email_user = query_Personnel.email,
                        email_personnel = HttpContext.Session.GetString("Useremail"),
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
                        email_user = query_Personnel.email,
                        email_personnel = HttpContext.Session.GetString("Useremail"),
                        command = 0,
                        date = DateTime.Now
                    };

                    _db.Bans.Add(ban);
                }
            }
           

            _db.SaveChanges();


            return Redirect($"/Personnel/Redirect_Search_user/{id_user}");
        }

        [HttpPost]
        public ActionResult addmoney()
        {
            string email_user = Request.Form["user_email"];

            var query_User = _db.Users.Where(u => u.email == email_user).FirstOrDefault();

            var query_Personnel = _db.Personnels.Where(p => p.email == email_user).FirstOrDefault();

            if(query_User != null)
            {
                query_User.money = query_User.money + int.Parse(Request.Form["value_money"]);
                _db.Users.Update(query_User);

                var create_addmoney = new Personnel_Add_User
                {
                    email_user = email_user,
                    email_personnel = HttpContext.Session.GetString("Useremail"),
                    money = int.Parse(Request.Form["value_money"]),
                    date = DateTime.Now
                };

                _db.Personnel_Add_Users.Add(create_addmoney);
            }
            else
            {
                query_Personnel.money = query_Personnel.money + int.Parse(Request.Form["value_money"]);
                _db.Personnels.Update(query_Personnel);

                var create_addmoney = new Personnel_Add_User
                {
                    email_user = email_user,
                    email_personnel = HttpContext.Session.GetString("Useremail"),
                    money = int.Parse(Request.Form["value_money"]),
                    date = DateTime.Now
                };

                _db.Personnel_Add_Users.Add(create_addmoney);
            }

         
            _db.SaveChanges();

            return Redirect($"/Personnel/Redirect_Search_user/{email_user}");
        }

        
        public ActionResult Search_History_banView()
        {
            var query_history = (from b in _db.Bans
                                 join u in _db.Users
                                 on b.email_user equals u.email
                                 join p in _db.Personnels
                                 on b.email_personnel equals p.email
                                 where (b.email_user == null)
                                 orderby b.date descending
                                 select new History_ban_ViewModel
                                 {
                                     email_user = u.email,
                                     name_user = u.name,
                                     phone_number_user = u.phone_number,
                                     state_user = u.state,
                                     status_user = u.status_ban,
                                     command = b.command,
                                     name_personnel = p.name,
                                     state_personnel = p.state,
                                     explanation = b.explanation,
                                     date = b.date
                                 }).ToList();

            return View("History_ban", query_history);
        }

        [HttpPost]
        public ActionResult Search_History_ban()
        {
            if (Request.Form["Search_table"] == "user")
            {

                if (Request.Form["Search_type"] == "email")
                {
                    var query_User = _db.Users.Where(u => u.email == Request.Form["Search"]).FirstOrDefault();

                    var query_history = (from b in _db.Bans
                                         join u in _db.Users
                                         on b.email_user equals u.email
                                         join p in _db.Personnels
                                         on b.email_personnel equals p.email
                                         where (b.email_user == query_User.email)
                                         orderby b.date descending
                                         select new History_ban_ViewModel
                                         {
                                             email_user         = u.email,
                                             name_user          = u.name,
                                             phone_number_user  = u.phone_number,
                                             state_user         = u.state,
                                             status_user        = u.status_ban,
                                             command            = b.command,
                                             name_personnel     = p.name,
                                             state_personnel    = p.state,
                                             explanation        = b.explanation,
                                             date               = b.date
                                         }).ToList();

                    return View("History_ban", query_history);

                }
                else
                {
                    var query_User = _db.Users.Where(u => u.email == Request.Form["Search"]).FirstOrDefault();

                    var query_history = (from b in _db.Bans
                                         join u in _db.Users
                                         on b.email_user equals u.email
                                         join p in _db.Personnels
                                         on b.email_personnel equals p.email
                                         where (b.email_user == query_User.email)
                                         orderby b.date descending
                                         select new History_ban_ViewModel
                                         {
                                             email_user = u.email,
                                             name_user = u.name,
                                             phone_number_user = u.phone_number,
                                             state_user = u.state,
                                             status_user = u.status_ban,
                                             command = b.command,
                                             name_personnel = p.name,
                                             state_personnel = p.state,
                                             explanation = b.explanation,
                                             date = b.date
                                         }).ToList();

                    return View("History_ban", query_history);
                }

            }
            else if (Request.Form["Search_table"] == "personnel")
            {
                if (Request.Form["Search_type"] == "email")
                {
                    var query_User = _db.Personnels.Where(p => p.name == Request.Form["Search"]).FirstOrDefault();

                    var query_history = (from b in _db.Bans
                                         join personnel_ban in _db.Personnels
                                         on b.email_user equals personnel_ban.email
                                         join p in _db.Personnels
                                         on b.email_personnel equals p.email
                                         where (b.email_personnel == query_User.email)
                                         orderby b.date descending
                                         select new History_ban_ViewModel
                                         {
                                             email_user         = personnel_ban.email,
                                             name_user          = personnel_ban.name,
                                             phone_number_user  = personnel_ban.phone_number,
                                             state_user         = personnel_ban.state,
                                             status_user        = personnel_ban.status_ban,
                                             command            = b.command,
                                             name_personnel     = p.name,
                                             state_personnel    = p.state,
                                             explanation        = b.explanation,
                                             date               = b.date

                                         }).ToList();

                    return View("History_ban", query_history);
                }
                else
                {
                    var query_User = _db.Personnels.Where(u => u.name == Request.Form["Search"]).FirstOrDefault();

                    var query_history = (from b in _db.Bans
                                         join personnel_ban in _db.Personnels
                                         on b.email_user equals personnel_ban.email
                                         join p in _db.Personnels
                                         on b.email_personnel equals p.email
                                         where (b.email_user == query_User.email)
                                         orderby b.date descending
                                         select new History_ban_ViewModel
                                         {
                                             email_user = personnel_ban.email,
                                             name_user = personnel_ban.name,
                                             phone_number_user = personnel_ban.phone_number,
                                             state_user = personnel_ban.state,
                                             status_user = personnel_ban.status_ban,
                                             command = b.command,
                                             name_personnel = p.name,
                                             state_personnel = p.state,
                                             explanation = b.explanation,
                                             date = b.date

                                         }).ToList();

                    return View("History_ban", query_history);
                }


            }
            else
            {
                History_ban_ViewModel query_Personnel = null;
                return View("History_ban", query_Personnel);
            }

        }

        [Route("{id}")]
        public ActionResult Check_userInschedule(int id)
        {
            var query = (from r in _db.Reservations

                         join p in _db.Personnels
                         on r.email_personnel_ticket equals p.email into joined_personnel

                         join u in _db.Users
                         on r.email_user_ticket equals u.email into joined_user

                         join bs in _db.Bus_schedules
                          on r.id_bus_schedule equals bs.id 

                         where (r.id_bus_schedule == id) 
                         orderby r.date descending 
                         from ju in joined_user.DefaultIfEmpty()
                         from jp in joined_personnel.DefaultIfEmpty()
                         select new Search_UserIN_busschedule
                          {
                              
                              email_personnel         = jp.email,
                              name_personnel          = jp.name,
                              phone_number_personnel  = jp.phone_number,

                              email_user         = ju.email,
                              name_user         = ju.name,
                              phone_number_user = ju.phone_number,

                              seat                    = r.seat,
                              first_station           = bs.station_set,
                              traget_station          = r.traget_station,
                              date                    = r.date

                          }).ToList();

           
         

            return View("Personnel_Search_bs", query);
        }
    }
}
