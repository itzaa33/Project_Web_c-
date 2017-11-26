using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Personnel_Add_User
    {
        public int id { get; set; }

        [ForeignKey("FK_Personnel_Add_Users_User_email")]
        public string email_user { get; set; }

        [ForeignKey("FK_Personnel_Add_Users_Personnels_email")]
        public string email_personnel { get; set; }

        public int money { get; set; }

        public DateTime date { get; set; }
    }
}
