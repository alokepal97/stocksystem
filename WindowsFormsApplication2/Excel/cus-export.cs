using System;
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
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
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
                sql = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country, b_contact, b_ph1, b_ph2, b_fax, b_email, tax_no, cst_no, vendor_code, o_details, notes, pan_no,ser_tax_no FROM customer";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Customer Name";
                xlWorkSheet.Cells[1, 2] = "Billing Address";
                xlWorkSheet.Cells[1, 3] = "Billing City";
                xlWorkSheet.Cells[1, 4] = "Billing Zip Code";
                xlWorkSheet.Cells[1, 5] = "Billing State";
                xlWorkSheet.Cells[1, 6] = "Billing Country";
                xlWorkSheet.Cells[1, 7] = "Billing Contact";
                xlWorkSheet.Cells[1, 8] = "Billing Phone No 1";
                xlWorkSheet.Cells[1, 9] = "Billing Phone No 2";
                xlWorkSheet.Cells[1, 10] = "Billing Fax";
                xlWorkSheet.Cells[1, 11] = "Billing Email";
                xlWorkSheet.Cells[1, 12] = "Tax No";
                xlWorkSheet.Cells[1, 13] = "CST No";
                xlWorkSheet.Cells[1, 14] = "Vendor Code";
                xlWorkSheet.Cells[1, 15] = "Other Details";
                xlWorkSheet.Cells[1, 16] = "Notes";
                xlWorkSheet.Cells[1, 17] = "Pan No";
                xlWorkSheet.Cells[1, 18] = "Service Tax No";


                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Customer Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Customer Report.xls");
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