using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventory.DAL
{
    public class ProductGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Product> SelectAll()
        {
            User userSession = HttpContext.Current.Session["user"] as User;
            List<Product> products = new List<Product>();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Product " +
                    "JOIN OrgProduct ON Product.orgProductID=OrgProduct.id " +
                    "JOIN Branch ON Branch.id=Product.branchID " +
                    "WHERE OrgProduct.organizationID=" + userSession.orgID+
                    " ORDER BY OrgProduct.name";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Product product = new Product();
                product.id = Convert.ToInt32(row[0].ToString());
                product.orgProductID = Convert.ToInt32(row[4].ToString());
                product.orgProduct = row[5].ToString();
                product.brand = row[6].ToString();
                product.branchID = Convert.ToInt32(row[9].ToString());
                product.branch = row[10].ToString();
                product.retailPrice = Convert.ToDouble(row[3].ToString());
                products.Add(product);
            }
            return products;
        }

        public Product Select(int id)
        {
            User userSession = HttpContext.Current.Session["user"] as User;
            Product product = new Product();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Product " +
                    "JOIN OrgProduct ON Product.orgProductID=OrgProduct.id " +
                    "JOIN Branch ON Branch.id=Product.branchID " +
                    "WHERE OrgProduct.organizationID=" + userSession.orgID +
                    " AND Product.id=" + id +
                    " ORDER BY OrgProduct.name";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }

            DataRow row = table.Rows[0];
                
            product.id = Convert.ToInt32(row[0].ToString());
            product.orgProductID = Convert.ToInt32(row[4].ToString());
            product.orgProduct = row[5].ToString();
            product.brand = row[6].ToString();
            product.branchID = Convert.ToInt32(row[9].ToString());
            product.branch = row[10].ToString();
            product.retailPrice = Convert.ToDouble(row[3].ToString());
            
            return product;
        }

        public int Insert(Product product)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Product VALUES(@branchID,@orgProductID,@retailPrice);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@branchID", product.branchID);
                sqlCmd.Parameters.AddWithValue("@orgProductID", product.orgProductID);
                sqlCmd.Parameters.AddWithValue("@retailPrice", product.retailPrice);

                con.Open();
                try
                {
                    id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
                catch
                {
                    id = 0;
                }
            }
            return id;
        }

        public int Update(Product product)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Product SET retailPrice=@retailPrice WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@retailPrice", product.retailPrice);
                sqlCmd.Parameters.AddWithValue("@id", product.id);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            return rowsAffected;
        }


    }
}