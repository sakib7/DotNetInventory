using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Inventory.DAL
{
    public class UserGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public User Select(int id)
        {
            User user = new User();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl WHERE id="+id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count != 0)
            {
                DataRow row = dataTable.Rows[0];
                user = new User();
                user.id = Convert.ToInt32(row[0].ToString());
                user.name = row[1].ToString();
                user.username = row[2].ToString();
                user.password = row[3].ToString();
                user.role = row[4].ToString();
                //user.orgID = Convert.ToInt32(row[5].ToString());
                //user.branchID = Convert.ToInt32(row[6].ToString());
            }
            return user;
        }

        public int Insert(User user)
        {
            int id;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO UserTbl VALUES(@name,@username,@password,@role,@org,@branch);" + "Select Scope_Identity()"; 
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", user.name);
                sqlCmd.Parameters.AddWithValue("@username", user.username);
                sqlCmd.Parameters.AddWithValue("@password", user.password);
                sqlCmd.Parameters.AddWithValue("@role", "admin");
                sqlCmd.Parameters.AddWithValue("@org", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@branch", DBNull.Value);
                con.Open();
                //var vret = sqlCmd.ExecuteScalar();
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

        public User Auth(string username, string password)
        {
            User user = null;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl WHERE user_name=@username AND pass=@password";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count != 0)
            {
                string query = "SELECT * FROM UserTbl JOIN Organization ON Organization.id=UserTbl.organizationID JOIN Branch ON Branch.id=UserTbl.branchID WHERE user_name=@username AND pass=@password";
                DataRow row = dataTable.Rows[0];
                user = new User();
                user.id = Convert.ToInt32(row[0].ToString());
                user.name = row[1].ToString();
                user.username = row[2].ToString();
                user.password = row[3].ToString();
                user.role = row[4].ToString();
                if(row[5] != DBNull.Value)
                    user.orgID = Convert.ToInt32(row[5].ToString());
                if (row[6] != DBNull.Value)
                    user.branchID = Convert.ToInt32(row[6].ToString());
            }
            return user;
        }

        public User SelectAdmin(string username, string password)
        {
            User user = null;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl  JOIN Organization ON Organization.id=UserTbl.organizationID WHERE user_name=@username AND pass=@password";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count != 0)
            {
                string query = "SELECT * FROM UserTbl JOIN Organization ON Organization.id=UserTbl.organizationID JOIN Branch ON Branch.id=UserTbl.branchID WHERE user_name=@username AND pass=@password";
                DataRow row = dataTable.Rows[0];
                user = new User();
                user.id = Convert.ToInt32(row[0].ToString());
                user.name = row[1].ToString();
                user.username = row[2].ToString();
                user.password = row[3].ToString();
                user.role = row[4].ToString();
                if (row[5] != DBNull.Value)
                {
                    user.orgID = Convert.ToInt32(row[5].ToString());
                    user.organization = row[8].ToString();
                }
                if (row[6] != DBNull.Value)
                    user.branchID = Convert.ToInt32(row[6].ToString());
            }
            return user;
        }

        public User SelectEmployee(string username, string password)
        {
            User user = null;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl JOIN Organization ON Organization.id=UserTbl.organizationID JOIN Branch ON Branch.id=UserTbl.branchID WHERE user_name=@username AND pass=@password";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count != 0)
            {
                string query = "SELECT * FROM UserTbl JOIN Organization ON Organization.id=UserTbl.organizationID JOIN Branch ON Branch.id=UserTbl.branchID WHERE user_name=@username AND pass=@password";
                DataRow row = dataTable.Rows[0];
                user = new User();
                user.id = Convert.ToInt32(row[0].ToString());
                user.name = row[1].ToString();
                user.username = row[2].ToString();
                user.password = row[3].ToString();
                user.role = row[4].ToString();
                if (row[5] != DBNull.Value)
                {
                    user.orgID = Convert.ToInt32(row[5].ToString());
                    user.organization = row[8].ToString();
                }
                if (row[6] != DBNull.Value)
                {
                    user.branchID = Convert.ToInt32(row[6].ToString());
                    user.branch = row[12].ToString();
                }
            }
            return user;
        }
    }
}