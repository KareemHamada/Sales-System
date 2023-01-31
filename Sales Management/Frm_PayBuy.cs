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
    public partial class Frm_PayBuy : Form
    {
        public Frm_PayBuy()
        {
            InitializeComponent();
        }

        private void Frm_PayBuy_Load(object sender, EventArgs e)
        {
            try
            {
                txtMatloub.Text = Properties.Settings.Default.TotalOrder.ToString();
            }
            catch (Exception)
            {

            }
            txtBakey.Text = "0.0";
            txtMadfoua.Text = "0.0";
            
            if (Properties.Settings.Default.isNakdy)
            {
                txtMadfoua.Text = (Properties.Settings.Default.TotalOrder).ToString();
                txtMadfoua.Enabled = false;
            }
            else
            {
                txtMadfoua.Focus();
            }
            txtMadfoua_TextChanged(null, null);
        }

        private void txtMadfoua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal bakey = Convert.ToDecimal(txtMatloub.Text) - Convert.ToDecimal(txtMadfoua.Text);

                txtBakey.Text = Math.Round(bakey, 2).ToString();
            }
            catch (Exception)
            {

            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(txtMadfoua.Text == "")
            {
                MessageBox.Show("من فضلك ادحل المبلغ المدفوع", "تاكيد");
                return;
            }

            Properties.Settings.Default.Bakey = Convert.ToDecimal(txtBakey.Text);
            Properties.Settings.Default.Madfoua = Convert.ToDecimal(txtMadfoua.Text);
            Properties.Settings.Default.CheckButton = true;
            Properties.Settings.Default.Save();

            Close();
        }

        private void Frm_PayBuy_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
                //if (txtMadfoua.Text == "")
                //{
                //    MessageBox.Show("من فضلك ادحل المبلغ المدفوع", "تاكيد");
                //    return;
                //}

                //Properties.Settings.Default.Bakey = Convert.ToDecimal(txtBakey.Text);
                //Properties.Settings.Default.Madfoua = Convert.ToDecimal(txtMadfoua.Text);
                //Properties.Settings.Default.CheckButton = true;
                //Properties.Settings.Default.Save();

                //Close();
            }
            else if(e.KeyCode == Keys.F12)
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
