using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Product
    {
        public int id { get; set; }

        public int branchID { get; set; }
        [DisplayName("Branch")]
        public string branch { get; set; }

        public int categoryID { get; set; }
        public string category { get; set; }

        public int orgProductID { get; set; }
        [DisplayName("Product Name")]
        public string orgProduct { get; set; }
        public string brand { get; set; }

        [DisplayName("Retail Price")]
        public double retailPrice { get; set; }
        public int stock { get; set; }

        public List<Category> categoryList { get; set; }
        public List<Branch> branchList { get; set; }
        public List<OrgProduct> orgProductList { get; set; }
    }
}