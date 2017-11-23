using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project_Web_db.Models;
using Project_Web_db.Models.AccountViewModels;
using Project_Web_db.Services;
using Project_Web_db.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Web_db.Models.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Project_Web_db.Controllers
{

    
    [Route("[controller]/[action]")]
    public class Ticket_personnelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public Ticket_personnelController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        [Route("{route}/{station}")]
        public async Task<IActionResult> Search_busschedule(int route,int station)
        {


            DateTime timeNow = DateTime.Now;


            if (route == 1 || route == 2)
            {

                var querySchedule = (from bs in _db.Bus_schedules
                                    join personnel in _db.Personnels
                                    on bs.email_personnel equals personnel.email
                                    where (bs.route == route && 
                                           bs.station_set == station && 
                                           bs.date.Date == timeNow.Date)
                                    select new Search_busscheduleViewModel {
                                        id = bs.id ,
                                        time = bs.time,
                                        route = bs.route,
                                        station = bs.station_set,
                                        car_number = personnel.car_number,
                                        phone_number = personnel.phone_number
                                    }).ToList();


                ViewBag.route = route;
                ViewBag.station = station;

                return View("Search_busschedule", querySchedule);
               
            }

                return View("Search_busschedule");
        }

        [HttpGet]
        [Route("{route}/{station}/{alert}")]
        public async Task<IActionResult> Search_busschedule_alert(int route, int station, int alert )
        {


            DateTime timeNow = DateTime.Now;

            if (alert == 1)
            {

                ViewBag.alert = "หมดเวลาการจองในรอบนี้แล้ว กรุณาจองรอบต่อไปครับ";
            }
            else if(alert == 2)
            {
                ViewBag.alert = "ที่นั่งบนรถเต็ม กรุณาจองรอบต่อไปครับ";
            }
            else
            {
                ViewBag.alert = "ระบบขัดข้อง กรุณาจองใหม่อีกครั้งครับ";
            }


            if (route == 1 || route == 2)
            {

                var querySchedule = (from bs in _db.Bus_schedules
                                     join personnel in _db.Personnels
                                     on bs.email_personnel equals personnel.email
                                     where (bs.route == route &&
                                            bs.station_set == station &&
                                            bs.date.Date == timeNow.Date)
                                     select new Search_busscheduleViewModel {
                                         id = bs.id,
                                         time = bs.time,
                                         route = bs.route,
                                         station = bs.station_set,
                                         car_number = personnel.car_number,
                                         phone_number = personnel.phone_number
                                     }).ToList();


                ViewBag.route = route;
                ViewBag.station = station;


                return View("Search_busschedule", querySchedule);
            }

            return View("Search_busschedule");
        }

        [HttpGet]
        [Route("{route}/{station}")]
        public async Task<IActionResult> Create_Bus_busscheduleView(int route, int station)
        {
        

            ViewBag.route = route;
            ViewBag.station = station;


            return View("Create_Bus_busschedule");
        }

        [HttpPost]
        [Route("{route}/{station}")]
        public async Task<IActionResult> Create_Bus_busschedule(int route, int station)
        {
            int count = int.Parse(Request.Form["count"]);
            Debug.WriteLine(count);

            if (route == 1)
            {
                for(int i = 0; i < count; i++)
                {

                    int bus_num = int.Parse(Request.Form[$"bus_number[{i}]"]);


                    var queryPersonnel = _db.Personnels.Where(Ps => Ps.car_number == bus_num).FirstOrDefault();
                    string time = Request.Form[$"time[{i}]"];

                    if (queryPersonnel != null)
                    {
                        
                            var Bus_schedule = new Bus_schedule
                            {
                                email_personnel = queryPersonnel.email,
                                station_set = station,
                                time = time,
                                route = 1,
                                date = DateTime.Now
                            };
                            _db.Bus_schedules.Add(Bus_schedule);
                            _db.SaveChanges();

                          

                       
                    }

                  
                }
               
                return Redirect("/Home/Index");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {

                    int bus_num = int.Parse(Request.Form[$"bus_number[{i}]"]);


                    var queryPersonnel = _db.Personnels.Where(Ps => Ps.car_number == bus_num).FirstOrDefault();
                    string time = Request.Form[$"time[{i}]"];

                    if (queryPersonnel != null)
                    {

                        var Bus_schedule = new Bus_schedule
                        {
                            email_personnel = queryPersonnel.email,
                            station_set = station,
                            time = time,
                            route = 2,
                            date = DateTime.Now
                        };
                        _db.Bus_schedules.Add(Bus_schedule);
                        _db.SaveChanges();




                    }


                }
                return Redirect("/Home/Index");
            }

 
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult ReservationCreateView(int id)
        {

            var querySchedule = (from bs in _db.Bus_schedules
                                 join personnel in _db.Personnels
                                 on bs.email_personnel equals personnel.email
                                 where (bs.id == id)
                                 select new Search_busscheduleViewModel { id = bs.id, time = bs.time, route = bs.route, station = bs.station_set, car_number = personnel.car_number }).FirstOrDefault();

            var query_reservation = _db.Reservations.Where(r => r.id_bus_schedule == id).Count();

          

            if (querySchedule != null)
            {
                var Hour = querySchedule.time.Split('.')[0];
                var Minute = querySchedule.time.Split('.')[1];

                var timeNow = DateTime.Now;

                DateTime timevalue = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, int.Parse(Hour), int.Parse(Minute), timeNow.Second);

                int result = DateTime.Compare(timevalue, timeNow);



                 if (result < 0)
                 {
                    return Redirect($"/Ticket_personnel/Search_busschedule_alert/{querySchedule.route}/{querySchedule.station}/1");
                 }
                 else if(query_reservation >= 15)
                 {
                    return Redirect($"/Ticket_personnel/Search_busschedule_alert/{querySchedule.route}/{querySchedule.station}/2");
                 }

            }




            return View("ReservationCreate", querySchedule);
        }

        [HttpPost]
        public ActionResult ReservationCreate()
        {
            var id_request = HttpContext.Session.GetString("UserID");

            var query_user = _db.Users.Where(u => u.id == int.Parse(id_request)).FirstOrDefault();
            var query_personnel = _db.Personnels.Where(u => u.id == int.Parse(id_request)).FirstOrDefault();



            if (query_user != null)
            {
                string email = query_user.email;

                var reservations = new Reservations
                {
                    email_user_ticket = email,
                    id_bus_schedule = int.Parse(Request.Form["id_bus"]),

                    traget_station  = Request.Form["destination"],
                    seat            = int.Parse(Request.Form["seat"]),
                    price           = int.Parse(Request.Form["price_return"]),
                    date            = DateTime.Now


                };

                _db.Reservations.Add(reservations);


                User user = _db.Users.Where(u => u.id == int.Parse(id_request)).FirstOrDefault();

                user.money = user.money - int.Parse(Request.Form["price_return"]);

                HttpContext.Session.SetString("Usermoney", user.money.ToString());

               _db.Users.Update(user);
               _db.SaveChanges();

            }
            else
            {

                string email = query_personnel.email;


                var reservations = new Reservations
                {
                    email_personnel_ticket = email,
                    id_bus_schedule = int.Parse(Request.Form["id_bus"]),
                    traget_station = Request.Form["destination"],
                    seat = int.Parse(Request.Form["seat"]),
                    price = int.Parse(Request.Form["price_return"]),
                    date = DateTime.Now

                };

                _db.Reservations.Add(reservations);

                if (HttpContext.Session.GetString("Userstate") != "Ticketing")
                {
                    Personnel personnel = _db.Personnels.Where(p => p.id == int.Parse(id_request)).FirstOrDefault();

                    personnel.money = personnel.money - int.Parse(Request.Form["pricere_turn"]);

                    HttpContext.Session.SetString("Usermoney", personnel.money.ToString());

                    _db.Personnels.Update(personnel);
                }


                _db.SaveChanges();

            }


           return Redirect("/Home/Index");

        }

        [HttpGet]
        public ActionResult queryReservation(int id_Bus)
        {
            var query = _db.Reservations.Where(r => r.id_bus_schedule == id_Bus).Count();
   

            bool checkuser = false;


            if (HttpContext.Session.GetString("UserID") != null && query != 0)
            {
                string email_User = Request.Cookies["Useremail"];
                

                var user = _db.Users.Where(u => u.email == email_User).FirstOrDefault();
                var personnel = _db.Personnels.Where(p => p.email == email_User).FirstOrDefault();



                if (user != null)
                {
                    var checkReservation = _db.Reservations.Where(r => r.id_bus_schedule == id_Bus && r.email_user_ticket == user.email);

                    if(checkReservation != null)
                    {
                        checkuser = true;
                    }


                }
                else if(personnel != null)
                {
                    var checkReservation = _db.Reservations.Where(r => r.id_bus_schedule == id_Bus && r.email_personnel_ticket == personnel.email).FirstOrDefault();

                    if (checkReservation != null)
                    {
                        checkuser = true;
                    }
                }

            }



            return Json(new {count = query,check = checkuser });
        }

        

        /*public ActionResult Add_moneyView()
        {
            return Redirect();
        }

        public ActionResult Add_money()
        {
            return Redirect();
        }*/

        /*[Route("{id:int}/{name}")]
        [HttpGet]
        public async Task<IActionResult> Search_busschedule2222(int id, string name)
        {
            ViewBag.data = id + " :: "+name;
            return View("Search_busschedule");
        }*/

        /* [Route("{id:int}/{name}")]
         [HttpGet]

         public async Task<IActionResult> Search_busschedule3(int id, string name)
         {
             string dateString = "2009-06-15T13:45:30.0000000-07:00";

             //DateTime d = DateTime.ParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture, DateTimeStyles.None);

             DateTime d = DateTime.Parse(dateString);
             DateTime min = new DateTime(2009, 06, 16);
             DateTime max = new DateTime(2009, 06, 17);

             ViewBag.date = d.ToString("O");
             ViewBag.result = d.Ticks >= min.Ticks && d.Ticks < max.Ticks;

             ViewBag.id = id;
             ViewBag.name = name;

             return View("Search_busschedule");
         }*/
    }
    }