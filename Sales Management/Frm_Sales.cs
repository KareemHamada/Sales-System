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
    public partial class Frm_Sales : Form
    {
        private static Frm_Sales frm;
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static Frm_Sales GetFormSales
        {
            get
            {
                if (frm == null)
                {
                    frm = new Frm_Sales();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public Frm_Sales()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void fillGroups()
        {
            cbxGroub.DataSource = db.readData("select * from Products_Group", "");
            cbxGroub.DisplayMember = "Group_Name";
            cbxGroub.ValueMember = "Group_ID";
        }
        public void FillCustomer()
        {

            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Cust_ID";
        }
        string stock_ID = "";
        private void Frm_Sales_Load(object sender, EventArgs e)
        {
            try
            {
                fillGroups();
                FillCustomer();
                db.FillComboBox(cbxItems, "select * from Products", "Pro_Name", "Pro_ID");

                //DtpDate.Text = DateTime.Now.ToShortDateString();
                DtpReminder.Text = DateTime.Now.ToShortDateString();
                rbtnCustNakdy_CheckedChanged(null, null);
                cbxChooseGroub_CheckedChanged(null, null);

                try
                {

                    AutoNumber();
                }
                catch (Exception) { }
                stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
                lblUsername.Text = Properties.Settings.Default.USERNAME;
                lblItemsCount.Text = "0";
            }
            catch (Exception)
            {

            }
            
        }

        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Sales", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            //DtpDate.Text = DateTime.Now.ToShortDateString();
            DtpReminder.Text = DateTime.Now.ToShortDateString();
            try
            {
                cbxItems.SelectedIndex = 0;
                cbxCustomer.SelectedIndex = 0;
            }
            catch (Exception) { };
            cbxItems.Text = "اختر منتج";
            DgvSale.Rows.Clear();
            rbtnCustNakdy.Checked = true;
            txtbarcode.Clear();
            txtbarcode.Focus();
            txtTotal.Clear();

        }

        private void rbtnCustNakdy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbxCustomer.Enabled = false;
                btnCustomerBrowes.Enabled = false;
                DtpReminder.Enabled = false;
            }
            catch (Exception) { }
        }

        private void rbtnCustAagel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbxCustomer.Enabled = true;
                btnCustomerBrowes.Enabled = true;
                DtpReminder.Enabled = true;
            }
            catch (Exception) { }
        }

        private void btnCustomerBrowes_Click(object sender, EventArgs e)
        {
            Frm_Customer frm = new Frm_Customer();
            frm.ShowDialog();
            FillCustomer();
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
            if (DgvSale.Rows.Count > 0)
            {
                for (int i = 0; i < DgvSale.Rows.Count; i++)
                {
                    if (Convert.ToInt32(cbxItems.SelectedValue) == Convert.ToInt32(DgvSale[0, i].Value))
                    {
                        MessageBox.Show("هذا العنصر تمت اضافتة من قبل اضف عليه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DgvSale.ClearSelection();
                        //DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                        DgvSale.Rows[i].Selected = true;

                        return;
                    }
                }
            }

            DataTable tblItems = new DataTable();
            tblItems.Clear(); 

            DataTable tblUnit = new DataTable();
            tblUnit.Clear();

            tblItems = db.readData("select * from Products where Pro_ID=" + cbxItems.SelectedValue + "", "");
            if (tblItems.Rows.Count >= 1)
            {
                try
                {
                    string Product_ID = tblItems.Rows[0][0].ToString();
                    string Product_Name = tblItems.Rows[0][1].ToString();
                    string Product_Qty = "1";
                    //string Product_Price = "0";
                    decimal Discount = 0;
                    string Product_Unit_ID = tblItems.Rows[0][13].ToString();
                    string unit_Name = db.readData("select Unit_Name from Unit where Unit_ID=" + Product_Unit_ID + "", "").Rows[0][0].ToString();

                    DgvSale.Rows.Add(1);
                    int rowindex = DgvSale.Rows.Count - 1;

                    DgvSale.Rows[rowindex].Cells[0].Value = Product_ID;
                    DgvSale.Rows[rowindex].Cells[1].Value = Product_Name;
                    DgvSale.Rows[rowindex].Cells[2].Value = unit_Name;
                    DgvSale.Rows[rowindex].Cells[3].Value = Product_Qty;
                    
                    tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + Product_ID + " and Unit_ID=N'" + Product_Unit_ID + "'", "");

                    decimal realPrice = 0;
                    try
                    {
                        realPrice = Convert.ToDecimal(tblUnit.Rows[0][4]) / Convert.ToDecimal(tblUnit.Rows[0][2]);
                    }
                    catch (Exception) { }
                    //decimal realPrice = Convert.ToDecimal(tblUnit.Rows[0][3]);
                    DgvSale.Rows[rowindex].Cells[4].Value = realPrice;
                    DgvSale.Rows[rowindex].Cells[5].Value = Discount;
                    decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(realPrice);
                    DgvSale.Rows[rowindex].Cells[6].Value = total;
                    DgvSale.Rows[rowindex].Cells[7].Value = Product_Unit_ID;
                }
                catch (Exception) { }


                try
                {
                    decimal TotalOrder = 0;
                    for (int i = 0; i < DgvSale.Rows.Count; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[6].Value);
                        DgvSale.ClearSelection();
                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

                }
                catch (Exception) { }
            }
        }

        private void Frm_Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnItems_Click(null, null);
            }
            else if (e.KeyCode == Keys.F1)
            {

                txtbarcode.Clear();
                txtbarcode.Focus();
            }
            else if (e.KeyCode == Keys.F11)

            {
                UpdateQty();
                //try
                //{

                //    int index = DgvSale.SelectedRows[0].Index;

                //    DgvSale.Rows[index].Cells[2].Value = Properties.Settings.Default.Item_Qty;
                //    DgvSale.Rows[index].Cells[3].Value = Properties.Settings.Default.Item_SalePrice;
                //    DgvSale.Rows[index].Cells[4].Value = Properties.Settings.Default.Item_Discount;

                //}
                //catch (Exception) { }
            }
            else if (e.KeyCode == Keys.F12)
            {
                PayOrder();
            }
            else if(e.KeyCode == Keys.Delete){
                btnDeleteItem_Click(null,null);
            }
        }
        private void UpdateQty()
        {

            if (DgvSale.Rows.Count >= 1)
            {
                int index = DgvSale.SelectedRows[0].Index;
                Properties.Settings.Default.Pro_ID = Convert.ToInt32(DgvSale.Rows[index].Cells[0].Value);
                Properties.Settings.Default.Pro_Unit = Convert.ToString(DgvSale.Rows[index].Cells[2].Value);
                Properties.Settings.Default.Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells[3].Value);
                Properties.Settings.Default.Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells[4].Value); 
                Properties.Settings.Default.Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells[5].Value);
                Properties.Settings.Default.Pro_Unit_ID = Convert.ToInt32(DgvSale.Rows[index].Cells[7].Value);


                Properties.Settings.Default.Save();


                Frm_SaleQty frm = new Frm_SaleQty();
                frm.ShowDialog();


                try
                {

                    int index2 = DgvSale.SelectedRows[0].Index;
                    DgvSale.Rows[index2].Cells[2].Value = Properties.Settings.Default.Pro_Unit;
                    DgvSale.Rows[index2].Cells[3].Value = Properties.Settings.Default.Item_Qty;
                    DgvSale.Rows[index2].Cells[4].Value = Properties.Settings.Default.Item_SalePrice;
                    DgvSale.Rows[index2].Cells[5].Value = Properties.Settings.Default.Item_Discount;
                    DgvSale.Rows[index2].Cells[7].Value = Properties.Settings.Default.Pro_Unit_ID;
                }
                catch (Exception) { }

            }
        }
        
        private void updateQtyInStoreTest(int pro_ID, decimal realQty,int x,decimal priceBeforeTaxOfBigUnit, decimal taxValue_ForOneBigUnit,decimal priceAfterTaxOfBigUnit)
        {
            DataTable tblQty = new DataTable();
            decimal Qty = 0;
            Int64 RawID = 0;
            int Store_ID = 0;
            while (realQty > 0)
            {
                db.executeData("delete from Products_Qty where Qty <=0", "", "");
                tblQty.Clear();
                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + "", "");
                Qty = Convert.ToDecimal(tblQty.Rows[0][2]);
                RawID = Convert.ToInt64(tblQty.Rows[0][5]);
                Store_ID = Convert.ToInt32(tblQty.Rows[0][1]);
                decimal buyPriceForBigUnit = Convert.ToDecimal(tblQty.Rows[0][3]);
                if (Qty - realQty > 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID + "", "", "");
                    insertSalesRb7h(x, priceBeforeTaxOfBigUnit, taxValue_ForOneBigUnit, realQty, buyPriceForBigUnit, priceAfterTaxOfBigUnit, Store_ID);
                    //insertSalesRb7h(x, priceBeforeTax, taxValue, Exact_Qty, Convert.ToDecimal(tblQty.Rows[0][3]));
                    realQty = 0;
                }
                else if (Qty - realQty == 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + realQty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete Products_Qty where Qty <= 0", "", "");
                    insertSalesRb7h(x, priceBeforeTaxOfBigUnit, taxValue_ForOneBigUnit, realQty, buyPriceForBigUnit, priceAfterTaxOfBigUnit, Store_ID);
                    realQty = 0;
                }
                else if (Qty - realQty < 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + Qty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete Products_Qty where Qty <= 0", "", "");
                    insertSalesRb7h(x, priceBeforeTaxOfBigUnit, taxValue_ForOneBigUnit, Qty, buyPriceForBigUnit, priceAfterTaxOfBigUnit, Store_ID);
                    realQty = Math.Abs(Qty - realQty);
                }
            }
            db.executeData("delete Products_Qty where Qty <= 0", "", "");
        }

        private void insertSalesRb7h(int i, decimal priceBeforeTaxOfBigUnit, decimal taxValue_ForOneBigUnit, decimal realQty, decimal buyPriceForBigUnit,decimal priceAfterTaxOfBigUnit,int Store_ID)
        {
            string d = DateTime.Now.ToString("dd/MM/yyyy");
            Int64 unit_ID = Convert.ToInt64(DgvSale.Rows[i].Cells[7].Value);
;
            decimal qtyOfUnit = Convert.ToDecimal(db.readData("select QtyNmain from Products_Unit where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + " and Unit_ID=" + unit_ID + "", "").Rows[0][0]);

            realQty = realQty * qtyOfUnit;
            priceBeforeTaxOfBigUnit = priceBeforeTaxOfBigUnit / qtyOfUnit;
            taxValue_ForOneBigUnit = taxValue_ForOneBigUnit / qtyOfUnit;
            priceAfterTaxOfBigUnit = priceAfterTaxOfBigUnit / qtyOfUnit;
            buyPriceForBigUnit = buyPriceForBigUnit / qtyOfUnit;

            db.executeData("insert into Sales_Rb7h values (" + txtID.Text + " , N'" + Cust_Name + "' , " + DgvSale.Rows[i].Cells[0].Value + " , '" + d + "' , " + realQty + " , N'" + Properties.Settings.Default.User_ID + "', " + priceBeforeTaxOfBigUnit + " , " + DgvSale.Rows[i].Cells[5].Value + " ," + DgvSale.Rows[i].Cells[6].Value + " , " + Properties.Settings.Default.TotalOrder + " , " + Properties.Settings.Default.Madfoua + " , " + Properties.Settings.Default.Bakey + " ,N'" + DgvSale.Rows[i].Cells[7].Value + "' , " + taxValue_ForOneBigUnit + " , " + priceAfterTaxOfBigUnit + " ,N'" + DateTime.Now.ToShortTimeString() + "' , " + buyPriceForBigUnit + ","+ Store_ID + ")", "", "");

        }

        string Cust_Name = "";
        //to insert data into sales table and update qty in store
        private bool insertDataInSalesUpdateStoreQty()
        {
            // check if products exist at store or not
            DataTable tblProTest = new DataTable();
            tblProTest.Clear();

            DataTable tblQtyTest = new DataTable();
            tblQtyTest.Clear();

            for (int i = 0; i < DgvSale.Rows.Count; i++)
            {
                tblProTest = db.readData("select * from Products where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + "", "");

                tblQtyTest = db.readData("select * from Products_Unit where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + " and Unit_ID=N'" + DgvSale.Rows[i].Cells[7].Value + "'", "");

                decimal qtyInMainTest = Convert.ToDecimal(tblQtyTest.Rows[0][2]);

                // الحصول علي الكمية اللي اتباعت بس بالوحدة الكبري علشان انقص من المخزن
                decimal realQtyTest = Convert.ToDecimal(DgvSale.Rows[i].Cells[3].Value) / qtyInMainTest;


                if (Convert.ToDecimal(tblProTest.Rows[0][2]) - realQtyTest < 0)
                {
                    MessageBox.Show("الكمية الموجودة في المخزن من المنتج "+ DgvSale.Rows[i].Cells[1].Value + " غير كافية للبيع ", "تاكيد");
                    return false;
                }
            }
            
            string d = DateTime.Now.ToString("dd/MM/yyyy");
            string dreminder = DtpReminder.Value.ToString("dd/MM/yyyy");

            if (rbtnCustAagel.Checked)
            { 
                if(cbxCustomer.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم العميل");
                    return false;
                }
                if (cbxCustomer.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك ادخل اسم العميل صحيحا");
                    return false;
                }
                Cust_Name = cbxCustomer.Text;
            }
            else
            {
                if (txtCustomer.Text == "")
                {
                    Cust_Name = "عميل نقدى";
                }
                    
                else if (txtCustomer.Text != "")
                {
                    Cust_Name = txtCustomer.Text;
                }

            }
            db.executeData("insert into Sales values (" + txtID.Text + " , '" + d + "' , N'" + Cust_Name + "')", "", "");

            decimal priceBeforeTax_ForOneProduct = 0;
            decimal taxValue_ForOneProduct = 0;
            decimal totalTaxesForAllOrder = 0;
            decimal qtyInMain = 0;
            decimal realQty = 0;

            DataTable tblPro = new DataTable();
            tblPro.Clear();

            DataTable tblQty = new DataTable();
            tblQty.Clear();

            for (int i = 0; i < DgvSale.Rows.Count; i++)
            {
                tblPro = db.readData("select * from Products where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + "", "");

                tblQty = db.readData("select * from Products_Unit where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + " and Unit_ID=N'" + DgvSale.Rows[i].Cells[7].Value + "'", "");

                qtyInMain = Convert.ToDecimal(tblQty.Rows[0][2]);

                // الضريبة علي المنتج
                // Convert.ToDecimal(tblPro.Rows[0][6]) => سعر المنتج بعد الضريبة
                // Convert.ToDecimal(tblPro.Rows[0][4]) => سعر المنتج قبل الضريبة


                taxValue_ForOneProduct = (Convert.ToDecimal(tblPro.Rows[0][6]) - Convert.ToDecimal(tblPro.Rows[0][4])) / qtyInMain;


                decimal taxValue_ForOneBigUnit = (Convert.ToDecimal(tblPro.Rows[0][6]) - Convert.ToDecimal(tblPro.Rows[0][4]));
                decimal priceAfterTaxOfBigUnit = Convert.ToDecimal(tblPro.Rows[0][6]);

                // السعر قبل الضريبة
                priceBeforeTax_ForOneProduct = Convert.ToDecimal(tblPro.Rows[0][4]) / qtyInMain;

                decimal priceBeforeTaxOfBigUnit = Convert.ToDecimal(tblPro.Rows[0][4]);
                // الحصول علي الكمية اللي اتباعت بس بالوحدة الكبري علشان انقص من المخزن
                realQty = Convert.ToDecimal(DgvSale.Rows[i].Cells[3].Value) / qtyInMain;

                totalTaxesForAllOrder += taxValue_ForOneProduct * Convert.ToDecimal(DgvSale.Rows[i].Cells[3].Value);
                

                db.executeData("insert into Sales_Details values (" + txtID.Text + " , N'" + Cust_Name + "' , " + DgvSale.Rows[i].Cells[0].Value + " , '" + d + "' , " + DgvSale.Rows[i].Cells[3].Value + " , N'" + Properties.Settings.Default.User_ID + "', " + priceBeforeTax_ForOneProduct + " , " + DgvSale.Rows[i].Cells[5].Value + " ," + DgvSale.Rows[i].Cells[6].Value + " , " + Properties.Settings.Default.TotalOrder + " , " + Properties.Settings.Default.Madfoua + " , " + Properties.Settings.Default.Bakey + " ,N'" + DgvSale.Rows[i].Cells[7].Value + "' , " + taxValue_ForOneProduct + " , " + DgvSale.Rows[i].Cells[4].Value + " ,N'" + DateTime.Now.ToShortTimeString() + "',"+stock_ID+")", "", "");


                db.executeData("update Products set Qty = Qty - " + realQty + " where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + "", "", "");

                updateQtyInStoreTest(Convert.ToInt32(DgvSale.Rows[i].Cells[0].Value), realQty,i,priceBeforeTaxOfBigUnit, taxValue_ForOneBigUnit, priceAfterTaxOfBigUnit);


            }


            decimal totalBeforeTax = 0;
            totalBeforeTax = Convert.ToDecimal(txtTotal.Text) - totalTaxesForAllOrder;
            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + txtID.Text + " ,N'فاتورة مبيعات' ,N'قيمة مضافة' ,N'لا يوجد' ,N'" + Cust_Name + "' ," + totalBeforeTax + " ," + totalTaxesForAllOrder + " ," + txtTotal.Text + " ,N'" + d + "')", "", "");

            return true;
        }

        private void PayOrder()
        {
            for (int j = 0; j < DgvSale.Rows.Count; j++)
            {
                if (Convert.ToDecimal(DgvSale.Rows[j].Cells[6].Value) == 0)
                {
                    MessageBox.Show("لا يمكن ان يكون اجمالي المنتج " + DgvSale.Rows[j].Cells[1].Value + " = 0");
                    return;
                }
            }

            string d = DateTime.Now.ToString("dd/MM/yyyy");
            string dreminder = DtpReminder.Value.ToString("dd/MM/yyyy");

            if (DgvSale.Rows.Count >= 1)
            {
                
                Properties.Settings.Default.TotalOrder = Convert.ToDecimal(txtTotal.Text);
                Properties.Settings.Default.Madfoua = 0;
                Properties.Settings.Default.Bakey = 0;
                

                Frm_PaySale frm = new Frm_PaySale();
                if (rbtnCustNakdy.Checked)
                {
                    Properties.Settings.Default.isNakdy = true;
                }
                else
                {
                    Properties.Settings.Default.isNakdy = false;
                }
                Properties.Settings.Default.Save();
                frm.ShowDialog();

                if (Properties.Settings.Default.CheckButton)
                {
                    //call to insert data in sales detalis and sales rb7h and update qty in store
                    bool check = insertDataInSalesUpdateStoreQty();
                    if(check == false)
                    {
                        return;
                    }

                    try
                    {
                        if (rbtnCustNakdy.Checked)
                        {
                            db.executeData("insert into Customer_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfoua + " , '" + d + "' , N'" + Cust_Name + "')", "", "");

                        }
                        else if (rbtnCustAagel.Checked)
                        {
                            db.executeData("insert into Customer_Money values (" + txtID.Text + " , N'" + Cust_Name + "' , " + Properties.Settings.Default.Bakey + " ,'" + d + "' ,'" + dreminder + "',"+cbxCustomer.SelectedValue+")", "", "");

                            if (Properties.Settings.Default.Madfoua >= 1)
                            {
                                db.executeData("insert into Customer_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfoua + " , '" + d + "' , N'" + Cust_Name + "')", "", "");
                            }
                        }
                        insertMoneyIntoStock();
                        if (Properties.Settings.Default.SalesPrint)
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

                            for (int i = 0; i <= Properties.Settings.Default.SalesPrintNum - 1; i++)
                            {
                                Print();
                            }
                        }
                        AutoNumber();
                    }
                    catch (Exception) { }
                }
            }



        }

        //call to insert money in stock insert and update money in stock
        private void insertMoneyIntoStock()
        {
            string d = DateTime.Now.ToString("dd/MM/yyyy");
            DataTable tblStock = new DataTable();
            if(Properties.Settings.Default.Madfoua > 0)
            {
                if (Properties.Settings.Default.Pay_Visa == false)
                {
                    db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + Properties.Settings.Default.Madfoua + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'عمليات مبيعات', N'') ", "", "");
                    db.executeData("update Stock set Money=Money + " + Properties.Settings.Default.Madfoua + " where Stock_ID=" + stock_ID + "", "", "");
                }
                else
                {
                    db.executeData("insert into Bank_Insert (Money ,Date ,Name ,Type ,Reason) values (" + Properties.Settings.Default.Madfoua + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'عمليات مبيعات', N'') ", "", "");
                    db.executeData("update Bank set Money=Money + " + Properties.Settings.Default.Madfoua + " ", "","");

                }
            }
            
        }

        private void Print()
        {
            int id = Convert.ToInt32(txtID.Text);
            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',[Price_AfterTax_ForOneProduct] as 'السعر شامل الضريب',[Discount] as 'الخصم',[TotalProductPrice] as 'الاجمالى',[TotalOrder] as 'اجمالى الفاتورة',[Madfoua] as 'المبلغ المدفوع',[Baky] as 'المتبقى',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',[Sales_Details].taxValue_ForOneProduct as'الضريبة' FROM [dbo].[Sales_Details] , Products,Unit,Users where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID = Sales_Details.Unit_ID and Users.User_Id = Sales_Details.User_ID and Order_ID=" + id + "", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();
                if (Properties.Settings.Default.SalePrintKind == "8CM")
                {
                    RptOrderSales rpt = new RptOrderSales();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = Properties.Settings.Default.PrinterName;
                    rpt.PrintToPrinter(1, true, 0, 0);
                    //frm.ShowDialog();

                }
                else if (Properties.Settings.Default.SalePrintKind == "A4")
                {
                    RptOrderSalesA4 rpt = new RptOrderSalesA4();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = Properties.Settings.Default.PrinterName;
                    rpt.PrintToPrinter(1, true, 0, 0);
                    //frm.ShowDialog();
                }

            }
            catch (Exception) { }
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
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

                DataTable tblUnit = new DataTable();
                tblUnit.Clear();

                tblItems = db.readData("select * from Products where Barcode=N'" + txtbarcode.Text + "'", "");
                if (tblItems.Rows.Count >= 1)
                {
                    string Product_ID = tblItems.Rows[0][0].ToString();
                    if (DgvSale.Rows.Count > 0)
                    {
                        for (int i = 0; i < DgvSale.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(Product_ID) == Convert.ToInt32(DgvSale[0, i].Value))
                            {
                                MessageBox.Show("هذا العنصر تمت اضافتة من قبل اضف عليه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DgvSale.ClearSelection();
                                //DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                                DgvSale.Rows[i].Selected = true;

                                return;
                            }
                        }
                    }
                    try
                    {
                        
                        string Product_Name = tblItems.Rows[0][1].ToString();
                        string Product_Qty = "1";
                        string Product_Price = "0";
                        decimal Discount = 0;
                        string Product_Unit_ID = tblItems.Rows[0][13].ToString(); ;
                        string unit_Name = db.readData("select Unit_Name from Unit where Unit_ID=" + Product_Unit_ID + "", "").Rows[0][0].ToString();

                        DgvSale.Rows.Add(1);
                        int rowindex = DgvSale.Rows.Count - 1;

                        DgvSale.Rows[rowindex].Cells[0].Value = Product_ID;
                        DgvSale.Rows[rowindex].Cells[1].Value = Product_Name;
                        DgvSale.Rows[rowindex].Cells[2].Value = unit_Name;
                        DgvSale.Rows[rowindex].Cells[3].Value = Product_Qty;

                        tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + Product_ID + " and Unit_ID=N'" + Product_Unit_ID + "'", "");

                        decimal realPrice = 0;
                        try
                        {
                            realPrice = Convert.ToDecimal(tblUnit.Rows[0][4]) / Convert.ToDecimal(tblUnit.Rows[0][2]);
                        }
                        catch (Exception) { }
                        DgvSale.Rows[rowindex].Cells[4].Value = realPrice;
                        DgvSale.Rows[rowindex].Cells[5].Value = Discount;
                        decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(realPrice);
                        DgvSale.Rows[rowindex].Cells[6].Value = total;
                        DgvSale.Rows[rowindex].Cells[7].Value = Product_Unit_ID;
                    }
                    catch (Exception) { }


                    try
                    {
                        decimal TotalOrder = 0;
                        for (int i = 0; i < DgvSale.Rows.Count; i++)
                        {

                            TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[6].Value);
                            DgvSale.ClearSelection();
                            DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                            DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                        }


                        txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                        lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

                    }
                    catch (Exception) { }
                }
                txtbarcode.Clear();

                //    DataTable tblItems = new DataTable();
                //    tblItems.Clear(); DataTable tblUnit = new DataTable();
                //    tblUnit.Clear();

                //    tblItems = db.readData("select * from Products where Barcode=N'" + txtbarcode.Text + "'", "");
                //    if (tblItems.Rows.Count >= 1)
                //    {
                //        try
                //        {
                //            cbxItems.SelectedValue = Convert.ToInt32(tblItems.Rows[0][0]);
                //            string Product_ID = tblItems.Rows[0][0].ToString();
                //            string Product_Name = tblItems.Rows[0][1].ToString();
                //            string Product_Qty = "1";
                //            string Product_Price = "0";
                //            decimal Discount = 0;
                //            string Product_Unit = tblItems.Rows[0][14].ToString(); ;


                //            DgvSale.Rows.Add(1);
                //            int rowindex = DgvSale.Rows.Count - 1;

                //            DgvSale.Rows[rowindex].Cells[0].Value = Product_ID;
                //            DgvSale.Rows[rowindex].Cells[1].Value = Product_Name;
                //            DgvSale.Rows[rowindex].Cells[3].Value = Product_Qty;
                //            DgvSale.Rows[rowindex].Cells[2].Value = Product_Unit;
                //            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + DgvSale.CurrentRow.Cells[0].Value + " and Unit_Name=N'" + DgvSale.CurrentRow.Cells[2].Value + "'", "");

                //            decimal realPrice = 0;
                //            try
                //            {
                //                realPrice = Convert.ToDecimal(tblUnit.Rows[0][5]) / Convert.ToDecimal(tblUnit.Rows[0][3]);
                //            }
                //            catch (Exception) { }
                //            DgvSale.Rows[rowindex].Cells[4].Value = realPrice;
                //            decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(realPrice);

                //            DgvSale.Rows[rowindex].Cells[5].Value = Discount;
                //            DgvSale.Rows[rowindex].Cells[6].Value = total;
                //        }
                //        catch (Exception) { }


                //        try
                //        {
                //            decimal TotalOrder = 0;
                //            for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                //            {

                //                TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[6].Value);
                //                DgvSale.ClearSelection();
                //                DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                //                DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                //            }


                //            txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                //            lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

                //        }
                //        catch (Exception) { }
                //    }
                //}
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (DgvSale.Rows.Count >= 1)
            {

                int index = DgvSale.SelectedRows[0].Index;

                DgvSale.Rows.RemoveAt(index);


                if (DgvSale.Rows.Count <= 0)
                {

                    txtTotal.Text = "0";
                }

                try
                {
                    decimal TotalOrder = 0;
                    for (int i = 0; i < DgvSale.Rows.Count; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[6].Value);
                        DgvSale.ClearSelection();
                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

                }
                catch (Exception) { }
            }
        }

        private void DgvSale_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal Item_Qty = 0, Item_SalePrice = 0, Item_Discount = 0;
            try
            {

                int index = DgvSale.SelectedRows[0].Index;

                Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells[3].Value);
                Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells[4].Value);
                Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells[5].Value);

                decimal Total = 0;

                Total = (Item_Qty * Item_SalePrice) - Item_Discount;

                DgvSale.Rows[index].Cells[6].Value = Total;



                decimal TotalOrder = 0;
                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {

                    TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[6].Value);

                }


                txtTotal.Text = Math.Round(TotalOrder, 2).ToString();

            }
            catch (Exception) { }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            PayOrder();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateQty();
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
                txtbarcode.Enabled = false;
            }
            else
            {
                cbxGroub.Enabled = false;
                try
                {
                    db.FillComboBox(cbxItems, "select * from Products", "Pro_Name", "Pro_ID");
                }
                catch (Exception) { }
                txtbarcode.Enabled = true;
            }
        }

        private void cbxGroub_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxItems, "select * from Products where Group_ID=" + cbxGroub.SelectedValue + "", "Pro_Name", "Pro_ID");
            }
            catch (Exception) { }
        }
    }
}
