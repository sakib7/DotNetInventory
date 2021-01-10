using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Vendor

    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }

        public int orgID { get; set; }
    }
}