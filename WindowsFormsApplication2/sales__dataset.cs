using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication2
{
    class sales__dataset
    {

        private OleDbConnection connection = new OleDbConnection();

        public DataSet Invoice_product(string invid)
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            connection.Open();
            string command = "select * from invoice where(in_no = @in) and (type=@type)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invid);
            cmdd.Parameters.AddWithValue("@type", "in");
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
           

        }
        public DataSet Invoice_main()
        {
            
            string command = "select * from in_main where(in_no = @in) and (type=@type)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invoice_print.in_no);
            cmdd.Parameters.AddWithValue("@type", "in");
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);
            return ds2;

        }

        public DataSet Customer_info()
        {
            
                connection con = new connection();
                connection.ConnectionString = con.ConnectionString;
                connection.Open();
                string command = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country FROM customer WHERE  (c_code = @Cust_id) ";
                OleDbCommand cmdd = new OleDbCommand(command, connection);
                cmdd.Parameters.AddWithValue("@Cust_id", invoice_print.c_name);
                OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
                DataSet ds3 = new DataSet();
                da.Fill(ds3);
                connection.Close();
                return ds3;
            
            

        }
        public DataSet tax_Invoice_product(string invid)
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            connection.Open();
            string command = "select * from invoice where(in_no = @in) and (type=@type)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invid);
            cmdd.Parameters.AddWithValue("@type", "tax");
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;

        }

        public DataSet invoice(string invid)
        {
            string command = "select * from invoice where(in_no = @in) and (type=@type)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invid);
            cmdd.Parameters.AddWithValue("@type", invid);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);
            connection.Close();
            return ds2;
        }         
        public DataSet tax_Invoice_main()
        {

            string command = "select * from in_main where(in_no = @in) and (type=@type)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invoice_print.in_no);
            cmdd.Parameters.AddWithValue("@type", "tax");
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);
            connection.Close();
            return ds2;

        }

        public DataSet p_order(Int32 p_no)
        {

            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            connection.Open();
            string command = "select p_no, p_date, d_date, s_name, amount from purchase_main where(p_no = @p_no)";
            OleDbCommand cmdd1 = new OleDbCommand(command, connection);
            cmdd1.Parameters.AddWithValue("@p_no", p_no);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd1);
            DataSet ds4 = new DataSet();
            da.Fill(ds4);
            connection.Close();
            return ds4;

        }


    }
}
