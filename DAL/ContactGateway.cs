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
    public class ContactGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        public List<Contact> SelectAll()
        {
            List<Contact> contacts = new List<Contact>();
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM contact";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(table);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Contact contact = new Contact();
                contact.id = Convert.ToInt32(row[0].ToString());
                contact.name = row[1].ToString();
                contact.email = row[2].ToString();
                contact.address = row[3].ToString();
                contact.phone = row[4].ToString();
                contacts.Add(contact);
            }
            return contacts;
        }

        public Contact Select(int id)
        {
            Contact contact = new Contact();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM contact WHERE id=" + id;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                contact.id = Convert.ToInt32(row[0].ToString());
                contact.name = row[1].ToString();
                contact.email = row[2].ToString();
                contact.address = row[3].ToString();
                contact.phone = row[4].ToString();
            }
            return contact;
        }

        public int Insert(Contact contact)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO contact VALUES(@name,@email,@address,@phone)";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", contact.name);
                sqlCmd.Parameters.AddWithValue("@email", contact.email);
                sqlCmd.Parameters.AddWithValue("@address", contact.address);
                sqlCmd.Parameters.AddWithValue("@phone", contact.phone);
                con.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public int Update(Contact contact)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE contact SET name=@name, email=@email, address=@address, phone=@phone WHERE id=@id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", contact.id);
                sqlCmd.Parameters.AddWithValue("@name", contact.name);
                sqlCmd.Parameters.AddWithValue("@email", contact.email);
                sqlCmd.Parameters.AddWithValue("@address", contact.address);
                sqlCmd.Parameters.AddWithValue("@phone", contact.phone);
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
                string query = "DELETE FROM contact WHERE id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@id", id);
                return sqlCmd.ExecuteNonQuery();
            }
        }
    }
}