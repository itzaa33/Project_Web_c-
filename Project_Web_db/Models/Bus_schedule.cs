using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Bus_schedule
    {
        public int id { get; set; }

        public int id_personnel { get; set; }

        public int sequence { get; set; }

        public int route { get; set; }

        public DateTime date { get; set; }
    }
}
