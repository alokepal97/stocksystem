using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Reflection;
//using ClosedXML.Excel;
using System.IO;
using Ex = Microsoft.Office.Interop.Excel;

//using Exce = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApplication2.Excel
{
    public partial class item_export : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public item_export()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = null;
            string data = null;
            int i = 0;
            int j = 0;

            Ex.Application xlApp;
            Ex.Workbook xlWorkBook;
            Ex.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Ex.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Ex.Worksheet)xlWorkBook.Worksheets.get_Item(1);

           
            connection.Open();
            sql = "SELECT  item_code,item_name, item_group, unit, price, dis_on_price, default_supplier, item_details FROM item";
            OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);
            xlWorkSheet.Cells[1, 1] = "Item Code";
            xlWorkSheet.Cells[1, 2] = "Item Name";
            xlWorkSheet.Cells[1, 3] = "Item Group";
            xlWorkSheet.Cells[1, 4] = "Unit";
            xlWorkSheet.Cells[1, 5] = "Price";
            xlWorkSheet.Cells[1, 6] = "Discount On Price";
            xlWorkSheet.Cells[1, 7] = "Default Supplier";
            xlWorkSheet.Cells[1, 8] = "Item Details";

          for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                    xlWorkSheet.Cells[i + 2, j + 1] = data;
                }
            }

            xlWorkBook.SaveAs("Item Report.xls", Ex.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Ex.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents Item Report.xls");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
