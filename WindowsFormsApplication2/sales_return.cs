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
    public partial class sales_return : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_return()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            getid();
        }
        //int selectedRow = 0;
        int selectedrow = 0;
        public static string type = "";
        public static string no = "";
        int return_no = 0;
       

        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox1.Text = Convert.ToString(get_id.sales_return_no);
           
        }

        //display code

        private void grid()
        {

            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_sales_return", connection);

            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["n_no"]), Convert.ToString(rdr["n_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["type"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["total"]), Convert.ToString(rdr["notes"]),Convert.ToString(rdr["stock_add"]));
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

        //!---------------Modify Button
        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow newDataRow = dataGridView2.Rows[selectedrow];
            DataGridViewRow row = dataGridView2.Rows[selectedrow];
            dataGridView1.Rows.Clear();
            this.tabControl1.SelectedTab = tabPage2;
            //!-------------------------  ------------------!
            textBox8.Text = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(row.Cells[2].Value.ToString());
            comboBox1.Text = row.Cells[3].Value.ToString();
            comboBox2.Text = row.Cells[4].Value.ToString();
            comboBox3.Text = row.Cells[5].Value.ToString();
            textBox7.Text = row.Cells[6].Value.ToString();
            textBox4.Text = row.Cells[7].Value.ToString();

            if (row.Cells[8].Value.ToString() == "1")
                    {
                        checkBox1.Checked = true;
                    }
            



            //!--------fetch from database
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from sales_return where  (n_no = @id)", connection);
            cmd.Parameters.AddWithValue("@id", row.Cells[1].Value.ToString());
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox2.Text = Convert.ToString(rdr["city"]);
                    dateTimePicker2.Text = Convert.ToString(rdr["in_date"]);
                    textBox3.Text = Convert.ToString(rdr["invoice_amount"]);
                    

                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["item_price"]), Convert.ToString(rdr["r_qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["disc"]), Convert.ToString(rdr["disc_amt"]), Convert.ToString(rdr["r_amt"]));

                    double total = 0.000;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        total += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                    }
                    textBox5.Text = total.ToString();

                    double disc = 0.000;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        disc += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    }
                    textBox6.Text = disc.ToString();

                    textBox7.Text = Convert.ToString(total - disc);

                    


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
        string val = "0";
        string val1 = "0";
        //save button code
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int id1 = Convert.ToInt32(textBox8.Text);
                if (id1 == 0)
                {
                    

                    if (checkBox1.Checked == true)
                    {
                        val = "1";

                    }
                    else 
                    {
                        val = "0";
                    }

                    connection.Open();
                    string command1 = "insert into main_sales_return(n_no, n_date, c_name,type,in_no,total,notes,stock_add) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox4.Text + "','" + val + "') ";
                    OleDbCommand cmdd1 = new OleDbCommand(command1, connection);
                    cmdd1.ExecuteNonQuery();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        string command = "insert into sales_return(n_no, n_date, c_name, city, type,in_no,in_date,invoice_amount,item_code,item_name,item_price,r_qty,unit,disc,disc_amt,r_amt,net_amount,notes) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Text + "','" + textBox3.Text + "',@item_code,@item_name,@item_price,@r_qty,@unit,@disc,@disc_amt,@r_amt,'" + textBox7.Text + "','" + textBox4.Text + "') ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);
                        cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        cmdd.Parameters.AddWithValue("@item_name", row.Cells[1].Value);
                        cmdd.Parameters.AddWithValue("@item_price", row.Cells[2].Value);
                        cmdd.Parameters.AddWithValue("@r_qty", row.Cells[3].Value);
                        cmdd.Parameters.AddWithValue("@unit", row.Cells[4].Value);
                        cmdd.Parameters.AddWithValue("@disc", row.Cells[5].Value);
                        cmdd.Parameters.AddWithValue("@disc_amt", row.Cells[6].Value);
                        cmdd.Parameters.AddWithValue("@r_amt", row.Cells[7].Value);
                                              
                        cmdd.ExecuteNonQuery();
                        if(checkBox1.Checked == true)
                        {
                        invoice.code = row.Cells[0].Value.ToString();
                        stock_check st = new stock_check();
                        st.getstock();
                        invoice.code = row.Cells[0].Value.ToString();
                        invoice.qty = Convert.ToString(stock_check.stock + Convert.ToDouble(row.Cells[3].Value.ToString()));
                        insert_update_invoice up = new insert_update_invoice();
                        up.update_stock();
                       
                       
                        }
                        
                    }
                    //update order id and ref no by 1

                    int idd = 1;
                    return_no = get_id.sales_return_no + 1;
                    
                    try
                    {
                        OleDbCommand command2 = new OleDbCommand(@"UPDATE get_id
                                                    SET sales_return_no = @p_order_no
                                                        WHERE ID = " + idd + "", connection);

                        command2.Parameters.AddWithValue("@p_order_no", return_no);
                       
                        command2.ExecuteNonQuery();
                        connection.Close();

                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("" + a);
                    }
                    resetform();
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
            finally
            {
                connection.Close();
            
            }


        }
        //select code
       

        private void button6_Click(object sender, EventArgs e)
        {
            type = comboBox2.Text;
            no = comboBox3.Text;
            sales_retuen_issue ui = new sales_retuen_issue();
            ui.ShowDialog();

            Boolean found = false;

            if (sales_retuen_issue.item_code.Length > 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {

                    for (int h = 0; h < dataGridView1.Rows.Count; ++h)
                    {
                        if (dataGridView1.Rows[h].Cells[0].Value.ToString() == sales_retuen_issue.item_code)
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

                    if (comboBox2.Text == "Invoice")
                    {
                        //invoice code
                        OleDbDataReader rdr = null;
                        OleDbCommand cmd = new OleDbCommand("select * from invoice where (item_code =@item_code) And (in_no = @no) ", connection);
                        cmd.Parameters.AddWithValue("@item_code", sales_retuen_issue.item_code);
                        cmd.Parameters.AddWithValue("@no", no);
                        try
                        {
                            connection.Close();
                            connection.Open();
                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                Double dis = ((Convert.ToDouble(rdr["disc"]) / 100) * Convert.ToDouble(rdr["price"])) * (Convert.ToDouble(rdr["qty"]));
                                Double amount = (Convert.ToDouble(rdr["qty"])) * Convert.ToDouble(rdr["price"]);
                                dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["price"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["disc"]), Convert.ToString(dis),Convert.ToString(amount));
                            
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
                    else
                    { 
                    //tax invoice code
                        OleDbDataReader rdr = null;
                        OleDbCommand cmd = new OleDbCommand("select * from tax_invoice where (item_code =@item_code) AND (in_no =@no) ", connection);
                        cmd.Parameters.AddWithValue("@item_code", sales_retuen_issue.item_code);
                        cmd.Parameters.AddWithValue("@no", no);

                        try
                        {
                            connection.Close();
                            connection.Open();
                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                Double dis = ((Convert.ToDouble(rdr["disc"]) / 100) * Convert.ToDouble(rdr["price"])) * (Convert.ToDouble(rdr["qty"]));
                                Double amount = (Convert.ToDouble(rdr["qty"])) * Convert.ToDouble(rdr["price"]);
                                dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["price"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["disc"]), Convert.ToString(dis),Convert.ToString(amount));
                                
                                         
                            
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
                    
                    double total = 0.000;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        total += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                    }
                    textBox5.Text = total.ToString();

                    double disc = 0.000;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        disc += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    }
                    textBox6.Text = disc.ToString();

                    textBox7.Text = Convert.ToString(Math.Round(total - disc));
                }

            }
            




        }

        //delete code
        private void button7_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            //DataGridViewRow row = dataGridView1.SelectedRows[0];
            if (Convert.ToInt32(textBox8.Text) == 0)
            {
                if (selectedRowCount == 0 || selectedRowCount > 0)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    }
                }

            }
            else
            {

                MessageBox.Show("You Can not Access.");
            }
        }

        //combobox
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.Text == "Invoice")
            {
                try
                {
                    type = comboBox2.Text;
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "select * from in_main";
                    command.CommandText = query;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = "in_no";
                    comboBox3.ValueMember = "in_no";
                    connection.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                try
                {
                    type = comboBox2.Text;
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "select * from tax_main";
                    command.CommandText = query;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = "in_no";
                    comboBox3.ValueMember = "in_no";
                    connection.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        //cell end edit
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[7].Value = Convert.ToString(Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value));
            dataGridView1.Rows[e.RowIndex].Cells[6].Value = Convert.ToString((((Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[5].Value) / 100) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value)));

            double total = 0.000;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                total += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
            }
            textBox5.Text = total.ToString();

            double disc = 0.000;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                disc += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox6.Text = disc.ToString();

            textBox7.Text = Convert.ToString(Math.Round(total - disc));
        
        }

        private void gridview()
        {
            dataGridView2.Rows.Clear();
        }

        //delete button code
        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView2.Rows[selectedrow];
            DataGridViewRow row = dataGridView2.Rows[selectedrow];
            string cf = row.Cells[0].Value.ToString();
            int cd = Convert.ToInt32(cf);
            if (cd > 0)
            {

                connection.Open();
                //dwelete from in_main
                OleDbCommand cmdd = new OleDbCommand("Delete from main_sales_return where ID =@item_code", connection);
                cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                cmdd.ExecuteNonQuery();
                //delete from invoice
                OleDbCommand cmd = new OleDbCommand("Delete from sales_return where n_no =@item_code", connection);
                cmd.Parameters.AddWithValue("@item_code", row.Cells[1].Value);
                cmd.ExecuteNonQuery();
                connection.Close();
                gridview();
                grid();
                resetform();
                MessageBox.Show("Data Deleted");
            }
        }
        //new button code
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox8.Text = "0";
            checkBox1.Checked = false;
            textBox6.Text = textBox4.Text = textBox2.Text = textBox7.Text = textBox8.Text = textBox5.Text = textBox3.Text = textBox8.Text = null;
            comboBox1.DataSource = null;
            comboBox2.DataSource = null;
            comboBox3.DataSource = null;
           
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != null)
            {
                if (comboBox2.Text == "Invoice")
                {
                   
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from in_main where (in_no = @id)", connection);
                    cmd.Parameters.AddWithValue("@id", comboBox3.Text);
                    try
                    {
                        connection.Close();
                        connection.Open();
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            textBox3.Text = Convert.ToString(rdr["amount"]);
                            dateTimePicker2.Text = Convert.ToString(rdr["in_date"]);

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

                else
                {
                   
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from tax_main where  (in_no = @id)", connection);
                    cmd.Parameters.AddWithValue("@id", comboBox3.Text);
                    try
                    {
                        connection.Close();
                        connection.Open();
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            textBox3.Text = Convert.ToString(rdr["amount"]);
                            dateTimePicker2.Text = Convert.ToString(rdr["in_date"]);

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
        }

        private void customer()
        {
            try
            {
                
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from customer where (o_details ='Active')";
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "c_name";
                comboBox1.ValueMember = "ID";
                connection.Close();
            }

            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            customer();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from customer where  (ID = @id) and (o_details ='Active')", connection);
            cmd.Parameters.AddWithValue("@id", comboBox1.SelectedValue.ToString());
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    textBox2.Text = Convert.ToString(rdr["b_city"]);


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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void resetform()
        {
            dataGridView1.Rows.Clear();
            textBox8.Text = "0";
            checkBox1.Checked = false;
            textBox6.Text = textBox4.Text = textBox2.Text = textBox7.Text = textBox8.Text = textBox5.Text = textBox3.Text = textBox8.Text = null;
            comboBox1.DataSource = null;
            comboBox2.DataSource = null;
            comboBox3.DataSource = null;
           
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
           
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView2.Rows[selectedrow];
            DataGridViewRow row = dataGridView2.Rows[selectedrow];
           
           sales_return_print.c_name= row.Cells[3].Value.ToString();;
           sales_return_print.n_no = row.Cells[1].Value.ToString();
           
            sales_return_print tr = new sales_return_print();
            tr.ShowDialog();
        }

        private void sales_return_Load(object sender, EventArgs e)
        {
            getid();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_sales_return where c_name like '" + textBox9.Text + "%'", connection);

            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["n_no"]), Convert.ToString(rdr["n_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["type"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["total"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["stock_add"]), Convert.ToString(rdr["gen_inv"]));
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


    }
}
