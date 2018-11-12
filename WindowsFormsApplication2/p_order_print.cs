using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WindowsFormsApplication2
{
    public partial class p_order_print : Form
    {
        sales__dataset dblayer = new sales__dataset();
        ReportDocument cryrpt = new ReportDocument();
        private OleDbConnection connection = new OleDbConnection();
        public p_order_print()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
        }
       

        private void p_order_print_Load(object sender, System.EventArgs e)
        {
            try
            {
                cryrpt.Load(System.Windows.Forms.Application.StartupPath + "\\Report\\p_order.rpt");
            }//List<tax_invoice_file> _List = new List<tax_invoice_file>();
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }
            try
            {
               
                OleDbDataAdapter sda = new OleDbDataAdapter("select item_code,item_name,unit,qty,purchase_price,total_amount from p_order where(or_no = '" + p_order.pt_no + "')", connection);
                DataSet dsd = new DataSet();
                sda.Fill(dsd, "p_order");
                cryrpt.SetDataSource(dsd);
                crystalReportViewer1.ReportSource = cryrpt;
                crystalReportViewer1.Refresh();
                connection.Close();
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }


         
              OleDbDataReader rdr = null;
              string command = "SELECT s_name, b_add, b_city, b_zip, b_state, b_country FROM supplier WHERE  (s_name = @Cust_id) ";
              OleDbCommand cmdd = new OleDbCommand(command, connection);
              cmdd.Parameters.AddWithValue("@Cust_id", p_order.supplier_name);
              try
              {
                  connection.Open();
                  rdr = cmdd.ExecuteReader();
                  if (rdr.Read())
                  {
                cryrpt.SetParameterValue("name", rdr["s_name"].ToString());
                cryrpt.SetParameterValue("address", rdr["b_add"].ToString());
                cryrpt.SetParameterValue("city", rdr["b_city"].ToString());
                cryrpt.SetParameterValue("zip", rdr["b_zip"].ToString());
                cryrpt.SetParameterValue("state", rdr["b_state"].ToString());
                cryrpt.SetParameterValue("country", rdr["b_country"].ToString());
                crystalReportViewer1.ReportSource = cryrpt;
               
                connection.Close();
                   
                 }
              }
              catch (Exception u)
              {
                  MessageBox.Show("" + u);
              }

              //OleDbDataReader rddr = null;
              //string comma = "SELECT * FROM p_order WHERE(or_no = @Cust_id) ";
              //OleDbCommand cm = new OleDbCommand(comma, connection);
              //cm.Parameters.AddWithValue("@Cust_id", p_order.pt_no);
              //try
              //{
              //    connection.Open();
              //    rddr = cm.ExecuteReader();
              //    if (rddr.Read())
              //    {
              //        cryrpt.SetParameterValue("or_ref", rddr["ref_no"].ToString());
              //        crystalReportViewer1.ReportSource = cryrpt;
              //    }
              //}
              //catch (Exception p)
              //{ 
              //}




            //main purchase 
              DataSet ds4 = dblayer.p_order(p_order.pt_no);
              foreach (DataRow dr in ds4.Tables[0].Rows)
              {


                  cryrpt.SetParameterValue("or_no", dr["p_no"].ToString());
                  cryrpt.SetParameterValue("or_date", dr["p_date"].ToString());
                  cryrpt.SetParameterValue("in_date", dr["d_date"].ToString());
                  cryrpt.SetParameterValue("grand_total", dr["amount"].ToString());
                 
                
                  crystalReportViewer1.ReportSource = cryrpt;

              }

           
              
          }
          



        }
    }

