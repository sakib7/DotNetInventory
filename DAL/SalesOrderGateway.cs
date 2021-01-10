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
    public class SalesOrderGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Product> SelectAll()
        {
            List<Product> list = new List<Product>();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT product.id,product.name,product.stock,product.price,
                                product.category_id,category.name,
                                product.warehouse_id,warehouse.name
                                FROM product
                                INNER JOIN category ON product.category_id = category.id
                                INNER JOIN warehouse ON product.warehouse_id = warehouse.id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Product product = new Product();
                DataRow row = dataTable.Rows[i];
                product.id = Convert.ToInt32(row[0].ToString());
                //product.name = row[1].ToString();
                //product.stock = Convert.ToInt32(row[2].ToString());
                //product.price = Convert.ToDouble(row[3].ToString());
                //product.category_id = Convert.ToInt32(row[4].ToString());
                //product.category_name = row[5].ToString();
                //product.warehouse_id = Convert.ToInt32(row[6].ToString());
                //product.warehouse_name = row[7].ToString();
                list.Add(product);
            }
            return list;
        }

        public int Insert(SalesOrder salesOrder)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO sales VALUES(@date,@charge,@contact_id)";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@date", salesOrder.orderDate);
                sqlCmd.Parameters.AddWithValue("@charge", salesOrder.CalculateTotal());
                sqlCmd.Parameters.AddWithValue("@contact_id", salesOrder.contact);
                SqlParameter idParameter = new SqlParameter("@id", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(idParameter);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
                int id = (int) idParameter.Value;
                foreach(CartItem item in salesOrder.cart)
                {
                    string query1 = "INSERT INTO sales_product VALUES(@sales_id,@product_id,@quantity)";
                    SqlCommand sqlCmd1 = new SqlCommand(query1, con);
                    sqlCmd.Parameters.AddWithValue("@sales_id", salesOrder.orderDate);
                    sqlCmd.Parameters.AddWithValue("@product_id", salesOrder.CalculateTotal());
                    sqlCmd.Parameters.AddWithValue("@quantity", salesOrder.contact);
                }
            }
            return rowsAffected;
        }
    }
}