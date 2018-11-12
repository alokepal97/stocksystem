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
    public partial class invoice : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public invoice()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            getid();
        }


        public static string code = "";
        public static string qty = "";
        int selectedrow = 0;
        int selectedRow = 0;
        int tax = 0;
        double dis = 0;
        double cgst1 = 0;
        double sgst1 = 0;

        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox2.Text = Convert.ToString(get_id.invoice_id);
           
        }

        private void grid()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from in_main where (type = 'in')", connection);

            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["or_no"]), Convert.ToString(rdr["or_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["status"]), Convert.ToString(rdr["due_amount"]), Convert.ToString(rdr["c_code"]));
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                connection.Close();
            }
        }
        //-----------------datagridview1 cell no-----------------!
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedrow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
            }
        }
        //---------------------modify button----------------
        private void button2_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
            dataGridView2.Rows.Clear();

            //!-------------------------  ------------------!
            textBox2.Text = row.Cells[0].Value.ToString();
            textBox3.Text = row.Cells[0].Value.ToString();
            dateTimePicker1.Text = row.Cells[1].Value.ToString();
            comboBox1.Text = row.Cells[2].Value.ToString();
            dateTimePicker2.Text = row.Cells[3].Value.ToString();
            comboBox2.Text = row.Cells[4].Value.ToString();

            int discount = 0;
            //!--------fetch from database
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from invoice where  (in_no = @id) and (type = 'in')", connection);
            cmd.Parameters.AddWithValue("@id", row.Cells[0].Value.ToString());
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox5.Text = Convert.ToString(rdr["b_add"]);
                    textBox6.Text = Convert.ToString(rdr["b_city"]);
                    textBox7.Text = Convert.ToString(rdr["b_zip"]);
                    textBox8.Text = Convert.ToString(rdr["b_state"]);
                    textBox9.Text = Convert.ToString(rdr["b_country"]);
                    textBox12.Text = Convert.ToString(rdr["s_add"]);
                    textBox11.Text = Convert.ToString(rdr["s_city"]);
                    textBox10.Text = Convert.ToString(rdr["s_zip"]);
                    textBox14.Text = Convert.ToString(rdr["s_state"]);
                    textBox13.Text = Convert.ToString(rdr["s_country"]);
                    comboBox3.Text = Convert.ToString(rdr["sales_person"]);
                    textBox15.Text = Convert.ToString(rdr["contact_name"]);
                    textBox16.Text = Convert.ToString(rdr["notes"]);

                    dataGridView2.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["price"]), Convert.ToString(rdr["disc"]), Convert.ToString(rdr["disc_amount"]), Convert.ToString(rdr["total"]), Convert.ToString(rdr["disamount"]), Convert.ToString(rdr["cgst"]), Convert.ToString(rdr["cgst_amt"]), Convert.ToString(rdr["sgst"]), Convert.ToString(rdr["sgst_amt"]));
                    textBox20.Text = Convert.ToString(rdr["receive_amount"]);
                    textBox21.Text = Convert.ToString(rdr["extra_discount"]);
                    discount += Convert.ToInt32(Convert.ToString(rdr["disc"]));
                    textBox17.Text = Convert.ToString(discount);
                    double net = 0.000;
                    for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                    {
                        net += Convert.ToDouble(dataGridView2.Rows[i].Cells[7].Value);
                    }
                    textBox18.Text = net.ToString();

                    double neta = 0.000;
                    for (int j = 0; j < dataGridView2.Rows.Count; ++j)
                    {
                        neta += Convert.ToDouble(dataGridView2.Rows[j].Cells[8].Value);
                    }
                    textBox19.Text = Convert.ToString(neta);
                  
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("" + w);
            }

            finally
            {
                connection.Close();
            }
        }

        //!--------- Row Edit-------------!!
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
                if (dataGridView2.Rows[e.RowIndex].Cells[2].Value != null)
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

                        // calucation for textbox

                        double net = 0.000;
                        for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                        {
                            net += Convert.ToDouble(dataGridView2.Rows[i].Cells[8].Value);
                        }
                        textBox18.Text = net.ToString();

                        double neta = 0.000;
                        for (int j = 0; j < dataGridView2.Rows.Count; ++j)
                        {
                            neta += Convert.ToDouble(dataGridView2.Rows[j].Cells[8].Value);
                        }
                        textBox19.Text = Convert.ToString(neta);

                        double discount_total = 0.000;
                        for (int k = 0; k < dataGridView2.Rows.Count; ++k)
                        {
                            discount_total += Convert.ToDouble(dataGridView2.Rows[k].Cells[5].Value);
                        }
                        textBox17.Text = Convert.ToString(discount_total);

                        // textBox17.Text = Convert.ToString(net - neta);
                    }
                    else
                    {
                        MessageBox.Show("Current Stock  =" + "   " + stock_check.stock);
                        dataGridView2.Rows[e.RowIndex].Cells[2].Value = "";
                    }

                }
                else
                {
                    button7.Select();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Try Again Later");
            }
        }
        //----------------save code------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            //---------------- insert query------------------------
            try
            {
                int id = Convert.ToInt32(textBox3.Text);

                if (id == 0)
                {
                    string status = "";
                    string due = "";
                    if (Convert.ToDouble(textBox19.Text) == Convert.ToDouble(textBox20.Text))
                    {

                        status = "Paid";
                        due = "0";
                    }
                    else
                    {
                        status = "Due";
                        due = Convert.ToString(Convert.ToDouble(textBox19.Text) - Convert.ToDouble(textBox20.Text));

                    }
                    //insert into in_main
                    connection.Open();
                    string com = "insert into in_main(in_no,in_date,or_no, or_date,c_code,c_name,amount,status,due_amount,type) values('" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + comboBox1.Text + "','" + dateTimePicker2.Text + "','" + comboBox2.SelectedValue + "','" + comboBox2.Text + "','" + textBox19.Text + "',@status,@due,@type) ";
                    OleDbCommand comm = new OleDbCommand(com, connection);
                    comm.Parameters.AddWithValue("@status", status);
                    comm.Parameters.AddWithValue("@due", due);
                    comm.Parameters.AddWithValue("@type", "in");
                    comm.ExecuteNonQuery();
                    connection.Close();

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {

                        connection.Open();
                        string command = "insert into invoice(in_no,in_date,order_no, order_date,c_name,b_add,b_city,b_zip,b_state,b_country,s_add,s_city,s_zip,s_state,s_country,sales_person,contact_name,item_code,item_name,qty,unit,price,disc,disc_amount,total,disamount,cgst,cgst_amt,sgst,sgst_amt,notes,extra_discount,net_amount,receive_amount,type) values('" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + comboBox1.Text + "','" + dateTimePicker2.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox12.Text + "','" + textBox11.Text + "','" + textBox10.Text + "','" + textBox14.Text + "','" + textBox13.Text + "','" + comboBox3.Text + "','" + textBox15.Text + "',@item_code,@item_name,@qty,@unit,@price,@disc,@disc_amount,@total,@dismount,@cgst,@cgst_amt,@sgst,@sgst_amt,'" + textBox16.Text + "',@extra_discount,'" + textBox19.Text + "','" + textBox20.Text + "',@type) ";
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
                        cmdd.Parameters.AddWithValue("@extra_discount", textBox21.Text);
                        cmdd.Parameters.AddWithValue("@type", "in");
                        cmdd.ExecuteNonQuery();

                        invoice.code = row.Cells[0].Value.ToString();
                        stock_check st = new stock_check();
                        st.getstock();
                        invoice.code = row.Cells[0].Value.ToString();
                        invoice.qty = Convert.ToString(stock_check.stock - Convert.ToDouble(row.Cells[2].Value.ToString()));
                        insert_update_invoice up = new insert_update_invoice();
                        up.update_stock();
                      
                        // update id 
                        int idd = 1;
                        tax = get_id.invoice_id + 1;
                         
                        try
                        {
                            OleDbCommand command2 = new OleDbCommand(@"UPDATE get_id
                                                    SET invoice_id = @p_order_no
                                                       WHERE ID = " + idd + "", connection);

                            command2.Parameters.AddWithValue("@p_order_no", tax);
                            command2.ExecuteNonQuery();
                            connection.Close();

                        }
                        catch (Exception a)
                        {
                            MessageBox.Show("" + a);
                        }
                        gridview();
                        grid();
                        clear();
                        getid();
                    }
                }
                else
                {
                    MessageBox.Show("Can't update Invoice");
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                connection.Close();
            }
        }

        //add blank row 
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

        //delete row
        private void button8_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];

            if (Convert.ToInt32(textBox3.Text) == 0)
            {
                dataGridView2.Rows.Remove(row);
            }
            else
            {

                MessageBox.Show("You Can not Access.");
            }
        }
        //select item
        private void button7_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox3.Text) == 0)
            {
                this.Show();
                sale_item co = new sale_item();
                co.ShowDialog();

                //check if the column is already exist


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
                            connection.Close();
                            connection.Open();
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                dataGridView2.Rows[selectedRow].Cells[0].Value = Convert.ToString(rdr["item_code"]);
                                dataGridView2.Rows[selectedRow].Cells[1].Value = Convert.ToString(rdr["item_Name"]);
                                dataGridView2.Rows[selectedRow].Cells[2].Value = "0";
                                dataGridView2.Rows[selectedRow].Cells[3].Value = Convert.ToString(rdr["unit"]);
                                dataGridView2.Rows[selectedRow].Cells[4].Value = Convert.ToString(rdr["price"]);
                                dataGridView2.Rows[selectedRow].Cells[5].Value = Convert.ToString(rdr["dis_on_price"]);

                                 dis = (Convert.ToDouble(rdr["dis_on_price"]) / 100) * Convert.ToDouble(rdr["price"]);
                                dataGridView2.Rows[selectedRow].Cells[6].Value = Convert.ToString(dis);
                                dataGridView2.Rows[selectedRow].Cells[9].Value = Convert.ToString(rdr["cgst"]);
                                //if (rdr["cgst"].ToString().Length > 0)
                                //{
                                //    Double cgst1 = (Convert.ToDouble(rdr["cgst"]) / 100) * Convert.ToDouble(rdr["price"]);
                                //    dataGridView2.Rows[selectedRow].Cells[10].Value = Convert.ToString(cgst1);
                                //}
                                dataGridView2.Rows[selectedRow].Cells[11].Value = Convert.ToString(rdr["sgst"]);
                                //if (rdr["sgst"].ToString().Length > 0)
                                //{
                                //    Double sgst1 = (Convert.ToDouble(rdr["sgst"]) / 100) * Convert.ToDouble(rdr["price"]);
                                //    dataGridView2.Rows[selectedRow].Cells[12].Value = Convert.ToString(sgst1);
                                //}
                            }
                        }
                        catch (Exception exq)
                        {
                            MessageBox.Show("Display into column errorrrr!!!!!!!!!!!" + exq);
                        }
                        finally
                        {
                            connection.Close();
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("Can't access ." + "   " + "Please create new Invoice");
            }
        }

        //cell click datagridview2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
            }
        }

        //-------new button Code--------------------
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            textBox3.Text = "0";
            comboBox1.Text = comboBox2.Text = "";
            textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text = comboBox3.Text = null;
            textBox16.Text = "Pay With In 15 Days";
            textBox17.Text = textBox18.Text = textBox19.Text = textBox20.Text = textBox21.Text = "0";
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
        }

        private void clear()
        {
            dataGridView2.Rows.Clear();
            textBox3.Text = "0";
            comboBox1.Text = comboBox2.Text = "";
                textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text = comboBox3.Text = textBox16.Text = textBox17.Text = textBox18.Text = textBox19.Text = textBox20.Text = null;
           
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
        }
        //------------datagridview1 row clear-----------
        private void gridview()
        {
            dataGridView1.Rows.Clear();
        }
        //---------------delect Code---------------
        private void button4_Click(object sender, EventArgs e)
        {

            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
            string cf = row.Cells[0].Value.ToString();
            int cd = Convert.ToInt32(cf);
            if (cd > 0)
            {

                connection.Open();
                //dwelete from in_main
                OleDbCommand cmdd = new OleDbCommand("Delete from in_main where (in_no =@item_code) and (type='in')", connection);
                cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                cmdd.ExecuteNonQuery();
                //delete from invoice
                OleDbCommand cmd = new OleDbCommand("Delete from invoice where (in_no =@item_code) and (type='in')", connection);
                cmd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                cmd.ExecuteNonQuery();
                connection.Close();
                gridview();
                grid();
            }
        }

        //order no in combobox

        private void scombo()
        {
            try
            {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select order_no from sales_order";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Sales");
                comboBox1.DisplayMember = "order_no";
                comboBox1.ValueMember = "order_no";
                comboBox1.DataSource = ds.Tables["Sales"];
                connection.Close();
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }

        }

        //customer in comboBox

        private void scomb()
        {
            try
            {
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
                connection.Close();

            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
        }


        // Sales Person in combo Box

        private void salesp()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select ID,p_name from tb_p";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Sales");
                comboBox3.DisplayMember = "p_name";
                comboBox3.ValueMember = "ID";
                comboBox3.DataSource = ds.Tables["Sales"];
                

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString().Length > 0)
            {
                connection.Close();
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from sales_order where order_no =@item_code", connection);
                cmd.Parameters.AddWithValue("@item_code", comboBox1.SelectedValue.ToString());
                try
                {
                    dataGridView2.Rows.Clear();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        dataGridView2.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["price"]), Convert.ToString(rdr["disc"]), Convert.ToString(rdr["disc_amount"]), Convert.ToString(rdr["total"]), Convert.ToString(rdr["disamount"]), Convert.ToString(rdr["cgst"]), Convert.ToString(rdr["cgst_amt"]), Convert.ToString(rdr["sgst"]), Convert.ToString(rdr["sgst_amt"]));

                    }
                }

                catch (Exception t)
                {
                    MessageBox.Show("Error" + t);
                }
                finally
                {
                    connection.Close();
                    double net = 0.000;
                    for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                    {
                        net += Convert.ToDouble(dataGridView2.Rows[i].Cells[7].Value);
                    }
                    textBox18.Text = net.ToString();

                    double neta = 0.000;
                    for (int j = 0; j < dataGridView2.Rows.Count; ++j)
                    {
                        neta += Convert.ToDouble(dataGridView2.Rows[j].Cells[8].Value);
                    }
                    textBox19.Text = Convert.ToString(neta);

                    // discount amount
                    textBox17.Text = Convert.ToString(net - neta);
                }
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
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        textBox4.Text = Convert.ToString(rdr["c_code"]);
                        textBox5.Text = Convert.ToString(rdr["b_add"]);
                        textBox6.Text = Convert.ToString(rdr["b_city"]);
                        textBox7.Text = Convert.ToString(rdr["b_zip"]);
                        textBox8.Text = Convert.ToString(rdr["b_state"]);
                        textBox9.Text = Convert.ToString(rdr["b_country"]);
                        textBox12.Text = Convert.ToString(rdr["d_add"]);
                        textBox11.Text = Convert.ToString(rdr["d_city"]);
                        textBox10.Text = Convert.ToString(rdr["d_zip"]);
                        textBox14.Text = Convert.ToString(rdr["d_state"]);
                        textBox13.Text = Convert.ToString(rdr["d_country"]);
                        textBox15.Text = Convert.ToString(rdr["b_contact"]);
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

        private void comboBox1_Click(object sender, EventArgs e)
        {
            scombo();
        }
        private void comboBox2_Click(object sender, EventArgs e)
        {

            scomb();
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            salesp();
        }
        //int id = 0;
   
        private void button9_Click(object sender, EventArgs e)
        {
              string in_no= "";
            // insert query into payment Receipt
              DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
          // check if the invoice no is already exist or not
            try
            {
                connection.Close();
                connection.Open();

                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from payment_receipt where invoice_type = 'Invoice' AND in_no = '" + row.Cells[0].Value + "' ", connection);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    in_no = Convert.ToString(rdr["in_no"]);
                    
                }
            }
            catch (Exception o)
            {
                MessageBox.Show("getid" + o);
            }
            finally
            {
                connection.Close();
            }

            if (in_no == row.Cells[0].Value.ToString())
            {
                MessageBox.Show("Already exist");
            }
            else
            {
                try
                {
                    string bla = "";
                    bla = "1";

                    get_id gi = new get_id();
                    gi.taxinvoice();
                    int receipt_no = get_id.pay_re;
                   

                    connection.Close();
                    connection.Open();
                    string command = "insert into payment_receipt(re_no,re_date,payment_type,invoice_type,ref_no,ref_date,in_no,in_date,c_name,total_amount,due_amount,receive_amount,total_receive,c_code) values(@re_no,@re_date,@payment_type,@invoice_type,@ref_no,@ref_date,@in_no,@in_date,@c_name,@in_amount,@due_amount,@receive_amount,@total_receive,@c_code) ";
                    OleDbCommand cmdd = new OleDbCommand(command, connection);
                    cmdd.Parameters.AddWithValue("@re_no", receipt_no);
                    cmdd.Parameters.AddWithValue("@re_date", dateTimePicker2.Text);
                    cmdd.Parameters.AddWithValue("@payment_type", "Against Invoice");
                    cmdd.Parameters.AddWithValue("@invoice_type", "Invoice");
                    cmdd.Parameters.AddWithValue("@re_no", receipt_no);
                    cmdd.Parameters.AddWithValue("@re_date", dateTimePicker2.Text);
                    cmdd.Parameters.AddWithValue("@in_no", row.Cells[0].Value);
                    cmdd.Parameters.AddWithValue("@in_date", row.Cells[1].Value);
                    cmdd.Parameters.AddWithValue("@c_name", row.Cells[4].Value);
                    cmdd.Parameters.AddWithValue("@in_amount", row.Cells[5].Value);
                    cmdd.Parameters.AddWithValue("@due_amount", row.Cells[7].Value);
                    string receive = Convert.ToString(Convert.ToDouble(row.Cells[5].Value.ToString()) - Convert.ToDouble(row.Cells[7].Value.ToString()));
                    cmdd.Parameters.AddWithValue("@receive_amount", receive);
                    cmdd.Parameters.AddWithValue("@total_receive", receive);
                    cmdd.Parameters.AddWithValue("@c_code", row.Cells[8].Value);
                    cmdd.ExecuteNonQuery();

                    //update into id
                    int df = get_id.pay_re+1;
                    OleDbCommand command1 = new OleDbCommand(@"UPDATE get_id
                                                    SET pay_re = @City_Name
                                                       
                                                    WHERE ID = " + bla + "", connection);

                    command1.Parameters.AddWithValue("@City_Name", df);

                    command1.ExecuteNonQuery();


                }
                catch (Exception p)
                {
                    MessageBox.Show("" + p);
                }
                finally
                {
                    connection.Close();

                    payment_re ts = new payment_re();
                    ts.ShowDialog();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];

            if (row.Cells[6].Value.ToString() == "Paid")
            {
                invoice_print.c_name = row.Cells[8].Value.ToString();
                invoice_print.in_no = row.Cells[0].Value.ToString();

                invoice_print ip = new invoice_print();
                ip.ShowDialog();
            }
            else
            {
                MessageBox.Show("Amount not Paid");
            }
        }

        private void invoice_Load(object sender, EventArgs e)
        {
            getid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from in_main where ( c_name like '" + textBox1.Text + "%') and (type='in')", connection);

            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["or_no"]), Convert.ToString(rdr["or_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["status"]), Convert.ToString(rdr["due_amount"]));
                }
            }
            catch (Exception u)
            {
                MessageBox.Show("" + u);
            }
            finally
            {
                connection.Close();
            }

        }
        //discount amount%
        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                    double Fdiscount = (Convert.ToDouble(textBox18.Text) / 100) * Convert.ToDouble(textBox21.Text);
                    textBox19.Text = Convert.ToString(Convert.ToDouble(textBox18.Text) - Fdiscount);
                

            }
            catch (Exception)
            {
                //MessageBox.Show("Try Again Later");
            }
        }

    }
}
