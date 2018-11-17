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
    public partial class stock_in_hand : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public stock_in_hand()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }


        private void grid()
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

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
            }
        }




       private void gridview()
        {
            try
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select item_code,item_name,receive_qty,unit from stock where (receive_qty > min_stock)", connection);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]), Convert.ToString(rdr["receive_qty"]), Convert.ToString(rdr["unit"]));

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
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            grid();
            gridview();
         
        }

        private void stock_in_hand_Load(object sender, EventArgs e)
        {
                       
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select item_code,item_name,receive_qty,unit from stock where (receive_qty > min_stock) and item_Name like '" + textBox1.Text + "%'", connection);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]), Convert.ToString(rdr["receive_qty"]), Convert.ToString(rdr["unit"]));

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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.stock_in_hand_ est = new Excel.stock_in_hand_();
            est.ShowDialog();
        }


      





   }
}
