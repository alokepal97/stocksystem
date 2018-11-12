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
    public partial class item_a : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public item_a()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
        }
        int selectedRow;
        private void item_a_Load(object sender, EventArgs e)
        {
            grid();
            // TODO: This line of code loads data into the 'stock_item.item' table. You can move, or remove it, as needed.
            // this.itemTableAdapter.Fill(this.stock_item.item);

        }
        public static string item_code = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                item_code = row.Cells[0].Value.ToString();
                this.Close();
            } 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grid() {

            dataGridView1.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select item.item_code, item.item_name from(item INNER JOIN stock ON item.item_code = stock.item_code) where (stock.receive_qty > stock.min_stock) AND (stock.item_name <> ' ') and (item.item_status='Active') ORDER BY stock.id", connection);
            try
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]));
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select item.item_code, item.item_name from(item INNER JOIN stock ON item.item_code = stock.item_code) where (stock.receive_qty > stock.min_stock) and (item.item_Name like '" + textBox1.Text + "%') and (stock.item_name <> ' ') and(item.item_status = 'Active') ORDER BY stock.id", connection);
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]));
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
