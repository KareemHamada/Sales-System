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
    public partial class Frm_Buy : Form
    {

        private static Frm_Buy frm;
        static void frm_FormClosed(object sender,FormClosedEventArgs e)
        {
            frm = null;
        }
        public static Frm_Buy GetFormBuy
        {
            get
            {
                if(frm == null)
                {
                    frm = new Frm_Buy();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public Frm_Buy()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("Select Max(Order_ID) from Buy", "");
            if (tbl.Rows[0][0].ToString() == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            cbxItems.Text = "اختر منتج";
            txtBarcode.Clear();
            txtTotal.Clear();
            DgvBuy.Rows.Clear();
            //DtpDate.Text = DateTime.Now.ToShortDateString();
            dtpAgel.Text = DateTime.Now.ToShortDateString();
            //try
            //{
            //    cbxItems.SelectedIndex = 0;
            //    cbxSupplier.SelectedIndex = 0;
            //}
            //catch (Exception) { }
            rbtnCash.Checked = true;
            txtBarcode.Focus();
        }


        public void FillSupplierComboBox()
        {
            cbxSupplier.DataSource = db.readData("select * from Suppliers where CurrentState=1", "");
            cbxSupplier.DisplayMember = "Sup_Name";
            cbxSupplier.ValueMember = "Sup_ID";
        }
        public void FillStore()
        {

            cbxStore.DataSource = db.readData("select * from Store where CurrentState=1", "");
            cbxStore.DisplayMember = "Store_Name";
            cbxStore.ValueMember = "Store_ID";
        }
        private void fillGroups()
        {
            cbxGroub.DataSource = db.readData("select * from Products_Group where CurrentState=1", "");
            cbxGroub.DisplayMember = "Group_Name";
            cbxGroub.ValueMember = "Group_ID";
        }
        private void Frm_Buy_Load(object sender, EventArgs e)
        {
            try
            {
                FillSupplierComboBox();
                db.FillComboBox(cbxItems, "select * from Products where CurrentState=1", "Pro_Name", "Pro_ID");
                FillStore();
                fillGroups();
                cbxChooseGroub_CheckedChanged(null, null);
                lblUsername.Text = Properties.Settings.Default.USERNAME;
                stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
                AutoNumber();
            }
            catch (Exception) { }
        }

        private void btnSupplierBro_Click(object sender, EventArgs e)
        {
            Frm_Suppliers frm = new Frm_Suppliers();
            frm.ShowDialog();
            FillSupplierComboBox();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            if (cbxItems.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك ادخل المنتجات اولا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbxItems.Text == "اختر منتج")
            {
                MessageBox.Show("من فضلك اختر منتج", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbxItems.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر منتج صحيحا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //MessageBox.Show(DateTime.Now.ToShortTimeString());
            if (DgvBuy.Rows.Count > 0)
            {
                for (int i = 0; i < DgvBuy.Rows.Count; i++)
                {
                    if (Convert.ToInt32(cbxItems.SelectedValue) == Convert.ToInt32(DgvBuy[0, i].Value))
                    {
                        MessageBox.Show("هذا العنصر تمت اضافتة من قبل اضف عليه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DgvBuy.ClearSelection();
                        //DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                        DgvBuy.Rows[i].Selected = true;

                        return;
                    }
                }
            }
            

            DataTable tblItems = new DataTable();
            tblItems.Clear();

            DataTable tblPrice = new DataTable();
            tblPrice.Clear();

            DataTable tblunit = new DataTable();
            tblunit.Clear();

            tblItems = db.readData("select * from Products where Pro_ID=" + cbxItems.SelectedValue + "", "");
            if (tblItems.Rows.Count >= 1)
            {
                try
                {
                    int countQty = 0;
                    try
                    {
                        // to get count of rows
                        countQty = Convert.ToInt32(db.readData("select count (Pro_ID) from Products_Qty where Pro_ID=" + cbxItems.SelectedValue + "", "").Rows[0][0]);
                    }
                    catch (Exception) { }


                    tblPrice = db.readData("select * from Products_Qty where Pro_ID=" + cbxItems.SelectedValue + "", "");

                    string Product_ID = tblItems.Rows[0][0].ToString();
                    string Product_Name = tblItems.Rows[0][1].ToString();
                    string Product_Qty = "1";
                    string Product_Price = "";
                    if (countQty > 0)
                    {
                        Product_Price = tblPrice.Rows[countQty - 1][3].ToString(); // get buy price of last row
                    }
                    else
                    {
                        Product_Price = "0"; // get buy price of last row
                    }
                    
                    string Product_Unit_ID = tblItems.Rows[0][14].ToString();
                    string unit_Name = db.readData("select Unit_Name from Unit where Unit_ID=" + Product_Unit_ID+"","").Rows[0][0].ToString();
                    decimal Discount = 0;


                    DgvBuy.Rows.Add(1);
                    int rowindex = DgvBuy.Rows.Count - 1;

                    DgvBuy.Rows[rowindex].Cells[0].Value = Product_ID;
                    DgvBuy.Rows[rowindex].Cells[1].Value = Product_Name;
                    DgvBuy.Rows[rowindex].Cells[2].Value = unit_Name;
                    DgvBuy.Rows[rowindex].Cells[3].Value = Product_Qty;
                    // get unit of buy now
                    tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Product_ID + " and Unit_ID=N'" + Product_Unit_ID + "'", "");

                    decimal realPrice = 0;
                    try
                    {
                        realPrice = Convert.ToDecimal(Product_Price) / Convert.ToDecimal(tblunit.Rows[0][2]);
                    }
                    catch (Exception) { }
                    decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(realPrice);

                    DgvBuy.Rows[rowindex].Cells[4].Value = Math.Round(realPrice, 2);
                    DgvBuy.Rows[rowindex].Cells[5].Value = Discount;
                    DgvBuy.Rows[rowindex].Cells[6].Value = Math.Round(total, 2);
                    DgvBuy.Rows[rowindex].Cells[7].Value = Product_Unit_ID;
                }
                catch (Exception) { }


                try
                {
                    decimal TotalOrder = 0;
                    for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells[6].Value);
                        DgvBuy.ClearSelection();
                        DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                        DgvBuy.Rows[DgvBuy.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    lblItemsCount.Text = (DgvBuy.Rows.Count).ToString();

                }
                catch (Exception) { }
            }
        }


        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (DgvBuy.Rows.Count >= 1)
            {
                int index = DgvBuy.SelectedRows[0].Index;
                DgvBuy.Rows.RemoveAt(index);

                try
                {
                    decimal totalOrder = 0;
                    for (int i = 0; i < DgvBuy.Rows.Count; i++)
                    {
                        totalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells[6].Value);
                    }

                    txtTotal.Text = Math.Round(totalOrder, 2).ToString();
                    lblItemsCount.Text = DgvBuy.Rows.Count.ToString();
                }
                catch (Exception) { }


            }
            if (DgvBuy.Rows.Count >= 1)
            {
                // select last row
                DgvBuy.ClearSelection();
                DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                DgvBuy.Rows[DgvBuy.Rows.Count - 1].Selected = true;
            }

        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {    
            if (e.KeyChar == 13)
            {
                if (cbxItems.Items.Count <= 0)
                {
                    MessageBox.Show("من فضلك ادخل المنتجات اولا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

  


                DataTable tblItems = new DataTable();
                tblItems.Clear();

                DataTable tblPrice = new DataTable();
                tblPrice.Clear();

                DataTable tblunit = new DataTable();
                tblunit.Clear();

                tblItems = db.readData("select * from Products where CurrentState=1 and Barcode=N'" + txtBarcode.Text + "'", "");
                if (tblItems.Rows.Count >= 1)
                {
                    cbxItems.SelectedValue = Convert.ToDecimal(tblItems.Rows[0][0]);
                    try
                    {
                        if (DgvBuy.Rows.Count > 0)
                        {
                            for (int i = 0; i < DgvBuy.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(cbxItems.SelectedValue) == Convert.ToInt32(DgvBuy[0, i].Value))
                                {
                                    MessageBox.Show("هذا العنصر تمت اضافتة من قبل اضف عليه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DgvBuy.ClearSelection();
                                    //DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                                    DgvBuy.Rows[i].Selected = true;

                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        int countQty = 0;
                        try
                        {
                            // to get count of rows
                            countQty = Convert.ToInt32(db.readData("select count (Pro_ID) from Products_Qty where Pro_ID=" + cbxItems.SelectedValue + "", "").Rows[0][0]);
                        }
                        catch (Exception) { }


                        tblPrice = db.readData("select * from Products_Qty where Pro_ID=" + cbxItems.SelectedValue + "", "");

                        string Product_ID = tblItems.Rows[0][0].ToString();
                        string Product_Name = tblItems.Rows[0][1].ToString();
                        string Product_Qty = "1";
                        ///
                        string Product_Price = "";
                        if (countQty > 0)
                        {
                            Product_Price = tblPrice.Rows[countQty - 1][3].ToString(); // get buy price of last row
                        }
                        else
                        {
                            Product_Price = "0"; // get buy price of last row
                        }

                        ///
                        //string Product_Price = tblPrice.Rows[countQty - 1][3].ToString(); // get buy price of last row
                        string Product_Unit_ID = tblItems.Rows[0][14].ToString();
                        string unit_Name = db.readData("select Unit_Name from Unit where Unit_ID=" + Product_Unit_ID + "", "").Rows[0][0].ToString();
                        decimal Discount = 0;


                        DgvBuy.Rows.Add(1);
                        int rowindex = DgvBuy.Rows.Count - 1;

                        DgvBuy.Rows[rowindex].Cells[0].Value = Product_ID;
                        DgvBuy.Rows[rowindex].Cells[1].Value = Product_Name;
                        DgvBuy.Rows[rowindex].Cells[2].Value = unit_Name;
                        DgvBuy.Rows[rowindex].Cells[3].Value = Product_Qty;
                        // get unit of buy now
                        tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Product_ID + " and Unit_ID=N'" + Product_Unit_ID + "'", "");

                        decimal realPrice = 0;
                        try
                        {
                            realPrice = Convert.ToDecimal(Product_Price) / Convert.ToDecimal(tblunit.Rows[0][2]);
                        }
                        catch (Exception) { }
                        decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(realPrice);

                        DgvBuy.Rows[rowindex].Cells[4].Value = Math.Round(realPrice, 2);
                        DgvBuy.Rows[rowindex].Cells[5].Value = Discount;
                        DgvBuy.Rows[rowindex].Cells[6].Value = Math.Round(total, 2);
                        DgvBuy.Rows[rowindex].Cells[7].Value = Product_Unit_ID;
                    }
                    catch (Exception) { }


                    try
                    {
                        decimal TotalOrder = 0;
                        for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                        {

                            TotalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells[6].Value);
                            DgvBuy.ClearSelection();
                            DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                            DgvBuy.Rows[DgvBuy.Rows.Count - 1].Selected = true;
                        }


                        txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                        lblItemsCount.Text = (DgvBuy.Rows.Count).ToString();

                    }
                    catch (Exception) { }
                }

                txtBarcode.Clear();

            }
        }

        //call to insert data in buy and buy detalis table and update qty in store
        private void insertAndUpdateData()
        {
            DataTable tblUnit = new DataTable();
            tblUnit.Clear(); 

            DataTable tblQty = new DataTable();
            tblQty.Clear();

            string d = DateTime.Now.ToString("dd/MM/yyyy");
            string dreminder = dtpAgel.Value.ToString("dd/MM/yyyy");

            //insert data into buy table
            db.executeData("insert into Buy values (" + txtID.Text + " , N'" + d + "' ," + cbxSupplier.SelectedValue + ")", "", "");

            decimal taxValue = 0, totalTax = 0, taxPersent = 0, priceBeforeTax = 0, qtyInMqin = 0, realQty = 0;

            //for statment to insert data in buy detalis table and update qty in store
            for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
            {
                try
                {
                    //to get products persent tax
                    taxPersent = Convert.ToDecimal(db.readData("select * from Products where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + "", "").Rows[0][5]);
                }
                catch (Exception) { }
                // old
                //to get product tax value
                //taxValue = (Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) / 100) * taxPersent;

                ////to get product price before tax
                //priceBeforeTax = Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) - taxValue;
                //totalTax += taxValue * Convert.ToDecimal(DgvBuy.Rows[i].Cells[3].Value);
                // end of old
                //new

                //to get product price before tax
                priceBeforeTax = Math.Round(Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) / (1 + (taxPersent / 100)),2);
                //to get product tax value
                taxValue = Math.Round(Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) - priceBeforeTax,2);
                totalTax += taxValue * Convert.ToDecimal(DgvBuy.Rows[i].Cells[3].Value);
                // end of new

                //insert data into buy detalis
                db.executeData("insert into Buy_Details values (" + txtID.Text + " , " + cbxSupplier.SelectedValue + " ," + DgvBuy.Rows[i].Cells[0].Value + " ,N'" + d + "' , " + DgvBuy.Rows[i].Cells[3].Value + " ,N'" + Properties.Settings.Default.User_ID + "' ," + priceBeforeTax + " , " + DgvBuy.Rows[i].Cells[5].Value + " , " + DgvBuy.Rows[i].Cells[6].Value + " , " + txtTotal.Text + " , " + Properties.Settings.Default.Madfoua + " , " + Properties.Settings.Default.Bakey + " ," + taxValue + " ," + DgvBuy.Rows[i].Cells[4].Value + " ,N'" + DateTime.Now.ToShortTimeString() + "' , N'" + DgvBuy.Rows[i].Cells[7].Value + "',"+cbxStore.SelectedValue+","+stock_ID+")", "", "");


                //to get real qty based unit name
                tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + " and Unit_ID=N'" + DgvBuy.Rows[i].Cells[7].Value + "'", "");

                qtyInMqin = Convert.ToDecimal(tblUnit.Rows[0][2]);
                realQty = Convert.ToDecimal(DgvBuy.Rows[i].Cells[3].Value) / qtyInMqin;

                //update All qty in products table
                db.executeData("update Products set Qty = Qty + " + realQty + " where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + "", "", "");

                //************************************
                //check if same product data exist or not
                tblQty = db.readData("select * from Products_Qty where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + " and Store_ID=" + cbxStore.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) * qtyInMqin + "", "");

                if (tblQty.Rows.Count >= 1)
                {
                    //if exist .. update data
                    Int64 Products_Qty_Id = Convert.ToInt64(tblQty.Rows[0][5]);

                    //db.executeData("update Products_Qty set Qty=Qty +" + realQty + " where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + " and Store_ID=" + cbxStore.SelectedValue + " and Buy_Price=" + DgvBuy.Rows[i].Cells[4].Value + " ", "");
                    db.executeData("update Products_Qty set Qty=Qty +" + realQty + " where ID=" + Products_Qty_Id +"", "", "");
                }
                else
                {
                    decimal salePrice = 0;
                    try
                    {
                        salePrice = Convert.ToDecimal(db.readData("select * from Products where Pro_ID=" + DgvBuy.Rows[i].Cells[0].Value + "", "").Rows[0][6]);
                    }
                    catch (Exception) { }
                    //if not exist .. insert new data
                    db.executeData("insert into Products_Qty values  (" + DgvBuy.Rows[i].Cells[0].Value + " ," + cbxStore.SelectedValue + ", " + realQty + "," + Convert.ToDecimal(DgvBuy.Rows[i].Cells[4].Value) * qtyInMqin + " ," + salePrice + ")", "", "");
                }
            }
            decimal totalBeforeTax = 0;
            totalBeforeTax = Convert.ToDecimal(txtTotal.Text) - totalTax;

            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + txtID.Text + " ,N'فاتورة مشتريات' ,N'قيمة مضافة' ,N'" + cbxSupplier.Text + "' ,N'لا يوجد' ," + totalBeforeTax + " ," + totalTax + " ," + txtTotal.Text + " ,N'" + d + "')", "", "");

        }

        string stock_ID = "";
        //call to check if the stock have money to pay or not
        private bool checkIfMoneyExist()
        {
            string d = DateTime.Now.ToString("dd/MM/yyyy");
            DataTable tblStock = new DataTable();
            decimal stock_Money = 0;
            tblStock.Clear();
            tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");
            stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);

            if (Convert.ToDecimal(Properties.Settings.Default.Madfoua) > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + Properties.Settings.Default.Madfoua + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'عمليات شراء', N'') ", "", "");

            db.executeData("update stock set Money=Money - " + Properties.Settings.Default.Madfoua + " where Stock_ID=" + stock_ID + "", "", "");
            return true;
        }

        private void PayOrder()
        {
            for(int j = 0; j < DgvBuy.Rows.Count; j++)
            {
                if(Convert.ToDecimal(DgvBuy.Rows[j].Cells[6].Value) == 0)
                {
                    MessageBox.Show("لا يمكن ان يكون اجمالي المنتج " + DgvBuy.Rows[j].Cells[1].Value + " = 0");
                    return;
                }
            }
            string dreminder = dtpAgel.Value.ToString("dd/MM/yyyy");
            string d = DateTime.Now.ToString("dd/MM/yyyy");

            if (DgvBuy.Rows.Count >= 1)
            {
                if (cbxSupplier.Items.Count <= 0) 
                { 
                    MessageBox.Show("من فضلك اختر مورد اولا", "تاكيد");
                    return; 
                }
                if (cbxSupplier.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مورد صحيحا", "تاكيد");
                    return;
                }
                if (cbxStore.Items.Count <= 0) 
                { 
                    MessageBox.Show("من فضلك ادخل المخازن اولا", "تاكيد"); 
                    return; 
                }
                if (cbxStore.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مخزنا صحيحا", "تاكيد");
                    return;
                }

                try
                {
                    if (DgvBuy.Rows.Count >= 1)
                    {
                        Properties.Settings.Default.TotalOrder = Convert.ToDecimal(txtTotal.Text);
                        Properties.Settings.Default.Madfoua = 0;
                        Properties.Settings.Default.Bakey = 0;

                        Frm_PayBuy frm = new Frm_PayBuy();

                        if (rbtnCash.Checked)
                        {
                            Properties.Settings.Default.isNakdy = true;
                        }
                        else
                        {
                            Properties.Settings.Default.isNakdy = false;
                        }
                        Properties.Settings.Default.Save();

                        frm.ShowDialog();

                    }

                    if (Properties.Settings.Default.CheckButton)
                    {

                        bool check = checkIfMoneyExist();
                        if (check == false)
                        {
                            return;
                        }
                        insertAndUpdateData();

                        if (rbtnCash.Checked)
                        {
                            db.executeData("insert into Supplier_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfoua + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "", "");

                        }
                        else if (rbtnAgel.Checked)
                        {
                            db.executeData("insert into Supplier_Money values (" + txtID.Text + " , " + cbxSupplier.SelectedValue + " , " + Properties.Settings.Default.Bakey + " ,'" + d + "' ,'" + dreminder + "')", "", "");

                            if (Properties.Settings.Default.Madfoua >= 1)
                            {
                                db.executeData("insert into Supplier_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfoua + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "", "");
                            }
                        }
                        if (Properties.Settings.Default.BuyPrint)
                        {
                            int data = 0;
                            if (Properties.Settings.Default.PrinterName == "")
                            { 
                                MessageBox.Show("من فضلك حدد اسم الطابعة من شاشة اعدادت البرنامج", "تاكيد");     
                                return; 
                            }
                            try
                            {
                                data = Convert.ToInt32(db.readData("select count(Name) from OrderPrintData", "").Rows[0][0]);
                            }
                            catch (Exception) { }

                            if (data <= 0)
                            { 
                                MessageBox.Show("من فضلك ادخل بيانات الفاتورة اولا فى شاشة اعدادت البرنامج", "تاكيد"); 
                                return;
                            }

                            for (int i = 0; i < Properties.Settings.Default.BuyPrintNum; i++)
                            {
                                Print();
                            }
                        }

                        AutoNumber();
                    }

                }
                catch (Exception) { }
            }
        }

        //to print 8 cm order or A4
        private void Print()
        {
            int id = Convert.ToInt32(txtID.Text);
            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',Users.User_Name as 'اسم المستخدم',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',[Madfoua] as 'المدفوع',[Baky] as 'المبلغ المتبقى' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Order_ID =" + id + "", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                if (Properties.Settings.Default.BuyPrintKind == "8CM")
                {
                    rptOrderBuy rpt = new rptOrderBuy();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = Properties.Settings.Default.PrinterName;

                    if (Properties.Settings.Default.ShowBeforePrint)
                    {
                        frm.ShowDialog();
                    }
                    else
                    {
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }

                    //rpt.PrintToPrinter(1, true, 0, 0);
                    //frm.ShowDialog();
                }
                else if (Properties.Settings.Default.BuyPrintKind == "A4")
                {
                    RptOrderBuyA4 rpt = new RptOrderBuyA4();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = Properties.Settings.Default.PrinterName;

                    if (Properties.Settings.Default.ShowBeforePrint)
                    {
                        frm.ShowDialog();
                    }
                    else
                    {
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }
                    //rpt.PrintToPrinter(1, true, 0, 0);
                    //frm.ShowDialog();
                }
            }
            catch (Exception) { }
        }

       
        private void UpdateQty()
        {
            if(DgvBuy.Rows.Count >= 1)
            {
                //take data from property to another screen

                int index = DgvBuy.SelectedRows[0].Index;

                Properties.Settings.Default.Pro_ID = Convert.ToInt32(DgvBuy.Rows[index].Cells[0].Value);
                Properties.Settings.Default.Pro_Unit = Convert.ToString(DgvBuy.Rows[index].Cells[2].Value);
                Properties.Settings.Default.Item_Qty = Convert.ToDecimal(DgvBuy.Rows[index].Cells[3].Value);
                Properties.Settings.Default.Item_BuyPrice = Convert.ToDecimal(DgvBuy.Rows[index].Cells[4].Value);
                Properties.Settings.Default.Item_Discount = Convert.ToDecimal(DgvBuy.Rows[index].Cells[5].Value);
                Properties.Settings.Default.Pro_Unit_ID = Convert.ToInt32(DgvBuy.Rows[index].Cells[7].Value);

                Properties.Settings.Default.Save();

                Frm_BuyQty frm = new Frm_BuyQty();
                frm.ShowDialog();
            }
        }
        private void Frm_Buy_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F12)
            {
                PayOrder();
            }else if (e.KeyCode == Keys.F11)
            {
                btnUpdate_Click(null, null);

            }
            else if(e.KeyCode == Keys.F2)
            {
                btnItems_Click(null, null);
            }
            else if(e.KeyCode == Keys.Delete)
            {
                btnDeleteItem_Click(null,null);
            }
            else if (e.KeyCode == Keys.F1){
                txtBarcode.Clear();
                txtBarcode.Focus();
            }
        }


       
        private void DgvBuy_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal Item_Qty = 0, Item_BuyPrice = 0, Item_Discount = 0,total = 0;
            try
            {
                int index = DgvBuy.SelectedRows[0].Index;

                Item_Qty = Convert.ToDecimal(DgvBuy.Rows[index].Cells[3].Value);
                Item_BuyPrice = Convert.ToDecimal(DgvBuy.Rows[index].Cells[4].Value);
                Item_Discount = Convert.ToDecimal(DgvBuy.Rows[index].Cells[5].Value);

                total = (Item_Qty * Item_BuyPrice) - Item_Discount;

                DgvBuy.Rows[index].Cells[6].Value = total;


                decimal totalOrder = 0;
                for (int i = 0; i < DgvBuy.Rows.Count; i++)
                {
                    totalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells[6].Value);
                }

                txtTotal.Text = Math.Round(totalOrder, 2).ToString();
   
            }
            catch (Exception)
            {

            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            PayOrder();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateQty();
            try
            {

                int index = DgvBuy.SelectedRows[0].Index;
                DgvBuy.Rows[index].Cells[2].Value = Properties.Settings.Default.Pro_Unit;
                DgvBuy.Rows[index].Cells[3].Value = Properties.Settings.Default.Item_Qty;
                DgvBuy.Rows[index].Cells[4].Value = Properties.Settings.Default.Item_BuyPrice;
                DgvBuy.Rows[index].Cells[5].Value = Properties.Settings.Default.Item_Discount;
                DgvBuy.Rows[index].Cells[7].Value = Properties.Settings.Default.Pro_Unit_ID;
            }
            catch (Exception) { }
        }

        private void cbxGroub_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbxGroub.Items.Count > 0)
            {
                try
                {
                    db.FillComboBox(cbxItems, "select * from Products where CurrentState=1 and Group_ID=" + cbxGroub.SelectedValue + "", "Pro_Name", "Pro_ID");
                }
                catch (Exception) { }
            }
            
        }

        private void cbxChooseGroub_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxChooseGroub.Checked)
            {
                try
                {
                    cbxGroub_SelectionChangeCommitted(null, null);
                }
                catch (Exception) { }

                cbxGroub.Enabled = true;
                txtBarcode.Enabled = false;
            }
            else
            {
                cbxGroub.Enabled = false;
                try
                {
                    db.FillComboBox(cbxItems, "select * from Products where CurrentState=1", "Pro_Name", "Pro_ID");
                }
                catch (Exception) { }
                txtBarcode.Enabled = true;
            }
        }
    }
}
