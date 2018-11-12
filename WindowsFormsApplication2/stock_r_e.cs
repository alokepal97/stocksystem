using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class stock_r_e : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public stock_r_e()
        {
            InitializeComponent();

            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            Gridview();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }
        int oldvalue = 0;
        int newvalue = 0;
        int selectedRow = 0;

        private void getid()
        {
            get_id order = new get_id();
            order.taxinvoice();
            textBox1.Text = Convert.ToString(get_id.stockreceipt_receipt_no);
            textBox2.Text = Convert.ToString(get_id.stockreceipt_ref_no);
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldvalue = Convert.ToInt32(dataGridView1.SelectedCells[2].Value);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            newvalue = Convert.ToInt32(dataGridView1.SelectedCells[2].Value);
            check();
        }


        private void scombo()
        {
            try
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select s_name from supplier";
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "s_name";
                comboBox1.ValueMember = "s_name";
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Gridview()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_receipt", connection);
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
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["re_no"]), Convert.ToString(rdr["re_date"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["or_no"]));
                }
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string combo = comboBox1.Text;
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "select * from purchase_main where (s_name=@supplier) And (status = @Status)";

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@supplier", combo);
                    command.Parameters.AddWithValue("@Status", "Pending");
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "p_no";
                    comboBox2.ValueMember = "p_no";
                }
                catch (Exception y)
                {
                    MessageBox.Show("" + y);
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row1 in dataGridView1.Rows)
            {
                dataGridView1.Rows.Remove(row1);
            }

            if (comboBox2.SelectedText != null)
            {
                try
                {
                    string or = comboBox2.Text;
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from p_order where or_no=@or_no", connection);
                    cmd.Parameters.AddWithValue("@or_no", or);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        textBox4.Text = Convert.ToString(rdr["or_date"]);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]), Convert.ToString(rdr["qty"]), Convert.ToString(rdr["unit"]));
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Something Wrong! Try again later");
                }
                finally
                {
                    if(connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox6.Text);
                if (id == 0)
                {
                    string command1 = "insert into main_receipt( re_no, re_date,ref_no,ref_date, s_name, or_no) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "','" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "') ";
                    OleDbCommand cmdd1 = new OleDbCommand(command1, connection);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    cmdd1.ExecuteNonQuery();
                    //update into purchage order
                    OleDbCommand comman = new OleDbCommand(@"UPDATE purchase_main
                                                    SET status = @Status
                                             WHERE p_no = @or", connection);

                    comman.Parameters.AddWithValue("@Status", "Received");
                    comman.Parameters.AddWithValue("@or", comboBox2.Text);
                    comman.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string command = "insert into stock_receipt( receipt_no, receipt_date, reference_no, reference_date, supplier_name, order_no, order_date, item_code, item_name, receive_qty, unit,notes) values('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "','" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "',@item_code,@item_name,@receive_qty,@unit,'" + textBox5.Text + "') ";

                        OleDbCommand cmdd = new OleDbCommand(command, connection);

                        cmdd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        cmdd.Parameters.AddWithValue("@item_name", row.Cells[1].Value);
                        cmdd.Parameters.AddWithValue("@receive_qty", row.Cells[2].Value);
                        cmdd.Parameters.AddWithValue("@unit", row.Cells[3].Value);
                        cmdd.ExecuteNonQuery();
                        cmdd.Dispose();
                        getdata();//update to stock table for the receive quantity
                        this.tabControl1.SelectedTab = tabPage1;
                        grid();
                        Gridview();
                        ResetForm();
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    //update order id and ref no by 1
                    int receipt_no = 0;
                    int reference_no = 0;
                    int idd = 1;
                    receipt_no = get_id.stockreceipt_receipt_no + 1;
                    reference_no = get_id.stockreceipt_ref_no + 1;
                    try
                    {
                        OleDbCommand command = new OleDbCommand(@"UPDATE get_id
                                                    SET stockreceipt_no = @p_order_no,
                                                        stockreceipt_ref = @p_orderref_no
                                                    WHERE ID = " + idd + "", connection);

                        command.Parameters.AddWithValue("@p_order_no", receipt_no);
                        command.Parameters.AddWithValue("@p_orderref_no", reference_no);
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("" + a);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }

                    textBox1.Text = Convert.ToString(receipt_no);
                    textBox2.Text = Convert.ToString(reference_no);
                }
                else
                {
                    MessageBox.Show("Cannot Access");

                }
            }
            catch (Exception i)
            {
                MessageBox.Show("insert" + i);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
        // !@-----------update into stock table
        private void getdata()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from stock where item_code =@item_code", connection);
                    cmd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    rdr = cmd.ExecuteReader();
                    //  MessageBox.Show("" + row.Cells[0].Value.ToString());
                    if (rdr.Read())
                    {
                        string value = rdr["receive_qty"].ToString();
                        string receive = row.Cells[2].Value.ToString();
                        Decimal s = Convert.ToDecimal(value);
                        Decimal qty = Convert.ToDecimal(receive);
                        s = s + qty;
                        OleDbCommand command = new OleDbCommand(@"UPDATE stock
                                                    SET receive_qty = @City_Name                                                        
                                                    WHERE item_code = @item_code", connection);
                        command.Parameters.AddWithValue("@City_Name", s);
                        command.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        try
                        {
                            command.ExecuteNonQuery();
                            //  MessageBox.Show("DATA UPDATED");
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
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // modify code
            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                string cg = row.Cells[0].Value.ToString();
                int ch = Convert.ToInt32(cg);
                if (ch > 0)
                {
                    this.tabControl1.SelectedTab = tabPage2;
                    textBox6.Text = row.Cells[0].Value.ToString();
                    textBox1.Text = row.Cells[1].Value.ToString();
                    dateTimePicker1.Text = row.Cells[2].Value.ToString();
                    textBox2.Text = row.Cells[3].Value.ToString();
                    dateTimePicker2.Text = row.Cells[4].Value.ToString();
                    comboBox1.Text = row.Cells[5].Value.ToString();
                    comboBox2.Text = row.Cells[6].Value.ToString();

                    //fetch from database


                    OleDbDataReader rdr = null;
                    OleDbCommand cmd = new OleDbCommand("select * from stock_receipt where  (receipt_no = @id)", connection);
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
                            textBox4.Text = Convert.ToString(rdr["order_date"]);
                            dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_name"]), Convert.ToString(rdr["receive_qty"]), Convert.ToString(rdr["unit"]));
                            textBox5.Text = Convert.ToString(rdr["notes"]);
                        }
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


                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                dataGridView1.Rows.Remove(row);
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

        private void check()
        {
            if (newvalue > 0)
            {
                if (newvalue > oldvalue)
                {
                    dataGridView1.SelectedCells[2].Value = Convert.ToInt32(newvalue);
                    dataGridView1.SelectedCells[2].Style.BackColor = Color.Green;
                }
                else
                {
                    dataGridView1.SelectedCells[2].Value = Convert.ToInt32(oldvalue);
                    dataGridView1.SelectedCells[2].Style.BackColor = Color.Red;
                }
            }
        }

        private void update()
        {
            if (newvalue > oldvalue)
            {
                // greater than oldvalue
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        OleDbDataReader rdr = null;
                        OleDbCommand cmd = new OleDbCommand("select * from stock where item_code =@item_code", connection);
                        cmd.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            string value = rdr["receive_qty"].ToString();
                            int var = newvalue - oldvalue;
                            Decimal s = Convert.ToDecimal(value);
                            Decimal qty = Convert.ToDecimal(var);                      
                            s = s + qty;
                            OleDbCommand command = new OleDbCommand(@"UPDATE stock
                                                    SET receive_qty = @City_Name                                                       
                                                    WHERE item_code = @item_code", connection);

                            command.Parameters.AddWithValue("@City_Name", s);
                            command.Parameters.AddWithValue("@item_code", row.Cells[0].Value);
                            try
                            {
                                command.ExecuteNonQuery();
                                //  MessageBox.Show("DATA UPDATED");
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
                }
                catch (Exception o)
                {
                    MessageBox.Show("" + o);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                //delete code
                if (selectedRow != -1)
                {
                    try
                    {
                        DataGridViewRow row = dataGridView2.Rows[selectedRow];
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        
                        OleDbCommand cmdd = new OleDbCommand("Delete from main_receipt where ID = @id", connection);
                        cmdd.Parameters.AddWithValue("@id", row.Cells[0].Value);
                        cmdd.ExecuteNonQuery();

                        OleDbCommand cmdd1 = new OleDbCommand("Delete from stock_receipt where receipt_no = @re_no", connection);
                        cmdd1.Parameters.AddWithValue("@re_no", row.Cells[1].Value);
                        cmdd1.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Try Again Later");
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        grid();
                        Gridview();
                        ResetForm();
                    }
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

        private void ResetForm()
        {

            textBox6.Text = "0";
            comboBox1.DataSource = null;
            comboBox1.Text = comboBox2.Text = null;
            comboBox2.DataSource = null;
            textBox4.Text = textBox5.Text = null;
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();

            try
            {

                dataGridView1.Rows.Clear();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            scombo();
        }

        private void stock_r_e_Load(object sender, EventArgs e)
        {
            getid();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from main_receipt where s_name like '" + textBox3.Text + "%'", connection);
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
                    dataGridView2.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["re_no"]), Convert.ToString(rdr["re_date"]), Convert.ToString(rdr["ref_no"]), Convert.ToString(rdr["ref_date"]), Convert.ToString(rdr["s_name"]), Convert.ToString(rdr["or_no"]));
                }
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            stock_receipt_print.re_no = row.Cells[1].Value.ToString();
            stock_receipt_print.c_name = row.Cells[5].Value.ToString();
            stock_receipt_print tr = new stock_receipt_print();
            tr.Show();
        }
    }
}
