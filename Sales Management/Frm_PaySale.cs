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
    public partial class Frm_PaySale : Form
    {
        public Frm_PaySale()
        {
            InitializeComponent();
        }

        private void Frm_PaySale_Load(object sender, EventArgs e)
        {
            try
            {

                txtMatloub.Text = (Properties.Settings.Default.TotalOrder).ToString();

            }
            catch (Exception) { }

            if (Properties.Settings.Default.isNakdy)
            {
                txtMadfoua.Text = (Properties.Settings.Default.TotalOrder).ToString();
                txtMadfoua.Enabled = false;
            }
            else
            {
                txtMadfoua.Focus();
            }
            //txtMadfoua.Text = "0.0";
            //txtBakey.Text = "0.0";
            txtMadfoua_TextChanged(null, null);
            if (Properties.Settings.Default.UsingVisa)
            {
                checkVisa.Enabled = true;
            }
            else
            {
                checkVisa.Enabled = false;
            }
            
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtMadfoua.Text == "") 
            { 
                MessageBox.Show("من فضلك ادخل المبلغ المدفوع", "تاكيد");
                return;
            }

            Properties.Settings.Default.Madfoua = Convert.ToDecimal(txtMadfoua.Text);
            Properties.Settings.Default.Bakey = Convert.ToDecimal(txtBakey.Text);

            Properties.Settings.Default.CheckButton = true;
            if (checkVisa.Checked)
            {
                Properties.Settings.Default.Pay_Visa = true;
            }
            else
            {
                Properties.Settings.Default.Pay_Visa = false;
            }

            Properties.Settings.Default.Save();

            Close();
        }

        private void Frm_PaySale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnEnter_Click(null, null);
            }
            else if (e.KeyCode == Keys.F12)
            {
                btnReturn_Click(null, null);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CheckButton = false;
            Properties.Settings.Default.Save();
            Close();
        }

        private void txtMadfoua_TextChanged(object sender, EventArgs e)
        {
            try
            {

                decimal baky = Convert.ToDecimal(txtMatloub.Text) - Convert.ToDecimal(txtMadfoua.Text);

                txtBakey.Text = Math.Round(baky, 2).ToString();

            }
            catch (Exception) { }

        }

        private void txtMadfoua_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
