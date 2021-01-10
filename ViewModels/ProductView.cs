using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
namespace Inventory.ViewModels
{
    public class ProductView
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stock { get; set; }
        public double price { get; set; }
        public int category_id { get; set; }
        public int warehouse_id { get; set; }

        public List<Category> categoryList { get; set; }
    }
}