using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Reservations
    {
        public int id { get; set; }

        public int id_user_ticket { get; set; }

        public int id_bus_schedule { get; set; }

        public String first_station { get; set; }

        public String traget_station { get; set; }

        public int seat { get; set; }

        public int price { get; set; }

        public DateTime date { get; set; }
    }
}
