using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Personnel_Add_User
    {
        public int id { get; set; }

        public string email_user { get; set; }

        public string email_personnel { get; set; }

        public int money { get; set; }

        public DateTime date { get; set; }
    }
}
