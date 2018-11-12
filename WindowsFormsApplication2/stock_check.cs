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

    class stock_check
    {
       
        public static Double stock = 0 ;
      
        public void connection()
        {

           
        }

        public void getstock()
        {
            connection con = new connection();
            string ConnectionString = con.ConnectionString;
            
       string strsql = "select * from stock where  (item_code = @id)";
       OleDbConnection conn = new OleDbConnection(ConnectionString);
       OleDbCommand cmd = new OleDbCommand(strsql, conn);
       cmd.Parameters.AddWithValue("@id", invoice.code.ToString());

       try
       {

           conn.Open();
           OleDbDataReader reader = cmd.ExecuteReader();

           if (reader.Read())
           {
               stock = Convert.ToInt32(reader["receive_qty"].ToString());
           }
       }
       catch (Exception tp)
       {

           MessageBox.Show("" + tp);
       }
       finally
       {

           conn.Close();
       }
        }

        }
    }

