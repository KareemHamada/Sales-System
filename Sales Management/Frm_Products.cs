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
    public partial class Frm_Products : Form
    {
        public Frm_Products()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        public bool updateMode = false;
        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Pro_ID) from Products", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            if(Properties.Settings.Default.Taxes == true)
            {
                checkTaxes.Checked = true;
            }
            else
            {
                checkTaxes.Checked = false;
            }
            txtBarcode.Clear();
            txtProName.Clear();
            txtProNameSearch.Clear();
            NudSalePrice.Value = 1;
            NudMinQty.Value = 0;
            //NudQtyStore.Value = 1;
            //NudBuyPriceStore.Value = 1;
            NudMaxDiscount.Value = 0;
            NudAllQty.Value = 0;
            NudUnitPrice.Value = 1;
            NudQtyInMain.Value = 1;
            
            //NudGomlaPrice.Value = 1;
            try
            {
                FillPro();
            }
            catch (Exception) { }
            try
            {
                cbxMainUnit.SelectedIndex = 0;
                cbxUnitSale.SelectedIndex = 0;
                cbxUnitBuy.SelectedIndex = 0;
                cbxGroup.SelectedIndex = 0;
            }
            catch (Exception) { }
            try
            {
                DgvStore.Rows.Clear();
                DgvUnits.Rows.Clear();
            }
            catch (Exception) { }
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;
            btnPrintBarcode.Enabled = false;
        }
        private void updateProduct()
        {
            tbl.Clear();
            tbl = db.readData("select * from Products where Pro_ID ="+Properties.Settings.Default.Update_Pro+"", "");

            if (tbl.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد هذا المنتج");
            }
            else
            {
                try
                {
                    txtID.Text = tbl.Rows[0][0].ToString();
                    txtProName.Text = tbl.Rows[0][1].ToString();
                    NudAllQty.Value = Convert.ToDecimal(tbl.Rows[0][2]);
                    //NudGomlaPrice.Value = Convert.ToDecimal(tbl.Rows[0][3]);
                    NudSalePrice.Value = Convert.ToDecimal(tbl.Rows[0][4]);
                    Nudtax.Value = Convert.ToDecimal(tbl.Rows[0][5]);

                    txtSalePriceTax.Text = tbl.Rows[0][6].ToString();
                    txtBarcode.Text = tbl.Rows[0][7].ToString();
                    NudMinQty.Value = Convert.ToDecimal(tbl.Rows[0][8]);
                    NudMaxDiscount.Value = Convert.ToDecimal(tbl.Rows[0][9]);
                    if (tbl.Rows[0][10].ToString() == "خاضع للضريبة")
                    {
                        checkTaxes.Checked = true;
                    }
                    else
                    {
                        checkTaxes.Checked = false;
                    }
                    cbxGroup.SelectedValue = Convert.ToDecimal(tbl.Rows[0][11]);
                    cbxMainUnit.SelectedValue = Convert.ToDecimal(tbl.Rows[0][12]);
                    cbxUnitSale.SelectedValue = Convert.ToDecimal(tbl.Rows[0][13]);
                    cbxUnitBuy.SelectedValue = Convert.ToDecimal(tbl.Rows[0][14]);
                }
                catch (Exception) { }


                try
                {

                    DataTable tblStore = new DataTable();
                    DataTable tblStoreName = new DataTable();

                    tblStore.Clear();
                    tblStore = db.readData("select * from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                    DgvStore.Rows.Clear();
                    if (tblStore.Rows.Count >= 1)
                    {
                        foreach (DataRow row in tblStore.Rows)
                        {
                            DgvStore.Rows.Add(1);
                            int indexrow = DgvStore.Rows.Count - 1;
                            DgvStore.Rows[indexrow].Cells[0].Value = row[1];
                            tblStoreName.Clear();
                            tblStoreName = db.readData("select Store_Name from Store where Store_ID=" + row[1] + "", "");
                            DgvStore.Rows[indexrow].Cells[1].Value = tblStoreName.Rows[0][0];
                            DgvStore.Rows[indexrow].Cells[2].Value = row[2];
                            DgvStore.Rows[indexrow].Cells[3].Value = row[3];
                        }
                    }

                }
                catch (Exception) { }

                try
                {

                    DataTable tblunit = new DataTable();
                    DataTable tblunitName = new DataTable();

                    tblunit.Clear();
                    tblunit = db.readData("select * from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                    DgvUnits.Rows.Clear();
                    if (tblunit.Rows.Count >= 1)
                    {
                        foreach (DataRow row in tblunit.Rows)
                        {
                            DgvUnits.Rows.Add(1);
                            int indexrow = DgvUnits.Rows.Count - 1;
                            DgvUnits.Rows[indexrow].Cells[0].Value = row[1];
                            tblunitName.Clear();
                            tblunitName = db.readData("select Unit_Name from Unit where Unit_ID=" + row[1] + "", "");
                            DgvUnits.Rows[indexrow].Cells[1].Value = tblunitName.Rows[0][0];
                            DgvUnits.Rows[indexrow].Cells[2].Value = row[2];
                            DgvUnits.Rows[indexrow].Cells[3].Value = row[3];
                        }
                    }
                }
                catch (Exception) { }
            }

            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnDeleteAll.Enabled = true;
            btnSave.Enabled = true;
            btnPrintBarcode.Enabled = true;
        }

        private void FillPro()
        {
            cbxProducts.DataSource = db.readData("select * from Products", "");
            cbxProducts.DisplayMember = "Pro_Name";
            cbxProducts.ValueMember = "Pro_ID";
        }
        private void fillGroups()
        {
            cbxGroup.DataSource = db.readData("select * from Products_Group", "");
            cbxGroup.DisplayMember = "Group_Name";
            cbxGroup.ValueMember = "Group_ID";
        }
        //private void fillStore()
        //{
        //    cbxStore.DataSource = db.readData("select * from Store", "");
        //    cbxStore.DisplayMember = "Store_Name";
        //    cbxStore.ValueMember = "Store_ID";
        //}
        private void fillUnit()
        {
            cbxMainUnit.DataSource = db.readData("select * from Unit", "");
            cbxMainUnit.DisplayMember = "Unit_Name";
            cbxMainUnit.ValueMember = "Unit_ID";

            cbxUnitSale.DataSource = db.readData("select * from Unit", "");
            cbxUnitSale.DisplayMember = "Unit_Name";
            cbxUnitSale.ValueMember = "Unit_ID";

            cbxUnitBuy.DataSource = db.readData("select * from Unit", "");
            cbxUnitBuy.DisplayMember = "Unit_Name";
            cbxUnitBuy.ValueMember = "Unit_ID";
            cbxUnit.DataSource = db.readData("select * from Unit", "");
            cbxUnit.DisplayMember = "Unit_Name";
            cbxUnit.ValueMember = "Unit_ID";
        }
        private void Frm_Products_Load(object sender, EventArgs e)
        {
            try
            {
                fillUnit();
                fillGroups();
                //fillStore();
                AutoNumber();
            }
            catch (Exception) { }
            if (Properties.Settings.Default.Taxes)
            {
                checkTaxes.Checked = true;
            }
            else
            {
                checkTaxes.Enabled = false;
                checkTaxes.Checked = false;
            }
            if (updateMode)
            {
                updateProduct();
            }
            
        }
        private bool productValidation()
        {
            if (txtProName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المنتج اولا");
                return false;
            }

            if (NudSalePrice.Value <= 0)
            {
                MessageBox.Show("لا يمكن ان يكون سعر البيع اقل من 1");
                return false;
            }
            if (NudMaxDiscount.Value >= NudSalePrice.Value)
            {
                MessageBox.Show("لا يمكن ان يكون الخصم المسموح اكبر من سعر البيع");
                return false; ;
            }
            // deleted
            //if (NudMinQty.Value > NudAllQty.Value)
            //{
            //    MessageBox.Show("لا يمكن ان يكون حد الطلب اكبر من الكميه الموجوده");
            //    return false;
            //}
            // deleted 
            //if (NudGomlaPrice.Value > Convert.ToDecimal(txtSalePriceTax.Text))
            //{
            //    MessageBox.Show("لا يمكن ان يكون سعر الجمله اكبر من سعر التجزئه");
            //    return false;
            //}

            if (cbxMainUnit.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك ادخل الوحدات اولا");
                return false;
            }
            if (cbxGroup.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك ادخل التصنيفات اولا");
                return false;
            }
            // deleted
            //if (DgvStore.Rows.Count <= 0)
            //{
            //    MessageBox.Show("لا يمكن اضافه المنتج قبل اضافه كمية له على الاقل");
            //    return false;
            //}
            // deleted

            if (cbxGroup.SelectedValue == null)
            {
                MessageBox.Show("يجب اختيار تصنيفا صحيحا");
                return false;
            }
            if (cbxMainUnit.SelectedValue == null)
            {
                MessageBox.Show("يجب اختيار وحدة كبري صحيحة");
                return false;
            }
            if (cbxUnitSale.SelectedValue == null)
            {
                MessageBox.Show("يجب اختيار الوحدة الاكثر استخدام فى البيع صحيحة");
                return false;
            }
            //if (cbxStore.SelectedValue == null)
            //{
            //    MessageBox.Show("يجب اختيار مخزن صحيح");
            //    return false;
            //}
            if (cbxUnitBuy.SelectedValue == null)
            {
                MessageBox.Show("يجب اختيار الوحدة الاكثر استخدام فى الشراء صحيحة");
                return false;
            }
            if (cbxUnit.SelectedValue == null)
            {
                MessageBox.Show("يجب اختيار الوحدة الصغري صحيحة");
                return false;
            }
            
            
            return true;
        }
   
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool check = false;
            check = productValidation();
            if (check == true)
            {
                if (txtBarcode.Text != "")
                {
                    DataTable tblBarcode = new DataTable();
                    tblBarcode.Clear();
                    tblBarcode = db.readData("Select * from Products where Barcode=N'" + txtBarcode.Text + "'", "");
                    if (tblBarcode.Rows.Count > 0)
                    {
                        MessageBox.Show("هذا الباركود موجود من قبل");
                        return;
                    }
                }

                string is_Tax = "";
                if (checkTaxes.Checked)
                {
                    is_Tax = "خاضع للضريبة";
                }
                else
                {
                    is_Tax = "غير خاضع للضريبة";
                }

                db.executeData("insert into Products Values (" + txtID.Text + " ,N'" + txtProName.Text + "' ," + NudAllQty.Value + " ,0 ," + NudSalePrice.Value + " ," + Nudtax.Value + " ," + txtSalePriceTax.Text + " ,N'" + txtBarcode.Text + "' , " + NudMinQty.Value + " , " + NudMaxDiscount.Value + ",N'" + is_Tax + "' ," + cbxGroup.SelectedValue + " ," + cbxMainUnit.SelectedValue + "," + cbxUnitSale.SelectedValue + " ," + cbxUnitBuy.SelectedValue + ")", "", "");


                for (int i = 0; i <= DgvStore.Rows.Count - 1; i++)
                {
                    db.executeData("insert into Products_Qty values (" + txtID.Text + " ," + DgvStore.Rows[i].Cells[0].Value + " ," + DgvStore.Rows[i].Cells[2].Value + " , " + DgvStore.Rows[i].Cells[3].Value + " ," + txtSalePriceTax.Text + ")", "", "");
                }

                for (int i = 0; i <= DgvUnits.Rows.Count - 1; i++)
                {
                    db.executeData("insert into Products_Unit values (" + txtID.Text + " ," + DgvUnits.Rows[i].Cells[0].Value + "," + DgvUnits.Rows[i].Cells[2].Value + " , " + DgvUnits.Rows[i].Cells[3].Value + " ," + txtSalePriceTax.Text + ")", "", "");
                }

                db.executeData("insert into Products_Unit values (" + txtID.Text + " ," + cbxMainUnit.SelectedValue + " ,1 , " + txtSalePriceTax.Text + " ," + txtSalePriceTax.Text + ")", "", "");
                MessageBox.Show("تم اضافة بيانات المنتج", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AutoNumber();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check = false;
            check = productValidation();
            if (check == true)
            {
                if (txtBarcode.Text != "")
                {
                    DataTable tblBarcode = new DataTable();
                    tblBarcode.Clear();
                    tblBarcode = db.readData("Select * from Products where Barcode=N'" + txtBarcode.Text + "' and Pro_ID != "+ txtID.Text + "", "");
                    if (tblBarcode.Rows.Count > 0)
                    {
                        MessageBox.Show("هذا الباركود موجود من قبل");
                        return;
                    }
                }


                string is_Tax = "";
                if (checkTaxes.Checked == true)
                {
                    is_Tax = "خاضع للضريبة";
                }
                else
                {
                    is_Tax = "غير خاضع للضريبة";
                }
                db.executeData("update  Products set Pro_Name=N'" + txtProName.Text + "' ,Qty=" + NudAllQty.Value + " ,Sale_Price_Before_Tax=" + NudSalePrice.Value + " ,Tax_Value=" + Nudtax.Value + " ,Sale_Price_After_Tax=" + txtSalePriceTax.Text + " ,Barcode=N'" + txtBarcode.Text + "' ,MinQty= " + NudMinQty.Value + " , MaxDiscount=" + NudMaxDiscount.Value + ",IS_Tax=N'" + is_Tax + "' ,Group_ID=" + cbxGroup.SelectedValue + ",Main_UnitID=" + cbxMainUnit.SelectedValue + ",Sale_UnitID=" + cbxUnitSale.SelectedValue + ",Buy_UnitID=" + cbxUnitBuy.SelectedValue + " where Pro_ID=" + txtID.Text + "", "", "");

                // delete from Products_Qty and insert again
                db.executeData("delete from Products_Qty where Pro_ID=" + txtID.Text + "", "", "");
                for (int i = 0; i <= DgvStore.Rows.Count - 1; i++)
                {
 
                    db.executeData("insert into Products_Qty values (" + txtID.Text + " ," + DgvStore.Rows[i].Cells[0].Value + " ," + DgvStore.Rows[i].Cells[2].Value + " , " + DgvStore.Rows[i].Cells[3].Value + " ," + txtSalePriceTax.Text + ")", "", "");
    
                }
                db.executeData("delete from Products_Unit where Pro_ID=" + txtID.Text + "", "", "");
                for (int i = 0; i <= DgvUnits.Rows.Count - 1; i++)
                {
                    
                    db.executeData("insert into Products_Unit values (" + txtID.Text + " ," + DgvUnits.Rows[i].Cells[0].Value + "," + DgvUnits.Rows[i].Cells[2].Value + " , " + DgvUnits.Rows[i].Cells[3].Value + " ," + txtSalePriceTax.Text + ")", "", "");
                    

                }
                //string unit_Name = cbxMainUnit.Text;

                for (int i = 0; i <= DgvUnits.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(cbxMainUnit.SelectedValue) == Convert.ToInt32(DgvUnits.Rows[i].Cells[0].Value))
                    {
                        MessageBox.Show("تم حفظ البيانات بيانات المنتج", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AutoNumber();
                        return;
                    }
                }


                db.executeData("insert into Products_Unit values (" + txtID.Text + " ," + cbxMainUnit.SelectedValue + " ,1 , " + txtSalePriceTax.Text + " ," + txtSalePriceTax.Text + ")", "", "");
                MessageBox.Show("تم حفظ البيانات بيانات المنتج", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AutoNumber();
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                db.readData("delete from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                db.readData("delete from Products where Pro_ID=" + txtID.Text + "", "تم مسح البيانات بنجاح");
                AutoNumber();
            }
        }

 
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from Products_Qty ", "");
                db.readData("delete from Products_Unit ", "");
                db.readData("delete from Products", "تم مسح البيانات بنجاح");
                AutoNumber();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtProNameSearch.Text != "")
            {
                DataTable tblSearch = new DataTable();
                tblSearch.Clear();
                tblSearch = db.readData("select * from Products where Pro_Name like N'%" + txtProNameSearch.Text + "%'", "");

                if (tblSearch.Rows.Count <= 0)
                {
                }
                else
                {
                    try
                    {
                        txtID.Text = tblSearch.Rows[0][0].ToString();
                        txtProName.Text = tblSearch.Rows[0][1].ToString();
                        NudAllQty.Value = Convert.ToDecimal(tblSearch.Rows[0][2]);
                        //NudGomlaPrice.Value = Convert.ToDecimal(tblSearch.Rows[0][3]);
                        NudSalePrice.Value = Convert.ToDecimal(tblSearch.Rows[0][4]);
                        Nudtax.Value = Convert.ToDecimal(tblSearch.Rows[0][5]);

                        txtSalePriceTax.Text = tblSearch.Rows[0][6].ToString();
                        txtBarcode.Text = tblSearch.Rows[0][7].ToString();
                        NudMinQty.Value = Convert.ToDecimal(tblSearch.Rows[0][8]);
                        NudMaxDiscount.Value = Convert.ToDecimal(tblSearch.Rows[0][9]);
                        if (tblSearch.Rows[0][10].ToString() == "خاضع للضريبة")
                        {
                            checkTaxes.Checked = true;
                        }
                        else
                        {
                            checkTaxes.Checked = false;
                        }
                        cbxGroup.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][11]);
                        cbxMainUnit.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][12]);
                        cbxUnitSale.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][13]);
                        cbxUnitBuy.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][14]);
                    }
                    catch (Exception) { }


                    try
                    {

                        DataTable tblStore = new DataTable();
                        DataTable tblStoreName = new DataTable();
                        tblStore.Clear();
                        tblStore = db.readData("select * from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                        DgvStore.Rows.Clear();
                        if (tblStore.Rows.Count >= 1)
                        {
                            foreach (DataRow row in tblStore.Rows)
                            {
                                DgvStore.Rows.Add(1);
                                int indexrow = DgvStore.Rows.Count - 1;
                                DgvStore.Rows[indexrow].Cells[0].Value = row[1];
                                tblStoreName.Clear();
                                tblStoreName = db.readData("select Store_Name from Store where Store_ID=" + row[1] + "", "");
                                DgvStore.Rows[indexrow].Cells[1].Value = tblStoreName.Rows[0][0];
                                DgvStore.Rows[indexrow].Cells[2].Value = row[2];
                                DgvStore.Rows[indexrow].Cells[3].Value = row[3];
                            }
                        }

                    }
                    catch (Exception) { }

                    try
                    {

                        DataTable tblunit = new DataTable();
                        DataTable tblunitName = new DataTable();
                        tblunit.Clear();
                        tblunit = db.readData("select * from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                        DgvUnits.Rows.Clear();
                        if (tblunit.Rows.Count >= 1)
                        {
                            foreach (DataRow row in tblunit.Rows)
                            {
                                DgvUnits.Rows.Add(1);
                                int indexrow = DgvUnits.Rows.Count - 1;
                                DgvUnits.Rows[indexrow].Cells[0].Value = row[1];
                                tblunitName.Clear();
                                tblunitName = db.readData("select Unit_Name from Unit where Unit_ID=" + row[1] + "", "");
                                DgvUnits.Rows[indexrow].Cells[1].Value = tblunitName.Rows[0][0];
                                DgvUnits.Rows[indexrow].Cells[2].Value = row[2];
                                DgvUnits.Rows[indexrow].Cells[3].Value = row[3];
                            }
                        }

                    }
                    catch (Exception) { }
                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                    btnPrintBarcode.Enabled = true;
                }

                
            }
        }


        private void cbxProducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxProducts.Items.Count >= 1)
            {
                DataTable tblSearch = new DataTable();
                tblSearch.Clear();
                tblSearch = db.readData("select * from Products where Pro_ID=" + cbxProducts.SelectedValue + "", "");

                if (tblSearch.Rows.Count <= 0)
                {
                }
                else
                {
                    try
                    {
                        txtID.Text = tblSearch.Rows[0][0].ToString();
                        txtProName.Text = tblSearch.Rows[0][1].ToString();
                        NudAllQty.Value = Convert.ToDecimal(tblSearch.Rows[0][2]);
                        //NudGomlaPrice.Value = Convert.ToDecimal(tblSearch.Rows[0][3]);
                        NudSalePrice.Value = Convert.ToDecimal(tblSearch.Rows[0][4]);
                        Nudtax.Value = Convert.ToDecimal(tblSearch.Rows[0][5]);

                        txtSalePriceTax.Text = tblSearch.Rows[0][6].ToString();
                        txtBarcode.Text = tblSearch.Rows[0][7].ToString();
                        NudMinQty.Value = Convert.ToDecimal(tblSearch.Rows[0][8]);
                        NudMaxDiscount.Value = Convert.ToDecimal(tblSearch.Rows[0][9]);
                        if (tblSearch.Rows[0][10].ToString() == "خاضع للضريبة")
                        {
                            checkTaxes.Checked = true;
                        }
                        else
                        {
                            checkTaxes.Checked = false;
                        }
                        cbxGroup.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][11]);
                        cbxMainUnit.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][12]);
                        cbxUnitSale.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][13]);
                        cbxUnitBuy.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][14]);
                    }
                    catch (Exception) { }


                    try
                    {

                        DataTable tblStore = new DataTable();
                        DataTable tblStoreName = new DataTable();
                        tblStore.Clear();
                        tblStore = db.readData("select * from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                        DgvStore.Rows.Clear();
                        if (tblStore.Rows.Count >= 1)
                        {
                            foreach (DataRow row in tblStore.Rows)
                            {
                                DgvStore.Rows.Add(1);
                                int indexrow = DgvStore.Rows.Count - 1;
                                DgvStore.Rows[indexrow].Cells[0].Value = row[1];
                                tblStoreName.Clear();
                                tblStoreName = db.readData("select Store_Name from Store where Store_ID=" + row[1] + "", "");
                                DgvStore.Rows[indexrow].Cells[1].Value = tblStoreName.Rows[0][0];
                                DgvStore.Rows[indexrow].Cells[2].Value = row[2];
                                DgvStore.Rows[indexrow].Cells[3].Value = row[3];
                            }
                        }

                    }
                    catch (Exception) { }

                    try
                    {

                        DataTable tblunit = new DataTable();
                        DataTable tblunitName = new DataTable();
                        tblunit.Clear();
                        tblunit = db.readData("select * from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                        DgvUnits.Rows.Clear();
                        if (tblunit.Rows.Count >= 1)
                        {
                            foreach (DataRow row in tblunit.Rows)
                            {
                                DgvUnits.Rows.Add(1);
                                int indexrow = DgvUnits.Rows.Count - 1;
                                DgvUnits.Rows[indexrow].Cells[0].Value = row[1];
                                tblunitName.Clear();
                                tblunitName = db.readData("select Unit_Name from Unit where Unit_ID=" + row[1] + "", "");
                                DgvUnits.Rows[indexrow].Cells[1].Value = tblunitName.Rows[0][0];
                                DgvUnits.Rows[indexrow].Cells[2].Value = row[2];
                                DgvUnits.Rows[indexrow].Cells[3].Value = row[3];
                            }
                        }

                    }
                    catch (Exception) { }
                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                    btnPrintBarcode.Enabled = true;
                }

                
            }
        }

        private void NudSalePrice_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal taxVal = 0, salePrice = 0;
                salePrice = NudSalePrice.Value;
                taxVal = (salePrice / 100) * Nudtax.Value;
                if (checkTaxes.Checked == true)
                {
                    txtSalePriceTax.Text = (salePrice + taxVal).ToString();
                }
                else
                {
                    txtSalePriceTax.Text = (salePrice).ToString();
                }

            }
            catch (Exception) { }
        }

        private void checkTaxes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTaxes.Checked)
            {
                //Nudtax.Value = 5;
                Nudtax.Enabled = true;
            }
            else
            {
                Nudtax.Value = 0;
                Nudtax.Enabled = false;
            }
        }

        private void Nudtax_ValueChanged(object sender, EventArgs e)
        {
                        
            try
            {
                decimal taxVal = 0, salePrice = 0;
                salePrice = NudSalePrice.Value;
                taxVal = (salePrice / 100) * Nudtax.Value;
                if (checkTaxes.Checked == true)
                {
                    txtSalePriceTax.Text = (salePrice + taxVal).ToString();
                }
                else
                {
                    txtSalePriceTax.Text = (salePrice).ToString();
                }

            }
            catch (Exception) { }
        }

        //private void btnAddQty_Click(object sender, EventArgs e)
        //{
        //    if (cbxStore.Items.Count >= 1)
        //    {

        //        if (NudBuyPriceStore.Value <= 0 || NudQtyStore.Value <= 0)
        //        {
        //            MessageBox.Show("من فضلك ادخل الكمية و سعر الشراء");
        //            return;
        //        }

        //        DgvStore.Rows.Add(1);
        //        int indexrow = DgvStore.Rows.Count - 1;
        //        DgvStore.Rows[indexrow].Cells[0].Value = cbxStore.SelectedValue;
        //        DgvStore.Rows[indexrow].Cells[1].Value = cbxStore.Text;
        //        DgvStore.Rows[indexrow].Cells[2].Value = NudQtyStore.Value;
        //        DgvStore.Rows[indexrow].Cells[3].Value = NudBuyPriceStore.Value;

        //        decimal total = 0;
        //        for (int i = 0; i <= DgvStore.Rows.Count - 1; i++)
        //        {
        //            total += Convert.ToDecimal(DgvStore.Rows[i].Cells[2].Value);
        //        }

        //        NudAllQty.Value = total;
        //        NudBuyPriceStore.Value = 1;
        //        NudQtyStore.Value = 1;

        //    }
        //}

        //private void btnRemoveStore_Click(object sender, EventArgs e)
        //{
        //    if (DgvStore.Rows.Count >= 1)
        //    {
        //        DgvStore.Rows.RemoveAt(DgvStore.CurrentCell.RowIndex);
        //        decimal total = 0;
        //        for (int i = 0; i <= DgvStore.Rows.Count - 1; i++)
        //        {
        //            total += Convert.ToDecimal(DgvStore.Rows[i].Cells[2].Value);
        //        }

        //        NudAllQty.Value = total;
        //    }
        //}

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            if (cbxUnit.Items.Count >= 1)
            {

                if (NudQtyInMain.Value <= 0 || NudUnitPrice.Value <= 0)
                {
                    MessageBox.Show("من فضلك ادخل عدد القطع و سعر الوحدة");
                    return;
                }
                if (Convert.ToInt32(cbxUnit.SelectedValue) == Convert.ToInt32(cbxMainUnit.SelectedValue))
                {
                    MessageBox.Show("لا يمكن اختيار الوحدة الصغرى مثل الوحدة الكبرى", "من فضلك راجع الوحدات");
                    return;
                }

                string unit_Name = cbxUnit.Text;

                for (int i = 0; i <= DgvUnits.Rows.Count - 1; i++)
                {
                    if (unit_Name == Convert.ToString(DgvUnits.Rows[i].Cells[1].Value))
                    {
                        MessageBox.Show("هذه الوحدة تم اضافتها من قبل", "تاكيد");
                        return;
                    }
                }

                DgvUnits.Rows.Add(1);
                int indexrow = DgvUnits.Rows.Count - 1;
                DgvUnits.Rows[indexrow].Cells[0].Value = cbxUnit.SelectedValue;
                DgvUnits.Rows[indexrow].Cells[1].Value = cbxUnit.Text;
                DgvUnits.Rows[indexrow].Cells[2].Value = NudQtyInMain.Value;
                DgvUnits.Rows[indexrow].Cells[3].Value = NudUnitPrice.Value;
                NudQtyInMain.Value = 1;
                NudUnitPrice.Value = 1;
            }
        }

        private void btnRemoveUnit_Click(object sender, EventArgs e)
        {
            if (DgvUnits.Rows.Count >= 1)
            {
                DgvUnits.Rows.RemoveAt(DgvUnits.CurrentCell.RowIndex);

            }
        }

        private void NudQtyInMain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NudUnitPrice.Value = Convert.ToDecimal(txtSalePriceTax.Text) / NudQtyInMain.Value;

            }
            catch (Exception) { }
        }

        private void txtSearchBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtSearchBarcode.Text != "")
                {
                    DataTable tblSearch = new DataTable();
                    tblSearch.Clear();
                    tblSearch = db.readData("select * from Products where Barcode=N'" + txtSearchBarcode.Text + "'", "");

                    if (tblSearch.Rows.Count <= 0)
                    {

                    }
                    else
                    {
                        try
                        {
                            txtID.Text = tblSearch.Rows[0][0].ToString();
                            txtProName.Text = tblSearch.Rows[0][1].ToString();
                            NudAllQty.Value = Convert.ToDecimal(tblSearch.Rows[0][2]);
                            //NudGomlaPrice.Value = Convert.ToDecimal(tblSearch.Rows[0][3]);
                            NudSalePrice.Value = Convert.ToDecimal(tblSearch.Rows[0][4]);
                            Nudtax.Value = Convert.ToDecimal(tblSearch.Rows[0][5]);

                            txtSalePriceTax.Text = tblSearch.Rows[0][6].ToString();
                            txtBarcode.Text = tblSearch.Rows[0][7].ToString();
                            NudMinQty.Value = Convert.ToDecimal(tblSearch.Rows[0][8]);
                            NudMaxDiscount.Value = Convert.ToDecimal(tblSearch.Rows[0][9]);
                            if (tblSearch.Rows[0][10].ToString() == "خاضع للضريبة")
                            {
                                checkTaxes.Checked = true;
                            }
                            else
                            {
                                checkTaxes.Checked = false;
                            }
                            cbxGroup.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][11]);
                            cbxMainUnit.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][12]);
                            cbxUnitSale.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][13]);
                            cbxUnitBuy.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][14]);
                        }
                        catch (Exception) { }


                        try
                        {

                            DataTable tblStore = new DataTable();
                            DataTable tblStoreName = new DataTable();
                            tblStore.Clear();
                            tblStore = db.readData("select * from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                            DgvStore.Rows.Clear();
                            if (tblStore.Rows.Count >= 1)
                            {
                                foreach (DataRow row in tblStore.Rows)
                                {
                                    DgvStore.Rows.Add(1);
                                    int indexrow = DgvStore.Rows.Count - 1;
                                    DgvStore.Rows[indexrow].Cells[0].Value = row[1];
                                    tblStoreName.Clear();
                                    tblStoreName = db.readData("select Store_Name from Store where Store_ID=" + row[1] + "", "");
                                    DgvStore.Rows[indexrow].Cells[1].Value = tblStoreName.Rows[0][0];
                                    DgvStore.Rows[indexrow].Cells[2].Value = row[2];
                                    DgvStore.Rows[indexrow].Cells[3].Value = row[3];
                                }
                            }

                        }
                        catch (Exception) { }

                        try
                        {

                            DataTable tblunit = new DataTable();
                            DataTable tblunitName = new DataTable();
                            tblunit.Clear();
                            tblunit = db.readData("select * from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                            DgvUnits.Rows.Clear();
                            if (tblunit.Rows.Count >= 1)
                            {
                                foreach (DataRow row in tblunit.Rows)
                                {
                                    DgvUnits.Rows.Add(1);
                                    int indexrow = DgvUnits.Rows.Count - 1;
                                    DgvUnits.Rows[indexrow].Cells[0].Value = row[1];
                                    tblunitName.Clear();
                                    tblunitName = db.readData("select Unit_Name from Unit where Unit_ID=" + row[1] + "", "");
                                    DgvUnits.Rows[indexrow].Cells[1].Value = tblunitName.Rows[0][0];
                                    DgvUnits.Rows[indexrow].Cells[2].Value = row[2];
                                    DgvUnits.Rows[indexrow].Cells[3].Value = row[3];
                                }
                            }

                        }
                        catch (Exception) { }
                        btnAdd.Enabled = false;
                        btnNew.Enabled = true;
                        btnDelete.Enabled = true;
                        btnDeleteAll.Enabled = true;
                        btnSave.Enabled = true;
                        btnPrintBarcode.Enabled = true;
                    }

                    
                }
            }
        }

        private void txtProNameSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtProNameSearch.Text != "")
                {
                    DataTable tblSearch = new DataTable();
                    tblSearch.Clear();
                    tblSearch = db.readData("select * from Products where Pro_Name like N'%" + txtProNameSearch.Text + "%'", "");

                    if (tblSearch.Rows.Count <= 0)
                    {
                    }
                    else
                    {
                        try
                        {
                            txtID.Text = tblSearch.Rows[0][0].ToString();
                            txtProName.Text = tblSearch.Rows[0][1].ToString();
                            NudAllQty.Value = Convert.ToDecimal(tblSearch.Rows[0][2]);
                            //NudGomlaPrice.Value = Convert.ToDecimal(tblSearch.Rows[0][3]);
                            NudSalePrice.Value = Convert.ToDecimal(tblSearch.Rows[0][4]);
                            Nudtax.Value = Convert.ToDecimal(tblSearch.Rows[0][5]);

                            txtSalePriceTax.Text = tblSearch.Rows[0][6].ToString();
                            txtBarcode.Text = tblSearch.Rows[0][7].ToString();
                            NudMinQty.Value = Convert.ToDecimal(tblSearch.Rows[0][8]);
                            NudMaxDiscount.Value = Convert.ToDecimal(tblSearch.Rows[0][9]);
                            if (tblSearch.Rows[0][10].ToString() == "خاضع للضريبة")
                            {
                                checkTaxes.Checked = true;
                            }
                            else
                            {
                                checkTaxes.Checked = false;
                            }
                            cbxGroup.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][11]);
                            cbxMainUnit.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][12]);
                            cbxUnitSale.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][13]);
                            cbxUnitBuy.SelectedValue = Convert.ToDecimal(tblSearch.Rows[0][14]);
                        }
                        catch (Exception) { }


                        try
                        {

                            DataTable tblStore = new DataTable();
                            DataTable tblStoreName = new DataTable();
                            tblStore.Clear();
                            tblStore = db.readData("select * from Products_Qty where Pro_ID=" + txtID.Text + "", "");
                            DgvStore.Rows.Clear();
                            if (tblStore.Rows.Count >= 1)
                            {
                                foreach (DataRow row in tblStore.Rows)
                                {
                                    DgvStore.Rows.Add(1);
                                    int indexrow = DgvStore.Rows.Count - 1;
                                    DgvStore.Rows[indexrow].Cells[0].Value = row[1];
                                    tblStoreName.Clear();
                                    tblStoreName = db.readData("select Store_Name from Store where Store_ID=" + row[1] + "", "");
                                    DgvStore.Rows[indexrow].Cells[1].Value = tblStoreName.Rows[0][0];
                                    DgvStore.Rows[indexrow].Cells[2].Value = row[2];
                                    DgvStore.Rows[indexrow].Cells[3].Value = row[3];
                                }
                            }

                        }
                        catch (Exception) { }

                        try
                        {

                            DataTable tblunit = new DataTable();
                            DataTable tblunitName = new DataTable();
                            tblunit.Clear();
                            tblunit = db.readData("select * from Products_Unit where Pro_ID=" + txtID.Text + "", "");
                            DgvUnits.Rows.Clear();
                            if (tblunit.Rows.Count >= 1)
                            {
                                foreach (DataRow row in tblunit.Rows)
                                {
                                    DgvUnits.Rows.Add(1);
                                    int indexrow = DgvUnits.Rows.Count - 1;
                                    DgvUnits.Rows[indexrow].Cells[0].Value = row[1];
                                    tblunitName.Clear();
                                    tblunitName = db.readData("select Unit_Name from Unit where Unit_ID=" + row[1] + "", "");
                                    DgvUnits.Rows[indexrow].Cells[1].Value = tblunitName.Rows[0][0];
                                    DgvUnits.Rows[indexrow].Cells[2].Value = row[2];
                                    DgvUnits.Rows[indexrow].Cells[3].Value = row[3];
                                }
                            }

                        }
                        catch (Exception) { }
                        btnAdd.Enabled = false;
                        btnNew.Enabled = true;
                        btnDelete.Enabled = true;
                        btnDeleteAll.Enabled = true;
                        btnSave.Enabled = true;
                        btnPrintBarcode.Enabled = true;
                    }

                    
                }
            }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Pro_Barcode_ID = txtID.Text;
            Properties.Settings.Default.Pro_Name = txtProName.Text;
            Properties.Settings.Default.Pro_Barcode = txtBarcode.Text;
            Properties.Settings.Default.Pro_Price = Convert.ToDecimal(txtSalePriceTax.Text);
            Properties.Settings.Default.Save();
            Frm_PrintBarcode frm = new Frm_PrintBarcode();
            frm.ShowDialog();
            txtBarcode.Text = Properties.Settings.Default.Pro_Barcode;
        }

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Pro_ID) from Products", "");
        //    if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
        //    {
        //        row = 0;
        //        Show();
        //    }
        //    else
        //    {
        //        row++;
        //        Show();
        //    }
        //}

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    row = 0;
        //    Show();
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Pro_ID) from Products", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    Show();
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    if (row == 0)
        //    {
        //        tbl.Clear();
        //        tbl = db.readData("select count(Pro_ID) from Products", "");
        //        row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //        Show();
        //    }
        //    else
        //    {


        //        row--;
        //        Show();
        //    }
        //}

        private void btnShowGroup_Click(object sender, EventArgs e)
        {
            Frm_ProductGroup frm = new Frm_ProductGroup();
            frm.ShowDialog();
            fillGroups();
        }

        private void btnShowUnit_Click(object sender, EventArgs e)
        {
            Frm_Unit frm = new Frm_Unit();
            frm.ShowDialog();
            fillUnit();
        }
    }
}
