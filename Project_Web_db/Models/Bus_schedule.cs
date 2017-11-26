using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Bus_schedule
    {
        public int id { get; set; }

        [ForeignKey("FK_Bus_schedules")]
        public string email_personnel { get; set; }

        public int station_set { get; set; }

        public string time { get; set; }

        public int route { get; set; }

        public DateTime date { get; set; }

    }
}
