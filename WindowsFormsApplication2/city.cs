using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class city : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public city()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedRow = 0;

        private void city_Load(object sender, EventArgs e)
        {
        }

        private void gridview()
        {
            try
            {
               
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from city", connection);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["city_name"]), Convert.ToString(rdr["zip_code"]), Convert.ToString(rdr["state"]), Convert.ToString(rdr["country"]), Convert.ToString(rdr["area"]));
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }
        private void mod_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                // display datagridview selected row data into textboxes
                textBox6.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                comboBox1.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
            }
            
        }
        private void ResetForm()
        {
            textBox6.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            comboBox1.Text = null;
            textBox5.Text = null;
            textBox6.Text = "0";

        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int id = Convert.ToInt32(textBox6.Text);

                    if (id == 0)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string command = "insert into city(city_name, zip_code, state, country, area) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "') ";

                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        ResetForm();
                        // MessageBox.Show("Data Inserted");
                        grid();
                        gridview();

                    }
                    else
                    {

                        OleDbCommand command = new OleDbCommand(@"UPDATE city
                                                    SET city_name = @City_Name,
                                                        zip_code = @zip_code,
                                                        state = @state,
                                                        country = @country,
                                                        area = @area
                                                    WHERE ID = " + id + "", connection);

                        command.Parameters.AddWithValue("@City_Name", textBox2.Text);
                        command.Parameters.AddWithValue("@zip_code", textBox3.Text);
                        command.Parameters.AddWithValue("@state", textBox4.Text);
                        command.Parameters.AddWithValue("@country", comboBox1.Text);
                        command.Parameters.AddWithValue("@area", textBox5.Text);
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
                            this.tabControl1.SelectedTab = tabPage1;


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
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                //validation
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
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
                OleDbCommand cmd = new OleDbCommand("Delete from city where id =" + cd, connection);
                cmd.ExecuteNonQuery();
                //  MessageBox.Show("DATA Deleted Sucessfully");
                grid();
                gridview();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, "");
            }

        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider2.SetError(textBox3, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(textBox3, "");
            }

        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider3.SetError(textBox4, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(textBox4, "");
            }
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                e.Cancel = true;
                comboBox1.Focus();
                errorProvider4.SetError(comboBox1, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(comboBox1, "");
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider5.SetError(textBox5, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(textBox5, "");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from city where city_name like '" + textBox1.Text + "%'", connection);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["city_name"]), Convert.ToString(rdr["zip_code"]), Convert.ToString(rdr["state"]), Convert.ToString(rdr["country"]), Convert.ToString(rdr["area"]));
                }

            }
            catch (Exception u)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!"+u);
                gridview();
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
}
