using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class items : Form
    {

        private OleDbConnection connection = new OleDbConnection();
        public items()
        {
            InitializeComponent();
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
           // group();
            //unit();
            //supplier();
        }

        private void items_Load(object sender, EventArgs e)
        {
            group();
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
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select * from item";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 18;
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

                dataGridView1.Columns[12].HeaderText = "IGST";
                dataGridView1.Columns[12].DataPropertyName = "igst";

                dataGridView1.Columns[13].HeaderText = "HSN Code";
                dataGridView1.Columns[13].DataPropertyName = "hsn_code";

                dataGridView1.Columns[14].HeaderText = "Min Stock";
                dataGridView1.Columns[14].DataPropertyName = "min_stock";

                dataGridView1.Columns[15].HeaderText = "Reorder Quantity";
                dataGridView1.Columns[15].DataPropertyName = "reorder_quantity";

                dataGridView1.Columns[16].HeaderText = "Item Status";
                dataGridView1.Columns[16].DataPropertyName = "item_status";

                dataGridView1.Columns[17].HeaderText = "Item Details";
                dataGridView1.Columns[17].DataPropertyName = "item_details";

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
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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

            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
            DataGridViewRow row = dataGridView1.Rows[selectedRow];

            // display datagridview selected row data into textboxes
            textBox12.Text = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            comboGroup.Text = row.Cells[3].Value.ToString();
            comboBox2.Text = row.Cells[4].Value.ToString();
            textBox3.Text = row.Cells[5].Value.ToString();
            textBox4.Text = row.Cells[6].Value.ToString();
            comboBox3.Text = row.Cells[7].Value.ToString();
            textBox5.Text = row.Cells[8].Value.ToString();
            textBox6.Text = row.Cells[9].Value.ToString();
            textBox13.Text = row.Cells[10].Value.ToString();
            textBox14.Text = row.Cells[11].Value.ToString();
            textBox15.Text = row.Cells[12].Value.ToString();
            textBox8.Text = row.Cells[13].Value.ToString();
            textBox9.Text = row.Cells[14].Value.ToString();
            textBox10.Text = row.Cells[15].Value.ToString();
            comboBox4.Text = row.Cells[16].Value.ToString();
            textBox11.Text = row.Cells[17].Value.ToString();
        }


        private void ResetForm()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox13.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox13.Text = textBox14.Text = textBox15.Text = null;
            textBox12.Text = "0";
            comboGroup.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = null;

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
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();

                        string command = "insert into item(item_code, item_name, item_group, unit, price, dis_on_price, default_supplier, purchase_r, discount_r, cgst,sgst,igst, hsn_code, min_stock, reorder_quantity, item_status, item_details ) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboGroup.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + comboBox4.Text + "','" + textBox11.Text + "' ) ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.ExecuteNonQuery();
                        string command1 = "insert into stock(item_code, item_name, receive_qty, unit,min_stock) values('" + textBox1.Text + "','" + textBox2.Text + "',@qty,'" + comboBox2.Text + "','" + textBox9.Text + "') ";
                        OleDbCommand cmd = new OleDbCommand(command1, connection);
                        cmd.Parameters.AddWithValue("@qty", 0);
                        cmd.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
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
                                                        igst = @igst,
                                                        hsn_code = @hsn_code,
                                                        min_stock = @min_stock,
                                                        reorder_quantity = @reorder_quantity,
                                                        item_status = @item_status,
                                                        item_details = @item_details
                                                        
                                                    WHERE item_code = @tex", connection);

                        command.Parameters.AddWithValue("@item_code", textBox1.Text);
                        command.Parameters.AddWithValue("@item_name", textBox2.Text);
                        command.Parameters.AddWithValue("@item_group", comboGroup.Text);
                        command.Parameters.AddWithValue("@unit", comboBox2.Text);
                        command.Parameters.AddWithValue("@price", textBox3.Text);
                        command.Parameters.AddWithValue("@dis_on_price", textBox4.Text);
                        command.Parameters.AddWithValue("@default_supplier", comboBox3.Text);
                        command.Parameters.AddWithValue("@purchase_r", textBox5.Text);
                        command.Parameters.AddWithValue("@discount_r", textBox6.Text);
                        command.Parameters.AddWithValue("@cgst", textBox13.Text);
                        command.Parameters.AddWithValue("@sgst", textBox14.Text);
                        command.Parameters.AddWithValue("@igst", textBox15.Text);
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

                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }

                        }
                        catch (Exception t)
                        {
                            MessageBox.Show("query error" + t);
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
                catch (Exception r)
                {
                    MessageBox.Show("Data Inserted Error!!!!!!!!!!!!!!" + r);

                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
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


        private void button4_Click(object sender, EventArgs e)
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


                //MessageBox.Show("DATA Deleted Sucessfully");
                grid();
                gridview();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void group()
        {
            try
            {

                ////connection.Close();
                //if(connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }
                //connection.Open();
                //OleDbCommand command = new OleDbCommand();
                //command.Connection = connection;
                //string query = " select ID,name from grou";
                //command.CommandText = query;
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "name");
                //comboBox1.DisplayMember = "name";
                //comboBox1.ValueMember = "name";
                //comboBox1.DataSource = ds.Tables["name"];
                //    if (connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }SELECT ID, name FROM grou order by name
                using (OleDbDataAdapter sda = new OleDbDataAdapter("SELECT ID, name FROM grou order by name", connection))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dtr = new DataTable();
                    sda.Fill(dtr);

                    //Insert the Default Item to DataTable.
                    DataRow row = dtr.NewRow();
                    row[0] = 0;
                    row[1] = " ";
                    dtr.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.
                    comboGroup.DataSource = dtr;
                    comboGroup.DisplayMember = "name";
                    comboGroup.ValueMember = "ID";

                    //Set AutoCompleteMode.
                    comboGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong!");
            }
            finally
            {
                MessageBox.Show("sdf");
            }
        }

        private void unit()
        {
            try
            {
                //    if (connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }
                //    connection.Open();
                //OleDbCommand command = new OleDbCommand();
                //command.Connection = connection;
                //string query = " select ID,unit_name from unit";
                //command.CommandText = query;
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "unit_name");
                //comboBox2.DisplayMember = "unit_name";
                //comboBox2.ValueMember = "unit_name";
                //comboBox2.DataSource = ds.Tables["unit_name"];
                //    if (connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }

                using (OleDbDataAdapter sda = new OleDbDataAdapter("select ID,unit_name from unit order by unit_name", connection))
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
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "unit_name";
                    comboBox2.ValueMember = "ID";

                    //Set AutoCompleteMode.
                    comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("combobox2" + p);
            }
        }
        private void supplier()
        {
            try
            {
                //    if (connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }
                //    connection.Open();
                //OleDbCommand command = new OleDbCommand();
                //command.Connection = connection;
                //string query = " select ID,s_name from supplier";
                //command.CommandText = query;
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "s_name");
                //comboBox3.DisplayMember = "s_name";
                //comboBox3.ValueMember = "s_name";
                //comboBox3.DataSource = ds.Tables["s_name"];
                //    if (connection.State == ConnectionState.Open)
                //    {
                //        connection.Close();
                //    }

                using (OleDbDataAdapter sda = new OleDbDataAdapter("select ID,s_name from supplier order by s_name", connection))
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
                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = "s_name";
                    comboBox3.ValueMember = "s_name";

                    //Set AutoCompleteMode.
                    comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                }

            }
            catch (Exception p)
            {
                MessageBox.Show("combobox3" + p);
            }
        }

       

        private void comboBox2_Click(object sender, EventArgs e)
        {
            //unit();
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            //supplier();
        }
    }
}


