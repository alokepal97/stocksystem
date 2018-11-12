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
    public partial class payment_due_export : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public payment_due_export()
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
                sql = "select re_no, re_date, c_name, payment_type, invoice_type, payment_mode, ref_no, ref_date, in_no, in_date, total_amount, due_amount, receive_amount, notes, total_receive from payment_receipt where (due_amount <> '0')";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Receipt No";
                xlWorkSheet.Cells[1, 2] = "Receipt Date";
                xlWorkSheet.Cells[1, 3] = "Customer Name";
                xlWorkSheet.Cells[1, 4] = "Payment Type";
                xlWorkSheet.Cells[1, 5] = "Invoice Type";
                xlWorkSheet.Cells[1, 6] = "Payment Mode";
                xlWorkSheet.Cells[1, 7] = "Reference No";
                xlWorkSheet.Cells[1, 8] = "Reference Date";
                xlWorkSheet.Cells[1, 9] = "Invoice No";
                xlWorkSheet.Cells[1, 10] = "Invoice Date";
                xlWorkSheet.Cells[1, 11] = "Total Amount";
                xlWorkSheet.Cells[1, 12] = "Due Amount";
                xlWorkSheet.Cells[1, 13] = "Receive Amount";
                xlWorkSheet.Cells[1, 14] = "Notes";
                xlWorkSheet.Cells[1, 15] = "Total Receive Amount";
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Payment Due Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Payment Due Report.xls");
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