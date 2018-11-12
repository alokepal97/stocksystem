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
    public partial class sales_order_print : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        public sales_order_print()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;

        }
        public static string or_no = "";
        public static string c_name = "";


        sales_order_dataset dblayer = new sales_order_dataset();


        private void sales_order_print_Load(object sender, EventArgs e)
        {
            ReportDocument cryrpt = new ReportDocument();
            try
            {
                cryrpt.Load(System.Windows.Forms.Application.StartupPath + "\\Report\\sales_order.rpt");
            }
            catch (Exception q)
            {
                MessageBox.Show("" + q);
            }

            try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter("select item_code,item_name,qty,unit,price,disamount from sales_order where(order_no = '" + or_no + "')", connection);
                DataSet dsd = new DataSet();
                sda.Fill(dsd, "sales_or");
                cryrpt.SetDataSource(dsd);
                crystalReportViewer1.ReportSource = cryrpt;
                crystalReportViewer1.Refresh();
                connection.Close();
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }

         //   List<sales_order_setcs> _List = new List<sales_order_setcs>();
            //customer fetch and display
            OleDbDataReader rddr = null;
            string comma = "SELECT C_name, b_add, b_city, b_zip, b_state, b_country FROM customer WHERE(c_code = @Cust_id) ";
            OleDbCommand cm = new OleDbCommand(comma, connection);
            cm.Parameters.AddWithValue("@Cust_id", c_name);
            try
            {
                connection.Close();
                connection.Open();
                rddr = cm.ExecuteReader();
                if (rddr.Read())
                {
                    cryrpt.SetParameterValue("name", rddr["C_name"].ToString());
                    cryrpt.SetParameterValue("address", rddr["b_add"].ToString());
                    cryrpt.SetParameterValue("city", rddr["b_city"].ToString());
                    cryrpt.SetParameterValue("zip", rddr["b_zip"].ToString());
                    cryrpt.SetParameterValue("state", rddr["b_state"].ToString());
                    cryrpt.SetParameterValue("country", rddr["b_country"].ToString());
                    crystalReportViewer1.ReportSource = cryrpt;
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }
            
            OleDbDataReader rddd = null;
            string commm = "SELECT * FROM main_sales WHERE(or_no = @Cust_id) ";
            OleDbCommand cmmmh = new OleDbCommand(commm, connection);
            cmmmh.Parameters.AddWithValue("@Cust_id", or_no);
            try
            {
                connection.Close();
                connection.Open();
                rddd = cmmmh.ExecuteReader();
                if (rddd.Read())
                {
                    cryrpt.SetParameterValue("in_no", rddd["or_no"].ToString());
                    cryrpt.SetParameterValue("in_date", rddd["or_date"].ToString());
                    cryrpt.SetParameterValue("or_no", rddd["d_date"].ToString());
                    cryrpt.SetParameterValue("or_date", rddd["ref_no"].ToString());
                    cryrpt.SetParameterValue("grand_total", rddd["net_amount"].ToString());
                    cryrpt.SetParameterValue("total_discount", rddd["total_disc"].ToString());

                   // this.crystalReportViewer1.ReportSource = tes;
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }

            //DataSet ds2 = dblayer.Invoice_main();
            //foreach (DataRow dr in ds2.Tables[0].Rows)
            //{


            //    cryrpt.SetParameterValue("in_no", dr["or_no"].ToString());
            //    cryrpt.SetParameterValue("in_date", dr["or_date"].ToString());
            //    cryrpt.SetParameterValue("or_no", dr["d_date"].ToString());
            //    cryrpt.SetParameterValue("or_date", dr["ref_no"].ToString());
            //    cryrpt.SetParameterValue("grand_total", dr["net_amount"].ToString());
            //    cryrpt.SetParameterValue("total_discount", dr["total_disc"].ToString());


            //}
            crystalReportViewer1.ReportSource = cryrpt;


        }
    }
}
