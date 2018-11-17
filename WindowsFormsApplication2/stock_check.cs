using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{

    class stock_check
    {

        public static Double stock = 0;

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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}

