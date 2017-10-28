using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.Models
{
    public class User
    {
        public int id { get; set; }

        [Required,EmailAddress , MaxLength(256), Display(Name = "email")]
        public String email { get; set; }

        [Required,MinLength(6),MaxLength(50), DataType(DataType.Password), Display(Name = "password")]
        public string password { get; set; }

        [Required, NotMapped, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public String name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public String phone_number { get; set; }

        public String provider { get; set; }

        public String state { get; set; }

        public int abuse { get; set; }

        public int status_ban { get; set; }

        public int money { get; set; }

        public DateTime date { get; set; }

        public DateTime date_update { get; set; }
    }
}
