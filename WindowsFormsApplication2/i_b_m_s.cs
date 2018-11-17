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
    public partial class i_b_m_s : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public i_b_m_s()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
           
        }
        int selectedRow = 0;
        public static string item_code = "";
        public static string current_stock = "";
        private void gridview()
        {
            try
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select item_code,item_name,receive_qty,min_stock from stock where ( min_stock > receive_qty)";
                
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 4;
               


                dataGridView1.Columns[0].HeaderText = "Item Code";
                dataGridView1.Columns[0].DataPropertyName = "item_code";
               

                dataGridView1.Columns[1].HeaderText = "Item Name";
                dataGridView1.Columns[1].DataPropertyName = "item_name";

                dataGridView1.Columns[2].HeaderText = "Minimum Stock Level";
                dataGridView1.Columns[2].DataPropertyName = "min_stock";

                dataGridView1.Columns[3].HeaderText = "Current Stock Level";
                dataGridView1.Columns[3].DataPropertyName = "receive_qty";
                
                dataGridView1.DataSource = dt;
               

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
            if (dataGridView1.Rows.Count > 0)
            {
                if (selectedRow != -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];

                    item_code = row.Cells[0].Value.ToString();
                    current_stock = row.Cells[3].Value.ToString();
                    connection.Close();
                    this.Show();
                    R_p_b_m_s_l rpq = new R_p_b_m_s_l();
                    rpq.ShowDialog();
                }

            }

        }

        private void i_b_m_s_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.msl_item msl = new Excel.msl_item();
            msl.ShowDialog();
        }


    }
}
