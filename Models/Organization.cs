using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Organization
    {
        public int id { get; set; }

        [Required]
        [DisplayName("Organization Name")]
        public string name { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [Required]
        [DisplayName("Address")]
        public string address { get; set; }
    }
}