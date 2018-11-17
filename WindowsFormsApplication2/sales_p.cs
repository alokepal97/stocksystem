using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class sales_p : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_p()
        {
            InitializeComponent();
            // this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
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
                OleDbCommand cmd = new OleDbCommand("select * from tb_p", connection);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["p_name"]), Convert.ToString(rdr["p_add"]), Convert.ToString(rdr["p_city"]), Convert.ToString(rdr["p_zip"]), Convert.ToString(rdr["p_state"]), Convert.ToString(rdr["p_country"]), Convert.ToString(rdr["p_ph"]), Convert.ToString(rdr["p_email"]));
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
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void modifybtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                // display datagridview selected row data into textboxes
                textBox5.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[5].Value.ToString();
                comboBox2.Text = row.Cells[6].Value.ToString();
                textBox6.Text = row.Cells[7].Value.ToString();
                textBox7.Text = row.Cells[8].Value.ToString();
            }
        }
        private void ResetForm()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = comboBox2.Text = textBox6.Text = textBox7.Text = null;
            textBox5.Text = "0";

        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox5.Text);

                if (id == 0)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    string command = "insert into tb_p(p_name,p_add,p_city,p_zip,p_state,p_country,p_ph,p_email) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "', '" + comboBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "') ";
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
                    OleDbCommand command = new OleDbCommand(@"UPDATE tb_p
                                                    SET p_name = @p_name,
                                                        p_add = @p_add,
                                                        p_city = @p_city,
                                                        p_zip = @p_zip,
                                                        p_state = @p_state,
                                                        p_country = @p_country,
                                                        p_ph = @p_ph,
                                                        p_email = @p_email
                                                        
                                                      WHERE ID = " + id + "", connection);

                    command.Parameters.AddWithValue("@p_name", textBox1.Text);
                    command.Parameters.AddWithValue("@p_add", textBox2.Text);
                    command.Parameters.AddWithValue("@p_city", comboBox1.Text);
                    command.Parameters.AddWithValue("@p_zip", textBox3.Text);
                    command.Parameters.AddWithValue("@p_state", textBox4.Text);
                    command.Parameters.AddWithValue("@p_country", comboBox2.Text);
                    command.Parameters.AddWithValue("@p_ph", textBox6.Text);
                    command.Parameters.AddWithValue("@p_email", textBox7.Text);

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
            catch (Exception exp)
            {
                MessageBox.Show("Error" + exp);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
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
                OleDbCommand cmd = new OleDbCommand("Delete from tb_p where id =" + cd, connection);
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
                MessageBox.Show("combobox3" + p);
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

                        textBox3.Text = Convert.ToString(rdr["zip_code"]);
                        textBox4.Text = Convert.ToString(rdr["state"]);

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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

            try
            {
                dataGridView1.Rows.Clear();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from tb_p where p_name like '" + textBox8.Text + "%'", connection);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["p_name"]), Convert.ToString(rdr["p_add"]), Convert.ToString(rdr["p_city"]), Convert.ToString(rdr["p_zip"]), Convert.ToString(rdr["p_state"]), Convert.ToString(rdr["p_country"]), Convert.ToString(rdr["p_ph"]), Convert.ToString(rdr["p_email"]));
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
    }
}

