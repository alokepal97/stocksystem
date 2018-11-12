﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Exce = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApplication2.Excel
{
    public partial class p_order : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public p_order()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string sql = null;

                string data = null;

                int i = 0;

                int j = 0;


                Exce.Application xlApp;

                Exce.Workbook xlWorkBook;

                Exce.Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;

                xlApp = new Exce.Application();

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Exce.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                connection.Open();
                sql = "SELECT p_no, p_date, d_date, s_name, amount, status FROM purchase_main WHERE p_date BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "'";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Purchase No";
                xlWorkSheet.Cells[1, 2] = "Purchase Date";
                xlWorkSheet.Cells[1, 3] = "Delivery Date";
                xlWorkSheet.Cells[1, 4] = "Supplier Name";
                xlWorkSheet.Cells[1, 5] = "Amount";
                xlWorkSheet.Cells[1, 6] = "Status";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Purchase Order Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Purchase Order Report.xls");
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
        }
        private void releaseObject(object obj)
        {

            try
            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;

            }

            catch (Exception ex)
            {

                obj = null;

                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());

            }

            finally
            {

                GC.Collect();

            }

        }
    }
}
