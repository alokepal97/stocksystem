using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class group : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public group()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
        }

        int selectedRow = 0;

        private void grid()
        {
            OleDbDataReader rdr = null;
            OleDbCommand cmd = new OleDbCommand("select * from grou", connection);

            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["ID"]), Convert.ToString(rdr["name"]));
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


        private void newbtn_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Add();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                //DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                if (string.IsNullOrEmpty(dataGridView1.Rows[selectedRow].Cells[0].Value as string))
                {

                }
                else
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    OleDbCommand cmdd = new OleDbCommand("Delete from grou where ID =@ID", connection);
                    cmdd.Parameters.AddWithValue("@ID", row.Cells[0].Value);
                    cmdd.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Data Deleted");
                gridview();
                grid();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                if (dataGridView1.Rows[selectedRow].Cells[0].Value != null)
                {
                    string id = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();

                    //update query
                    OleDbCommand command = new OleDbCommand(@"UPDATE grou
                                                    SET name = @City_Name
                                                       
                                                    WHERE ID = " + id + "", connection);

                    command.Parameters.AddWithValue("@City_Name", dataGridView1.Rows[selectedRow].Cells[1].Value);

                    try
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
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

                        MessageBox.Show("Data Updated");
                        gridview();
                        grid();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("query error");
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
                        //insert query
                        connection.Close();
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Open();
                        string com = "insert into grou(name) values (@item_code) ";
                        OleDbCommand comm = new OleDbCommand(com, connection);
                        comm.Parameters.AddWithValue("@item_code", dataGridView1.Rows[selectedRow].Cells[1].Value);
                        comm.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Data Inserted");
                        gridview();
                        grid();
                    }
                    catch (Exception o)
                    {
                        MessageBox.Show("" + o);
                    }
                }
            }
        }

        private void gridview()
        {
            dataGridView1.Rows.Clear();
        }

    }
}
