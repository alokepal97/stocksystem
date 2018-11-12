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
    public partial class sales_return_details : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public sales_return_details()
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
                sql = "SELECT n_no, n_date, c_name, city, type, in_no, in_date, invoice_amount, item_code, item_name, item_price, r_qty, unit, disc, disc_amt, r_amt, status, net_amount, notes FROM sales_return WHERE n_date BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "'";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Note No";
                xlWorkSheet.Cells[1, 2] = "Note Date";
                xlWorkSheet.Cells[1, 3] = "Customer Name";
                xlWorkSheet.Cells[1, 4] = "City";
                xlWorkSheet.Cells[1, 5] = "Type";
                xlWorkSheet.Cells[1, 6] = "Invoice No";
                xlWorkSheet.Cells[1, 7] = "Invoice Date";
                xlWorkSheet.Cells[1, 8] = "Invoice Amount";
                xlWorkSheet.Cells[1, 9] = "Item Code";
                xlWorkSheet.Cells[1, 10] = "Item Name";
                xlWorkSheet.Cells[1, 11] = "Item Price";
                xlWorkSheet.Cells[1, 12] = "Return Quantity";
                xlWorkSheet.Cells[1, 13] = "Unit";
                xlWorkSheet.Cells[1, 14] = "Discount";
                xlWorkSheet.Cells[1, 15] = "Discount Amount"; 
                xlWorkSheet.Cells[1, 16] = "Return Amount";
                xlWorkSheet.Cells[1, 17] = "Status";
                xlWorkSheet.Cells[1, 18] = "Net Amount";
                xlWorkSheet.Cells[1, 19] = "Notes";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Sales Return Details Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Sales Return Details Report.xls");
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
