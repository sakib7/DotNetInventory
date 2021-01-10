using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Data.SqlClient;
using System.Data;

namespace Inventory.DAL
{
    public class CategoryGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        
        public List<Category> SelectAll()
        {
            User userSession = HttpContext.Current.Session["user"] as User;
            List<Category> categories = new List<Category>();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM category WHERE organizationID="+userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            for(int i=0; i<table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Category category = new Category();
                category.id = Convert.ToInt32(row[0].ToString());
                category.name = row[1].ToString();
                category.description = row[2].ToString();
                category.orgID = Convert.ToInt32(row[3].ToString());
                categories.Add(category);
            }
            return categories;
        }

        public Category Select(int id)
        {
            Category category = new Category();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM category WHERE id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                category.id = Convert.ToInt32(row[0].ToString());
                category.name = row[1].ToString();
                category.description = row[2].ToString();
                category.orgID = Convert.ToInt32(row[3].ToString());
            }
            return category;
        }

        public int Insert(Category category)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO category VALUES(@name,@description,@orgID);" + "Select Scope_Identity()"; 
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", category.name);
                if (category.description != null)
                {
                    sqlCmd.Parameters.AddWithValue("@description", category.description);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@description", System.DBNull.Value);
                }
                sqlCmd.Parameters.AddWithValue("@orgID", category.orgID);

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

        public int Update(Category category)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE category SET name=@name, description=@description WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", category.id);
                sqlCmd.Parameters.AddWithValue("@name", category.name);
                sqlCmd.Parameters.AddWithValue("@description", category.description);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            return rowsAffected;
        }

        public int Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "DELETE FROM category WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}