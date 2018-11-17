using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class tax_dataset_print
    {
        private OleDbConnection connection = new OleDbConnection();

        public DataSet Invoice_product()
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string command = "select * from invoice where(in_no = @in) and (type='tax')";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", tax_invoice_print.in_no);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return ds;


        }
        public DataSet Invoice_main()
        {

            string command = "select * from in_main where(in_no = @in) and (type='tax')";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", tax_invoice_print.in_no);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);
            return ds2;

        }

        public DataSet Customer_info()
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string command = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country FROM customer WHERE  (c_code = @Cust_id)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@Cust_id", tax_invoice_print.c_name);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds3 = new DataSet();
            da.Fill(ds3);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return ds3;

        }
        public DataSet gst()
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string command = "SELECT c_gst FROM company where (setDefault =0)";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds4 = new DataSet();
            da.Fill(ds4);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return ds4;
        }
    }
}
