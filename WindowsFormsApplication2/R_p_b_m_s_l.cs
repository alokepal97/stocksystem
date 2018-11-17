using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    public partial class R_p_b_m_s_l : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public R_p_b_m_s_l()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            gridview();
        }
        int selectedRow = 0;

        public void gridview()
        {
            if (i_b_m_s.item_code.Length > 0)
            {
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from item where item_code =@item_code", connection);
                cmd.Parameters.AddWithValue("@item_code", i_b_m_s.item_code);
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
                        dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]), i_b_m_s.current_stock, Convert.ToString(rdr["reorder_quantity"]), Convert.ToString(rdr["unit"]), Convert.ToString(rdr["purchase_r"]), Convert.ToString(rdr["discount_r"]), Convert.ToString(rdr["default_supplier"]), Convert.ToString(rdr["cgst"]), Convert.ToString(rdr["sgst"]));
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
        private void button1_Click(object sender, EventArgs e)
        {
            string total = Convert.ToString(Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value) * Convert.ToDouble(dataGridView1.Rows[0].Cells[5].Value));

            try
            {
                get_id order = new get_id();
                order.taxinvoice();
                string or = Convert.ToString(get_id.p_order_no);
                string refe = Convert.ToString(get_id.p_orderref_no);
                string command = "insert into p_order(or_no, or_date, ref_no, deli_date, supplier_name,item_code, item_name, unit, qty, purchase_price, dis_on_p, cgst, sgst, total_amount, status,amount) values(@or_no,@or_date,@ref_no,@deli_date,@supplier,@item_code,@item_name,@unit,@qty,@purchase_price,@dis_on_p,@cgst,@sgst,@total_amount,@status,@amount) ";
                OleDbCommand cmdd = new OleDbCommand(command, connection);
                cmdd.Parameters.AddWithValue("@or_no", or);
                cmdd.Parameters.AddWithValue("@or_date", DateTime.Now.ToShortDateString());
                cmdd.Parameters.AddWithValue("@ref_no", refe);
                cmdd.Parameters.AddWithValue("@deli_date", DateTime.Now.ToShortDateString());
                cmdd.Parameters.AddWithValue("@supplier", dataGridView1.Rows[0].Cells[7].Value);
                cmdd.Parameters.AddWithValue("@item_code", dataGridView1.Rows[0].Cells[0].Value);
                cmdd.Parameters.AddWithValue("@item_name", dataGridView1.Rows[0].Cells[1].Value);
                cmdd.Parameters.AddWithValue("@unit", dataGridView1.Rows[0].Cells[4].Value);
                cmdd.Parameters.AddWithValue("@qty", dataGridView1.Rows[0].Cells[3].Value);
                cmdd.Parameters.AddWithValue("@purchase_price", dataGridView1.Rows[0].Cells[5].Value);
                cmdd.Parameters.AddWithValue("@dis_on_p", dataGridView1.Rows[0].Cells[6].Value);
                cmdd.Parameters.AddWithValue("@cgst", dataGridView1.Rows[0].Cells[8].Value);
                cmdd.Parameters.AddWithValue("@sgst", dataGridView1.Rows[0].Cells[9].Value);
                cmdd.Parameters.AddWithValue("@total_amount", total);
                cmdd.Parameters.AddWithValue("@status", "pending");
                cmdd.Parameters.AddWithValue("@amount", "Due");
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                cmdd.ExecuteNonQuery();

                string comman = "insert into purchase_main(p_no, p_date, d_date, s_name, amount, status) values(@or_no,@or_date,@deli_date,@supplier,@amount,@status) ";
                OleDbCommand cmd = new OleDbCommand(comman, connection);
                cmd.Parameters.AddWithValue("@or_no", or);
                cmd.Parameters.AddWithValue("@or_date", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@deli_date", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@supplier", dataGridView1.Rows[0].Cells[7].Value);
                cmd.Parameters.AddWithValue("@amount", total);
                cmd.Parameters.AddWithValue("@status", "pending");
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Please check your order details");

                //update order id and ref no by 1
                int order_no = 0;
                int reference_no = 0;
                int idd = 1;
                order_no = get_id.p_order_no + 1;
                reference_no = get_id.p_orderref_no + 1;
                try
                {
                    OleDbCommand command1 = new OleDbCommand(@"UPDATE get_id
                                                    SET p_order_no = @p_order_no,
                                                        p_orderref_no = @p_orderref_no
                                                    WHERE ID = " + idd + "", connection);

                    command1.Parameters.AddWithValue("@p_order_no", order_no);
                    command1.Parameters.AddWithValue("@p_orderref_no", reference_no);
                    command1.ExecuteNonQuery();

                }
                catch (Exception a)
                {
                    MessageBox.Show("" + a);
                }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
