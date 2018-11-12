using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication2
{
    class insert_update_invoice
    {
        public static Double id = 0;



        public void update_stock()
        {
            connection con = new connection();
            string ConnectionString = con.ConnectionString;
            
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand comm = new OleDbCommand(@"UPDATE stock
                                                   SET receive_qty = @receive_qty
                                                  WHERE item_code = @item_code", conn);

            comm.Parameters.AddWithValue("@receive_qty", invoice.qty);
            comm.Parameters.AddWithValue("@item_code", invoice.code);


            try
            {

                conn.Open();
                comm.ExecuteNonQuery();


            }
            catch (Exception tp)
            {

                MessageBox.Show("update stock" + tp);
            }
            finally
            {

                conn.Close();
            }
        }


      /*  public void checkpayment_id()
        {
            string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Aloke\Documents\Visual Studio 2010\Projects\WindowsFormsApplication2\WindowsFormsApplication2\stock.accdb;
Persist Security Info=False;";
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand com = new OleDbCommand(@"SELECT re_no FROM payment_receipt WHERE (id =(SELECT MAX(id) AS Expr1 FROM payment_receipt payment_receipt_1))", conn);

            try
            {
                conn.Open();
                OleDbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    id = Convert.ToDouble(reader["id"].ToString());
                }
            }
            catch (Exception tp)
            {
                MessageBox.Show("payment receipt" + tp);
            }
            finally
            {
                conn.Close();
            }
        }*/


    }
}
