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
    public class ProductGatewayOld
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

        public Product Select(int id)
        {
            Product product = new Product();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT product.id,product.name,product.stock,product.price,
                                product.category_id,category.name,
                                product.warehouse_id,warehouse.name
                                FROM product
                                INNER JOIN category ON product.category_id = category.id
                                INNER JOIN warehouse ON product.warehouse_id = warehouse.id WHERE product.id="+id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                product.id = Convert.ToInt32(row[0].ToString());
                //product.name = row[1].ToString();
                //product.stock = Convert.ToInt32(row[2].ToString());
                //product.price = Convert.ToDouble(row[3].ToString());
                //product.category_id = Convert.ToInt32(row[4].ToString());
                //product.category_name = row[5].ToString();
                //product.warehouse_id = Convert.ToInt32(row[6].ToString());
                //product.warehouse_name = row[7].ToString();
            }
            return product;
        }

        public int Insert(Product product)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO product VALUES(@name,@stock,@price,@category_id,@warehouse_id)";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                //sqlCmd.Parameters.AddWithValue("@name", product.name);
                //sqlCmd.Parameters.AddWithValue("@stock", product.stock);
                //sqlCmd.Parameters.AddWithValue("@price", product.price);
                //sqlCmd.Parameters.AddWithValue("@category_id", product.category_id);
                //sqlCmd.Parameters.AddWithValue("@warehouse_id", product.warehouse_id);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public int Update(Product product)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE product SET name=@name,stock=@stock,price=@price,category_id=@category_id,warehouse_id=@warehouse_id WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", product.id);
                //sqlCmd.Parameters.AddWithValue("@name", product.name);
                //sqlCmd.Parameters.AddWithValue("@stock", product.stock);
                //sqlCmd.Parameters.AddWithValue("@price", product.price);
                //sqlCmd.Parameters.AddWithValue("@category_id", product.category_id);
                //sqlCmd.Parameters.AddWithValue("@warehouse_id", product.warehouse_id);
                //con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            return rowsAffected;
        }
    }
}