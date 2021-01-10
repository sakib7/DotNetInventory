using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Inventory.DAL
{
    public class AdminSalesGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<SalesOrder> SelectAll()
        {
            List<SalesOrder> list = new List<SalesOrder>();
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"select Sale.id,Sale.customer,SalePurchase.quantity,Sale.retailPrice,Sale.date,OrgProduct.name,Branch.name from Sale 
                                join SalePurchase on Sale.id=SalePurchase.saleID 
                                join Purchase on Purchase.id=SalePurchase.purchaseID 
                                join Product on Product.id=Sale.productID 
								join OrgProduct on OrgProduct.id=Product.orgProductID 
                                join Branch on Branch.id=Product.branchID 
                                where OrgProduct.organizationID=" + userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SalesOrder saleOrder = new SalesOrder();
                DataRow row = dataTable.Rows[i];
                saleOrder.id = Convert.ToInt32(row[0].ToString());
                saleOrder.contact = row[1].ToString();
                saleOrder.selectedProductQuantity = Convert.ToInt32(row[2]);
                saleOrder.total = Convert.ToDouble(row[3]);
                saleOrder.orderDate = Convert.ToDateTime(row[4].ToString());
                saleOrder.productName = row[5].ToString();
                saleOrder.branch = row[6].ToString();
                list.Add(saleOrder);
            }
            return list;
        }
    }
}