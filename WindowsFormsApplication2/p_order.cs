using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace WindowsFormsApplication2
{
    public partial class p_order : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public p_order()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;

            gridview();
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            
            
        }
        public static Int32 pt_no = 0;
        public static string supplier_name = "";
        int selectedRow = 0;
        

        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox1.Text = Convert.ToString(get_id.p_order_no);
            textBox2.Text = Convert.ToString(get_id.p_orderref_no);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
                      
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            dataGridView1.Rows.Remove(row);
            textBox11.Text = "0";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["sno"].Value = (e.RowIndex + 1).ToString();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // display to textbox for subtotal
            double subtotal = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[5].Value.ToString().Length > 0)
                {
                    subtotal += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                }
            }
            textBox10.Text = subtotal.ToString();

            //display to textbox for discount

            double discount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[6].Value.ToString().Length > 0)
                {
                    discount += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                }
            }
            textBox11.Text = discount.ToString();

            //display to textbox for Cgst

            double cgst = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Length > 0)
                {
                    cgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                }
            }
            textBox12.Text = cgst.ToString();

            //display to textbox for sgst

            double sgst = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[8].Value.ToString().Length > 0)
                {
                    sgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);
                }
            }
            textBox13.Text = sgst.ToString();

            //display to textbox for igst
            if (string.IsNullOrEmpty(dataGridView1.Rows[0].Cells[4].Value as string))
            {
            }
            else
            {
                //----------------display data into datagrid view----------------------------------
                if (dataGridView1.SelectedCells[4].Value.ToString().Length > 0)
                {
                    if (string.IsNullOrEmpty(dataGridView1.Rows[0].Cells[5].Value as string))
                    {
                    }
                    else
                    {
                        if (dataGridView1.SelectedCells[5].Value.ToString().Length > 0)
                        {

                            dataGridView1.Rows[e.RowIndex].Cells[9].Value = Convert.ToString(Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[5].Value));
                            double net = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                net += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value);
                            }
                            textBox14.Text = net.ToString();
                        }
                    }
                }
            }
        }

        //!-------save button---------------!--
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox3.Text);

                if (id == 0)
                {
                    string command1 = "insert into purchase_main(p_no, p_date, d_date, s_name, amount, status) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + comboBox2.Text + "',@amount,@status) ";

                    OleDbCommand cmdd1 = new OleDbCommand(command1, connection);

                    cmdd1.Parameters.AddWithValue("@amount", textBox14.Text);
                    cmdd1.Parameters.AddWithValue("@status", "Pending");
                    if(connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    cmdd1.ExecuteNonQuery();
                    connection.Close();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string command = "insert into p_order(or_no, or_date, ref_no, deli_date, supplier_name,item_code, item_name, unit, qty, purchase_price, dis_on_p, cgst, sgst, total_amount, status,amount) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "','" + dateTimePicker2.Text + "','" + comboBox2.Text + "',@item_code,@item_name,@unit,@qty,@purchase_price,@dis_on_p,@cgst,@sgst,@total_amount,@status,@amount) ";

                        OleDbCommand cmdd = new OleDbCommand(command, connection);

                        cmdd.Parameters.AddWithValue("@item_name", row.Cells[1].Value);
                        cmdd.Parameters.AddWithValue("@item_code", row.Cells[2].Value);
                        cmdd.Parameters.AddWithValue("@unit", row.Cells[3].Value);
                        cmdd.Parameters.AddWithValue("@qty", row.Cells[4].Value);
                        cmdd.Parameters.AddWithValue("@purchase_price", row.Cells[5].Value);
                        cmdd.Parameters.AddWithValue("@dis_on_p", row.Cells[6].Value);
                        cmdd.Parameters.AddWithValue("@cgst", row.Cells[7].Value);
                        cmdd.Parameters.AddWithValue("@sgst", row.Cells[8].Value);
                        cmdd.Parameters.AddWithValue("@total_amount", row.Cells[9].Value);
                        cmdd.Parameters.AddWithValue("@status", "Pending");
                        cmdd.Parameters.AddWithValue("@amount", textBox14.Text);

                        cmdd.ExecuteNonQuery();
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    //update order id and ref no by 1
                    int order_no = 0;
                    int reference_no = 0;
                    int idd = 1;
                    order_no = get_id.p_order_no + 1;
                    reference_no = get_id.p_orderref_no + 1;
                    try
                    {
                        OleDbCommand command = new OleDbCommand(@"UPDATE get_id
                                                    SET p_order_no = @p_order_no,
                                                        p_orderref_no = @p_orderref_no
                                                    WHERE ID = " + idd + "", connection);

                        command.Parameters.AddWithValue("@p_order_no",order_no );
                        command.Parameters.AddWithValue("@p_orderref_no", reference_no);
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                    catch(Exception a)
                    {
                        MessageBox.Show(""+a);
                    }

                    ResetForm();
                    grid();
                    gridview();
                    getid();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Access");
                }
            }

            catch (Exception r)
            {
                MessageBox.Show("" + r);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Show();
            item_a co = new item_a();
            co.ShowDialog();


            if (item_a.item_code.Length > 0)
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from item where item_code =@item_code", connection);
                cmd.Parameters.AddWithValue("@item_code", item_a.item_code);
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
                        dataGridView1.Rows[selectedRow].Cells[1].Value = Convert.ToString(rdr["item_code"]);
                        dataGridView1.Rows[selectedRow].Cells[2].Value = Convert.ToString(rdr["item_Name"]);
                        dataGridView1.Rows[selectedRow].Cells[3].Value = Convert.ToString(rdr["unit"]);
                        dataGridView1.Rows[selectedRow].Cells[5].Value = Convert.ToString(rdr["purchase_r"]);
                        dataGridView1.Rows[selectedRow].Cells[6].Value = Convert.ToString(rdr["discount_r"]);
                        dataGridView1.Rows[selectedRow].Cells[7].Value = Convert.ToString(rdr["cgst"]);
                        dataGridView1.Rows[selectedRow].Cells[8].Value = Convert.ToString(rdr["sgst"]);
                        

                    }
                }
                catch (Exception exq)
                {
                    MessageBox.Show("Display into column errorrrr!!!!!!!!!!!" + exq);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            string cg = row.Cells[0].Value.ToString();
            int ch = Convert.ToInt32(cg);
            if (ch > 0)
            {
                this.tabControl1.SelectedTab = tabPage2;
                dataGridView1.Rows.Clear();


                // display datagridview selected row data into textboxes
                textBox3.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                dateTimePicker2.Text = row.Cells[3].Value.ToString();
                comboBox2.Text = row.Cells[4].Value.ToString();
                textBox14.Text = row.Cells[5].Value.ToString();

                //!--------fetch from database
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from p_order where  (or_no = @id)", connection);
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
                        textBox2.Text = Convert.ToString(rdr["ref_no"]);
                        int no = 0;
                        no = no + 1;
                        //-------------disply into datagridview1---------------------
                        dataGridView1.Rows.Add(Convert.ToString(no), Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["purchase_price"]), Convert.ToString(rdr["dis_on_p"]), Convert.ToString(rdr["cgst"]), Convert.ToString(rdr["sgst"]), Convert.ToString(rdr["total_amount"]));
                        // other code
                    }
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        double subtotal = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[5].Value.ToString().Length > 0)
                        {
                            subtotal += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                        }
                    }
                    textBox10.Text = subtotal.ToString();

                    //display to textbox for discount

                    double discount = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[6].Value.ToString().Length > 0)
                        {
                            discount += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                        }
                    }
                    textBox11.Text = discount.ToString();

                    //display to textbox for Cgst

                    double cgst = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[7].Value.ToString().Length > 0)
                        {
                            cgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                        }
                    }
                    textBox12.Text = cgst.ToString();

                    //display to textbox for sgst

                    double sgst = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[8].Value.ToString().Length > 0)
                        {
                            sgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);
                        }
                    }
                    textBox13.Text = sgst.ToString();

                    //display to textbox for igst



                }
                catch (Exception o)
                {
                    MessageBox.Show("" + o);
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
            }

        }

        private void gridview()
        {    // display code

            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from purchase_main", connection);
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
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["p_no"]), Convert.ToString(rdr["p_date"]), Convert.ToString(rdr["d_date"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["status"]));
                }
            }


            catch (Exception i)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!" + i);
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
                dataGridView2.Rows.Clear();

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
            if (selectedRow != -1)
            {
                DataGridViewRow newDataRow = dataGridView2.Rows[selectedRow];
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                string cf = row.Cells[0].Value.ToString();
                string cf1 = row.Cells[1].Value.ToString();
                int cd1 = Convert.ToInt32(cf1);
                int cd = Convert.ToInt32(cf);

                if (cd > 0)
                {
                    try
                    {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            connection.Open();

                        OleDbCommand cmd1 = new OleDbCommand("Delete from p_order where or_no = @or_no", connection);
                        cmd1.Parameters.AddWithValue("@or_no", row.Cells[1].Value);
                        cmd1.ExecuteNonQuery();

                        OleDbCommand cmd = new OleDbCommand("Delete from purchase_main where ID = @id", connection);
                        cmd.Parameters.AddWithValue("@id", row.Cells[0].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("DATA Deleted Sucessfully");
                        connection.Close();
                        grid();
                        gridview();
                        ResetForm();

                    }
                    catch (Exception t)
                    {
                        MessageBox.Show("" + t);
                    }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    // InitializeComponent();
                }

            }
        }
        }
        private void ResetForm()
        {
            comboBox2.Text = null;

            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            textBox3.Text = "0";
            textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = null; 
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

            }
            catch (Exception g)
            {
                MessageBox.Show("" + g);
            }
            getid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
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
                comboBox2.DisplayMember = "s_name";
                comboBox2.ValueMember = "s_name";
                comboBox2.DataSource = ds.Tables["s_name"];
              }
            catch (Exception p)
            {
                MessageBox.Show("combobox3" + p);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        
        private void comboBox2_Click(object sender, EventArgs e)
        {

            supplier();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView2.Rows[selectedRow];
            DataGridViewRow row = dataGridView2.Rows[selectedRow];

            if (row.Cells[6].Value.ToString() == "Pending")
            {
                pt_no =Convert.ToInt32(row.Cells[1].Value.ToString());
                supplier_name = row.Cells[4].Value.ToString();
                p_order_print ip = new p_order_print();
                ip.ShowDialog();
            }
            else
            {
                MessageBox.Show("Product is not Received");
            }
        }

        private void p_order_Load(object sender, EventArgs e)
        {
            getid();
        }
       

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from purchase_main where s_name like '"+textBox6.Text+"%'", connection);
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
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["p_no"]), Convert.ToString(rdr["p_date"]), Convert.ToString(rdr["d_date"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["status"]));
                }
            }


            catch (Exception i)
            {
                MessageBox.Show("ERROR TO LOAD!!!!!!!!!!!" + i);
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
            
           
        
    

