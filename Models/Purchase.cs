using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Purchase
    {
        public int id { get; set; }
        public int productID { get; set; }
        public string product { get; set; }
        public string status { get; set; }
        public int quantity { get; set; }
        [DisplayName("Requested quantity")]
        public int quantityReq { get; set; }
        public int stock { get; set; }
        public double purchasePrice { get; set; }
        public DateTime date { get; set; }
        public int vendorID { get; set; }
        public string vendorName { get; set; }
        public int branchID { get; set; }
        public string branchName { get; set; }

        public List<Category> categoryList { get; set; }
        public List<Product> productList { get; set; }
        public List<Vendor> vendorList { get; set; }
    }
}