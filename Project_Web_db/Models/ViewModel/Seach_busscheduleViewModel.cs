using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project_Web_db.Models.ViewModel
{
    public class Search_busscheduleViewModel
    {
        public int id { get; set; }

        public int route { get; set; }

        public int station { get; set; }

        public int car_number { get; set; }

        public string time { get; set; }

    }
}
