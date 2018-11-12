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
    public partial class payment_re : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public payment_re()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            reecipt();
        }
        string receive = "";
        private void reecipt()
        {
            get_id gie = new get_id();
            gie.taxinvoice();
            int text = get_id.pay_re;
            textBox1.Text = Convert.ToString(text);
            textBox2.Text = Convert.ToString(text);
        }
        
        int selectedRow = 0;
        int selectedrow = 0;
        
        private void grid()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from payment_receipt", connection);
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["re_no"]), Convert.ToString(rdr["re_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["payment_type"]), Convert.ToString(rdr["invoice_type"]), Convert.ToString(rdr["payment_mode"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["total_amount"]), Convert.ToString(rdr["due_amount"]), Convert.ToString(rdr["receive_amount"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["total_receive"]), Convert.ToString(rdr["id"]), Convert.ToString(rdr["c_code"]));
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedrow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
            dataGridView2.Rows.Clear();

            //!-------------------------  ------------------!
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox3.Text = row.Cells[0].Value.ToString();
            DateTime date = Convert.ToDateTime(row.Cells[1].Value.ToString());
            dateTimePicker1.Value = date;
            comboBox4.Text = row.Cells[2].Value.ToString();
            comboBox2.Text = row.Cells[3].Value.ToString();
            comboBox5.Text = row.Cells[4].Value.ToString();
            comboBox1.Text = row.Cells[5].Value.ToString();
            textBox2.Text = row.Cells[6].Value.ToString();
            dateTimePicker2.Text = row.Cells[7].Value.ToString();
            dataGridView2.Rows.Add(row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(),Convert.ToString(0));
            textBox4.Text = row.Cells[13].Value.ToString();
            textBox5.Text = row.Cells[14].Value.ToString();
          
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
                  
             
            try
            {
                int id = Convert.ToInt32(textBox3.Text);

                if (id == 0)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        connection.Close();
                        connection.Open();
                        string command = "insert into payment_receipt(re_no,re_date,c_name,payment_type,invoice_type,payment_mode,ref_no,ref_date,in_no,in_date,total_amount,due_amount,receive_amount,notes,total_receive) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox4.Text + "','" + comboBox2.Text + "','" + comboBox5.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker2.Text + "',@in_no,@in_date,@total_amount,@due_amount,@receive_amount,'" + textBox4.Text + "','" + textBox5.Text + "') ";
                        OleDbCommand cmdd = new OleDbCommand(command, connection);

                        cmdd.Parameters.AddWithValue("@in_no", row.Cells[0].Value);
                        cmdd.Parameters.AddWithValue("@in_date", row.Cells[1].Value);
                        cmdd.Parameters.AddWithValue("@total_amount", row.Cells[2].Value);
                        string dueamount = Convert.ToString(Convert.ToDouble(row.Cells[2].Value.ToString()) - Convert.ToDouble(row.Cells[4].Value.ToString()));
                        cmdd.Parameters.AddWithValue("@due_amount", dueamount);
                        cmdd.Parameters.AddWithValue("@receive_amount", row.Cells[4].Value);
                        cmdd.ExecuteNonQuery();
                        
                    }
                    string bla = "";
                    bla = "1";

                    get_id gi = new get_id();
                    gi.taxinvoice();
                    int receipt_no = get_id.pay_re + 1;
                   OleDbCommand command1 = new OleDbCommand(@"UPDATE get_id
                                                    SET pay_re = @City_Name
                                                       
                                                    WHERE ID = " + bla + "", connection);

                command1.Parameters.AddWithValue("@City_Name", receipt_no);
                command1.ExecuteNonQuery();


                }
                else 
               {
                   foreach (DataGridViewRow row in dataGridView2.Rows)
                   {
                       OleDbCommand command = new OleDbCommand(@"UPDATE payment_receipt
                                                    SET payment_mode = @payment_mode,
                                                        due_amount = @due_amount,
                                                        receive_amount = @receive_amount,
                                                        notes = @notes,
                                                        total_receive = @total_receive
                                                      WHERE re_no = @re_no", connection);


                       command.Parameters.AddWithValue("@payment_mode", comboBox1.Text);
                       command.Parameters.AddWithValue("@due_amount", row.Cells[3].Value);
                       command.Parameters.AddWithValue("@receive_amount", row.Cells[4].Value);
                       command.Parameters.AddWithValue("@notes", textBox4.Text);
                       command.Parameters.AddWithValue("@total_receive", textBox5.Text);
                       command.Parameters.AddWithValue("@re_no", textBox1.Text);
                       try
                       {
                           connection.Close();
                           connection.Open();
                       }
                       catch (Exception)
                       {
                           MessageBox.Show("connection error");
                       }
                       try
                       {
                           command.ExecuteNonQuery();
                           MessageBox.Show("Updated");
                           connection.Close();
                           reset();
                           gridview();
                           grid();
                           //   comm.ExecuteNonQuery();

                       }
                       catch (Exception q)
                       {
                           MessageBox.Show("update errorrrrrrrrrrrr" + q);
                       }

                   }
                
                }


            }
            catch (Exception t)
            {
                MessageBox.Show("" + t);
            }
            finally
            {
                connection.Close();
                reset();
                reecipt();
                gridview();
                grid();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            select_no ts = new select_no();
            ts.ShowDialog();

            if (select_no.tbl == "invoice" && invoice_no.in_no !=null)
            {
                
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from in_main where in_no =@in_no And type= @type", connection);
                cmd.Parameters.AddWithValue("@in_no", invoice_no.in_no);
                cmd.Parameters.AddWithValue("@type", "in");
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {

                        dataGridView2.Rows.Clear();
                        double receiveAmount = Convert.ToDouble(Convert.ToString(rdr["amount"])) - Convert.ToDouble(Convert.ToString(rdr["due_amount"]));
                        textBox5.Text = Convert.ToString(receiveAmount);
                        dataGridView2.Rows.Add(Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["due_amount"]), Convert.ToString("0"));
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
            else if (select_no.tbl == "tax_invoice" && invoice_no.in_no != null)
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from invoice where in_no =@in_no And type='tax'", connection);
                cmd.Parameters.AddWithValue("@in_no", invoice_no.in_no);
                try
                {
                    connection.Close();
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {

                        dataGridView2.Rows.Clear();
                        double receiveAmount = Convert.ToDouble(Convert.ToString(rdr["amount"])) - Convert.ToDouble(Convert.ToString(rdr["due_amount"]));
                        textBox5.Text = Convert.ToString(receiveAmount);
                        dataGridView2.Rows.Add(Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["amount"]), Convert.ToString(rdr["due_amount"]), Convert.ToString("0"));
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

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
             if (dataGridView2.Rows[e.RowIndex].Cells[4].Value == null || dataGridView2.Rows[e.RowIndex].Cells[4].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString()))
            {
                dataGridView2.Rows[e.RowIndex].Cells[4].Value ="0";
              
            }
             else 
            {
                if (Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString()) != Convert.ToDouble(receive))
                {

                    double val = 0;
                    val = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[4].Value) + Convert.ToDouble(textBox5.Text);
                  if (val < Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString()) || val == Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString()))
                    {
                        textBox5.Text = Convert.ToString(Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[4].Value) + Convert.ToDouble(textBox5.Text));
                        dataGridView2.Rows[e.RowIndex].Cells[3].Value = Convert.ToString(Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value) - Convert.ToDouble(textBox5.Text));
                    }
                    else {
                      
                        MessageBox.Show("Enter Proper Amount");
                        dataGridView2.Rows[e.RowIndex].Cells[4].Value = "0";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //user click yes
                try
                {
                    DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
                    DataGridViewRow row = dataGridView1.Rows[selectedrow];
                    connection.Open();
                    OleDbCommand cmd = new OleDbCommand("Delete from payment_receipt where id = @re" , connection);
                    cmd.Parameters.AddWithValue("@re", row.Cells[15].Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("DATA Deleted Sucessfully");
                   
                    gridview();
                    grid();
                    reset();
                    connection.Close();
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
                // user clicked no
               
            }
          
        }

        private void gridview()
        {
            dataGridView1.Rows.Clear();
        }
        //new button code
        private void reset()
        {
            textBox4.Text = textBox5.Text = null;
            textBox3.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox1.DataSource = comboBox2.DataSource = comboBox4.DataSource = comboBox5.DataSource = null;
            comboBox1.Text = comboBox2.Text = comboBox4.Text = comboBox5.Text = null;
            dataGridView2.Rows.Clear();
       
        }


        private void button1_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from payment_receipt where c_name like '"+textBox6.Text+"%'", connection);
            try
            {
                connection.Close();
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["re_no"]), Convert.ToString(rdr["re_date"]), Convert.ToString(rdr["c_name"]), Convert.ToString(rdr["payment_type"]), Convert.ToString(rdr["invoice_type"]), Convert.ToString(rdr["payment_mode"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["in_no"]), Convert.ToString(rdr["in_date"]), Convert.ToString(rdr["total_amount"]), Convert.ToString(rdr["due_amount"]), Convert.ToString(rdr["receive_amount"]), Convert.ToString(rdr["notes"]), Convert.ToString(rdr["total_receive"]), Convert.ToString(rdr["id"]));
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
        //print
        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedrow];
            DataGridViewRow row = dataGridView1.Rows[selectedrow];
            if (row.Cells[4].Value.ToString() == "Invoice")
            {
                invoice_print.c_name = row.Cells[16].Value.ToString();
                invoice_print.in_no = row.Cells[8].Value.ToString();


                invoice_print ip = new invoice_print();
                ip.ShowDialog();
            }
            else if (row.Cells[4].Value.ToString() == "Tax-Invoice")
            {
                tax_invoice_print.c_name = row.Cells[2].Value.ToString();
                tax_invoice_print.in_no = row.Cells[8].Value.ToString();


                tax_invoice_print ip = new tax_invoice_print();
                ip.ShowDialog();
            
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

        private void payment_re_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            receive = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[4].Value = "";
        }

       private void combobox2()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select c_name,c_code from in_main";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Customer");
                comboBox4.DisplayMember = "c_name";
                comboBox4.ValueMember = "c_code";
                comboBox4.DataSource = ds.Tables["Customer"];
                connection.Close();

            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
            combobox2();
        }
    }
            
    }

