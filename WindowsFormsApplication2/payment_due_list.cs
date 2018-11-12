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
    public partial class payment_due_list : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public payment_due_list()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
        }

        private void grid()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from payment_receipt where (due_amount <> '0') Order by in_date ASC", connection);
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["re_no"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["total_amount"]), Convert.ToString(rdr["due_amount"]), Convert.ToString(rdr["total_receive"]));
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
