using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class OrgProduct
    {
        public int id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Brand")]
        public string brand { get; set; }

        public int categoryID { get; set; }
        public string category { get; set; }

        public int orgID { get; set; }
        public string organization { get; set; }

        [Required]
        [DisplayName("Category")]
        public List<Category> categories { get; set; }
    }
}