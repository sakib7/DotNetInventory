using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Inventory.DAL
{
    public class EmployeeSalesGateway
    {
        public string cs = ConfigurationManager.ConnectionStrings["inventorydb"].ConnectionString;

        

        public List<SalesOrder> SelectAll()
        {
            List<SalesOrder> list = new List<SalesOrder>();
            DataTable dataTable = new DataTable();
            User userSession = HttpContext.Current.Session["user"] as User;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"select Sale.id,Sale.customer,Sale.quantity,Sale.retailPrice,Sale.date,OrgProduct.name from Sale 
                                join SalePurchase on Sale.id=SalePurchase.saleID 
                                join Purchase on Purchase.id=SalePurchase.purchaseID 
                                join Product on Product.id=Sale.productID 
								join OrgProduct on OrgProduct.id=Product.orgProductID
                                where Product.branchID=" + userSession.branchID;
                SqlDataAdapter adapter = new SqlDataAdapter(query, cs);
                adapter.Fill(dataTable);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SalesOrder saleOrder = new SalesOrder();
                DataRow row = dataTable.Rows[i];
                saleOrder.id = Convert.ToInt32(row[0].ToString());
                saleOrder.contact = row[1].ToString();
                saleOrder.selectedProductQuantity = Convert.ToInt32(row[2]);
                saleOrder.total = Convert.ToDouble(row[3]);
                saleOrder.orderDate = Convert.ToDateTime(row[4].ToString());
                saleOrder.productName = row[5].ToString();
                list.Add(saleOrder);
            }
            return list;
        }

        public int Insert(SalesOrder salesOrder)
        {
            int id=0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                foreach (CartItem item in salesOrder.cart)
                {

                    List<PurchaseSlot> purchaseSlots = new List<PurchaseSlot>();
                    int productID = item.Product.id;
                    int quantity = item.Quantity;
                    string cust = salesOrder.contact;
                    double rate = item.Product.retailPrice;
                    double subtotal = item.SubTotal;
                    DateTime dateTime = salesOrder.orderDate;
                    string query = "INSERT INTO Sale VALUES("+productID+",'"+cust+"',"+quantity+","+rate+",@d);" + "Select Scope_Identity()";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    var sql_parameter = new SqlParameter("@d", SqlDbType.DateTime2);
                    sql_parameter.Value = dateTime;
                    sqlCmd.Parameters.Add(sql_parameter);
                    con.Open();
                    id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    con.Close();
                    DataTable table = new DataTable();
                    query = "select id, remStock from Purchase where productID=" + productID + " AND status='received'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(table);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        PurchaseSlot purchaseSlot = new PurchaseSlot();
                        purchaseSlot.purchaseID = Convert.ToInt32(row[0]);
                        purchaseSlot.stock = Convert.ToInt32(row[1]);
                        purchaseSlots.Add(purchaseSlot);
                    }
                    foreach(PurchaseSlot slot in purchaseSlots)
                    {
                        if (quantity <= slot.stock)
                        {
                            slot.stock -= quantity;
                            query = "INSERT INTO SalePurchase VALUES("+id+","+slot.purchaseID+","+quantity+");";
                            sqlCmd = new SqlCommand(query, con);
                            con.Open();
                            Convert.ToInt32(sqlCmd.ExecuteScalar());
                            con.Close();

                            query = "UPDATE Purchase SET remStock=@stock WHERE id=@id";
                            sqlCmd = new SqlCommand(query, con);
                            sqlCmd.Parameters.AddWithValue("@stock", slot.stock);
                            sqlCmd.Parameters.AddWithValue("@id", slot.purchaseID);
                            con.Open();
                            sqlCmd.ExecuteNonQuery();
                            con.Close();

                            if(slot.stock == 0)
                            {
                                query = "UPDATE Purchase SET status='sold' WHERE id=@id";
                                sqlCmd = new SqlCommand(query, con);
                                sqlCmd.Parameters.AddWithValue("@id", slot.purchaseID);
                                con.Open();
                                sqlCmd.ExecuteNonQuery();
                                con.Close();
                            }
                            break;
                        }
                        else
                        {
                            quantity -= slot.stock;
                            query = "INSERT INTO SalePurchase VALUES(" + id + "," + slot.purchaseID + "," + slot.stock + ");";
                            sqlCmd = new SqlCommand(query, con);
                            con.Open();
                            Convert.ToInt32(sqlCmd.ExecuteScalar());
                            con.Close();
                            slot.stock = 0;
                            query = "UPDATE Purchase SET remStock=@stock, status='sold' WHERE id=@id";
                            sqlCmd = new SqlCommand(query, con);
                            sqlCmd.Parameters.AddWithValue("@stock", slot.stock);
                            sqlCmd.Parameters.AddWithValue("@id", slot.purchaseID);
                            con.Open();
                            sqlCmd.ExecuteNonQuery();
                            con.Close();
                        }

                    }
                }

                /*string query = "INSERT INTO Branch VALUES(@name,@address,@organizationID);" + "Select Scope_Identity()";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@name", salesOrder.cart);
                sqlCmd.Parameters.AddWithValue("@address", salesOrder.address);
                sqlCmd.Parameters.AddWithValue("@organizationID", salesOrder.orgID);
                con.Open();
                try
                {
                    id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
                catch
                {
                    id = 0;
                }*/
            }
            return id;
        }
    }

    public class PurchaseSlot
    {
        public int purchaseID { get; set; }
        public int stock { get; set; }
    }
}