using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Reservations
    {
        public int id { get; set; }

        [ForeignKey("FK_Reservations_Users_email")]
        public string email_user_ticket { get; set; }

        [ForeignKey("FK_Reservations_Personnels_email")]
        public string email_personnel_ticket { get; set; }

        [ForeignKey("FK_Reservations_Bus_schedules")]
        public int id_bus_schedule { get; set; }

        public string traget_station { get; set; }

        public int seat { get; set; }

        public int price { get; set; }

        public DateTime date { get; set; }
    }
}
