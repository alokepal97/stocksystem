using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.IO;
namespace WindowsFormsApplication2
{
    public partial class Customer : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Customer()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
            
        }

        int selectedRow=0;
        public static String cusname= "";
        public static int sales_ref = 0;

        //display on grid View
        private void gridview()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from customer", connection);
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
                        dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["c_code"]), Convert.ToString(rdr["b_add"]), Convert.ToString(rdr["b_city"]), Convert.ToString(rdr["b_zip"]), Convert.ToString(rdr["b_state"]), Convert.ToString(rdr["b_country"]), Convert.ToString(rdr["b_contact"]), Convert.ToString(rdr["b_ph1"]), Convert.ToString(rdr["b_ph2"]), Convert.ToString(rdr["b_fax"]), Convert.ToString(rdr["b_email"]), Convert.ToString(rdr["d_add"]), Convert.ToString(rdr["d_city"]), Convert.ToString(rdr["d_zip"]), Convert.ToString(rdr["d_state"]), Convert.ToString(rdr["d_country"]), Convert.ToString(rdr["d_contact"]), Convert.ToString(rdr["d_ph1"]), Convert.ToString(rdr["d_ph2"]), Convert.ToString(rdr["d_fax"]), Convert.ToString(rdr["d_email"]), Convert.ToString(rdr["tax_no"]), Convert.ToString(rdr["cst_no"]), Convert.ToString(rdr["o_details"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["adh_no"]), Convert.ToString(rdr["ser_tax_no"]));
                    }
              }
            catch (Exception u)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!"+u);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //clear grid view data
        private void grid()
        {
            try
            {
                dataGridView1.Rows.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                c_name.Text = row.Cells[1].Value.ToString();
                c_code.Text = row.Cells[2].Value.ToString();
                address.Text = row.Cells[3].Value.ToString();
                city_name.Text = row.Cells[4].Value.ToString();
                zip_code.Text = row.Cells[5].Value.ToString();
                state.Text = row.Cells[6].Value.ToString();
                comboBox1.Text = row.Cells[7].Value.ToString();
                contacttxt.Text = row.Cells[8].Value.ToString();
                phonetxt.Text = row.Cells[9].Value.ToString();
                phone2txt.Text = row.Cells[10].Value.ToString();
                faxtxt.Text = row.Cells[11].Value.ToString();
                emailtxt.Text = row.Cells[12].Value.ToString();
                textBox2.Text = row.Cells[23].Value.ToString();
                textBox3.Text = row.Cells[24].Value.ToString();
                comboBox5.Text = row.Cells[25].Value.ToString();
                textBox23.Text = row.Cells[26].Value.ToString();
                textBox5.Text = row.Cells[27].Value.ToString();
                textBox6.Text = row.Cells[28].Value.ToString();
                if (row.Cells[14].Value.ToString() != null && row.Cells[14].Value.ToString() != "")
                {

                    checkBox1.Checked = true;

                }
            }
        }

        private void ResetForm()
        {
            c_name.Text = c_code.Text = address.Text = city_name.Text = zip_code.Text = state.Text = comboBox1.Text = contacttxt.Text = phonetxt.Text = phone2txt.Text = faxtxt.Text = emailtxt.Text = comboBox5.Text = textBox23.Text = null;
            textBox1.Text = "0";
            checkBox1.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int id = Convert.ToInt32(textBox1.Text);
                    string text = textBox1.Text;

                    if (id == 0)
                    {
                        if (checkBox1.Checked == true)
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            connection.Open();

                            string command = "insert into customer(c_name, c_code, b_add, b_city, b_zip, b_state, b_country, b_contact, b_ph1, b_ph2, b_fax, b_email, d_add, d_city, d_zip,d_state,d_country,d_contact,d_ph1,d_ph2,d_fax,d_email,tax_no,cst_no,o_details,notes,adh_no,ser_tax_no ) values( '" + c_name.Text + "','" + c_code.Text + "','" + address.Text + "','" + city_name.Text + "','" + zip_code.Text + "', '" + state.Text + "', '" + comboBox1.Text + "', '" + contacttxt.Text + "','" + phonetxt.Text + "', '" + phone2txt.Text + "','" + faxtxt.Text + "','" + emailtxt.Text + "','"+ address.Text + "','" + city_name.Text + "','" + zip_code.Text + "','" + state.Text + "','" + comboBox1.Text + "','" + contacttxt.Text + "','" + phonetxt.Text + "','" + phone2txt.Text + "' ,'" + faxtxt.Text + "' ,'" + emailtxt.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox5.Text + "','" + textBox23.Text + "','" + textBox5.Text + "','" + textBox6.Text + "' )";
                            OleDbCommand cmdd = new OleDbCommand(command, connection);
                            cmdd.ExecuteNonQuery();
                            ResetForm();
                            grid();
                            gridview();
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                        else
                        {
                            if (connection.State == System.Data.ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            connection.Open();

                            string comman = "insert into customer(c_name, c_code, b_add, b_city, b_zip, b_state, b_country, b_contact, b_ph1, b_ph2, b_fax, b_email,tax_no,cst_no,o_details,notes,adh_no,ser_tax_no) values( '" + c_name.Text + "','" + c_code.Text + "','" + address.Text + "','" + city_name.Text + "','" + zip_code.Text + "', '" + state.Text + "', '" + comboBox1.Text + "', '" + contacttxt.Text + "','" + phonetxt.Text + "', '" + phone2txt.Text + "','" + faxtxt.Text + "','" + emailtxt.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox5.Text + "','" + textBox23.Text + "','" + textBox5.Text + "','" + textBox6.Text + "' )";
                            OleDbCommand cmd = new OleDbCommand(comman, connection);
                            cmd.ExecuteNonQuery();
                            ResetForm();
                            grid();
                            gridview();
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }

                    }
                    else
                    {


                        if (checkBox1.Checked == true)
                        {
                            
                            //check code for update

                            OleDbCommand command = new OleDbCommand(@"UPDATE customer
                                                    SET c_name = @c_name,
                                                        c_code = @c_code,
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
                                                        d_add = @d_add,
                                                        d_city = @d_city,
                                                        d_zip = @d_zip,
                                                        d_state = @d_state,
                                                        d_country = @d_country,
                                                        d_contact = @d_contact,
                                                        d_ph1 = @d_ph1,
                                                        d_ph2 = @d_ph2,
                                                        d_fax = @d_fax,
                                                        d_email = @d_email,

                                                        tax_no = @tax_no,
                                                        cst_no = @cst_no,
                                                        o_details = @o_details,
                                                        notes = @notes,
                                                        adh_no = @adh_no,
                                                        ser_tax_no = @ser_tax_no

                                                                                                        
                                                    WHERE ID = " + id + "", connection);

                            command.Parameters.AddWithValue("@c_name", c_name.Text);
                            command.Parameters.AddWithValue("@c_code", c_code.Text);
                            command.Parameters.AddWithValue("@b_add", address.Text);
                            command.Parameters.AddWithValue("@b_city", city_name.Text);
                            command.Parameters.AddWithValue("@b_zip", zip_code.Text);
                            command.Parameters.AddWithValue("@b_state", state.Text);
                            command.Parameters.AddWithValue("@b_country", comboBox1.Text);
                            command.Parameters.AddWithValue("@b_contact", contacttxt.Text);
                            command.Parameters.AddWithValue("@b_ph1", phonetxt.Text);
                            command.Parameters.AddWithValue("@b_ph2", phone2txt.Text);
                            command.Parameters.AddWithValue("@b_fax", faxtxt.Text);
                            command.Parameters.AddWithValue("@b_email", emailtxt.Text);
                            command.Parameters.AddWithValue("@d_add", address.Text);
                            command.Parameters.AddWithValue("@d_city", city_name.Text);
                            command.Parameters.AddWithValue("@d_zip", zip_code.Text);
                            command.Parameters.AddWithValue("@d_state", state.Text);
                            command.Parameters.AddWithValue("@d_country", comboBox1.Text);
                            command.Parameters.AddWithValue("@d_contact", contacttxt.Text);
                            command.Parameters.AddWithValue("@d_ph1", phonetxt.Text);
                            command.Parameters.AddWithValue("@d_ph2", phone2txt.Text);
                            command.Parameters.AddWithValue("@d_fax", faxtxt.Text);
                            command.Parameters.AddWithValue("@d_email", emailtxt.Text);

                            command.Parameters.AddWithValue("@tax_no", textBox2.Text);
                            command.Parameters.AddWithValue("@cst_no", textBox3.Text);

                            command.Parameters.AddWithValue("@o_details", comboBox5.Text);
                            command.Parameters.AddWithValue("@notes", textBox23.Text);

                            command.Parameters.AddWithValue("@adh_no", textBox5.Text);
                            command.Parameters.AddWithValue("@ser_tax_no", textBox6.Text);
                          
                            try
                            {
                                if (connection.State == System.Data.ConnectionState.Open)
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
                                if (connection.State == ConnectionState.Open)
                                {
                                    connection.Close();
                                }
                                MessageBox.Show("DATA UPDATED");
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
                        //uncheck code
                        else
                        {

                            OleDbCommand command = new OleDbCommand(@"UPDATE customer
                                                    SET c_name = @c_name,
                                                        c_code = @c_code,
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
                                                        d_add = @d_add,
                                                        d_city = @d_city,
                                                        d_zip = @d_zip,
                                                        d_state = @d_state,
                                                        d_country = @d_country,
                                                        d_contact = @d_contact,
                                                        d_ph1 = @d_ph1,
                                                        d_ph2 = @d_ph2,
                                                        d_fax = @d_fax,
                                                        d_email = @d_email,

                                                        tax_no = @tax_no,
                                                        cst_no = @cst_no,
                                                        o_details = @o_details,
                                                        notes = @notes,
                                                        adh_no = @adh_no,
                                                        ser_tax_no = @ser_tax_no
                                                                                                        
                                                    WHERE ID = " + id + "", connection);

                            command.Parameters.AddWithValue("@c_name", c_name.Text);
                            command.Parameters.AddWithValue("@c_code", c_code.Text);
                            command.Parameters.AddWithValue("@b_add", address.Text);
                            command.Parameters.AddWithValue("@b_city", city_name.Text);
                            command.Parameters.AddWithValue("@b_zip", zip_code.Text);
                            command.Parameters.AddWithValue("@b_state", state.Text);
                            command.Parameters.AddWithValue("@b_country", comboBox1.Text);
                            command.Parameters.AddWithValue("@b_contact", contacttxt.Text);
                            command.Parameters.AddWithValue("@b_ph1", phonetxt.Text);
                            command.Parameters.AddWithValue("@b_ph2", phone2txt.Text);
                            command.Parameters.AddWithValue("@b_fax", faxtxt.Text);
                            command.Parameters.AddWithValue("@b_email", emailtxt.Text);

                            command.Parameters.AddWithValue("@d_add", "");
                            command.Parameters.AddWithValue("@d_city", "");
                            command.Parameters.AddWithValue("@d_zip", "");
                            command.Parameters.AddWithValue("@d_state", "");
                            command.Parameters.AddWithValue("@d_country", "");
                            command.Parameters.AddWithValue("@d_contact", "");
                            command.Parameters.AddWithValue("@d_ph1", "");
                            command.Parameters.AddWithValue("@d_ph2", "");
                            command.Parameters.AddWithValue("@d_fax", "");
                            command.Parameters.AddWithValue("@d_email", "");

                            command.Parameters.AddWithValue("@tax_no", textBox2.Text);
                            command.Parameters.AddWithValue("@cst_no", textBox3.Text);

                            command.Parameters.AddWithValue("@o_details", comboBox5.Text);
                            command.Parameters.AddWithValue("@notes", textBox23.Text);

                            command.Parameters.AddWithValue("@adh_no", textBox5.Text);
                            command.Parameters.AddWithValue("@ser_tax_no", textBox6.Text);

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
                                if (connection.State == ConnectionState.Open)
                                {
                                    connection.Close();
                                }
                                MessageBox.Show("DATA UPDATED");
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
            else { 
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        
        // valication code
        private void c_name_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(c_name.Text))
            {
                e.Cancel = true;
                c_name.Focus();
                errorProvider1.SetError(c_name, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(c_name, "");
            } 
        }

        private void c_code_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(c_code.Text))
            {
                e.Cancel = true;
                c_code.Focus();
                errorProvider2.SetError(c_code, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(c_code, "");
            } 
        }

        private void address_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(address.Text))
            {
                e.Cancel = true;
                address.Focus();
                errorProvider3.SetError(address, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(address, "");
            } 
        }

        private void city_name_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(city_name.Text))
            {
                e.Cancel = true;
                city_name.Focus();
                errorProvider4.SetError(city_name, "City Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(city_name, "");
            }
        }

        private void zip_code_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(zip_code.Text))
            {
                e.Cancel = true;
                zip_code.Focus();
                errorProvider5.SetError(zip_code, "Zip Code should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(zip_code, "");
            }
        }
        private void phonetxt_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phonetxt.Text))
            {
                e.Cancel = true;
                phonetxt.Focus();
                errorProvider6.SetError(phonetxt, "Phone No should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider6.SetError(phonetxt, "");
            }
        }
        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider7.SetError(textBox5, "Adhar No should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider7.SetError(textBox5, "");
            }
        }

        //validation Completed


        private void salesp()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
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
                city_name.DisplayMember = "city_name";
                city_name.ValueMember = "ID";
                city_name.DataSource = ds.Tables["city"];
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("combobox3" + p);
            }
        }

        private void city_name_Click(object sender, EventArgs e)
        {
            salesp();
        }

        private void city_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (city_name.SelectedValue != null)
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from city where (ID = @id)", connection);
                cmd.Parameters.AddWithValue("@id", city_name.SelectedValue.ToString());
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

                        zip_code.Text = Convert.ToString(rdr["zip_code"]);
                        state.Text = Convert.ToString(rdr["state"]);

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

        private void salesp1()
        {
            //try
            //{
            //    connection.Open();
            //    OleDbCommand command = new OleDbCommand();
            //    command.Connection = connection;
            //    string query = " select ID,city_name from city";
            //    command.CommandText = query;
            //    OleDbDataAdapter da = new OleDbDataAdapter(command);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds, "city");
            //    comboBox2.DisplayMember = "city_name";
            //    comboBox2.ValueMember = "ID";
            //    comboBox2.DataSource = ds.Tables["city"];
            //    connection.Close();

            //}
            //catch (Exception p)
            //{
            //    MessageBox.Show("combobox3" + p);
            //}
        }




        private void comboBox2_Click(object sender, EventArgs e)
        {
            salesp1();
        }

        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBox2.SelectedValue != null)
        //    {


        //        OleDbDataReader rdr = null;
        //        OleDbCommand cmd = new OleDbCommand("select * from city where (ID = @id)", connection);
        //        cmd.Parameters.AddWithValue("@id", comboBox2.SelectedValue.ToString());
        //        try
        //        {
        //            connection.Close();
        //            connection.Open();
        //            rdr = cmd.ExecuteReader();
        //            if (rdr.Read())
        //            {

        //                textBox13.Text = Convert.ToString(rdr["zip_code"]);
        //                textBox14.Text = Convert.ToString(rdr["state"]);

        //            }
        //        }
        //        catch (Exception t)
        //        {
        //            MessageBox.Show("Error" + t);
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        private void country()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
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
                comboBox1.DisplayMember = "country";
                comboBox1.ValueMember = "country";
                comboBox1.DataSource = ds.Tables["country"];
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

        private void comboBox1_Click(object sender, EventArgs e)
        {
            country();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from customer where c_name like '" + textBox7.Text + "%'", connection);
            try
            {
              
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["c_code"]), Convert.ToString(rdr["b_add"]), Convert.ToString(rdr["b_city"]), Convert.ToString(rdr["b_zip"]), Convert.ToString(rdr["b_state"]), Convert.ToString(rdr["b_country"]), Convert.ToString(rdr["b_contact"]), Convert.ToString(rdr["b_ph1"]), Convert.ToString(rdr["b_ph2"]), Convert.ToString(rdr["b_fax"]), Convert.ToString(rdr["b_email"]), Convert.ToString(rdr["d_add"]), Convert.ToString(rdr["d_city"]), Convert.ToString(rdr["d_zip"]), Convert.ToString(rdr["d_state"]), Convert.ToString(rdr["d_country"]), Convert.ToString(rdr["d_contact"]), Convert.ToString(rdr["d_ph1"]), Convert.ToString(rdr["d_ph2"]), Convert.ToString(rdr["d_fax"]), Convert.ToString(rdr["d_email"]), Convert.ToString(rdr["tax_no"]), Convert.ToString(rdr["cst_no"]), Convert.ToString(rdr["vendor_code"]), Convert.ToString(rdr["o_details"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["adh_no"]), Convert.ToString(rdr["ser_tax_no"]));
                }


            }
            catch (Exception u)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!" + u);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sales_order bd = new sales_order();
            bd.Show();
        }

        
    }

  }



        
    

