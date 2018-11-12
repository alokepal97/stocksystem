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
    public partial class select_no : Form
    {
        public select_no()
        {
            InitializeComponent();
        }
        public static string tbl="";
        private void button1_Click(object sender, EventArgs e)
        {
            tbl = "invoice";
            this.Hide();
            invoice_no ts = new invoice_no();
            ts.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbl = "tax_invoice";
            this.Hide();
            invoice_no ts = new invoice_no();
            ts.ShowDialog();
        }
    }
}
