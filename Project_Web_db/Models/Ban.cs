using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Ban
    {
        public int id { get; set; }

        [ForeignKey("FK_Personnel_Add_Users_User_email")]
        public string email_user { get; set; }

        [ForeignKey("FK_Personnel_Add_Users_Personnels_email_personnel_reaction")]
        public string email_personnel_reaction { get; set; }

        [ForeignKey("FK_Personnel_Add_Users_Personnels_email")]
        public string email_personnel { get; set; }

        public int command { get; set; }

        public String explanation { get; set; }

        public DateTime date { get; set; }
    }
}
