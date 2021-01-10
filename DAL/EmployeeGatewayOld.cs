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
    public class EmployeeGatewayOld
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public EmployeeOld Auth(string username, string password)
        {
            EmployeeOld emp = null;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM employee WHERE user_name=@username AND pass=@password";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count != 0)
            {
                DataRow row = dataTable.Rows[0];
                emp = new EmployeeOld();
                emp.id = Convert.ToInt32(row[0].ToString());
                emp.username = row[1].ToString();
                emp.password = row[2].ToString();
                emp.email = row[3].ToString();
                emp.firstname = row[4].ToString();
                emp.lastname = row[5].ToString();
                emp.role = row[6].ToString();
            }
            return emp;
        }

    }
}