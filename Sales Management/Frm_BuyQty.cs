﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Frm_BuyQty : Form
    {
        public Frm_BuyQty()
        {
            InitializeComponent();
        }
        Database db = new Database();

        private void Frm_BuyQty_Load(object sender, EventArgs e)
        {
            txtQty.Text = (Properties.Settings.Default.Item_Qty).ToString();
            txtBuyPrice.Text = (Properties.Settings.Default.Item_BuyPrice).ToString();
            txtDiscount.Text = (Properties.Settings.Default.Item_Discount).ToString();

            try
            {
                cbxUnit.DataSource = db.readData("select Products_Unit.Unit_ID as Unit_ID,Unit.Unit_Name as Unit_Name from Products_Unit,Unit where Unit.Unit_ID = Products_Unit.Unit_ID and Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
                cbxUnit.DisplayMember = "Unit_Name";
                cbxUnit.ValueMember = "Unit_ID";
            }
            catch (Exception) { }

            cbxUnit.Text = (Properties.Settings.Default.Pro_Unit).ToString();
            txtQty.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "" || txtQty.Text == "0") { 
                MessageBox.Show("من فضلك ادخل الكمية", "تاكيد"); 
                return; 
            }
            if (txtBuyPrice.Text == "" || txtBuyPrice.Text == "0") 
            { 
                MessageBox.Show("من فضلك ادخل سعر الشراء", "تاكيد"); 
                return; 
            }
            if (txtDiscount.Text == "") { 
                MessageBox.Show("من فضلك ادخل  الخصم", "تاكيد"); 
                return; 
            }
            if (cbxUnit.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر وحدة صحيحة", "تاكيد");
                return;
            }
            Properties.Settings.Default.Item_Qty = Convert.ToDecimal(txtQty.Text);
            Properties.Settings.Default.Item_Discount = Convert.ToDecimal(txtDiscount.Text);
            Properties.Settings.Default.Item_BuyPrice = Convert.ToDecimal(txtBuyPrice.Text);
            Properties.Settings.Default.Pro_Unit = Convert.ToString(cbxUnit.Text);
            Properties.Settings.Default.Pro_Unit_ID = Convert.ToInt32(cbxUnit.SelectedValue);
            Properties.Settings.Default.Save();
            Close();
        }

        private void Frm_BuyQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
                //if (txtQty.Text == "") { MessageBox.Show("من فضلك ادخل الكمية", "تاكيد"); return; }
                //if (txtBuyPrice.Text == "") { MessageBox.Show("من فضلك ادخل سعر الشراء", "تاكيد"); return; }
                //if (txtDiscount.Text == "") { MessageBox.Show("من فضلك ادخل  الخصم", "تاكيد"); return; }
                //Properties.Settings.Default.Item_Qty = Convert.ToDecimal(txtQty.Text);
                //Properties.Settings.Default.Item_Discount = Convert.ToDecimal(txtDiscount.Text);
                //Properties.Settings.Default.Item_BuyPrice = Convert.ToDecimal(txtBuyPrice.Text);
                //Properties.Settings.Default.Pro_Unit = Convert.ToString(cbxUnit.Text);
                //Properties.Settings.Default.Save();

                //Close();

            }
        }

        private void Frm_BuyQty_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                int index = Frm_Buy.GetFormBuy.DgvBuy.SelectedRows[0].Index;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells[2].Value = Properties.Settings.Default.Pro_Unit;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells[3].Value = Properties.Settings.Default.Item_Qty;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells[4].Value = Properties.Settings.Default.Item_BuyPrice;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells[5].Value = Properties.Settings.Default.Item_Discount;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells[7].Value = Properties.Settings.Default.Pro_Unit_ID;

            }
            catch (Exception) { }
        }

        private void cbxUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable tblItems = new DataTable();
            tblItems.Clear();

            DataTable tblPrice = new DataTable();
            tblPrice.Clear();

            DataTable tblunit = new DataTable();
            tblunit.Clear();
            try
            {
                int countQty = 0;
                try
                {
                    countQty = Convert.ToInt32(db.readData("select count(Pro_ID) from Products_Qty where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "").Rows[0][0]);
                }
                catch (Exception) { }


                tblPrice = db.readData("select * from Products_Qty where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
                string Product_Price = tblPrice.Rows[countQty - 1][3].ToString();

                tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue + "", "");
                decimal realPrice = 0;
                try
                {
                    realPrice = Convert.ToDecimal(Product_Price) / Convert.ToDecimal(tblunit.Rows[0][2]);
                }
                catch (Exception) { }
                txtBuyPrice.Text = Math.Round(realPrice, 2) + "";

            }
            catch (Exception) { }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtBuyPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if(txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
            }
        }
    }
}
