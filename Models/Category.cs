using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }
        [Required(ErrorMessage ="A category must have a name")]
        [DisplayName("Category Name")]
        public string name { get; set; }
        public string description { get; set; }
        public int orgID { get; set; }
    }
}