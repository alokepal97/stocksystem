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
    public partial class supplier : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public supplier()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedRow = 0;
        private void gridview()
        {
            try
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from supplier", connection);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["s_code"]), Convert.ToString(rdr["b_add"]), Convert.ToString(rdr["b_city"]), Convert.ToString(rdr["b_zip"]), Convert.ToString(rdr["b_State"]), Convert.ToString(rdr["b_country"]), Convert.ToString(rdr["b_contact"]), Convert.ToString(rdr["b_ph1"]), Convert.ToString(rdr["b_ph2"]), Convert.ToString(rdr["b_fax"]), Convert.ToString(rdr["b_email"]), Convert.ToString(rdr["p_terms"]), Convert.ToString(rdr["gst_no"]));
                }

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
                dataGridView1.Rows.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                // display datagridview selected row data into textboxes
                textBox7.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                comboBox1.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
                comboBox2.Text = row.Cells[7].Value.ToString();
                textBox8.Text = row.Cells[8].Value.ToString();
                textBox9.Text = row.Cells[9].Value.ToString();
                textBox10.Text = row.Cells[10].Value.ToString();
                textBox11.Text = row.Cells[11].Value.ToString();
                textBox12.Text = row.Cells[12].Value.ToString();
                textBox13.Text = row.Cells[13].Value.ToString();
                textBox14.Text = row.Cells[14].Value.ToString();
            }
        }
        private void ResetForm()
        {
            textBox7.Text = "0";
            textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = textBox5.Text = textBox6.Text = comboBox2.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int id = Convert.ToInt32(textBox7.Text);

                    if (id == 0)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string command = "insert into supplier(s_name, s_code, b_add, b_city, b_zip, b_state, b_country, b_contact, b_ph1, b_ph2, b_fax, b_email,p_terms,gst_no) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + comboBox2.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "' ,'" + textBox12.Text + "' ,'" + textBox13.Text + "','" + textBox14.Text + "') ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        ResetForm();
                        // MessageBox.Show("Data Inserted");
                        grid();
                        gridview();

                    }
                    else
                    {

                        OleDbCommand command = new OleDbCommand(@"UPDATE supplier
                                                    SET s_name = @s_name,
                                                        s_code = @s_code,
                                                        b_add = @b_add,
                                                        b_city = @b_city,  
                                                        b_zip = @b_zip,
                                                        b_state = @b_state,
                                                        b_country = @b_country,
                                                        b_contact = @b_contact,
                                                        b_ph1 = @b_ph1,
                                                        b_ph2 = @b_ph2,
                                                        b_fax = @b_fax,
                                                        b_email = @b_email,
                                                        p_terms = @p_terms,
                                                        gst_no = @gst_no
                                                    WHERE ID = " + id + "", connection);

                        command.Parameters.AddWithValue("@s_name", textBox2.Text);
                        command.Parameters.AddWithValue("@s_code", textBox3.Text);
                        command.Parameters.AddWithValue("@b_add", textBox4.Text);
                        command.Parameters.AddWithValue("@b_city", comboBox1.Text);
                        command.Parameters.AddWithValue("@b_zip", textBox5.Text);
                        command.Parameters.AddWithValue("@b_state", textBox6.Text);
                        command.Parameters.AddWithValue("@b_country", comboBox2.Text);
                        command.Parameters.AddWithValue("@b_contact", textBox8.Text);
                        command.Parameters.AddWithValue("@b_ph1", textBox9.Text);
                        command.Parameters.AddWithValue("@b_ph2", textBox10.Text);
                        command.Parameters.AddWithValue("@b_fax", textBox11.Text);
                        command.Parameters.AddWithValue("@b_email", textBox12.Text);
                        command.Parameters.AddWithValue("@b_fax", textBox13.Text);
                        command.Parameters.AddWithValue("@b_email", textBox14.Text);
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
                //validation else part
            }
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
        //    DataGridViewRow row = dataGridView1.Rows[selectedRow];
        //    string cf = row.Cells[0].Value.ToString();
        //    int cd = Convert.ToInt32(cf);
        //    if (connection.State == ConnectionState.Open)
        //    {
        //        connection.Close();
        //    }
        //    connection.Open();
        //    OleDbCommand cmd = new OleDbCommand("Delete from supplier where ID =" + cd, connection);
        //    cmd.ExecuteNonQuery();
        //    //MessageBox.Show("DATA Deleted Sucessfully");
        //    grid();
        //    gridview();
        //    connection.Close();
        //}

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

        private void textBox14_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox14.Text))
            {
                e.Cancel = true;
                textBox14.Focus();
                errorProvider4.SetError(textBox14, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox14, "");
            }
        }



        private void salesp()
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
                string query = " select ID,city_name from city";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "city");
                comboBox1.DisplayMember = "city_name";
                comboBox1.ValueMember = "ID";
                comboBox1.DataSource = ds.Tables["city"];
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            catch (Exception p)
            {
                MessageBox.Show("comboBox1" + p);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            salesp();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from city where (ID = @id)", connection);
                cmd.Parameters.AddWithValue("@id", comboBox1.SelectedValue.ToString());
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {

                        textBox5.Text = Convert.ToString(rdr["zip_code"]);
                        textBox6.Text = Convert.ToString(rdr["state"]);

                    }
                }
                catch (Exception t)
                {
                    MessageBox.Show("Error" + t);
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

        private void country()
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
                string query = " select ID,country from country";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "country");
                comboBox2.DisplayMember = "country";
                comboBox2.ValueMember = "country";
                comboBox2.DataSource = ds.Tables["country"];
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            catch (Exception p)
            {
                MessageBox.Show("combobox2" + p);
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            country();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from supplier where s_name like '" + textBox1.Text + "%'", connection);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["s_code"]), Convert.ToString(rdr["b_add"]), Convert.ToString(rdr["b_city"]), Convert.ToString(rdr["b_zip"]), Convert.ToString(rdr["b_State"]), Convert.ToString(rdr["b_country"]), Convert.ToString(rdr["b_contact"]), Convert.ToString(rdr["b_ph1"]), Convert.ToString(rdr["b_ph2"]), Convert.ToString(rdr["b_fax"]), Convert.ToString(rdr["b_email"]), Convert.ToString(rdr["p_terms"]), Convert.ToString(rdr["gst_no"]));
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }




    }



