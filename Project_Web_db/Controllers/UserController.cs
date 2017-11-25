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
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        public ActionResult Search_Reservation_User()
        {


            if (Request.Cookies["Userstate"] == "User")
            {
                string email_user = Request.Cookies["Useremail"];

                var Reservation_User = (from bs in _db.Bus_schedules
                                        join reservation in _db.Reservations
                                        on bs.id equals reservation.id_bus_schedule
                                        join personnel in _db.Personnels
                                        on bs.email_personnel equals personnel.email
                                        where (reservation.email_user_ticket == email_user)
                                        orderby reservation.date descending
                                        select new Search_ReservationViewModel
                                        {
                                            car_number = personnel.car_number,
                                            first_station = bs.station_set,
                                            last_station = reservation.traget_station,
                                            seat = reservation.seat,
                                            price = reservation.price,
                                            date = reservation.date
                                        }).ToList();

                return View("Search_Reservation_User", Reservation_User);
            }
            else
            {
                string email_personnel = Request.Cookies["Useremail"];

                var Reservation_User = (from bs in _db.Bus_schedules
                                        join reservation in _db.Reservations
                                        on bs.id equals reservation.id_bus_schedule
                                        join personnel in _db.Personnels
                                        on bs.email_personnel equals personnel.email
                                        where (reservation.email_user_ticket == email_personnel)
                                        orderby reservation.date descending
                                        select new Search_ReservationViewModel
                                        {
                                            car_number = personnel.car_number,
                                            first_station = bs.station_set,
                                            last_station = reservation.traget_station,
                                            seat = reservation.seat,
                                            price = reservation.price,
                                            date = reservation.date
                                        }).ToList();

                return View("Search_Reservation_User", Reservation_User);
            }



        }


      
        public ActionResult Search_Addmoney_User()
        {
            if (Request.Cookies["Userstate"] == "User")
            {
                string email_user = Request.Cookies["Useremail"];

                var Reservation_User = (from add in _db.Personnel_Add_Users
                                        join u in _db.Users
                                        on add.email_user equals u.email
                                        join personnel in _db.Personnels
                                        on add.email_personnel equals personnel.email
                                        where (add.email_user == email_user)
                                        orderby add.date descending
                                        select new Search_Addmoney_UserViewModel
                                        {
                                            id_add = add.id,
                                            name_user = u.name,
                                            name_personnel = personnel.name,
                                            money = add.money,
                                            date = add.date
                                        }).ToList();

                return View("Search_Addmoney_User", Reservation_User);
            }
            else
            {
                string email_personnel = Request.Cookies["Useremail"];

                var Reservation_Personnel = (from add in _db.Personnel_Add_Users
                                             join p in _db.Personnels
                                             on add.email_user equals p.email
                                             join personnel in _db.Personnels
                                             on add.email_personnel equals personnel.email
                                             where (add.email_personnel == email_personnel)
                                             orderby add.date descending
                                             select new Search_Addmoney_UserViewModel
                                             {
                                                 id_add = add.id,
                                                 name_user = p.name,
                                                 name_personnel = personnel.name,
                                                 money = add.money,
                                                 date = add.date
                                             }).ToList();

                return View("Search_Addmoney_User", Reservation_Personnel);
            }


        }

        [HttpGet]
        public ActionResult deleteReservation(int id)
        {

            var query = _db.Reservations.Where(r => r.id_bus_schedule == id).FirstOrDefault();

            var bus_s = _db.Bus_schedules.Where(bs => bs.id == query.id_bus_schedule).FirstOrDefault();


            var Hour = bus_s.time.Split('.')[0];

            var Minute = bus_s.time.Split('.')[1];

            var timeNow = DateTime.Now;

          
            DateTime timecheck = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, int.Parse(Hour),  int.Parse(Minute) , timeNow.Second);

            timecheck = timecheck.AddMinutes(-30);

            int result = DateTime.Compare(timecheck, timeNow);


            if (result > 0)
            {

                string email_user = Request.Cookies["Useremail"];

                var user = _db.Users.Where(u => u.email == email_user).FirstOrDefault();

                user.money = user.money + query.price;

                HttpContext.Session.SetString("Usermoney", user.money.ToString());

                _db.Users.Update(user);

                _db.Reservations.Remove(query);
                 
                _db.SaveChanges();

                return Json(new { val_s = "ระบบได้ทำการยกเลิกการจองของท่านแล้ว",check = true });
                
            }
            else
            {
                return Json(new { val_s = "ไม่สามารถเยิกเลิกการจองได้ เนื่องจากหมดเวลา", check = false });
            }


        }
    }
}
