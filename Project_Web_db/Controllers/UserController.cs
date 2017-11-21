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

        [Route("{id}")]
        public ActionResult Search_Reservation_User(int id)
        {


            if (HttpContext.Session.GetString("Userstate") == "User")
            {
                int id_user = id;

                var Reservation_User = (from bs in _db.Bus_schedules
                                        join reservation in _db.Reservations
                                        on bs.id equals reservation.id_bus_schedule
                                        join personnel in _db.Personnels
                                        on bs.id_personnel equals personnel.id
                                        where (reservation.id_user_ticket == id_user)
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
                int id_personnel = id;

                var Reservation_User = (from bs in _db.Bus_schedules
                                        join reservation in _db.Reservations
                                        on bs.id equals reservation.id_bus_schedule
                                        join personnel in _db.Personnels
                                        on bs.id_personnel equals personnel.id
                                        where (reservation.id_user_ticket == id_personnel)
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


        [Route("{id}")]
        public ActionResult Search_Addmoney_User(int id)
        {
            if (HttpContext.Session.GetString("Userstate") == "User")
            {
                int id_user = id;

                var Reservation_User = (from add in _db.Personnel_Add_Users
                                        join u in _db.Users
                                        on add.id_user equals u.id
                                        join personnel in _db.Personnels
                                        on add.id_personnel equals personnel.id
                                        where (add.id_user == id_user)
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
                int id_personnel = id;

                var Reservation_Personnel = (from add in _db.Personnel_Add_Users
                                             join p in _db.Personnels
                                             on add.id_user equals p.id
                                             join personnel in _db.Personnels
                                             on add.id_personnel equals personnel.id
                                             where (add.id_personnel == id_personnel)
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

                var id_user = HttpContext.Session.GetString("UserID");

                var user = _db.Users.Where(u => u.id == int.Parse(id_user)).FirstOrDefault();

                user.money = user.money + query.price;

                HttpContext.Session.SetString("Usermoney", user.money.ToString());

                _db.Users.Update(user);

                _db.Reservations.Remove(query);
                 
                _db.SaveChanges();

                return Json(new { val_s = "ระบบได้ทำการลบการจองของท่านแล้ว",check = true });
                
            }
            else
            {
                return Json(new { val_s = "ไม่สามารถเยิกเลิกการจองได้ เนื่องจากหมดเวลา", check = false });
            }


        }
    }
}
