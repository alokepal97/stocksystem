using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication2
{
    public partial class backup : Form
    {
        public backup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CurrentDatabasePath = Environment.CurrentDirectory + @"\stock.accdb";

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {

                string PathtobackUp = fbd.SelectedPath.ToString();
                try
                {
                    File.Copy(CurrentDatabasePath, PathtobackUp + @"\BackUp.bak", true);

                    MessageBox.Show("Back Up SuccessFull! ");
                }
                catch (Exception t)
                {
                    MessageBox.Show("Database Backup Error"+t);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string PathToRestoreDB = Environment.CurrentDirectory + @"\stock.accdb";

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string Filetorestore = ofd.FileName;
                    //// Rename Current Database to .Bak
                    //File.Move(PathToRestoreDB, PathToRestoreDB);
                    ////Restore the Databse From Backup Folder
                    File.Copy(Filetorestore, PathToRestoreDB, true);
                    MessageBox.Show("Restore SuccessFull! ");
                }
                catch (Exception r)
                {
                    MessageBox.Show("Database Restore Error"+r);
                }

            }
        }
    }
}
