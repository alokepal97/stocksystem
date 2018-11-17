using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class company : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public company()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
            salesp();
        }
        int selectedRow = 0;
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
                string query = "select * from company";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.ColumnCount = 18;
                dataGridView1.Columns[0].HeaderText = "Id";
                dataGridView1.Columns[0].DataPropertyName = "ID";
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].HeaderText = "Company Name";
                dataGridView1.Columns[1].DataPropertyName = "c_name";

                dataGridView1.Columns[2].HeaderText = "Short Name";
                dataGridView1.Columns[2].DataPropertyName = "s_name";

                dataGridView1.Columns[3].HeaderText = "Address";
                dataGridView1.Columns[3].DataPropertyName = "c_add";

                dataGridView1.Columns[4].HeaderText = "City";
                dataGridView1.Columns[4].DataPropertyName = "c_city";

                dataGridView1.Columns[5].HeaderText = "Zip Code";
                dataGridView1.Columns[5].DataPropertyName = "c_zip";

                dataGridView1.Columns[6].HeaderText = "State";
                dataGridView1.Columns[6].DataPropertyName = "c_state";

                dataGridView1.Columns[7].HeaderText = "Country";
                dataGridView1.Columns[7].DataPropertyName = "c_country";

                dataGridView1.Columns[8].HeaderText = "Phone No 1";
                dataGridView1.Columns[8].DataPropertyName = "c_ph1";

                dataGridView1.Columns[9].HeaderText = "Phone No 2";
                dataGridView1.Columns[9].DataPropertyName = "c_ph2";

                dataGridView1.Columns[10].HeaderText = "Fax";
                dataGridView1.Columns[10].DataPropertyName = "c_fax";

                dataGridView1.Columns[11].HeaderText = "Email";
                dataGridView1.Columns[11].DataPropertyName = "c_email";

                dataGridView1.Columns[12].HeaderText = "Website";
                dataGridView1.Columns[12].DataPropertyName = "c_website";

                dataGridView1.Columns[13].HeaderText = "GST NO";
                dataGridView1.Columns[13].DataPropertyName = "c_gst";

                dataGridView1.Columns[14].HeaderText = "PAN No";
                dataGridView1.Columns[14].DataPropertyName = "c_pan";

                dataGridView1.Columns[15].HeaderText = "CIN";
                dataGridView1.Columns[15].DataPropertyName = "c_cin";

                dataGridView1.Columns[16].HeaderText = "Bank Imformation";
                dataGridView1.Columns[16].DataPropertyName = "c_bank";

                dataGridView1.Columns[17].HeaderText = "Default Company";
                dataGridView1.Columns[17].DataPropertyName = "setDefault";
                dataGridView1.Columns[17].Visible = false;


                dataGridView1.DataSource = dt;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }

        private void modibtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                // DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                // display datagridview selected row data into textboxes
                textBox6.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                comboBox1.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[5].Value.ToString();
                textBox5.Text = row.Cells[6].Value.ToString();
                comboBox2.Text = row.Cells[7].Value.ToString();
                textBox7.Text = row.Cells[8].Value.ToString();
                textBox8.Text = row.Cells[9].Value.ToString();
                textBox9.Text = row.Cells[10].Value.ToString();
                textBox10.Text = row.Cells[11].Value.ToString();
                textBox11.Text = row.Cells[12].Value.ToString();
                textBox12.Text = row.Cells[13].Value.ToString();
                textBox13.Text = row.Cells[14].Value.ToString();
                textBox14.Text = row.Cells[15].Value.ToString();
                textBox15.Text = row.Cells[16].Value.ToString();
                int val = Convert.ToInt32(row.Cells[17].Value.ToString());
                if (val == 1)
                {
                    detailsChk.Checked = true;
                }
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int id = Convert.ToInt32(textBox6.Text);
                    int detailsCheck = 0;
                    if (detailsChk.Checked)
                    {
                        detailsCheck = 1;
                    }

                    if (id == 0)
                    {
                        checkDefaultCompany(0);
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string command = "insert into company(c_name, s_name, c_add, c_city, c_zip, c_state, c_country, c_ph1, c_ph2, c_fax, c_email, c_website, c_gst,c_pan,c_cin,c_bank,setDefault) values( '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "', '" + textBox5.Text + "', '" + comboBox2.Text + "', '" + textBox7.Text + "','" + textBox8.Text + "', '" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "', '" + detailsCheck + "')";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        ResetForm();
                        grid();
                        gridview();
                    }
                    else
                    {
                        checkDefaultCompany(0);
                        OleDbCommand command = new OleDbCommand(@"UPDATE company
                                                    SET c_name = @c_name,
                                                        s_name = @s_name,
                                                        c_add = @c_add,
                                                        c_city = @c_city,  
                                                        c_zip = @c_zip,
                                                        c_state = @c_state,
                                                        c_country = @c_country,
                                                        c_ph1 = @c_ph1,
                                                        c_ph2 = @c_ph2,
                                                        c_fax = @c_fax,
                                                        c_email = @c_email,
                                                        c_website=@c_website,
                                                        c_gst = @c_gst,
                                                        c_pan=@c_pan,
                                                        c_cin=@c_cin,
                                                        c_bank = @c_bank,
                                                       setDefault =@setDefault                                                                                  
                                                    WHERE ID = " + id + "", connection);

                        command.Parameters.AddWithValue("@c_name", textBox1.Text);
                        command.Parameters.AddWithValue("@s_name", textBox2.Text);
                        command.Parameters.AddWithValue("@c_add", textBox3.Text);
                        command.Parameters.AddWithValue("@c_city", comboBox1.Text);
                        command.Parameters.AddWithValue("@c_zip", textBox4.Text);
                        command.Parameters.AddWithValue("@c_state", textBox5.Text);
                        command.Parameters.AddWithValue("@c_country", comboBox2.Text);
                        command.Parameters.AddWithValue("@c_ph1", textBox7.Text);
                        command.Parameters.AddWithValue("@c_ph2", textBox8.Text);
                        command.Parameters.AddWithValue("@c_fax", textBox9.Text);
                        command.Parameters.AddWithValue("@c_email", textBox10.Text);
                        command.Parameters.AddWithValue("@c_website", textBox11.Text);
                        command.Parameters.AddWithValue("@c_gst", textBox12.Text);
                        command.Parameters.AddWithValue("@c_pan", textBox13.Text);
                        command.Parameters.AddWithValue("@c_cin", textBox14.Text);
                        command.Parameters.AddWithValue("@c_bank", textBox15.Text);
                        command.Parameters.AddWithValue("@setDefault", detailsCheck);
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

                            //MessageBox.Show("DATA UPDATED");
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
                            connection.Close();
                        }

                    }
                }
                catch (Exception t)
                {
                    MessageBox.Show("Data Inserted Error!!!!!!!!!!!!!!" + t);

                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {

            }
        }
        private void ResetForm()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = comboBox1.Text = textBox4.Text = textBox5.Text = comboBox2.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox13.Text = textBox14.Text = textBox15.Text = null;
            textBox6.Text = "0";
            detailsChk.Checked = false;

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                string cf = row.Cells[0].Value.ToString();
                int cd = Convert.ToInt32(cf);
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("Delete from company where ID =" + cd, connection);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("DATA Deleted Sucessfully");
                grid();
                gridview();
                ResetForm();
                connection.Close();
            }
           
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
            ResetForm();
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

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider2.SetError(textBox4, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(textBox4, "");
            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider3.SetError(textBox7, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(textBox7, "");
            }
        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider4.SetError(textBox8, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox8, "");
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //salesp();
        }

        private void salesp()
        {
            try
            {
                //if (connection.State == ConnectionState.Open)
                //{
                //    connection.Close();
                //}
                //connection.Open();
                //OleDbCommand command = new OleDbCommand();
                //command.Connection = connection;
                //string query = "select ID,city_name from city";
                //command.CommandText = query;
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "city");
                //comboBox1.DisplayMember = "city_name";
                //comboBox1.ValueMember = "ID";
                //comboBox1.DataSource = ds.Tables["city"];
                //comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                //comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
                //comboBox1.AutoCompleteCustomSource = combData;SELECT ID, city_name FROM city order by city_name
                //connection.Close();select ID, name from grou order by name
                using (OleDbDataAdapter sda = new OleDbDataAdapter("SELECT ID, city_name FROM city order by city_name", connection))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //Insert the Default Item to DataTable.
                        DataRow row = dt.NewRow();
                        row[0] = 0;
                        row[1] = "";
                        dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "city_name";
                    comboBox1.ValueMember = "ID";

                    //Set AutoCompleteMode.
                    comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                

            }
            catch (Exception p)
            {
                MessageBox.Show("combobox3" + p);
            }
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

                        textBox4.Text = Convert.ToString(rdr["zip_code"]);
                        textBox5.Text = Convert.ToString(rdr["state"]);

                    }
                }
                catch (Exception t)
                {
                    MessageBox.Show("Error" + t);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void checkDefaultCompany(int defaultVal)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                if (dataGridView1.Rows.Count > 0)
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE company
                                                    SET setDefault =@default                                                                                                        
                                                  ", connection);

                    command.Parameters.AddWithValue("@default", defaultVal);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Opps! Cannot set default Company Please Remove Previous Delault Company");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
