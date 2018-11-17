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
    public partial class due_invoice : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public due_invoice()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedrow = 0;
        DataSet dueinvoiceds = new DataSet();
        private void gridview()
        {
            OleDbDataReader rdr = null;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT id, in_no, in_date, c_name, amount, status, due_amount FROM in_main WHERE (status = 'Due') Order by in_date ASC", connection);
            try
            {
                OleDbDataAdapter dt = new OleDbDataAdapter(cmd);
                dt.Fill(dueinvoiceds);

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    
                    dataGridView1.Rows.Add(Convert.ToString(rdr["id"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["due_amount"]));
                  
                }
             }
            catch (Exception)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                // gridview1();
            }
        }

        //private void gridview1()
        //{
        //    OleDbDataReader rdr = null;
        //    OleDbCommand cmd = new OleDbCommand("SELECT id, in_no, in_date, c_name, amount, status, due_amount FROM tax_main WHERE (status = 'Due') Order By in_date ASC", connection);
        //    try
        //    {
        //        connection.Close();
        //        connection.Open();
        //        rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {

        //            dataGridView1.Rows.Add(Convert.ToString(rdr["id"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["due_amount"]));

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (e.RowIndex != -1)
            {
                try
                {
                    selectedrow = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[selectedrow];

                    DateTime strt_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    DateTime end_date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                    //DateTime add_days = end_date.AddDays(1);
                    TimeSpan nod = (strt_date - end_date);
                    var days = nod.TotalDays;
                    dataGridView2.Rows.Add(row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), days.ToString());
                }
                catch (Exception y)
                {
                    MessageBox.Show(y.ToString());
                }
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            payment_re it = new payment_re();
            it.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.due_invoice_report tr = new Excel.due_invoice_report();
            tr.ShowDialog();
        }



    }
}
