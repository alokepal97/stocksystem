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
    public partial class Form1 : Form
   
    {
        public static string UserID;
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            textbox();
        }

        private void textbox()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "select * from login where username = 'admin'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txtPassword.Text = Convert.ToString(reader["password"]);
                }
            }
            catch (Exception y)
            {
                MessageBox.Show("" + y);
            }
            finally
            {
                connection.Close();
            }

        }
       private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                label4.Text = "connection Sucessful";
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        
        private void btnOkay_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "select * from login where username = '" + txtusername.Text + "' and password = '" + txtPassword.Text + "'";
            OleDbDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
                //count increment
            }
            if(count==1)
            {
                Form1.UserID = txtusername.Text;
                connection.Close();
                connection.Dispose();
                this.Hide();
                Form_Home fm = new Form_Home();
                fm.ShowDialog();
            }
            else if (count > 1)
            {
                MessageBox.Show("Duplicate User and password ");
            }
            else
            {
                MessageBox.Show("Invalid User and password ");
            }
            connection.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
