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
    public partial class stock_receipt_print : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        ReportDocument tes = new ReportDocument();

        public stock_receipt_print()
        {
            InitializeComponent();
            connection con = new connection();
            connection.ConnectionString = con.ConnectionString;
        }
        public static string re_no = "";
        public static string c_name = "";

        private void stock_receipt_print_Load(object sender, EventArgs e)
        {
            try
            {
                tes.Load(System.Windows.Forms.Application.StartupPath + "\\Report\\stock_receipt.rpt");
            }
            catch (Exception t)
            { 
            MessageBox.Show(""+t);
            }
            try
            {
                
                connection.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter("select item_code,item_name,receive_qty,unit from stock_receipt where (receipt_no ='" + re_no + "')", connection);
                DataSet ds = new DataSet();
                sda.Fill(ds, "stock_r_entry");
                tes.SetDataSource(ds);
                crystalReportViewer1.ReportSource = tes;
                connection.Close();
            }
            catch (Exception o)
            {
                MessageBox.Show("" + o);
            }

            //customer fetch and display
            OleDbDataReader rddr = null;
            string comma = "SELECT s_name, b_add, b_city, b_zip, b_state, b_country FROM supplier WHERE(s_name = @Cust_id) ";
            OleDbCommand cm = new OleDbCommand(comma, connection);
            cm.Parameters.AddWithValue("@Cust_id", c_name);
            try
            {
                connection.Close();
                connection.Open();
                rddr = cm.ExecuteReader();
                if (rddr.Read())
                {
                    // tes.SetParameterValue("or_ref", rddr["ref_no"].ToString());
                    tes.SetParameterValue("name", rddr["s_name"].ToString());
                    tes.SetParameterValue("address", rddr["b_add"].ToString());
                    tes.SetParameterValue("city", rddr["b_city"].ToString());
                    tes.SetParameterValue("zip", rddr["b_zip"].ToString());
                    tes.SetParameterValue("state", rddr["b_state"].ToString());
                    tes.SetParameterValue("country", rddr["b_country"].ToString());
                    crystalReportViewer1.ReportSource = tes;
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }

            OleDbDataReader rddd = null;
            string commm = "SELECT * FROM main_receipt WHERE(re_no = @Cust_id) ";
            OleDbCommand cmmmh = new OleDbCommand(commm, connection);
            cmmmh.Parameters.AddWithValue("@Cust_id", re_no);
            try
            {
                connection.Close();
                connection.Open();
                rddd = cmmmh.ExecuteReader();
                if (rddd.Read())
                {
                    tes.SetParameterValue("in_no", rddd["re_no"].ToString());
                    tes.SetParameterValue("in_date", rddd["re_date"].ToString());
                    tes.SetParameterValue("or_no", rddd["ref_no"].ToString());
                    tes.SetParameterValue("or_date", rddd["ref_date"].ToString());

                    this.crystalReportViewer1.ReportSource = tes;
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("" + p);
            }

            //OleDbDataReader rddd1 = null;
            //string commm1 = "SELECT net_amount FROM main_receipt WHERE(re_no = @Cust_id) ";
            //OleDbCommand cmmmh1 = new OleDbCommand(commm1, connection);
            //cmmmh1.Parameters.AddWithValue("@Cust_id", re_no);
            //try
            //{
            //    connection.Close();
            //    connection.Open();
            //    rddd1 = cmmmh1.ExecuteReader();
            //    if (rddd1.Read())
            //    {
            //        tes.SetParameterValue("net_amount", rddd1["net_amount"].ToString());
            //        this.crystalReportViewer1.ReportSource = tes;
            //    }
            //}
            //catch (Exception p)
            //{
            //    MessageBox.Show("" + p);
            //}

        }

    }
}
