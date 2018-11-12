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
    public partial class tax_invoice_details : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public tax_invoice_details()
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
                sql = "SELECT in_no, in_date, order_no, order_date, c_name, b_add, b_city, b_zip, b_state, b_country, s_add, s_city, s_zip, s_state, s_country, sales_person, due_date, contact_name, item_code, item_name, qty, unit, price, disc, disc_amount, total, disamount, cgst, cgst_amt, sgst, sgst_amt, notes, net_amount, receive_amount FROM tax_invoice WHERE in_date BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "' AND (type = 'tax')";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Invoice No";
                xlWorkSheet.Cells[1, 2] = "Invoice Date";
                xlWorkSheet.Cells[1, 3] = "Order No";
                xlWorkSheet.Cells[1, 4] = "Order Date";
                xlWorkSheet.Cells[1, 5] = "Customer Name";
                xlWorkSheet.Cells[1, 6] = "Billing Address";
                xlWorkSheet.Cells[1, 7] = "Billing City";
                xlWorkSheet.Cells[1, 8] = "Billing Zip Code";
                xlWorkSheet.Cells[1, 9] = "Billing State";
                xlWorkSheet.Cells[1, 10] = "Billing Country";
                xlWorkSheet.Cells[1, 11] = "Delivery Address";
                xlWorkSheet.Cells[1, 12] = "Delivery City";
                xlWorkSheet.Cells[1, 13] = "Delivery Zip Code";
                xlWorkSheet.Cells[1, 14] = "Delivery State";
                xlWorkSheet.Cells[1, 15] = "Delivery Country";
                xlWorkSheet.Cells[1, 16] = "Sales Person";
                xlWorkSheet.Cells[1, 17] = "Due Date";
                xlWorkSheet.Cells[1, 18] = "Contact Name";
                xlWorkSheet.Cells[1, 19] = "Item Code";
                xlWorkSheet.Cells[1, 20] = "Item Name";
                xlWorkSheet.Cells[1, 21] = "Quantity";
                xlWorkSheet.Cells[1, 22] = "Unit";
                xlWorkSheet.Cells[1, 23] = "Price";
                xlWorkSheet.Cells[1, 24] = "Discount";
                xlWorkSheet.Cells[1, 25] = "Discount Amount";
                xlWorkSheet.Cells[1, 26] = "Total Amount without Discount";
                xlWorkSheet.Cells[1, 27] = "Total Amount with Discount";
                xlWorkSheet.Cells[1, 28] = "Cgst(%)";
                xlWorkSheet.Cells[1, 29] = "Cgst Amount";
                xlWorkSheet.Cells[1, 30] = "Sgst(%)";
                xlWorkSheet.Cells[1, 31] = "Sgst Amount";
                xlWorkSheet.Cells[1, 32] = "Notes";
                xlWorkSheet.Cells[1, 33] = "Net Amount";
                xlWorkSheet.Cells[1, 34] = "Receive Amount";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Tax Invoice Details Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Tax Invoice Details Report.xls");
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