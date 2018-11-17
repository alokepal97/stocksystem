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
    public partial class tax_invoice_print : Form
    {
        tax_dataset_print dblayer = new tax_dataset_print();
        ReportDocument cryrpt = new ReportDocument();
        private OleDbConnection connection = new OleDbConnection();
        public tax_invoice_print()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
        }
        public static string in_no = "";
        public static string c_name = "";
         string cgst = "";
            double cgst1 = 0;
            string sgst = "";
            double sgst1 = 0;
            double gst = 0;


       
        private void tax_invoice_print_Load(object sender, EventArgs e)
        {
            try
            {
                cryrpt.Load(System.Windows.Forms.Application.StartupPath + "\\Report\\tax_invoice.rpt");
            }
            catch (Exception t)
            {
                MessageBox.Show("" + t);
            }
           try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter("select item_code,item_name,qty,unit,price,disc,disamount from invoice where(in_no = '" + in_no + "')", connection);
                DataSet dsd = new DataSet();
                sda.Fill(dsd, "invoice_p");
                cryrpt.SetDataSource(dsd);
                crystalReportViewer1.ReportSource = cryrpt;
                crystalReportViewer1.Refresh();
                connection.Close();
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }
            


            DataSet ds3 = dblayer.Customer_info();
            foreach (DataRow dr in ds3.Tables[0].Rows)
            {

                //cryrpt.SetDataSource(_List);
                cryrpt.SetParameterValue("name", dr["C_name"].ToString());
                cryrpt.SetParameterValue("address", dr["b_add"].ToString());
                cryrpt.SetParameterValue("city", dr["b_city"].ToString());
                cryrpt.SetParameterValue("zip", dr["b_zip"].ToString());
                cryrpt.SetParameterValue("state", dr["b_state"].ToString());
                cryrpt.SetParameterValue("country", dr["b_country"].ToString());

                crystalReportViewer1.ReportSource = cryrpt;

            }

           
            DataSet ds = dblayer.Invoice_product();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               
                cgst = dr["cgst_amt"].ToString();
               cgst1 = Convert.ToDouble(cgst) + cgst1;
               sgst = dr["sgst_amt"].ToString();
               sgst1 = Convert.ToDouble(sgst) + sgst1;
               gst = cgst1 + sgst1;
                   
                
            }
            
            DataSet ds2 = dblayer.Invoice_main();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {


                cryrpt.SetParameterValue("in_no", dr["in_no"].ToString());
                cryrpt.SetParameterValue("in_date", dr["in_date"].ToString());
                cryrpt.SetParameterValue("or_no", dr["or_no"].ToString());
                cryrpt.SetParameterValue("or_date", dr["or_date"].ToString());
                cryrpt.SetParameterValue("grand_total", dr["amount"].ToString());
                cryrpt.SetParameterValue("total_gst", gst.ToString());
                crystalReportViewer1.ReportSource = cryrpt;

            }
            DataSet ds4 = dblayer.gst();
            foreach (DataRow dr in ds4.Tables[0].Rows)
            {
                cryrpt.SetParameterValue("gst_no", dr["c_gst"].ToString());
                crystalReportViewer1.ReportSource = cryrpt;

            }

             
        }
    }
}
