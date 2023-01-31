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
    public partial class Frm_StoreTransfire : Form
    {
        public Frm_StoreTransfire()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();


        private void FillPro()
        {
            cbxProducts.DataSource = db.readData("select * from Products", "");
            cbxProducts.DisplayMember = "Pro_Name";
            cbxProducts.ValueMember = "Pro_ID";
        }

        private void FillStore()
        {
            cbxStoreFrom.DataSource = db.readData("select * from Store", "");
            cbxStoreFrom.DisplayMember = "Store_Name";
            cbxStoreFrom.ValueMember = "Store_ID";

            cbxStoreTo.DataSource = db.readData("select * from Store", "");
            cbxStoreTo.DisplayMember = "Store_Name";
            cbxStoreTo.ValueMember = "Store_ID";
        }
        private void Frm_StoreTransfire_Load(object sender, EventArgs e)
        {
            try
            {
                FillPro();
                FillStore();
                cbxProducts_SelectionChangeCommitted(null,null);
            }
            catch (Exception) { }
            
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtBarcode.Text != "")
                {
                    tbl.Clear();
                    tbl = db.readData("select * from Products where Barcode =N'" + txtBarcode.Text + "'", "");
                    if (tbl.Rows.Count >= 1)
                    {
                        cbxProducts.SelectedValue = Convert.ToDecimal(tbl.Rows[0][0]);
                        cbxProducts_SelectionChangeCommitted(null, null);
                    }

                }
            }
        }

        private void cbxProducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxUnit.DataSource = db.readData("select Unit_ID,(select Unit_Name from Unit where Unit.Unit_ID = Products_Unit.Unit_ID)as Unit_Name from Products_Unit where Pro_ID=" + cbxProducts.SelectedValue + "", "");
                cbxUnit.DisplayMember = "Unit_Name";
                cbxUnit.ValueMember = "Unit_ID";
            }
            catch (Exception) { }
        }
        //call to update qty in store
        private void updateQtyInStoreTest(int pro_ID, decimal realQty)
        {
            DataTable tblQty = new DataTable();
            decimal Qty = 0;
            Int64 RawID = 0;
            //int rowCount = 0;
            //rowCount = Convert.ToInt32(db.readData("select Count(Pro_ID) from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "").Rows[0][0]);
            while(realQty > 0)
            {
                db.executeData("delete from Products_Qty where Qty <=0", "", "");
                tblQty.Clear();
                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");
                Qty = Convert.ToDecimal(tblQty.Rows[0][2]);
                RawID = Convert.ToInt64(tblQty.Rows[0][5]);
                if (Qty - realQty > 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID + "", "", "");
                    realQty = 0;
                }
                else if (Qty - realQty == 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete Products_Qty where Qty <= 0", "", "");
                    realQty = 0;
                }
                else if (Qty - realQty < 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + Qty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete Products_Qty where Qty <= 0", "", "");

                    realQty = Math.Abs(Qty - realQty);
                }
            }
            
            

           
        }
        //call to update qty in store
        //private void updateQtyInStore(int pro_ID, decimal realQty)
        //{
        //    DataTable tblQty = new DataTable();
        //    decimal QtyInStoreFirstRaw = 0;
        //    Int64 RawID = 0;
        //    db.executeData("delete from Products_Qty where Qty <=0", "");

        //    tblQty.Clear();
        //    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

        //    QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][2]);
        //    RawID = Convert.ToInt64(tblQty.Rows[0][5]);
        //    if (QtyInStoreFirstRaw - realQty >= 1)
        //    {
        //        db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID +"", "");
        //    }
        //    else if (QtyInStoreFirstRaw - realQty == 0)
        //    {
        //        db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID+ "", "");
        //        db.executeData("delete Products_Qty where Qty <= 0", "");
        //    }
        //    else if (QtyInStoreFirstRaw - realQty < 0)
        //    {

        //        db.executeData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where ID=" + RawID +"", "");
        //        db.executeData("delete Products_Qty where Qty <= 0", "");

        //        decimal baky = Math.Abs(QtyInStoreFirstRaw - realQty);

        //        tblQty.Clear();
        //        tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

        //        QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][2]);
        //        RawID = Convert.ToInt64(tblQty.Rows[0][5]);
        //        ///////////
        //        if (QtyInStoreFirstRaw - baky >= 0)
        //        {
        //            db.executeData("update Products_Qty set Qty=Qty - " + baky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //            db.executeData("delete Products_Qty where Qty <= 0", "");

        //        }
        //        else if (QtyInStoreFirstRaw - baky < 0)
        //        {
        //            decimal secondbaky = Math.Abs(QtyInStoreFirstRaw - baky);
        //            db.executeData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //            db.executeData("delete Products_Qty where Qty <= 0", "");

        //            tblQty.Clear();
        //            tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

        //            QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);

        //            if (QtyInStoreFirstRaw - secondbaky >= 0)
        //            {
        //                db.executeData("update Products_Qty set Qty=Qty - " + secondbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                db.executeData("delete Products_Qty where Qty <= 0", "");
        //            }
        //            else if (QtyInStoreFirstRaw - secondbaky < 0)
        //            {
        //                decimal thirdbaky = Math.Abs(QtyInStoreFirstRaw - secondbaky);
        //                db.executeData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                db.executeData("delete Products_Qty where Qty <= 0", "");

        //                tblQty.Clear();
        //                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

        //                QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);
        //                if (QtyInStoreFirstRaw - thirdbaky >= 0)
        //                {
        //                    db.executeData("update Products_Qty set Qty=Qty - " + thirdbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                    db.executeData("delete Products_Qty where Qty <= 0", "");
        //                }
        //                else if (QtyInStoreFirstRaw - thirdbaky < 0)
        //                {
        //                    decimal forthbaky = Math.Abs(QtyInStoreFirstRaw - thirdbaky);
        //                    db.executeData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                    db.executeData("delete Products_Qty where Qty <= 0", "");


        //                    tblQty.Clear();
        //                    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

        //                    QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);

        //                    if (QtyInStoreFirstRaw - forthbaky >= 0)
        //                    {
        //                        db.executeData("update Products_Qty set Qty=Qty - " + forthbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                        db.executeData("delete Products_Qty where Qty <= 0", "");
        //                    }
        //                    else if (QtyInStoreFirstRaw - forthbaky < 0)
        //                    {
        //                        db.executeData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
        //                        db.executeData("delete Products_Qty where Qty <= 0", "");
        //                    }
        //                }
        //            }
        //        }

        //    }

        //}

        private void insertQtyinStore(int pro_ID, decimal realQty)
        {
            DataTable tblCheck = new DataTable();
            tblCheck.Clear();
           tblCheck= db.readData("select * from Products_Qty where Pro_ID="+ pro_ID + " and Store_ID="+cbxStoreTo.SelectedValue+ " and Buy_Price="+ NudBuyPrice.Value + " and Sale_Price_After_Tax=" + NudSalePrice.Value + "", "");
            if(tblCheck.Rows.Count > 0)
            {
                db.executeData("update Products_Qty set Qty=Qty+"+realQty+" where ID = "+tblCheck.Rows[0][5]+"", "", "");
            }
            else
            {
                db.executeData("insert into Products_Qty values (" + pro_ID + " ," + cbxStoreTo.SelectedValue + " ," + realQty + " ," + NudBuyPrice.Value + " ," + NudSalePrice.Value + ")", "", "");
            }

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable tblUnit = new DataTable();
            decimal QtyInMain = 0, realQty = 0, totalQtyInStore = 0;
            try
            {
                if (NudBuyPrice.Value <= 0 || NudSalePrice.Value <= 0)
                {
                    MessageBox.Show("من فضلك ادخل سعر الشراء وسعر البيع للمنتج الذى تم تحويله", "تاكيد");
                    return;
                }
                if (Convert.ToInt32(cbxStoreFrom.SelectedValue) == Convert.ToInt32(cbxStoreTo.SelectedValue))
                {
                    MessageBox.Show("لا يمكن التحويل لنفس المخزن", "تاكيد");
                    return;
                }
                if(cbxStoreFrom.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مخزن صحيح", "تاكيد");
                    return;
                }
                if (cbxStoreTo.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مخزن صحيح", "تاكيد");
                    return;
                }

                int pro_ID = Convert.ToInt32(cbxProducts.SelectedValue);
                tbl.Clear();
                tbl = db.readData("select * from Products_Unit where Pro_ID=" + pro_ID + " and Unit_ID=N'" + cbxUnit.SelectedValue + "'", "");
                try
                {
                    QtyInMain = Convert.ToDecimal(tbl.Rows[0][2]);
                }
                catch (Exception) { }

                if (QtyInMain > 1)
                {
                    realQty = NudQty.Value / QtyInMain;
                }
                else
                {
                    realQty = NudQty.Value;
                }

                try
                {
                    totalQtyInStore = Convert.ToDecimal(db.readData("select sum(Qty) from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "").Rows[0][0]);
                }
                catch (Exception) { }

                if (totalQtyInStore - realQty < 0)
                {
                    MessageBox.Show("الكمية المراد تحويلها غير موجود فى المخزن حاليا", "تاكيد");
                    return;
                }

                updateQtyInStoreTest(pro_ID, realQty);
                insertQtyinStore(pro_ID, realQty);
                insertIntoProductTransfire();
                MessageBox.Show("تمت عملية التحويل بنجاح", "تاكيد");
                autonumber();
            }
            catch (Exception) { }
        }


        private void insertIntoProductTransfire()
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            db.executeData("insert into Products_Transfire (Pro_ID,Store_From,Store_To,Qty,Unit,Buy_Price,Sale_Price,Date,Name,Reason) Values (" + cbxProducts.SelectedValue + " ,N'" + cbxStoreFrom.SelectedValue + "' ,N'" + cbxStoreTo.SelectedValue + "' ," + NudQty.Value + " ,N'" + cbxUnit.SelectedValue + "' ," + NudBuyPrice.Value + " ," + NudSalePrice.Value + " , N'" + d + "' ,N'" + txtName.Text + "' ,N'" + txtReason.Text + "')", "", "");
        }
        private void autonumber()
        {
            try
            {
                cbxProducts.SelectedIndex = 0;
                cbxStoreFrom.SelectedIndex = 0;
                cbxStoreTo.SelectedIndex = 0;
                cbxUnit.SelectedIndex = 0;
                NudSalePrice.Value = 1;
                NudBuyPrice.Value = 1;
                NudQty.Value = 1;
                txtName.Clear();
                txtReason.Clear();
            }
            catch (Exception) { }
        }

    }
}
