using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form_Home : Form
    {
        public Form_Home()
        {
            InitializeComponent();
            timer1.Start();
            label2.Text = DateTime.Now.ToLongDateString();
              
        }
         private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CompanyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            company co = new company();
            co.ShowDialog();
        }

        private void taxSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            taxsetup ts = new taxsetup();
            ts.ShowDialog();
        }

        private void userCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemsGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            group gp = new group();
            gp.ShowDialog();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            item it = new item();
            it.ShowDialog();
        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Customer cs = new Customer();
            cs.ShowDialog();
        }

        private void supplierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            supplier sup = new supplier();
            sup.ShowDialog();
        }

        private void cityMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            city ct = new city();
            ct.ShowDialog();
        }

        private void salesPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            sales_p s_p = new sales_p();
            s_p.ShowDialog();
        }

        private void paymentTermsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            payment_re pr = new payment_re();
            pr.ShowDialog();
        }

        private void paymentModesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            paymodes pd = new paymodes();
            pd.ShowDialog();
        }

        private void unitMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            unit un = new unit();
            un.ShowDialog();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            p_order po = new p_order();
            po.ShowDialog();
        }

        private void stockReceiptEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            stock_r_e se = new stock_r_e();
            se.ShowDialog();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void stockReturnNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            stock_r_n s_n = new stock_r_n();
            s_n.ShowDialog();
        }

        private void viewStockInHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            stock_in_hand sh = new stock_in_hand();
            sh.ShowDialog();
        }

        private void viewMSLItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            i_b_m_s b = new i_b_m_s();
            b.ShowDialog();
        }

        private void reorderMSLItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            sales_order so = new sales_order();
            so.ShowDialog();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            invoice voi = new invoice();
            voi.ShowDialog();
        }

        private void taxInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            tax_invoice ti = new tax_invoice();
            ti.ShowDialog();
        }

        private void deliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void salesReturnNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            sales_return sr = new sales_return();
            sr.ShowDialog();
        }

        private void paymentReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            payment_re pr = new payment_re();
            pr.ShowDialog();
        }

        private void dueInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            due_invoice di = new due_invoice();
            di.ShowDialog();
        }

        private void paymentDueListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            payment_due_list pdl = new payment_due_list();
            pdl.ShowDialog();
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
             item mn = new item();
            mn.ShowDialog();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Customer bs = new Customer();
            bs.ShowDialog();
        }

        private void salesOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            sales_order pr = new sales_order();
            pr.ShowDialog();
        }

        private void StockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            invoice sh = new invoice();
            sh.ShowDialog();
        }

        private void taxInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            tax_invoice ti = new tax_invoice();
            ti.ShowDialog();
        }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            payment_re pr = new payment_re();
            pr.ShowDialog();
        }

        private void invoiceDueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            due_invoice di = new due_invoice();
            di.ShowDialog();
        }

        private void SupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            supplier sr = new supplier();
            sr.ShowDialog();
        }

        private void ServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            stock_r_e se = new stock_r_e();
            se.ShowDialog();
        }

        private void LogoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        //private void testToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    test f2 = new test();
        //    f2.ShowDialog();
        //}

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void pUrchaseOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.unit eunit = new Excel.unit();
            eunit.Show();
        }

        private void taxInvoiceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.tax_main etax = new Excel.tax_main();
            etax.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
           // this.label1.Text = DateTime.Now.ToString();
            this.label1.Text = DateTime.Now.ToString("hh:mm:ss");
            
        }

        //  Report
        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.company rcp = new Excel.company();
            rcp.ShowDialog();
        }

        private void customerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.Form1 ecum = new Excel.Form1();
            ecum.ShowDialog();
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.city ecity = new Excel.city();
            ecity.ShowDialog();
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.Sales_person es_person = new Excel.Sales_person();
            es_person.ShowDialog();
        }

        private void itemGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.group_export egroup = new Excel.group_export();
            egroup.ShowDialog();
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.item_export eitem = new Excel.item_export();
            eitem.ShowDialog();
        }

        private void itemUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockReceiptDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.p_order epor = new Excel.p_order();
            epor.ShowDialog();
        }

        private void purchaseOrderDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.purchase_order eporder = new Excel.purchase_order();
            eporder.ShowDialog();
        }

        private void stockReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.receipt er = new Excel.receipt();
            er.ShowDialog();
        }

        private void stockReceiptDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.stock_receipt_details erd = new Excel.stock_receipt_details();
            erd.ShowDialog();

        }

        private void stockReturnDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.stock_return_export erx = new Excel.stock_return_export();
            erx.ShowDialog();
        }

        private void stockReturnDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.sales_return_details erdx = new Excel.sales_return_details();
            erdx.ShowDialog();
        }

        private void stockInHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.stock_in_hand_ est = new Excel.stock_in_hand_();
            est.ShowDialog();
        }

        private void mSLItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.msl_item msl = new Excel.msl_item();
            msl.ShowDialog();
        }

        private void salesOrderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.sales sls = new Excel.sales();
            sls.ShowDialog();
        }

        private void salesOrderDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.sales_order_details slsd = new Excel.sales_order_details();
            slsd.ShowDialog();
        }

        private void invoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.invoice_export invoicex = new Excel.invoice_export();
            invoicex.ShowDialog();
        }

        private void invoiceDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.invoice_details invoicexd = new Excel.invoice_details();
            invoicexd.ShowDialog();
        }

        private void taxInvoiceDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.tax_invoice_details tinvoicex = new Excel.tax_invoice_details();
            tinvoicex.ShowDialog();
        }

        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.main_sales ret = new Excel.main_sales();
            ret.ShowDialog();
        }

        private void salesReturnDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.sales_return_details retd = new Excel.sales_return_details();
            retd.ShowDialog();
        }

        private void paymentReceiptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.payment_receipt_export epcrx = new Excel.payment_receipt_export();
            epcrx.ShowDialog();
        }

        private void dueInvoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.due_invoice_report edue = new Excel.due_invoice_report();
            edue.ShowDialog();
        }

        private void duePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            Excel.payment_due_export epdue = new Excel.payment_due_export();
            epdue.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pass pwd = new change_pass();
            pwd.ShowDialog();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void backupRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void backupRestoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            backup bck = new backup();
            bck.ShowDialog();
        }

        
        

        

       
     
    }
}
