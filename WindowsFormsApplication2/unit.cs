using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class unit : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public unit()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedRow = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }
        private void gridview()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select * from unit";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].HeaderText = "Id";
                dataGridView1.Columns[0].DataPropertyName = "ID";
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].HeaderText = "Unit";
                dataGridView1.Columns[1].DataPropertyName = "unit_name";

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
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                // display datagridview selected row data into textboxes
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
            }
        }
        private void ResetForm()
        {
            textBox1.Text = null;
            textBox2.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int id = Convert.ToInt32(textBox2.Text);

                    if (id == 0)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string command = "insert into unit(unit_name) values('" + textBox1.Text + "') ";

                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        ResetForm();

                        grid();
                        gridview();

                    }
                    else
                    {

                        OleDbCommand command = new OleDbCommand(@"UPDATE unit
                                                    SET unit_name = @unit_name
                                                        
                                                      
                                                    WHERE ID = " + id + "", connection);

                        command.Parameters.AddWithValue("@unit_name", textBox1.Text);


                        try
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            connection.Open();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("connection error");
                        }
                        try
                        {
                            command.ExecuteNonQuery();

                            //  MessageBox.Show("DATA UPDATED");
                            ResetForm();
                            grid();
                            gridview();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("query error");
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
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
            else
            {
                //validation
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    string cf = row.Cells[0].Value.ToString();
                    int cd = Convert.ToInt32(cf);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    OleDbCommand cmd = new OleDbCommand("Delete from unit where ID =" + cd, connection);
                    cmd.ExecuteNonQuery();
                    //  MessageBox.Show("DATA Deleted Sucessfully");
                    ResetForm();
                    grid();
                    gridview();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");
            }
        }
    }
}
