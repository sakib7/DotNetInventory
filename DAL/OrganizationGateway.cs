using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Inventory.DAL
{
    public class OrganizationGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        public int Insert(Organization org)
        {
            int id;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Organization VALUES(@name,@description,@address);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", org.name);
                sqlCmd.Parameters.AddWithValue("@description", org.description);
                sqlCmd.Parameters.AddWithValue("@address", org.address);
                con.Open();
                //var vret = sqlCmd.ExecuteScalar();
                try
                {
                    id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    User userSession = HttpContext.Current.Session["user"] as User;
                    query = "UPDATE UserTbl SET organizationID=@id WHERE id=" + userSession.id;
                    sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    sqlCmd.ExecuteNonQuery();
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