using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.DAL
{
    public class OrgProductGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        User userSession = HttpContext.Current.Session["user"] as User;

        public List<OrgProduct> SelectAll()
        {
            
            List<OrgProduct> list = new List<OrgProduct>();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT * FROM OrgProduct JOIN
                                Organization ON OrgProduct.organizationID=Organization.id JOIN
                                Category ON OrgProduct.categoryID = Category.id 
                                WHERE OrgProduct.organizationID="+ userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                OrgProduct orgProduct = new OrgProduct();
                DataRow row = dataTable.Rows[i];
                orgProduct.id = Convert.ToInt32(row[0].ToString());
                orgProduct.name = row[1].ToString();
                orgProduct.brand = row[2].ToString();
                orgProduct.orgID = Convert.ToInt32(row[3].ToString());
                orgProduct.categoryID = Convert.ToInt32(row[4].ToString());
                orgProduct.organization = row[6].ToString();
                orgProduct.category = row[10].ToString();
                list.Add(orgProduct);
            }
            return list;
        }

        public OrgProduct Select(int id)
        {
            OrgProduct orgProduct = new OrgProduct();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM OrgProduct WHERE id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                orgProduct.id = Convert.ToInt32(row[0].ToString());
                orgProduct.name = row[1].ToString();
                orgProduct.brand = row[2].ToString();
                orgProduct.orgID = Convert.ToInt32(row[3].ToString());
                orgProduct.categoryID = Convert.ToInt32(row[4].ToString());
            }
            return orgProduct;
        }

        public int Insert(OrgProduct orgProduct)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO OrgProduct VALUES(@name,@brand,@orgID,@categoryID);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", orgProduct.name);
                sqlCmd.Parameters.AddWithValue("@brand", orgProduct.brand);
                sqlCmd.Parameters.AddWithValue("@orgID", orgProduct.orgID);
                sqlCmd.Parameters.AddWithValue("@categoryID", orgProduct.categoryID);
                con.Open();
                id = Convert.ToInt32(sqlCmd.ExecuteScalar());
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

        public int Update(OrgProduct orgProduct)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE OrgProduct SET name=@name, brand=@brand, categoryID=@categoryID WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", orgProduct.name);
                sqlCmd.Parameters.AddWithValue("@brand", orgProduct.brand);
                sqlCmd.Parameters.AddWithValue("@categoryID", orgProduct.categoryID);
                sqlCmd.Parameters.AddWithValue("@id", orgProduct.id);
                con.Open();
                id = sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            return id;
        }

        public int Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "DELETE FROM OrgProduct WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}