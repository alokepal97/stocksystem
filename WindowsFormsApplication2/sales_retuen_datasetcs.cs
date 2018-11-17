using System.Data;
using System.Data.OleDb;


namespace WindowsFormsApplication2
{
    class sales_retuen_datasetcs
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
            string command = "select * from sales_return where(n_no = @in) ";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", 1);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Tables[0].TableName = "sales-return";
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
            string command = "select * from tax_main where(in_no = @in) ";
            OleDbCommand cmdd = new OleDbCommand(command, connection);
            cmdd.Parameters.AddWithValue("@in", tax_invoice_print.in_no);
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
            string command = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country FROM customer WHERE  (C_name = @Cust_id) ";
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
            string command = "SELECT c_gst FROM company";
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
