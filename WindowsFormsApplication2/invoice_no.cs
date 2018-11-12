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
    public partial class invoice_no : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public invoice_no()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
        }
        public static string in_no = "";
        int selectedRow=0;
        private void grid()
        {
            if (select_no.tbl == "invoice")
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from in_main where (type='in')", connection);
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]));
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
            else if (select_no.tbl == "tax_invoice")
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from in_main where (type='in')", connection);
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]));
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            in_no = row.Cells[0].Value.ToString();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void invoice_no_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (select_no.tbl == "invoice")
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from in_main where (in_no like '" + textBox1.Text + "%') and (type = 'in')", connection);
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]));
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
            else if (select_no.tbl == "tax_invoice")
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from in_main where (in_no like '" + textBox1.Text + "%') and (type = 'in')", connection);
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]));
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
}
