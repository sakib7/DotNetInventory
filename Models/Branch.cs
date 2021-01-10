using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Inventory.Models

{
    public class Branch
    {
        public int id { get; set; }

        [DisplayName("Branch Name")]
        [Required(ErrorMessage = "A Name is required")]
        public string name { get; set; }

        [DisplayName("Branch Address")]
        [Required(ErrorMessage = "An Address is required")]
        public string address { get; set; }

        public int orgID { get; set; }
        public string organization { get; set; }
    }
}