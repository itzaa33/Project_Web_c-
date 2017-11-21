using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models.ViewModel
{
    public class Search_ReservationViewModel
    {
        public int car_number { get; set; }
        public int      first_station { get; set; }
        public string   last_station { get; set; }
        public int      seat { get; set; }
        public int      price { get; set; }
        public DateTime date { get; set; }

    }
}
