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
    public partial class stock_r_n : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public stock_r_n()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
           
        }
        string text = "Customer";
        public static string combo1= "";
        public static string combo2 = "";
        public static string supplier = "";
        public static string customer = "";
        public static string delivery = "";
        int selectedrow = 0;

        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox1.Text = Convert.ToString(get_id.stockreturn_note_no);
            textBox2.Text = Convert.ToString(get_id.stockreturn_ref_no);
        }

        private void grid()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_return", connection);

            try
            {
                if(connection.State== ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]),Convert.ToString(rdr["n_no"]), 
                   Convert.ToString(rdr["n_date"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), 
                   Convert.ToString(rdr["type"]), Convert.ToString(rdr["name"]), Convert.ToString(rdr["rec_no"]));
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
           private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = null;
            comboBox3.DataSource = null;

                string combo = comboBox1.Text;
  
                if (combo == text)
                {
                    combo2 = "";
                    
                    label6.Text = "Customer Name";
                    // code for customer sale table
                     combo1 = "Customer Name";
                    try
                    {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                         connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "select * from customer";
                        command.CommandText = query;
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBox2.DataSource = dt;
                        comboBox2.DisplayMember = "C_name";
                        comboBox2.ValueMember = "C_name";

                    }
                    catch (Exception c)
                    {
                        MessageBox.Show("Error"+c);
                    }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                   
                }
                else if (combo == "Supplier")
                {
                    combo1 = "";
                    label6.Text = "Supplier Name";
                    // code for stock receipt page
                    combo2 = "Supplier";
                    try
                    {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "select s_name from supplier";
                        command.CommandText = query;
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBox2.DataSource = dt;
                        comboBox2.DisplayMember = "s_name";
                        comboBox2.ValueMember = "s_name";
                    }
                    catch (Exception y)
                    {
                        MessageBox.Show("Error"+y);
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
                    label6.Text = "Customer Name";
                }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.DataSource = null;
            if (comboBox2.Text != null)
            {
                
                if (comboBox1.Text == "Customer")
                {
                  //!------------------------------------------------------
                    try
                    {
                        customer = comboBox2.Text;
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "select notes from main_sales where c_name=@supplier ";
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@supplier", comboBox2.Text);                        
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBox3.DataSource = dt;
                        comboBox3.DisplayMember = "notes";
                        comboBox3.ValueMember = "notes";

                        
                    }

                    catch (Exception i)
                    {
                        MessageBox.Show("Error" +i);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }

                   
                }
                else if (comboBox1.Text == "Supplier")
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        //string query = "select * from stock_receipt where supplier_name=@supplier group by supplier_name";
                        string query = "select id,notes from stock_receipt ";
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@supplier", comboBox2.Text);
                        //command.Parameters.AddWithValue("@receipt", comboBox2.ValueMember);
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBox3.DataSource = dt;
                        comboBox3.DisplayMember = "notes";
                        comboBox3.ValueMember = "id";
                    }

                    catch (Exception u)
                    {
                        MessageBox.Show("Error" +u);
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
       //select Button
        private void button6_Click(object sender, EventArgs e)
        {
            
            this.Show();
            stockissue co = new stockissue();
            co.ShowDialog();
            Boolean found = false;
                  
                   if (stockissue.item_code.Length>0)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {

                        for (int h = 0; h < dataGridView1.Rows.Count; ++h)
                        {
                            if (dataGridView1.Rows[h].Cells[0].Value.ToString() == stockissue.item_code)
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
                         OleDbCommand cmd = new OleDbCommand("select * from stock where item_code =@item_code ", connection);
                         cmd.Parameters.AddWithValue("@item_code", stockissue.item_code);

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
                                 dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString("0"), Convert.ToString(rdr["unit"]));
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            delivery = comboBox3.Text;
        }

         private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedrow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedrow];
            }
        }

         private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex != -1)
             {
                 selectedrow = e.RowIndex;
                 DataGridViewRow row = dataGridView2.Rows[selectedrow];
             }
         }
        //deletebutton
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
                //DataGridViewRow row = dataGridView1.SelectedRows[0];
                if (Convert.ToInt32(textBox3.Text) == 0)
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (textBox3.Text == "0")
                {
                    try
                    {
                        //insert into in_main
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string com = "insert into main_return(n_no,n_date,ref_no, ref_date,type,name,rec_no)" +
                            " values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "'," +
                            "'" + dateTimePicker3.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "'," +
                            "'" + comboBox2.Text + "') ";
                        OleDbCommand comm = new OleDbCommand(com, connection);
                        comm.ExecuteNonQuery();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {


                            string command = "insert into stock_return(n_no,n_date,ref_no, ref_date,type,name,deli_note," +
                                "d_date,item_code,item_name,r_qty,unit,notes) " +
                                "values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "'," +
                                "'" + dateTimePicker3.Text + "','" + comboBox1.Text + "'," +
                                "'" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Text +
                                "',@item_code,@item_name,@qty,@unit,'" + textBox4.Text + "') ";
                            OleDbCommand cmdd = new OleDbCommand(command, connection);
                            cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                            cmdd.Parameters.AddWithValue("@item_name", row.Cells[1].Value);
                            cmdd.Parameters.AddWithValue("@qty", row.Cells[2].Value);
                            cmdd.Parameters.AddWithValue("@unit", row.Cells[3].Value);
                            cmdd.ExecuteNonQuery();

                            invoice.code = row.Cells[0].Value.ToString();
                            stock_check st = new stock_check();
                            st.getstock();
                            invoice.code = row.Cells[0].Value.ToString();
                            invoice.qty = Convert.ToString(stock_check.stock - Convert.ToDouble(row.Cells[2].Value.ToString()));
                            insert_update_invoice up = new insert_update_invoice();
                            up.update_stock();
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
                        reset();
                        gridview();
                        grid();

                        //update order id and ref no by 1
                        int note_no = 0;
                        int reference_no = 0;
                        int idd = 1;
                        note_no = get_id.stockreturn_note_no + 1;
                        reference_no = get_id.stockreturn_ref_no + 1;
                        try
                        {
                            OleDbCommand command = new OleDbCommand(@"UPDATE get_id
                                                    SET stockreturn_no = @p_order_no,
                                                        stockreturnref = @p_orderref_no
                                                    WHERE ID = " + idd + "", connection);

                            command.Parameters.AddWithValue("@p_order_no", note_no);
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
                        catch (Exception a)
                        {
                            MessageBox.Show("" + a);
                        }
                        textBox1.Text = Convert.ToString(note_no);
                        textBox2.Text = Convert.ToString(reference_no);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Access");
                }
            }
        }
        // modify button
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
            this.tabControl1.SelectedTab = tabPage2;
            DataGridViewRow row = dataGridView2.Rows[selectedrow];
            dataGridView1.Rows.Clear();

            //!-------------------------  ------------------!
            textBox3.Text = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Text = row.Cells[2].Value.ToString();
            textBox2.Text = row.Cells[3].Value.ToString();
            dateTimePicker3.Text = row.Cells[4].Value.ToString();
            comboBox1.Text = row.Cells[5].Value.ToString();
            comboBox2.Text = row.Cells[6].Value.ToString();

            //!--------fetch from database
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from stock_return where  (n_no = @id)", connection);
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
                    comboBox3.Text = Convert.ToString(rdr["deli_note"]);
                    dateTimePicker2.Text = Convert.ToString(rdr["d_date"]);
                    textBox4.Text = Convert.ToString(rdr["notes"]);

                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["r_qty"]), Convert.ToString(rdr["unit"]));

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

        //delete code
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedrow];
                string cf = row.Cells[0].Value.ToString();
                int cd = Convert.ToInt32(cf);
                if (cd > 0)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    //dwelete from in_main
                    OleDbCommand cmdd = new OleDbCommand("Delete from main_return where ID =@ID", connection);
                    cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                    cmdd.ExecuteNonQuery();
                    //delete from invoice
                    OleDbCommand cmd = new OleDbCommand("Delete from stock_return where n_no =@item_code", connection);
                    cmd.Parameters.AddWithValue("@item_code", row.Cells[1].Value);
                    cmd.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    gridview();
                    grid();
                    MessageBox.Show("Data Deleted");
                }
            }
        }
        private void gridview()
        {

            dataGridView2.Rows.Clear();
           
        }
        private void reset()
        {

            dataGridView1.Rows.Clear();
            textBox4.Text = null;
            textBox3.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            comboBox1.Text = comboBox2.Text = comboBox3.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
        }
        //print pdf
        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.Rows[selectedrow];
            stock_return_print.re_no = row.Cells[1].Value.ToString();
            stock_return_print.type = row.Cells[5].Value.ToString();
            stock_return_print.c_name = row.Cells[6].Value.ToString();
            stock_return_print fr = new stock_return_print();
            fr.Show();
        }

        private void stock_r_n_Load(object sender, EventArgs e)
        {
            getid();
        }

    }
}
