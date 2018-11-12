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
    public partial class taxsetup : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public taxsetup()
        {
           InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            BindGrid();
        }
        int selectedRow;
        private void BindGrid()
        {
            try
            {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select * from tax";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
              
                dataGridView1.ColumnCount = 4;
                 dataGridView1.Columns[0].HeaderText = "Id";
                 dataGridView1.Columns[0].DataPropertyName = "ID";
                 dataGridView1.Columns[0].Visible = false;

                 dataGridView1.Columns[1].HeaderText = "Tax Name";
                 dataGridView1.Columns[1].DataPropertyName = "tax_name";

                 dataGridView1.Columns[2].HeaderText = "Tax Rate";
                 dataGridView1.Columns[2].DataPropertyName = "tax_rate";

                 dataGridView1.Columns[3].HeaderText = "Date";
                 dataGridView1.Columns[3].DataPropertyName = "applicable";
                 dataGridView1.DataSource = dt;

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
            }
            finally
            {
                connection.Close();
            } 
            }


        private void grid()
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
            }
            finally
            {
                connection.Close();
            }
        }


        private void ResetForm()
        {
          
            textBox1.Text = null;
            textBox2.Text = null;
            dateTimePicker1.ResetText();
            textBox3.Text = "0";

        }
 
        private void newbtn_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void modifybtn_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
            DataGridViewRow row = dataGridView1.Rows[selectedRow];

            // display datagridview selected row data into textboxes
            textBox3.Text = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            string cf = row.Cells[0].Value.ToString();
            int cd = Convert.ToInt32(cf);
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("Delete from tax where id =" + cd, connection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("DATA Deleted Sucessfully");
            grid();
            BindGrid();
            connection.Close();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
             try
            {
                int id = Convert.ToInt32(textBox3.Text);

                if (id == 0 )
                {
            connection.Open();
            OleDbCommand cmr = new OleDbCommand("insert into tax(tax_name,tax_rate,applicable)values('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Text + "')", connection);
                     cmr.ExecuteNonQuery();
               MessageBox.Show("DATA inserted Sucessfully");
                    grid();
                    BindGrid();
               
        }
                else{
                     OleDbCommand command = new OleDbCommand(@"UPDATE tax
                                                    SET tax_name = @name,
                                                        tax_rate = @rate,
                                                        applicable = @app
                                                     WHERE ID = " + id + "", connection);

                    command.Parameters.AddWithValue("@name", textBox1.Text);
                    command.Parameters.AddWithValue("@rate", textBox2.Text);
                    command.Parameters.AddWithValue("@app", dateTimePicker1.Text);
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("connection error");
                    }
                    try
                    {
                        command.ExecuteNonQuery();

                        MessageBox.Show("DATA UPDATED");
                        ResetForm();
                         grid();
                         BindGrid();
                        this.tabControl1.SelectedTab = tabPage1;


                    }
                    catch (Exception )
                    {
                        MessageBox.Show("query error");
                    }
                    finally
                    {
                        connection.Close();
                    }
              }
            }
            catch (Exception)
            {
                MessageBox.Show("Data Inserted Error!!!!!!!!!!!!!!");

            }
            finally
            {
                connection.Close();
            }  

                }
             }
      
 

              
    }

