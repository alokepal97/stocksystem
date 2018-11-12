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
    public partial class sales_retuen_issue : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_retuen_issue()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();

        }
        public static string item_code = "";
        int selectedrow = 0;
        private void gridview()
        {
            if (sales_return.type == "Invoice")
            { 
            //invoice 

                try
                {

                    connection.Open();
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from invoice where (in_no = @in_no )", connection);
                    cmd.Parameters.AddWithValue("@in_no", sales_return.no);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show("" + r);
                }
                finally
                {
                    connection.Close();
                }
            }
            else  
            {
            //tax_invoice


                try
                {

                    connection.Open();
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from tax_invoice where (in_no = @in_no )", connection);
                    cmd.Parameters.AddWithValue("@in_no", sales_return.no);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show("" + r);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        //add button code
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
            item_code = row.Cells[0].Value.ToString();
            //data fetch and carry to another page

            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedrow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (sales_return.type == "Invoice")
            {
                //invoice 

                try
                {

                    connection.Open();
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from invoice where (in_no = @in_no ) and where item_Name like '" + textBox1.Text + "%'", connection);
                    cmd.Parameters.AddWithValue("@in_no", sales_return.no);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show("" + r);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                //tax_invoice


                try
                {

                    connection.Open();
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from tax_invoice where (in_no = @in_no ) and where item_Name like '" + textBox1.Text + "%'", connection);
                    cmd.Parameters.AddWithValue("@in_no", sales_return.no);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show("" + r);
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        private string name()
        {
            return "aloke";
        }


    }
}
