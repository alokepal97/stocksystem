using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class sales_order : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_order()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            getid();

        }
        int selectedRow = 0;
        Double dis = 0;
        Double cgst1 = 0;
        Double sgst1 = 0;
        int selectedrow = 0;
        int order_no = 0;
        int reference_no = 0;


        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox2.Text = Convert.ToString(get_id.sales_no);
            textBox4.Text = Convert.ToString(get_id.sales_ref);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedrow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Show();
            sale_item co = new sale_item();
            co.ShowDialog();

            Boolean found = false;
            if (sale_item.item_code.Length > 0)
            {
                if (dataGridView2.Rows.Count > 0)
                {

                    for (int h = 0; h < dataGridView2.Rows.Count - 1; ++h)
                    {
                        if (dataGridView2.Rows[h].Cells[0].Value.ToString() == sale_item.item_code)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found)
                {
                    MessageBox.Show("Already Exists");
                }
                else if (!found)
                {
                    found = false;
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from item where item_code =@item_code", connection);
                    cmd.Parameters.AddWithValue("@item_code", sale_item.item_code);
                    try
                    {
                        if(connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {

                            dataGridView2.Rows[selectedRow].Cells[0].Value = Convert.ToString(rdr["item_code"]);
                            dataGridView2.Rows[selectedRow].Cells[1].Value = Convert.ToString(rdr["item_Name"]);
                            //  dataGridView2.Rows[selectedRow].Cells[2].Value = "";
                            dataGridView2.Rows[selectedRow].Cells[2].Value = "0";
                            dataGridView2.Rows[selectedRow].Cells[3].Value = Convert.ToString(rdr["unit"]);
                            dataGridView2.Rows[selectedRow].Cells[4].Value = Convert.ToString(rdr["price"]);
                            dataGridView2.Rows[selectedRow].Cells[5].Value = Convert.ToString(rdr["dis_on_price"]);

                            dis = (Convert.ToDouble(rdr["dis_on_price"]) / 100) * Convert.ToDouble(rdr["price"]);
                            dataGridView2.Rows[selectedRow].Cells[6].Value = Convert.ToString(dis);
                            dataGridView2.Rows[selectedRow].Cells[9].Value = Convert.ToString(rdr["cgst"]);
                            //if (rdr["cgst"].ToString().Length > 0)
                            //{
                            //    cgst1 = (Convert.ToDouble(rdr["cgst"]) / 100) * Convert.ToDouble(rdr["price"]);
                            //    dataGridView2.Rows[selectedRow].Cells[10].Value = Convert.ToString(cgst1);
                            //}
                            dataGridView2.Rows[selectedRow].Cells[11].Value = Convert.ToString(rdr["sgst"]);
                            //if (rdr["sgst"].ToString().Length > 0)
                            //{
                            //    sgst1 = (Convert.ToDouble(rdr["sgst"]) / 100) * Convert.ToDouble(rdr["price"]);
                            //    dataGridView2.Rows[selectedRow].Cells[12].Value = Convert.ToString(sgst1);
                            //}

                            //price - discount_on_price 
                            // then calculate 
                            // gst price 
                            //dataGridView2.Rows[selectedRow].Cells[4].Value = 
                            //dataGridView2.Rows[selectedRow].Cells[8].Value = Convert.ToString(Convert.ToDouble(dataGridView2.Rows[selectedRow].Cells[4].Value) - dis);

                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something Wrong!");
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

        private void button6_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(textBox3.Text) == 0)
            {
                if (dataGridView2.RowCount == 0)
                {
                    dataGridView2.Rows.Add();

                }

                if (dataGridView2.Rows[selectedRow].Cells[2].Value != null)
                {
                    dataGridView2.Rows.Add();
                }
                else
                {

                    button7.Select();
                }
            }
            else
            {
                MessageBox.Show("Can not Access.");
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    invoice.code = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    stock_check st = new stock_check();
                    st.getstock();
                }
                if (dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)
                //Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                {
                    if (Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString()) < stock_check.stock)
                    {
                        dis = ((Convert.ToDouble(dataGridView2.Rows[selectedRow].Cells[5].Value)) / 100) *
                          Convert.ToDouble(Convert.ToDouble(dataGridView2.Rows[selectedRow].Cells[4].Value));

                        dataGridView2.Rows[selectedRow].Cells[6].Value = Convert.ToString(dis);

                        dataGridView2.Rows[e.RowIndex].Cells[6].Value = Convert.ToString(Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[6].Value) *
                            Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value));
                        //total
                        double tot = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value) *
                            Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[4].Value);

                        double totalamt = tot - Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[6].Value);

                        if (totalamt >= 0)
                        {
                            dataGridView2.Rows[e.RowIndex].Cells[7].Value = Convert.ToString(totalamt);
                        }
                        else
                        {
                            dataGridView2.Rows[e.RowIndex].Cells[7].Value = Convert.ToString("0");
                        }

                        //discountamount
                        double var = tot - Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[6].Value);
                        //cgst amount on discount price
                        cgst1 = (Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[9].Value) / 100) * var;
                        //sgst amount on discount price
                        sgst1 = (Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[11].Value) / 100) * var;

                        dataGridView2.Rows[e.RowIndex].Cells[8].Value = Convert.ToString(var + cgst1 + sgst1);
                        dataGridView2.Rows[e.RowIndex].Cells[10].Value = Convert.ToString(cgst1);
                        dataGridView2.Rows[e.RowIndex].Cells[12].Value = Convert.ToString(sgst1);

                        double net = 0.000;
                        for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                        {
                            net += Convert.ToDouble(dataGridView2.Rows[i].Cells[8].Value);
                        }
                        textBox12.Text = net.ToString();

                        double neta = 0.000;
                        for (int j = 0; j < dataGridView2.Rows.Count; ++j)
                        {
                            neta += Convert.ToDouble(dataGridView2.Rows[j].Cells[8].Value);
                        }
                        textBox16.Text = Convert.ToString(neta);

                        double cgstamount = 0.000;
                        for (int h = 0; h < dataGridView2.Rows.Count; ++h)
                        {
                            cgstamount += Convert.ToDouble(dataGridView2.Rows[h].Cells[10].Value);
                        }
                        textBox14.Text = Convert.ToString(cgstamount);

                        double sgstamount = 0.000;
                        for (int k = 0; k < dataGridView2.Rows.Count; ++k)
                        {
                            sgstamount += Convert.ToDouble(dataGridView2.Rows[k].Cells[12].Value);
                        }
                        textBox15.Text = Convert.ToString(sgstamount);

                        double discount_total = 0.000;
                        for (int k = 0; k < dataGridView2.Rows.Count; ++k)
                        {
                            discount_total += Convert.ToDouble(dataGridView2.Rows[k].Cells[5].Value);
                        }
                        textBox13.Text = Convert.ToString(discount_total);
                    }
                }
                else
                {
                    MessageBox.Show("Enter the Quantity");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something Goes Wrong!");
            }
        }
        private void scombo()
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
                string query = "select C_name,c_code from customer where (o_details ='Active')";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Customer");
                comboBox2.DisplayMember = "C_name";
                comboBox2.ValueMember = "c_code";
                comboBox2.DataSource = ds.Tables["Customer"];
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {


                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from customer where  (c_code = @id)", connection);
                cmd.Parameters.AddWithValue("@id", comboBox2.SelectedValue.ToString());
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
                        textBox17.Text = Convert.ToString(rdr["c_code"]);
                        textBox5.Text = Convert.ToString(rdr["b_add"]);
                        textBox6.Text = Convert.ToString(rdr["b_city"]);
                        textBox7.Text = Convert.ToString(rdr["b_zip"]);
                        textBox8.Text = Convert.ToString(rdr["b_state"]);
                        textBox9.Text = Convert.ToString(rdr["b_country"]);
                        textBox10.Text = Convert.ToString(rdr["b_contact"]);

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
        private void grid()
        {

            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_sales", connection);

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
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["or_no"]), Convert.ToString(rdr["or_date"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["d_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["net_amount"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["c_code"]));
                }

            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void sales_order_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stockDataSet6.customer' table. You can move, or remove it, as needed.
            getid();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox3.Text);

                if (id == 0)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    string command1 = "insert into main_sales(or_no, or_date, ref_no, ref_date, d_date,c_code,c_name,net_amount,notes,total_disc) values('" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "','" + dateTimePicker3.Text + "','" + dateTimePicker4.Text + "','" + textBox17.Text + "','" + comboBox2.Text + "','" + textBox16.Text + "','" + textBox11.Text + "','" + textBox13.Text + "') ";
                    OleDbCommand cmdd1 = new OleDbCommand(command1, connection);
                    cmdd1.ExecuteNonQuery();
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {

                        string command = "insert into sales_order(order_no, order_date, ref_no, ref_date, delivery_date,c_name,c_add,c_city,c_zip,c_state,c_country,c_contact,item_code,item_name,qty,unit,price,disc,disc_amount,total,disamount,cgst,cgst_amt,sgst,sgst_amt,notes) values('" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "','" + dateTimePicker3.Text + "','" + dateTimePicker4.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "',@item_code,@item_name,@qty,@unit,@price,@disc,@disc_amount,@total,@dismount,@cgst,@cgst_amt,@sgst,@sgst_amt,'" + textBox11.Text + "') ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        cmdd.Parameters.AddWithValue("@item_name", row.Cells[1].Value);
                        cmdd.Parameters.AddWithValue("@qty", row.Cells[2].Value);
                        cmdd.Parameters.AddWithValue("@unit", row.Cells[3].Value);
                        cmdd.Parameters.AddWithValue("@price", row.Cells[4].Value);
                        cmdd.Parameters.AddWithValue("@disc", row.Cells[5].Value);
                        cmdd.Parameters.AddWithValue("@disc_amount", row.Cells[6].Value);
                        cmdd.Parameters.AddWithValue("@total", row.Cells[7].Value);
                        cmdd.Parameters.AddWithValue("@disamount", row.Cells[8].Value);
                        cmdd.Parameters.AddWithValue("@cgst", row.Cells[9].Value);
                        cmdd.Parameters.AddWithValue("@cgst_amt", row.Cells[10].Value);
                        cmdd.Parameters.AddWithValue("@sgst", row.Cells[11].Value);
                        cmdd.Parameters.AddWithValue("@sgst_amt", row.Cells[12].Value);

                        cmdd.ExecuteNonQuery();
                    }
                    //update order id and ref no by 1

                    int idd = 1;
                    order_no = get_id.sales_no + 1;
                    reference_no = get_id.sales_ref + 1;
                    try
                    {
                        OleDbCommand command = new OleDbCommand(@"UPDATE get_id
                                                    SET sales_no = @p_order_no,
                                                        sales_ref = @p_orderref_no
                                                    WHERE ID = " + idd + "", connection);

                        command.Parameters.AddWithValue("@p_order_no", order_no);
                        command.Parameters.AddWithValue("@p_orderref_no", reference_no);
                        command.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("" + a);
                    }

                    ResetForm();
                    gridview();
                    grid();
                    getid();
                }
                else
                {
                    MessageBox.Show("Cannot Access");
                }
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
                dataGridView2.Rows.Clear();
                this.tabControl1.SelectedTab = tabPage2;
                //!-------------------------  ------------------!

                textBox3.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                dateTimePicker3.Text = row.Cells[4].Value.ToString();
                dateTimePicker4.Text = row.Cells[5].Value.ToString();
                comboBox2.Text = row.Cells[6].Value.ToString();
                textBox16.Text = row.Cells[7].Value.ToString();
                textBox11.Text = row.Cells[8].Value.ToString();

                //!--------fetch from database
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from sales_order where  (order_no = @id)", connection);
                cmd.Parameters.AddWithValue("@id", row.Cells[1].Value.ToString());
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
                        textBox5.Text = Convert.ToString(rdr["c_add"]);
                        textBox6.Text = Convert.ToString(rdr["c_city"]);
                        textBox7.Text = Convert.ToString(rdr["c_zip"]);
                        textBox8.Text = Convert.ToString(rdr["c_state"]);
                        textBox9.Text = Convert.ToString(rdr["c_country"]);
                        textBox10.Text = Convert.ToString(rdr["c_contact"]);


                        dataGridView2.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["price"]), Convert.ToString(rdr["disc"]), Convert.ToString(rdr["disc_amount"]), Convert.ToString(rdr["total"]), Convert.ToString(rdr["disamount"]), Convert.ToString(rdr["cgst"]), Convert.ToString(rdr["cgst_amt"]), Convert.ToString(rdr["sgst"]), Convert.ToString(rdr["sgst_amt"]));

                        double net = 0.000;
                        for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                        {
                            net += Convert.ToDouble(dataGridView2.Rows[i].Cells[8].Value);
                        }
                        textBox12.Text = net.ToString();

                        double neta = 0.000;
                        for (int j = 0; j < dataGridView2.Rows.Count; ++j)
                        {
                            neta += Convert.ToDouble(dataGridView2.Rows[j].Cells[8].Value);
                        }
                        textBox16.Text = Convert.ToString(neta);

                        double cgstamount = 0.000;
                        for (int h = 0; h < dataGridView2.Rows.Count; ++h)
                        {
                            cgstamount += Convert.ToDouble(dataGridView2.Rows[h].Cells[10].Value);
                        }
                        textBox14.Text = Convert.ToString(cgstamount);

                        double sgstamount = 0.000;
                        for (int k = 0; k < dataGridView2.Rows.Count; ++k)
                        {
                            sgstamount += Convert.ToDouble(dataGridView2.Rows[k].Cells[12].Value);
                        }
                        textBox15.Text = Convert.ToString(sgstamount);



                        // discount %
                        double discount = 0.000;
                        for (int k = 0; k < dataGridView2.Rows.Count; ++k)
                        {
                            discount += Convert.ToDouble(dataGridView2.Rows[k].Cells[5].Value);
                        }
                        textBox13.Text = Convert.ToString(discount);
                    }
                }
                catch (Exception w)
                {
                    MessageBox.Show("" + w);
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

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {

                int or = Convert.ToInt32(textBox3.Text);
                if (or == 0)
                {
                    int row = dataGridView2.CurrentCell.RowIndex;
                    dataGridView2.Rows.RemoveAt(row);

                    //DataGridViewRow row = dataGridView2.SelectedRows[0];
                    //dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
                }
                else
                {

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    OleDbCommand cmd = new OleDbCommand("Delete from sales_order where order_no =@item_code and item_code =@item ", connection);
                    cmd.Parameters.AddWithValue("@item_code", textBox3.Text);
                    cmd.Parameters.AddWithValue("@item", dataGridView2.Rows[selectedRow].Cells[0].Value);

                    cmd.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    DataGridViewRow row = dataGridView2.Rows[selectedRow];
                    dataGridView2.Rows.Remove(row);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedrow];
                    string cf = row.Cells[0].Value.ToString();
                    int cd = Convert.ToInt32(cf);
                    if (cd > 0)
                    {
                        if(connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        } 
                        connection.Open();
                        OleDbCommand cmd1 = new OleDbCommand("Delete from main_sales where ID =@item_code", connection);
                        cmd1.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        cmd1.ExecuteNonQuery();
                        OleDbCommand cmd = new OleDbCommand("Delete from sales_order where order_no =@item_code", connection);
                        cmd.Parameters.AddWithValue("@item_code", row.Cells[1].Value);
                        cmd.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        gridview();
                        grid();
                        ResetForm();
                        MessageBox.Show("Data Deleted");

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Something Wrong!!!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            comboBox2.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text = textBox16.Text = null;

            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
            dateTimePicker4.ResetText();
            textBox3.Text = "0";

            try
            {
                dataGridView2.Rows.Clear();
            }
            catch (Exception g)
            {
                MessageBox.Show("" + g);
            }

            getid();
        }

        private void gridview()
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
            catch (Exception g)
            {
                MessageBox.Show("" + g);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
                sales_order_print.c_name = row.Cells[9].Value.ToString();
                sales_order_print.or_no = row.Cells[1].Value.ToString();
                sales_order_print ip = new sales_order_print();
                ip.ShowDialog();
            }
            catch (Exception y)
            {
                MessageBox.Show("" + y);
            }

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            scombo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_sales where c_name like '" + textBox1.Text + "%'", connection);

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
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["or_no"]), Convert.ToString(rdr["or_date"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["d_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["net_amount"]), Convert.ToString(rdr["notes"]));
                }

            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
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
