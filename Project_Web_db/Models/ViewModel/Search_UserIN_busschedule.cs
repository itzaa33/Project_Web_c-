using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models.ViewModel
{
    public class Search_UserIN_busschedule
    {


        public string email_user { set; get; }

        public string email_personnel { set; get; }
        
        public string name_user { set; get; }

        public string name_personnel { set; get; }

        public string phone_number_user { set; get; }

        public string phone_number_personnel { set; get; }

        public int seat { set; get; }

        public int first_station { set; get; }

        public string traget_station { set; get; }

        public DateTime date { set; get; }

    }
}
