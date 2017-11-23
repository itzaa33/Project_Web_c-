using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models.ViewModel
{
    public class History_ban_ViewModel
    {
        public string email_user        { get; set; }
        public string name_user         { get; set; }
        public string phone_number_user { get; set; }
        public string state_user        { get; set; }
        public int    status_user       { get; set; }
        public int    command           { get; set; }
        public string name_personnel    { get; set; }
        public string state_personnel   { get; set; }
        public string explanation       { get; set; }
        public DateTime date            { get; set; }

    }
}
