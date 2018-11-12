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
    public partial class supplier : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public supplier()
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
                sql = "SELECT s_name, s_code, b_add, b_city, b_zip, b_state, b_country, b_contact, b_ph1, b_ph2, b_fax, b_email, p_terms, gst_no FROM supplier";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Supplier Name";
                xlWorkSheet.Cells[1, 2] = "Supplier Code";
                xlWorkSheet.Cells[1, 3] = "Billing Address";
                xlWorkSheet.Cells[1, 4] = "Billing City";
                xlWorkSheet.Cells[1, 5] = "Billing Zip Code";
                xlWorkSheet.Cells[1, 6] = "Billing State";
                xlWorkSheet.Cells[1, 7] = "Billing Country";
                xlWorkSheet.Cells[1, 8] = "Billing Contact";
                xlWorkSheet.Cells[1, 9] = "Billing Phone No 1";
                xlWorkSheet.Cells[1, 10] = "Billing Phone No 2";
                xlWorkSheet.Cells[1, 11] = "Billing Fax";
                xlWorkSheet.Cells[1, 12] = "Billing Email";
                xlWorkSheet.Cells[1, 13] = "Payment Terms";
                xlWorkSheet.Cells[1, 14] = "GST No";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Supplier Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Supplier Report.xls");
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