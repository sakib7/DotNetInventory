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
    public class EmployeeProductGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Product> SelectAll()
        {
            User userSession = HttpContext.Current.Session["user"] as User;
            List<Product> products = new List<Product>();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT * FROM Product
                                JOIN OrgProduct ON Product.orgProductID = OrgProduct.id
                                JOIN Category ON Category.id = OrgProduct.categoryID
                                JOIN Branch ON Branch.id = Product.branchID
                                WHERE Product.branchID=" + userSession.branchID;
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
                product.categoryID = Convert.ToInt32(row[9].ToString());
                product.category = row[10].ToString();
                product.branchID = Convert.ToInt32(row[13].ToString());
                product.branch = row[14].ToString();
                product.retailPrice = Convert.ToDouble(row[3].ToString());
                product.stock = Stock(product.id);
                products.Add(product);
            }
            return products;
        }

        public Product Select(int productID)
        {
            User userSession = HttpContext.Current.Session["user"] as User;
            Product product = new Product();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT * FROM Product
                                JOIN OrgProduct ON Product.orgProductID = OrgProduct.id
                                JOIN Category ON Category.id = OrgProduct.categoryID
                                JOIN Branch ON Branch.id = Product.branchID
                                WHERE Product.branchID=" + userSession.branchID+
                                " AND Product.id=" + productID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                
                product.id = Convert.ToInt32(row[0].ToString());
                product.orgProductID = Convert.ToInt32(row[4].ToString());
                product.orgProduct = row[5].ToString();
                product.brand = row[6].ToString();
                product.categoryID = Convert.ToInt32(row[9].ToString());
                product.category = row[10].ToString();
                product.branchID = Convert.ToInt32(row[13].ToString());
                product.branch = row[14].ToString();
                product.retailPrice = Convert.ToDouble(row[3].ToString());
                product.stock = Stock(product.id);
            }
            return product;
        }

        public int Stock(int id)
        {
            int stock=0;
            User userSession = HttpContext.Current.Session["user"] as User;
            List<Product> products = new List<Product>();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT SUM(remStock) FROM Purchase WHERE productID=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            DataRow row = table.Rows[0];
            if (row[0] != DBNull.Value)
            {
                stock = Convert.ToInt32(row[0].ToString());
            }
            
            return stock;
        }
    }
}