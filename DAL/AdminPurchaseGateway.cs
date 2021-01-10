using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;
using Inventory.DAL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.DAL
{
    public class AdminPurchaseGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Purchase> SelectAll()
        {
            List<Purchase> list = new List<Purchase>();
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,
                                Purchase.remStock,Purchase.date,Purchase.purchasePrice ,
                                Vendor.id,Vendor.name,
                                Branch.id,Branch.name,Purchase.quantityReq
                                FROM Organization
                                JOIN Branch ON Branch.organizationID=Organization.id
                                JOIN Product ON Product.branchID = Branch.id
                                JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
                                JOIN Purchase ON Purchase.productID = Product.id
                                LEFT JOIN Vendor ON Vendor.id = Purchase.vendorID
                                WHERE Organization.id = " + userSession.orgID +
                                " ORDER BY Purchase.date desc";
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
                    purchase.purchasePrice = Convert.ToDouble(row[6].ToString());
                }
                if (!string.IsNullOrEmpty(row[7].ToString()))
                {
                    purchase.vendorID = Convert.ToInt32(row[7].ToString());
                }
                if (!string.IsNullOrEmpty(row[8].ToString()))
                {
                    purchase.vendorName = row[8].ToString();
                }
                else
                {
                    purchase.vendorName = "---";
                }
                purchase.branchID = Convert.ToInt32(row[9].ToString());
                purchase.branchName = row[10].ToString();
                if (!string.IsNullOrEmpty(row[11].ToString()))
                {
                    purchase.quantityReq = Convert.ToInt32(row[11]);
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
                string query = @"SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,
                                Purchase.remStock,Purchase.date,Purchase.purchasePrice ,
                                Vendor.id,Vendor.name,
                                Branch.id,Branch.name,Purchase.quantityReq
                                FROM Organization
                                JOIN Branch ON Branch.organizationID=Organization.id
                                JOIN Product ON Product.branchID = Branch.id
                                JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
                                JOIN Purchase ON Purchase.productID = Product.id
                                LEFT JOIN Vendor ON Vendor.id = Purchase.vendorID
                                WHERE Purchase.id = " + id;
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
            if (!string.IsNullOrEmpty(row[6].ToString()))
            {
                purchase.purchasePrice = Convert.ToDouble(row[6].ToString());
            }
            if (!string.IsNullOrEmpty(row[7].ToString()))
            {
                purchase.vendorID = Convert.ToInt32(row[7].ToString());
            }
            if (!string.IsNullOrEmpty(row[8].ToString()))
            {
                purchase.vendorName = row[8].ToString();
            }
            else
            {
                purchase.vendorName = "---";
            }
            purchase.branchID = Convert.ToInt32(row[9].ToString());
            purchase.branchName = row[10].ToString();
            if (!string.IsNullOrEmpty(row[11].ToString()))
            {
                purchase.quantityReq = Convert.ToInt32(row[11]);
            }

            return purchase;
        }

        public int Update(Purchase purchase)
        {
            int id;
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Purchase SET vendorID=@vendorID, status=@status, quantity=@quantity, purchasePrice=@purchasePrice WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@vendorID", purchase.vendorID);
                sqlCmd.Parameters.AddWithValue("@quantity", purchase.quantity);
                sqlCmd.Parameters.AddWithValue("@purchasePrice", purchase.purchasePrice);
                sqlCmd.Parameters.AddWithValue("@id", purchase.id);
                sqlCmd.Parameters.AddWithValue("@status", "accepted");
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