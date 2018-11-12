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
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class invoice_print : Form
    {

        private OleDbConnection connection = new OleDbConnection();
        ReportDocument cryrpt = new ReportDocument();

        public invoice_print()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;

        }
        
       
        public static string in_no = "";
        public static string c_name = "";

          sales__dataset dblayer = new sales__dataset();
        private void invoice_print_Load(object sender, EventArgs e)
        {

            try
            {
                cryrpt.Load(System.Windows.Forms.Application.StartupPath + "\\Report\\invoice.rpt");
            }
            catch (Exception r)
            {
                MessageBox.Show("" + r);
            }
        //    List<sales__invoice_setcs> _List = new List<sales__invoice_setcs>();


            try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter("select item_code,item_name,qty,unit,price,disc,disamount from invoice where(in_no = '" + in_no + "')", connection);
                DataSet ds = new DataSet();
                sda.Fill(ds, "invoice_p");
                cryrpt.SetDataSource(ds);
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
                try
                {
                    cryrpt.SetParameterValue("name", dr["C_name"].ToString());
                    cryrpt.SetParameterValue("address", dr["b_add"].ToString());
                    cryrpt.SetParameterValue("city", dr["b_city"].ToString());
                    cryrpt.SetParameterValue("zip", dr["b_zip"].ToString());
                    cryrpt.SetParameterValue("state", dr["b_state"].ToString());
                    cryrpt.SetParameterValue("country", dr["b_country"].ToString());

                crystalReportViewer1.ReportSource = cryrpt;
                }
                catch
                {
                    MessageBox.Show("Try Again Later");
                }

            }

            DataSet ds2 = dblayer.Invoice_main();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                try
                {
                    cryrpt.SetParameterValue("in_no", dr["in_no"].ToString());
                    cryrpt.SetParameterValue("in_date", dr["in_date"].ToString());
                    cryrpt.SetParameterValue("or_no", dr["or_no"].ToString());
                    cryrpt.SetParameterValue("or_date", dr["or_date"].ToString());
                    cryrpt.SetParameterValue("grand_total", dr["amount"].ToString());
                    crystalReportViewer1.ReportSource = cryrpt;
                }
                catch
                {
                    MessageBox.Show("Try Again Later");
                }
            }

           }
        }
    }

