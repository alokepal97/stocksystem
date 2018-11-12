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
    public partial class sales_order_details : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_order_details()
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
                sql = "SELECT  order_no, order_date, ref_no, ref_date, delivery_date, c_name, c_add, c_city, c_zip, c_state, c_country, c_contact, item_code, item_name, qty, unit, price, disc, disc_amount, total, disamount, cgst, cgst_amt, sgst, sgst_amt, notes FROM sales_order WHERE or_date BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "'";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                
                xlWorkSheet.Cells[1, 1] = "Order No";
                xlWorkSheet.Cells[1, 2] = "Order Date";
                xlWorkSheet.Cells[1, 3] = "Reference No";
                xlWorkSheet.Cells[1, 4] = "Reference Date";
                xlWorkSheet.Cells[1, 5] = "Delivery Date";
                xlWorkSheet.Cells[1, 6] = "Customer Name";
                xlWorkSheet.Cells[1, 7] = "Billing Address";
                xlWorkSheet.Cells[1, 8] = "Billing City";
                xlWorkSheet.Cells[1, 9] = "Billing Zip Code";
                xlWorkSheet.Cells[1, 10] = "Billing State";
                xlWorkSheet.Cells[1, 11] = "Billing Country";
                xlWorkSheet.Cells[1, 12] = "Billing Contact";
                xlWorkSheet.Cells[1, 13] = "Item Code";
                xlWorkSheet.Cells[1, 14] = "Item Name";
                xlWorkSheet.Cells[1, 15] = "Quantity";
                xlWorkSheet.Cells[1, 16] = "Unit";
                xlWorkSheet.Cells[1, 17] = "Price";
                xlWorkSheet.Cells[1, 18] = "Discount(%)";
                xlWorkSheet.Cells[1, 19] = "Discount Amount";
                xlWorkSheet.Cells[1, 20] = "Total Amount without Discount";
                xlWorkSheet.Cells[1, 21] = "Total Amount with Discount";
                xlWorkSheet.Cells[1, 22] = "Cgst(%)";
                xlWorkSheet.Cells[1, 23] = "Cgst Amount";
                xlWorkSheet.Cells[1, 24] = "Sgst(%)";
                xlWorkSheet.Cells[1, 25] = "Sgst Amount";
                xlWorkSheet.Cells[1, 26] = "Notes";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Sales ORder Details Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Sales ORder Details Report Report.xls");
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
