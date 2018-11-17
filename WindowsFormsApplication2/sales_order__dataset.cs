using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
namespace WindowsFormsApplication2
{
    class sales_order_dataset
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
            string command = "select * from invoice where(in_no = @in) ";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", invoice_print.in_no);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string command = "select * from main_sales where(or_no = @in) ";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", sales_order_print.or_no);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
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
            string command = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country FROM customer WHERE (C_name = @Cust_id) ";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@Cust_id", sales_order_print.c_name);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds3 = new DataSet();
            da.Fill(ds3);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return ds3;

        }
       

    }
}
