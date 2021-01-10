using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Inventory.Models;

namespace Inventory.DAL
{
    public class BranchGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        User userSession = HttpContext.Current.Session["user"] as User;

        public List<Branch> SelectAll()
        {
            List<Branch> list = new List<Branch>();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Branch JOIN Organization ON Branch.organizationID = Organization.id WHERE Branch.organizationID="+userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for(int i=0; i<dataTable.Rows.Count; i++)
            {
                Branch branch = new Branch();
                DataRow row = dataTable.Rows[i];
                branch.id = Convert.ToInt32(row[0].ToString());
                branch.name = row[1].ToString();
                branch.address = row[2].ToString();
                branch.orgID = Convert.ToInt32(row[3].ToString());
                branch.organization = row[5].ToString();
                list.Add(branch);
            }
            return list;
        }

        public Branch Select(int id)
        {
            Branch branch = new Branch();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Branch JOIN Organization ON Branch.organizationID = Organization.id WHERE Branch.organizationID=" + userSession.orgID+ " and Branch.id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if(dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                branch.id = Convert.ToInt32(row[0].ToString());
                branch.name = row[1].ToString();
                branch.address = row[2].ToString();
                branch.orgID = Convert.ToInt32(row[3].ToString());
                branch.organization = row[5].ToString();
            }
            return branch;
        }

        public int Insert(Branch branch)
        {
            int id;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Branch VALUES(@name,@address,@organizationID);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", branch.name);
                sqlCmd.Parameters.AddWithValue("@address", branch.address);
                sqlCmd.Parameters.AddWithValue("@organizationID", branch.orgID);
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

        public int Update(Branch branch)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Branch SET name=@name, address=@address WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", branch.name);
                sqlCmd.Parameters.AddWithValue("@address", branch.address);
                sqlCmd.Parameters.AddWithValue("@id", branch.id);
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
                string query = "DELETE FROM Branch WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}