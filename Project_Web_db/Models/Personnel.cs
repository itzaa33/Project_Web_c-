using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class Personnel
    {
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String password { get; set; }

        public String name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public String phone_number { get; set; }

        public String provider { get; set; }

        public String state { get; set; }

        public int car_number { get; set; }

        public int abuse { get; set; }

        public int status_ban { get; set; }

        public int money { get; set; }

        public DateTime date { get; set; }

        public DateTime date_update { get; set; }
    }
}
