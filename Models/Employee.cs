using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string role { get; set; }
        public int branchID { get; set; }
        public string branch { get; set; }
        public int orgID { get; set; }
        public List<Branch> branchList { get; set; }
    }
}