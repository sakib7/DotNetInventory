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
    public class EmployeeGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;
        

        public List<Employee> SelectAll()
        {
            List<Employee> list = new List<Employee>();
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl JOIN Branch ON " +
                    "UserTbl.branchID=Branch.id  " +
                    "WHERE branchID IS NOT NULL AND UserTbl.organizationID=" + userSession.orgID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Employee employee = new Employee();
                DataRow row = dataTable.Rows[i];
                employee.id = Convert.ToInt32(row[0].ToString());
                employee.name = row[1].ToString();
                employee.username = row[2].ToString();
                employee.password = row[3].ToString();
                employee.role = row[4].ToString();
                employee.orgID = Convert.ToInt32(row[5].ToString());
                employee.branchID = Convert.ToInt32(row[6].ToString());
                employee.branch = row[8].ToString();
                list.Add(employee);
            }
            return list;
        }

        public Employee Select(int id)
        {
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM UserTbl JOIN Branch ON " +
                    "UserTbl.branchID=Branch.id  " +
                    "WHERE branchID IS NOT NULL AND UserTbl.id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            Employee employee = new Employee();
            DataRow row = dataTable.Rows[0];
            employee.id = Convert.ToInt32(row[0].ToString());
            employee.name = row[1].ToString();
            employee.username = row[2].ToString();
            employee.password = row[3].ToString();
            employee.role = row[4].ToString();
            employee.orgID = Convert.ToInt32(row[5].ToString());
            employee.branchID = Convert.ToInt32(row[6].ToString());
            employee.branch = row[8].ToString();
            return employee;
        }

        public int Insert(Employee employee)
        {
            int id;
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO UserTbl VALUES(@name,@username,@password,@role,@org,@branch);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", employee.name);
                sqlCmd.Parameters.AddWithValue("@username", employee.username);
                sqlCmd.Parameters.AddWithValue("@password", employee.password);
                sqlCmd.Parameters.AddWithValue("@role", "employee");
                sqlCmd.Parameters.AddWithValue("@org", userSession.orgID);
                sqlCmd.Parameters.AddWithValue("@branch", employee.branchID);
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

        public int Update(Employee employee)
        {
            int id;
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE UserTbl SET name=@name, user_name=@username, pass=@password, branchID=@branchID WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", employee.name);
                sqlCmd.Parameters.AddWithValue("@username", employee.username);
                sqlCmd.Parameters.AddWithValue("@password", employee.password);
                sqlCmd.Parameters.AddWithValue("@branchID", employee.branchID);
                sqlCmd.Parameters.AddWithValue("@id", employee.id);
                con.Open();
                id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                //var vret = sqlCmd.ExecuteScalar();
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

        public int Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "DELETE FROM UserTbl WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}