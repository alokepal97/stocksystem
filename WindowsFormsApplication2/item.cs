using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class item : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public item()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedRow;

        //display on grid View
        private void gridview()
        {
            try
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select * from item";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 17;
                dataGridView1.Columns[0].HeaderText = "Id";
                dataGridView1.Columns[0].DataPropertyName = "ID";
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].HeaderText = "Item Code";
                dataGridView1.Columns[1].DataPropertyName = "item_code";

                dataGridView1.Columns[2].HeaderText = "Item Name";
                dataGridView1.Columns[2].DataPropertyName = "item_name";

                dataGridView1.Columns[3].HeaderText = "Item Group";
                dataGridView1.Columns[3].DataPropertyName = "item_group";

                dataGridView1.Columns[4].HeaderText = "Unit";
                dataGridView1.Columns[4].DataPropertyName = "unit";

                dataGridView1.Columns[5].HeaderText = "Price";
                dataGridView1.Columns[5].DataPropertyName = "price";

                dataGridView1.Columns[6].HeaderText = "Discount On Price";
                dataGridView1.Columns[6].DataPropertyName = "dis_on_price";

                dataGridView1.Columns[7].HeaderText = "Default Supplier";
                dataGridView1.Columns[7].DataPropertyName = "default_supplier";

                dataGridView1.Columns[8].HeaderText = "Purchase Rate";
                dataGridView1.Columns[8].DataPropertyName = "purchase_r";

                dataGridView1.Columns[9].HeaderText = "Discount Rate";
                dataGridView1.Columns[9].DataPropertyName = "discount_r";

                dataGridView1.Columns[10].HeaderText = "CGST";
                dataGridView1.Columns[10].DataPropertyName = "cgst";

                dataGridView1.Columns[11].HeaderText = "SGST";
                dataGridView1.Columns[11].DataPropertyName = "sgst";

                dataGridView1.Columns[12].HeaderText = "HSN Code";
                dataGridView1.Columns[12].DataPropertyName = "hsn_code";

                dataGridView1.Columns[13].HeaderText = "Min Stock";
                dataGridView1.Columns[13].DataPropertyName = "min_stock";

                dataGridView1.Columns[14].HeaderText = "Reorder Quantity";
                dataGridView1.Columns[14].DataPropertyName = "reorder_quantity";

                dataGridView1.Columns[15].HeaderText = "Item Status";
                dataGridView1.Columns[15].DataPropertyName = "item_status";

                dataGridView1.Columns[16].HeaderText = "Item Details";
                dataGridView1.Columns[16].DataPropertyName = "item_details";

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
        //clear grid view data
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
        //select cell number
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void mod_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                // display datagridview selected row data into textboxes
                textBox12.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                comboBox2.Text = row.Cells[4].Value.ToString();
                textBox3.Text = row.Cells[5].Value.ToString();
                textBox4.Text = row.Cells[6].Value.ToString();
                comboBox3.Text = row.Cells[7].Value.ToString();
                textBox5.Text = row.Cells[8].Value.ToString();
                textBox6.Text = row.Cells[9].Value.ToString();
                textBox13.Text = row.Cells[10].Value.ToString();
                textBox14.Text = row.Cells[11].Value.ToString();
                textBox8.Text = row.Cells[12].Value.ToString();
                textBox9.Text = row.Cells[13].Value.ToString();
                textBox10.Text = row.Cells[14].Value.ToString();
                comboBox4.Text = row.Cells[15].Value.ToString();
                textBox11.Text = row.Cells[16].Value.ToString();
            }
        }

        private void ResetForm()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox13.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox13.Text = textBox14.Text = null;
            textBox12.Text = "0";
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = null;

        }
        //Insert Query
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {

                    int id = Convert.ToInt32(textBox12.Text);
                    string code = textBox1.Text;
                    if (id == 0)
                    {

                        if(connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();

                        string command = "insert into item(item_code, item_name, item_group, unit, price, dis_on_price, default_supplier, purchase_r, discount_r, cgst,sgst, hsn_code, min_stock, reorder_quantity, item_status, item_details ) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + comboBox4.Text + "','" + textBox11.Text + "' ) ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        string command1 = "insert into stock(item_code, item_name, receive_qty, unit,min_stock) values('" + textBox1.Text + "','" + textBox2.Text + "',@qty,'" + comboBox2.Text + "','" + textBox9.Text + "') ";
                        OleDbCommand cmd = new OleDbCommand(command1, connection);
                        cmd.Parameters.AddWithValue("@qty", 0);
                        cmd.ExecuteNonQuery();

                        ResetForm();
                        grid();
                        gridview();
                    }
                    else
                    {
                        OleDbCommand command = new OleDbCommand(@"UPDATE item
                                                    SET item_code = @item_code,
                                                        item_name = @item_name,
                                                        item_group = @item_group,
                                                        unit = @unit,
                                                        price = @price,
                                                        dis_on_price = @dis_on_price,
                                                        default_supplier = @default_supplier,
                                                        purchase_r = @purchase_r,
                                                        discount_r = @discount_r,
                                                        cgst = @cgst,
                                                        sgst = @sgst,
                                                        hsn_code = @hsn_code,
                                                        min_stock = @min_stock,
                                                        reorder_quantity = @reorder_quantity,
                                                        item_status = @item_status,
                                                        item_details = @item_details
                                                        
                                                    WHERE item_code = @tex", connection);

                        command.Parameters.AddWithValue("@item_code", textBox1.Text);
                        command.Parameters.AddWithValue("@item_name", textBox2.Text);
                        command.Parameters.AddWithValue("@item_group", comboBox1.Text);
                        command.Parameters.AddWithValue("@unit", comboBox2.Text);
                        command.Parameters.AddWithValue("@price", textBox3.Text);
                        command.Parameters.AddWithValue("@dis_on_price", textBox4.Text);
                        command.Parameters.AddWithValue("@default_supplier", comboBox3.Text);
                        command.Parameters.AddWithValue("@purchase_r", textBox5.Text);
                        command.Parameters.AddWithValue("@discount_r", textBox6.Text);
                        command.Parameters.AddWithValue("@cgst", textBox13.Text);
                        command.Parameters.AddWithValue("@sgst", textBox14.Text);
                        command.Parameters.AddWithValue("@hsn_code", textBox8.Text);
                        command.Parameters.AddWithValue("@min_stock", textBox9.Text);
                        command.Parameters.AddWithValue("@reorder_quantity", textBox10.Text);
                        command.Parameters.AddWithValue("@item_status", comboBox4.Text);
                        command.Parameters.AddWithValue("@item_details", textBox11.Text);
                        command.Parameters.AddWithValue("@tex", code);
                        OleDbCommand command1 = new OleDbCommand(@"UPDATE stock
                                                    SET item_code = @item_code,
                                                        item_name = @item_name,
                                                        unit = @unit,
                                                        min_stock=@min_stock
                                                       WHERE item_code = @text", connection);


                        command1.Parameters.AddWithValue("@item_code", textBox1.Text);
                        command1.Parameters.AddWithValue("@item_name", textBox2.Text);
                        command1.Parameters.AddWithValue("@unit", comboBox2.Text);
                        command1.Parameters.AddWithValue("@min_stock", textBox9.Text);
                        command1.Parameters.AddWithValue("@text", code);
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
                            command1.ExecuteNonQuery();
                            grid();
                            gridview();
                            ResetForm();
                            this.tabControl1.SelectedTab = tabPage1;
                        }
                        catch (Exception t)
                        {
                            MessageBox.Show("query error" + t);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show("Data Inserted Error!!!!!!!!!!!!!!" + r);
                }
                finally
                {
                    connection.Close();
                }
            }
            // code
            else
            {
                //none button will not work
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void textbox1_validating(object sender, CancelEventArgs e)
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

       /* private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                string code = row.Cells[1].Value.ToString();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                //delete fromn stock
                OleDbCommand cm = new OleDbCommand("Delete from stock where item_code = @item_cod", connection);
                cm.Parameters.AddWithValue("@item_cod", code);
                cm.ExecuteNonQuery();
                //delete from item
                OleDbCommand cmd = new OleDbCommand("Delete from item where item_code =@item_code", connection);
                cmd.Parameters.AddWithValue("@item_code", code);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            //MessageBox.Show("DATA Deleted Sucessfully");
            grid();
            gridview();

        }*/

        private void group()
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
                string query = "select ID,name from grou";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "name");
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "name";
                comboBox1.DataSource = ds.Tables["name"];
            }
            catch (Exception p)
            {
                MessageBox.Show("combobox1" + p);
            }
            finally
            {
                connection.Close();
            }
        }

        private void unit()
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
                string query = " select ID,unit_name from unit";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "unit_name");
                comboBox2.DisplayMember = "unit_name";
                comboBox2.ValueMember = "unit_name";
                comboBox2.DataSource = ds.Tables["unit_name"];
            }
            catch (Exception p)
            {
                MessageBox.Show("combobox2" + p);
            }
            finally
            {
                connection.Close();
            }
        }

        private void supplier()
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
                string query = "select ID,s_name from supplier";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "s_name");
                comboBox3.DisplayMember = "s_name";
                comboBox3.ValueMember = "s_name";
                comboBox3.DataSource = ds.Tables["s_name"];
            }
            catch (Exception p)
            {
                MessageBox.Show("combobox3" + p);
            }
            finally
            {
                connection.Close();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            group();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            unit();
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            supplier();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sales_order so = new sales_order();
            so.ShowDialog();
        }
    }

}
