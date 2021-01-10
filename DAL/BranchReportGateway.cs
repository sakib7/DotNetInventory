using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Inventory.Models;

namespace Inventory.DAL
{
    public class BranchReportGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        User userSession = HttpContext.Current.Session["user"] as User;

        public BranchReport SelectAll()
        {
            BranchReport branchReport = new BranchReport();
            List<BranchItem> branchItemList = new List<BranchItem>();
            branchReport.branchItemList = branchItemList;
            
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Branch.id,Branch.name 
                                FROM Branch JOIN Organization ON Branch.organizationID = Organization.id 
                                WHERE Organization.id=" + userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            branchReport.branchItemCount = dataTable.Rows.Count;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                BranchItem branchItem = new BranchItem();
                DataRow row = dataTable.Rows[i];
                branchItem.branchID = Convert.ToInt32(row[0]);
                branchItem.branchName = row[1].ToString();
                branchItemList.Add(branchItem);
            }

            foreach(BranchItem branch in branchItemList)
            {
                DataTable branchDataTable = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = @"SELECT Product.id,OrgProduct.name FROM Product 
                                    JOIN OrgProduct ON Product.orgProductID=OrgProduct.id 
                                    JOIN Category ON Category.id=OrgProduct.categoryID
                                    JOIN Branch ON Branch.id=Product.branchID 
                                    WHERE Branch.id=" + branch.branchID;
                    SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                    adapter.Fill(branchDataTable);
                }
                branch.productCount = branchDataTable.Rows.Count+1;
                List<BranchProduct> branchProductList = new List<BranchProduct>();
                for (int i = 0; i < branchDataTable.Rows.Count; i++)
                {
                    BranchProduct branchProduct = new BranchProduct();
                    DataRow row = branchDataTable.Rows[i];
                    branchProduct.id = Convert.ToInt32(row[0]);
                    branchProduct.name = row[1].ToString();
                    branchProductList.Add(branchProduct);
                }

                branch.productList = branchProductList;


                foreach (BranchProduct product in branchProductList)
                {
                    DataTable productDataTable = new DataTable();
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        string query = @"SELECT SUM(SalePurchase.quantity) AS quantity,
                                        SUM(Sale.retailPrice*SalePurchase.quantity) AS revenue,
                                        SUM(Purchase.purchasePrice*SalePurchase.quantity) AS cost, 
                                        SUM(sale.retailPrice*SalePurchase.quantity-Purchase.purchasePrice*SalePurchase.quantity) AS profit
                                        FROM Organization
                                        JOIN Branch ON Branch.organizationID=Organization.id
                                        JOIN Product ON Product.branchID=Branch.id
                                        JOIN OrgProduct ON OrgProduct.id=Product.orgProductID
                                        JOIN Sale ON Sale.productID=Product.id
                                        JOIN SalePurchase ON SalePurchase.saleID=Sale.id
                                        JOIN Purchase ON Purchase.id=SalePurchase.purchaseID
                                        WHERE Product.id=" + product.id;
                        SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                        adapter.Fill(productDataTable);
                    }
                    for (int i = 0; i < productDataTable.Rows.Count; i++)
                    {
                        DataRow row = productDataTable.Rows[i];
                        if (String.IsNullOrEmpty(row[0].ToString()))
                            continue;
                        product.quantity = Convert.ToInt32(row[0]);
                        product.revenue = Convert.ToDouble(row[1]);
                        product.cost = Convert.ToDouble(row[2]);
                        product.profit = Convert.ToDouble(row[3]);
                    }
                }
            }

            return branchReport;
        }
    }
}