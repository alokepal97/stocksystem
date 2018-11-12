using System;
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
    class get_id
    {
        private OleDbConnection connection = new OleDbConnection();

                
                 public static int p_order_no = 0;
                 public static int p_orderref_no = 0;
                 public static int stockreceipt_receipt_no = 0;
                 public static int stockreceipt_ref_no = 0;
                 public static int stockreturn_note_no = 0;
                 public static int stockreturn_ref_no = 0;
                 public static int invoice_id = 0;
                 public static int taxinvoice_id = 0;
                 public static int sales_no = 0;
                 public static int sales_ref = 0;
                 public static int sales_return_no = 0;
                 public static int pay_re = 0;
               


        public void taxinvoice()
        {
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
            
               try
            {
                  
                connection.Open();
             
                OleDbDataReader rdr = null;
                OleDbCommand cmd = new OleDbCommand("select * from get_id ", connection);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    sales_no = Convert.ToInt32(rdr["sales_no"]);
                    sales_ref = Convert.ToInt32(rdr["sales_ref"]);
                    p_order_no = Convert.ToInt32(rdr["p_order_no"]);
                    p_orderref_no = Convert.ToInt32(rdr["p_orderref_no"]);
                    stockreceipt_receipt_no = Convert.ToInt32(rdr["stockreceipt_no"]);
                    stockreceipt_ref_no = Convert.ToInt32(rdr["stockreceipt_ref"]);
                    stockreturn_note_no = Convert.ToInt32(rdr["stockreturn_no"]);
                    stockreturn_ref_no = Convert.ToInt32(rdr["stockreturnref"]);
                    invoice_id = Convert.ToInt32(rdr["invoice_id"]);
                    taxinvoice_id = Convert.ToInt32(rdr["invoice_id"]);
                    sales_return_no = Convert.ToInt32(rdr["sales_return_no"]);
                    pay_re = Convert.ToInt32(rdr["pay_re"]);
                   
                }
               

            }
            catch (Exception o)
            {
                MessageBox.Show("getid"+o); 
            }
            finally
            {
                connection.Close();
            }

        }


    }
}
