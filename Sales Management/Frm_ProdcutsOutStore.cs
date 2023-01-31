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
    public partial class Frm_ProdcutsOutStore : Form
    {
        public Frm_ProdcutsOutStore()
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

        }
        private void Frm_ProdcutsOutStore_Load(object sender, EventArgs e)
        {
            try
            {
                FillStore();
                FillPro();
                cbxProducts_SelectionChangeCommitted(null, null);
            }
            catch{}
        }

        private void cbxProducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //cbxUnit.DataSource = db.readData("select * from Products_Unit where Pro_ID=" + cbxProducts.SelectedValue + "", "");
                //cbxUnit.DisplayMember = "Unit_Name";
                //cbxUnit.ValueMember = "Unit_ID";\
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
            while (realQty > 0)
            {
                db.executeData("delete from Products_Qty where Qty <=0", "", "");
                tblQty.Clear();
                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");
                Qty = Convert.ToDecimal(tblQty.Rows[0][2]);
                RawID = Convert.ToInt64(tblQty.Rows[0][5]);
                if (Qty - realQty >= 1)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbxProducts.Items.Count >= 1)
            {
                if (cbxUnit.Items.Count >= 1)
                {
                    if (cbxStoreFrom.Items.Count >= 1)
                    {
                        DataTable tblUnit = new DataTable();
                        decimal QtyInMain = 0, realQty = 0, totalQtyInStore = 0;
                        try
                        {
                            int pro_ID = Convert.ToInt32(cbxProducts.SelectedValue);
                            tbl.Clear();
                            //tbl = db.readData("select * from Products_Unit where Pro_ID=" + pro_ID + " and Unit_Name=N'" + cbxUnit.Text + "'", "");
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
                                MessageBox.Show("الكمية المراد اخرجها غير موجود فى المخزن حاليا", "تاكيد");
                                return;
                            }
                            db.executeData("update Products set Qty=Qty - " + realQty + " where Pro_ID=" + pro_ID + "", "", "");

                            //updateQtyInStore(pro_ID, realQty);
                            updateQtyInStoreTest(pro_ID, realQty);
                            insertIntoProductOutStore();
                            MessageBox.Show("تمت عملية الاخراج بنجاح", "تاكيد");
                            autonumber();
                        }
                        catch (Exception) { }

                    }
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل المنتجات");
                return;
            }
        }
        private void insertIntoProductOutStore()
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            db.executeData("insert into Products_OutStore (Pro_ID,Store_ID,Qty,Unit_ID,Date,Name,Reason) Values (" + cbxProducts.SelectedValue + "," + cbxStoreFrom.SelectedValue + "," + NudQty.Value + " ," + cbxUnit.SelectedValue + ", N'" + d + "' ,N'" + txtName.Text + "' ,N'" + txtReason.Text + "')", "", "");
        }

        private void autonumber()
        {
            try
            {
                cbxProducts.SelectedIndex = 0;
                cbxStoreFrom.SelectedIndex = 0;
                cbxUnit.SelectedIndex = 0;
                NudQty.Value = 1;
                txtName.Clear();
                txtReason.Clear();
            }
            catch (Exception) { }
        }
    }
}
