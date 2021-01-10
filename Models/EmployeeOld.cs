using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class EmployeeOld
    {
        public int id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Compare("password",ErrorMessage ="Please confirm password correctly")]
        public string confirmpass { get; set; }

        [Required]
        public string role { get; set; }

        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

    }
}