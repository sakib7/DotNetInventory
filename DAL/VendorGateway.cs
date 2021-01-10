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
    public class VendorGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Vendor> SelectAll()
        {
            List<Vendor> vendors = new List<Vendor>();
            User userSession = HttpContext.Current.Session["user"] as User;
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Vendor WHERE organizationID="+userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Vendor vendor = new Vendor();
                vendor.id = Convert.ToInt32(row[0].ToString());
                vendor.name = row[1].ToString();
                vendor.address = row[2].ToString();
                vendor.phone = row[3].ToString();
                vendor.orgID = Convert.ToInt32(row[4].ToString());
                vendors.Add(vendor);
            }
            return vendors;
        }

        public Vendor Select(int id)
        {
            Vendor vendor = new Vendor(); ;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Vendor WHERE id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                vendor.id = Convert.ToInt32(row[0].ToString());
                vendor.name = row[1].ToString();
                vendor.address = row[2].ToString();
                vendor.phone = row[3].ToString();
                vendor.orgID = Convert.ToInt32(row[4].ToString());
            }
            return vendor;
        }

        public int Insert(Vendor vendor)
        {
            int rowsAffected = 0;

            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Vendor VALUES(@name,@address,@phone,@orgID)";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", vendor.name);
                sqlCmd.Parameters.AddWithValue("@address", vendor.address);
                sqlCmd.Parameters.AddWithValue("@phone", vendor.phone);
                sqlCmd.Parameters.AddWithValue("@orgID", userSession.orgID);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public int Update(Vendor vendor)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Vendor SET name=@name, address=@address, phone=@phone WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", vendor.id);
                sqlCmd.Parameters.AddWithValue("@name", vendor.name);
                sqlCmd.Parameters.AddWithValue("@address", vendor.address);
                sqlCmd.Parameters.AddWithValue("@phone", vendor.phone);
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
                string query = "DELETE FROM Vendor WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}