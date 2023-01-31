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
    public partial class Frm_SaleQty : Form
    {
        public Frm_SaleQty()
        {
            InitializeComponent();
        }
        Database db = new Database();
        private void Frm_SaleQty_Load(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.SaleDiscountForCasher)
            {
                txtDiscount.Enabled = false;
            }
            txtQty.Text = (Properties.Settings.Default.Item_Qty).ToString();
            txtSalePrice.Text = (Properties.Settings.Default.Item_SalePrice).ToString();
            txtDiscount.Text = (Properties.Settings.Default.Item_Discount).ToString();


            try
            {
                //cbxUnit.DataSource = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
                //cbxUnit.DisplayMember = "Unit_Name";
                //cbxUnit.ValueMember = "Unit_ID";

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
            if (txtSalePrice.Text == "" || txtSalePrice.Text == "0") {
                MessageBox.Show("من فضلك ادخل سعر البيع", "تاكيد");
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
            Properties.Settings.Default.Item_SalePrice = Convert.ToDecimal(txtSalePrice.Text);
            Properties.Settings.Default.Pro_Unit = cbxUnit.Text;
            Properties.Settings.Default.Pro_Unit_ID = Convert.ToInt32(cbxUnit.SelectedValue);
            Properties.Settings.Default.Save();

            if (Properties.Settings.Default.SaleDiscountForCasher)
            {
                //DataTable tblunit = new DataTable();
                decimal tblUnitQty = Convert.ToDecimal(db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue + "", "").Rows[0][2]);

                decimal maxDiscount = Convert.ToDecimal(db.readData("select MaxDiscount from Products where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "").Rows[0][0]) / tblUnitQty;
                try
                {
                    if (Properties.Settings.Default.ItemDiscount == "Value")
                    {
                        if(Convert.ToDecimal(txtDiscount.Text) <= maxDiscount)
                        {
                            Properties.Settings.Default.Item_Discount = Convert.ToDecimal(txtDiscount.Text);
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MessageBox.Show("اقصي خصم مسموح لهذا المنتج هو " + maxDiscount);
                        }
                        
                    }
                    else if (Properties.Settings.Default.ItemDiscount == "Present")
                    {
                        decimal presentValue = 0;
                        presentValue = (Convert.ToDecimal(txtSalePrice.Text) / 100) * Convert.ToDecimal(txtDiscount.Text);
                        if (presentValue <= maxDiscount)
                        {
                            Properties.Settings.Default.Item_Discount = presentValue;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MessageBox.Show("اقصي خصم مسموح لهذا المنتج هو " + maxDiscount);
                        }


                    }
                }
                catch (Exception) { }
            }
            else
            {
                if (Convert.ToDecimal(txtDiscount.Text) >= 1)
                {
                    MessageBox.Show("غير مسموح لك بعمل خصم على المنتج", "تاكيد");
                    txtDiscount.Text = "0";
                    return;
                }

            }

            //Properties.Settings.Default.Save();

            Close();
        }

        private void Frm_SaleQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
            }
        }

        private void Frm_SaleQty_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                int index = Frm_Sales.GetFormSales.DgvSale.SelectedRows[0].Index;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells[2].Value = Properties.Settings.Default.Pro_Unit;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells[3].Value = Properties.Settings.Default.Item_Qty;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells[4].Value = Properties.Settings.Default.Item_SalePrice;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells[5].Value = Properties.Settings.Default.Item_Discount;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells[7].Value = Properties.Settings.Default.Pro_Unit_ID;

            }
            catch (Exception) { }
        }

        private void cbxUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable tblItems = new DataTable();
            tblItems.Clear();
            DataTable tblunit = new DataTable();
            tblunit.Clear();
            try
            {
                tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue + "", "");
                decimal realPrice = 0;
                try
                {
                    realPrice = Convert.ToDecimal(tblunit.Rows[0][4]) / Convert.ToDecimal(tblunit.Rows[0][2]);
                }
                catch (Exception) { }

                //if (cbxGomla.Checked)
                //{
                //    try
                //    {
                //        realPrice = Convert.ToDecimal(db.readData("select Gomla_Price from Products where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "").Rows[0][0]) / Convert.ToDecimal(tblunit.Rows[0][2]);
                //    }
                //    catch (Exception) { }

                //}
                //else
                //{

                //}

                txtSalePrice.Text = Math.Round(realPrice, 2) + "";
            }
            catch (Exception) { }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtSalePrice_KeyPress(object sender, KeyPressEventArgs e)
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

        //private void cbxGomla_CheckedChanged(object sender, EventArgs e)
        //{
        //    DataTable tblItems = new DataTable();
        //    tblItems.Clear();
        //    DataTable tblunit = new DataTable();
        //    tblunit.Clear();
        //    try
        //    {
        //        tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue + "", "");
        //        decimal realPrice = 0;
        //        if (cbxGomla.Checked)
        //        {
        //            try
        //            {
        //                realPrice = Convert.ToDecimal(db.readData("select Gomla_Price from Products where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "").Rows[0][0]) / Convert.ToDecimal(tblunit.Rows[0][2]);
        //            }
        //            catch (Exception) { }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                realPrice = Convert.ToDecimal(tblunit.Rows[0][4]) / Convert.ToDecimal(tblunit.Rows[0][2]);

        //            }
        //            catch (Exception) { }
        //        }

        //        txtSalePrice.Text = Math.Round(realPrice, 2) + "";
        //    }
        //    catch (Exception) { }
        //}
    }
}
