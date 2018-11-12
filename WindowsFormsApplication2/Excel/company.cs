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
    public partial class company : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public company()
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
                sql = "SELECT c_name, s_name, c_add, c_city, c_zip, c_state, c_country, c_ph1, c_ph2, c_fax, c_email, c_website, c_gst, c_pan, c_cin, c_bank FROM company";
                OleDbDataAdapter dscmd = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                xlWorkSheet.Cells[1, 1] = "Company Name";
                xlWorkSheet.Cells[1, 2] = "Short Name";
                xlWorkSheet.Cells[1, 3] = "Company Address";
                xlWorkSheet.Cells[1, 4] = "Company City";
                xlWorkSheet.Cells[1, 5] = "Company Zip Code";
                xlWorkSheet.Cells[1, 6] = "Company State";
                xlWorkSheet.Cells[1, 7] = "Company Country";
                xlWorkSheet.Cells[1, 8] = "Company Phone No 1";
                xlWorkSheet.Cells[1, 9] = "Company Phone No 2";
                xlWorkSheet.Cells[1, 10] = "Company Fax";
                xlWorkSheet.Cells[1, 11] = "Company Email";
                xlWorkSheet.Cells[1, 12] = "Company Website";
                xlWorkSheet.Cells[1, 13] = "Gst No";
                xlWorkSheet.Cells[1, 14] = "Pan No";
                xlWorkSheet.Cells[1, 15] = "Cin No";
                xlWorkSheet.Cells[1, 16] = "Bank";

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                }

                xlWorkBook.SaveAs("Company Report.xls", Exce.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Exce.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);



                MessageBox.Show("Excel file created , you can find the file C:\\Users\\User\\Documents. Company Report.xls");
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