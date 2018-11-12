using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication2
{
    public partial class change_pass : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public change_pass()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
        }
        OleDbCommand com;
        string str;
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            try
            {
                str = "UPDATE login SET [password] = @sno WHERE (ID = 1) AND (password = @name)";

                com = new OleDbCommand(str, connection);

                com.Parameters.AddWithValue("@sno", textBox2.Text);

                com.Parameters.AddWithValue("@name", textBox1.Text);
                int num=com.ExecuteNonQuery();
                if (num > 0)
                {
                    MessageBox.Show("Password change");
                }
                else {
                    MessageBox.Show("Password Mismatch");
                }
                connection.Close();
            }
            catch (Exception y)
            {
                MessageBox.Show("" + y);
            }
        }
    }
}
