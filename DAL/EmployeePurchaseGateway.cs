using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inventory.DAL
{
    public class EmployeePurchaseGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Purchase> SelectAll()
        {
            List<Purchase> list = new List<Purchase>();
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,Purchase.remStock,Purchase.date,Purchase.quantityReq
                                FROM Purchase
                                JOIN Product ON Product.id = Purchase.productID
                                JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
                                JOIN Branch ON Product.branchID = Branch.id
                                WHERE Branch.id=" + userSession.branchID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Purchase purchase = new Purchase();
                DataRow row = dataTable.Rows[i];
                purchase.id = Convert.ToInt32(row[0].ToString());
                purchase.product = row[1].ToString();
                purchase.status = row[2].ToString();
                purchase.quantity = Convert.ToInt32(row[3].ToString());
                if (!string.IsNullOrEmpty(row[4].ToString()))
                {
                    purchase.stock = Convert.ToInt32(row[4].ToString());
                }
                
                purchase.date = Convert.ToDateTime(row[5].ToString());
                if (!string.IsNullOrEmpty(row[6].ToString()))
                {
                    purchase.quantityReq = Convert.ToInt32(row[6].ToString());
                }
                list.Add(purchase);
            }
            return list;
        }

        public Purchase Select(int id)
        {
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,Purchase.remStock,Purchase.date
                                FROM Purchase
                                JOIN Product ON Product.id = Purchase.productID
                                JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
                                JOIN Branch ON Product.branchID = Branch.id
                                WHERE Purchase.id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }

                Purchase purchase = new Purchase();
                DataRow row = dataTable.Rows[0];
                purchase.id = Convert.ToInt32(row[0].ToString());
                purchase.product = row[1].ToString();
                purchase.status = row[2].ToString();
                purchase.quantity = Convert.ToInt32(row[3].ToString());
                if (!string.IsNullOrEmpty(row[4].ToString()))
                {
                    purchase.stock = Convert.ToInt32(row[4].ToString());
                }

                purchase.date = Convert.ToDateTime(row[5].ToString());
                
            return purchase;
        }

        public int Insert(Purchase purchase)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Purchase VALUES(@productID,@vendorID,@status,@quantity,@remStock,@purchasePrice,@date,@quantityReq);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@productID", purchase.productID);
                sqlCmd.Parameters.AddWithValue("@vendorID", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@status", purchase.status);
                sqlCmd.Parameters.AddWithValue("@quantity", id);
                sqlCmd.Parameters.AddWithValue("@quantityReq", purchase.quantityReq);
                sqlCmd.Parameters.AddWithValue("@remStock", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@purchasePrice", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@date", purchase.date);
                con.Open();
                id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                try
                {
                    //id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
                catch
                {
                    id = 0;
                }
            }
            return id;
        }

        public int Update(Purchase purchase)
        {
            int id;
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Purchase SET status=@status, remStock=@quantity WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@quantity", purchase.quantity);
                sqlCmd.Parameters.AddWithValue("@purchasePrice", purchase.purchasePrice);
                sqlCmd.Parameters.AddWithValue("@id", purchase.id);
                sqlCmd.Parameters.AddWithValue("@status", "received");
                con.Open();
                id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                //var vret = sqlCmd.ExecuteScalar();
                try
                {

                }
                catch
                {
                    id = 0;
                }
            }
            return id;
        }
    }
}