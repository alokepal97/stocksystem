﻿using System;
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
    public partial class sale_item : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sale_item()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            grid();
        }
        public static string item_code = "";
        int selectedRow = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Close();
        }

        private void grid()
        {

            try
            {

                connection.Open();//select item_code,item_name from stock where (receive_qty > 0) AND (item_name <> ' ') ORDER BY id
                OleDbDataReader rdr = null; 
                OleDbCommand cmd = new OleDbCommand("select item.item_code, item.item_name from(item INNER JOIN stock ON item.item_code = stock.item_code) where (stock.receive_qty > stock.min_stock) AND (stock.item_name <> ' ') and (item.item_status='Active') ORDER BY stock.id", connection);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                }

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
            DataGridViewRow newDataRow = dataGridView1.Rows[selectedRow];
            DataGridViewRow row = dataGridView1.Rows[selectedRow];


            item_code = row.Cells[0].Value.ToString();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                connection.Open();
                OleDbDataReader rdr = null; 
                 OleDbCommand cmd = new OleDbCommand("select item.item_code, item.item_name from(item INNER JOIN stock ON item.item_code = stock.item_code) where (stock.receive_qty > stock.min_stock) and (item.item_Name like '" + textBox1.Text + "%') and (stock.item_name <> ' ') and(item.item_status = 'Active') ORDER BY stock.id", connection);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(Convert.ToString(rdr["item_code"]), Convert.ToString(rdr["item_Name"]));

                }

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






    }

}
