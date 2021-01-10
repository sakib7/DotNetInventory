using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class BranchReport
    {
        public int orgID { get; set; }
        public string orgName { get; set; }

        public int branchItemCount { get; set; }
        public List<BranchItem> branchItemList { get; set; }
    }

    public class BranchItem
    {
        public int branchID { get; set; }
        public string branchName { get; set; }

        public int productCount { get; set; }
        public List<BranchProduct> productList { get; set; }
    }

    public class BranchProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double revenue { get; set; }
        public double cost { get; set; }
        public double profit { get; set; }
        public int profitMargin { get; set; }
    }
}