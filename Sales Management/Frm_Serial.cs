using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Frm_Serial : Form
    {
        public Frm_Serial()
        {
            InitializeComponent();
        }
        private string identifier(string wmiClass, string wmiProperty)
        // return a hardware identifier
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                // only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {

                    }
                }
            }

            return result;
        }
        string x = "0";
        private void Frm_Serial_Load(object sender, EventArgs e)
        {
            string serial = identifier("Win32_DiskDrive", "SerialNumber");
            string signature = identifier("Win32_DiskDrive", "Signature"); // for hard drive
            label2.Text = signature;
            label1.Text = serial;
            x = (((Convert.ToDecimal(signature) * 12345 - 3) * 21 - 9) * 2000).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("من فضلك ادخل كود التفعيل","تاكيد",MessageBoxButtons.OK);
                return;
            }
            if(textBox1.Text == x)
            {
                Properties.Settings.Default.Product_Key = "Yes";
                Properties.Settings.Default.Save();
                MessageBox.Show("تم تفعيل البرنامج بنجاح", "تاكيد", MessageBoxButtons.OK);
                Close();
            }
            else
            {
                MessageBox.Show("كود التفعيل خطا", "تاكيد", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }
    }
}
