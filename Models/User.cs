using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class User
    {

        public int id { get; set; }

        [Required]
        [DisplayName("Owner Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("password", ErrorMessage = "Please confirm password correctly")]
        public string confirmPassword { get; set; }


        public string role { get; set; }
        public int orgID { get; set; }
        public string organization { get; set; }
        public int branchID { get; set; }
        public string branch { get; set; }
    }
}