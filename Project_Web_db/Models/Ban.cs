using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Ban
    {
        public int id { get; set; }


        public string email_user { get; set; }

        public string email_personnel { get; set; }

        public int command { get; set; }

        public String explanation { get; set; }

        public DateTime date { get; set; }
    }
}
