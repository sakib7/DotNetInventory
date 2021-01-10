using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Models
{
    public class ProductOld
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please Enter stock")]
        [Range(0, 999999, ErrorMessage = "stock must be positive")]
        [DisplayName("Total Stock")]
        public int stock { get; set; }

        [Required(ErrorMessage = "Please Enter price")]
        [Range(0.00, 999999.00, ErrorMessage = "price must be positive")]
        public double price { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int category_id { get; set; }
        [DisplayName("Category Name")]
        public string category_name { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int warehouse_id { get; set; }
        [DisplayName("Warehouse Name")]
        public string warehouse_name { get; set; }

        [DisplayName("Category")]
        public List<Category> categoryList { get; set; }
        public List<Branch> warehouseList { get; set; }
    }
}