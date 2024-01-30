using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Sales_Management
{
    public partial class Frm_Return : Form
    {
        public Frm_Return()
        {
            InitializeComponent();
        }
        DataTable tbl = new DataTable();
        Database db = new Database();
        private void fillStore()
        {
            cbxStore1.DataSource = db.readData("select * from Store where CurrentState=1", "");
            cbxStore1.DisplayMember = "Store_Name";
            cbxStore1.ValueMember = "Store_ID";
            cbxStore2.DataSource = db.readData("select * from Store where CurrentState=1", "");
            cbxStore2.DisplayMember = "Store_Name";
            cbxStore2.ValueMember = "Store_ID";
        }
        string stock_ID = "";
        private void Frm_Return_Load(object sender, EventArgs e)
        {
            if (rbtnSales.Checked)
            {
                lblName1.Text = "اسم العميل:";
            }
            else
            {
                lblName1.Text = "اسم المورد:";
            }
            DtpDate.Text = DateTime.Now.ToShortDateString();
            try
            {
                fillStore();
            }
            catch (Exception) { }
            
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
        }

        //when press sales return
        private void salesReturn()
        {// Sales_Details //Madfoua
            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',priceBeforeTax_ForOneProduct as 'السعر قبل الضريبة',([Sales_Details].taxValue_ForOneProduct * [Sales_Details].Qty) as 'اجمالي الضريبة',[Sales_Details].[Price_AfterTax_ForOneProduct] as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالي الصنف',Unit.Unit_Name as 'الوحدة',[TotalOrder] as 'اجمالى الفاتورة',[Madfoua] as 'المبلغ المدفوع',[Baky] as 'المتبقى',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',Sales_Details.Unit_ID as 'رقم الوحدة',Sales_Details.Pro_ID as 'رقم المنتج',Sales_Details.ID as 'المسلسل' FROM [dbo].[Sales_Details],Products,Unit,Users where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID = Sales_Details.Unit_ID and Sales_Details.User_ID = Users.User_Id and Order_ID=" + txtID.Text + "", "");
            DgvSearch.DataSource = tbl;
            DgvSearch.Columns[11].Visible = false;
            DgvSearch.Columns[12].Visible = false;
            DgvSearch.Columns[15].Visible = false;
            DgvSearch.Columns[16].Visible = false;
            DgvSearch.Columns[17].Visible = false;

            //decimal bakyDB = 0;
            //try {

            //    bakyDB =Convert.ToDecimal( db.readData("select baky from Sales_Details where Order_ID="+txtID.Text+" ", "").Rows[0][0]);
            //} catch (Exception) { }
            decimal totalOrder = 0, totalMadfoua = 0, baky = 0, taxValue = 0, totalAfterTax = 0;

            for (int i = 0; i < DgvSearch.Rows.Count ; i++)
            {
                totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value) - Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value);
                taxValue += Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value);
                totalAfterTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);
            }
            try
            {
                totalMadfoua = Convert.ToDecimal(DgvSearch.Rows[0].Cells[11].Value);
                txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                txtMadfou3.Text = (Math.Round(totalMadfoua, 2)).ToString();
                txtTotalTax.Text = (Math.Round(taxValue, 2)).ToString();
                txtTotalOrderAfterTax.Text = (Math.Round(totalAfterTax, 2)).ToString();

                baky = totalAfterTax - totalMadfoua;
                txtbaky.Text = (Math.Round(baky, 2)).ToString();
            }
            catch (Exception) { }

            ResetCustSup();
        }
        //when press buy return
        private void buyReturn()
        {//Buy_Details //Madfoua

            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',([Buy_Details].taxValue_ForOneProduct * [Buy_Details].[Qty]) as 'اجمالي الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم' ,[Madfoua] as 'المدفوع',[Baky] as 'المبلغ المتبقى',Buy_Details.Unit_ID as 'رقم الوحدة',Buy_Details.Pro_ID as 'رقم المنتج',ID as 'المسلسل' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Unit.Unit_ID = Buy_Details.Unit_ID and Users.User_Id = Buy_Details.User_ID and Order_ID=" + txtID.Text + "", "");
            DgvSearch.DataSource = tbl;
            DgvSearch.Columns[13].Visible = false;
            DgvSearch.Columns[14].Visible = false;
            DgvSearch.Columns[15].Visible = false;
            DgvSearch.Columns[16].Visible = false;
            DgvSearch.Columns[17].Visible = false;

            //decimal bakyDB = 0;
            //try {

            //    bakyDB =Convert.ToDecimal( db.readData("select baky from Sales_Details where Order_ID="+txtID.Text+" ", "").Rows[0][0]);
            //} catch (Exception) { }

            decimal totalOrder = 0, totalMadfoua = 0, baky = 0, taxValue = 0, totalAfterTax = 0;

            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[10].Value) - Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);
                taxValue += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);
                totalAfterTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[10].Value);
            }
            try
            {
                totalMadfoua = Convert.ToDecimal(DgvSearch.Rows[0].Cells[13].Value);
                txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                txtMadfou3.Text = (Math.Round(totalMadfoua, 2)).ToString();
                txtTotalTax.Text = (Math.Round(taxValue, 2)).ToString();
                txtTotalOrderAfterTax.Text = (Math.Round(totalAfterTax, 2)).ToString();

                baky = totalAfterTax - totalMadfoua;
                txtbaky.Text = (Math.Round(baky, 2)).ToString();
            }
            catch (Exception) { }
            ResetCustSup();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل رقم فاتورة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (rbtnSales.Checked)
                {
                    salesReturn();
                }
                else if (rbtnBuy.Checked == true)
                {
                    buyReturn();
                }
            }
        }

        private void rbtnSales_CheckedChanged(object sender, EventArgs e)
        {
            ResetCustSup();
        }

        private void rbtnBuy_CheckedChanged(object sender, EventArgs e)
        {
            ResetCustSup();
        }
        private void ResetCustSup()
        {

            if (rbtnSales.Checked)
            {
                lblName1.Text = "اسم العميل:";
                lblName2.Text = "اسم العميل:";
                lblStore1.Text = "الى المخزن:";
                lblStore2.Text = "الى المخزن:";
            }
            else
            {
                lblName1.Text = "اسم المورد:";

                lblName2.Text = "اسم المورد:";
                lblStore1.Text = "من المخزن:";
                lblStore2.Text = "من المخزن:";
            }
        }
        private void onLoadScreen()
        {
            tbl.Clear();
            DgvSearch.DataSource = tbl;
            txtbaky.Clear();
            txtMadfou3.Clear();
            txtTotalOrder.Clear();
            txtTotalTax.Clear();
            txtTotalOrderAfterTax.Clear();
            txtID.Clear();
            txtName1.Clear();
            txtName2.Clear();
            
            txtbaky.Text = "0";
            txtMadfou3.Text = "0";
            txtID.Focus();
        }

        string d = "";
        private void returnAllSales() 
        {
            DataTable tblStock = new DataTable();
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName1.Text == "") { 
                MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد");
                return;
            }
            decimal stock_Money = 0;
            tblStock.Clear();
            tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");

            stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);

            if (Convert.ToDecimal(txtTotalOrderAfterTax.Text) > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مبيعات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }

            decimal totalTax = 0;

            //insert into return detalis 
            for (int i = 0; i < DgvSearch.Rows.Count; i++)
            {
                db.executeData("insert into Returns_Details values (" + id + " ,N'' ,N'" + txtName1.Text + "' ,N'" + DgvSearch.Rows[i].Cells[16].Value + "' , N'" + d + "' ," + DgvSearch.Rows[i].Cells[3].Value + " ," + DgvSearch.Rows[i].Cells[4].Value + " ," + DgvSearch.Rows[i].Cells[8].Value + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + DgvSearch.Rows[i].Cells[5].Value + " , " + DgvSearch.Rows[i].Cells[6].Value + " ,N'" + DgvSearch.Rows[i].Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");

                int proID = Convert.ToInt32(DgvSearch.Rows[i].Cells[16].Value);

                decimal QtyInMain = 0, realQty = 0;
                DataTable tblUnit = new DataTable();
                tblUnit.Clear();
                tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.Rows[i].Cells[15].Value + "'", "");


                try
                {
                    QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
                }
                catch (Exception) { }
                

                realQty = Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value) / QtyInMain;
                db.executeData("update Products set Qty=Qty + " + realQty + " where Pro_ID=" + proID + "", "", "");

                // sales rebh table
                DataTable tblSalesRebh = new DataTable();
                tblSalesRebh.Clear();
                tblSalesRebh = db.readData("select * from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Pro_ID =" + DgvSearch.Rows[i].Cells[16].Value + "", "");


                for (int x=0;x< tblSalesRebh.Rows.Count; x++)
                {
                    Int64 unit_ID = Convert.ToInt64(DgvSearch.Rows[i].Cells[15].Value);
                    decimal qtyOfUnit = Convert.ToDecimal(db.readData("select QtyNmain from Products_Unit where Pro_ID=" + DgvSearch.Rows[i].Cells[16].Value + " and Unit_ID=" + unit_ID + "", "").Rows[0][0]);

                    decimal requiredBuyPrice = Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * qtyOfUnit;
                    decimal qtyFromSalesRebh = Convert.ToDecimal(tblSalesRebh.Rows[x][4]) / qtyOfUnit;
                    DataTable tblQty = new DataTable();
                    tblQty.Clear();
                    tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore1.SelectedValue + " and Buy_Price="+ requiredBuyPrice + "", "");


                    if (tblQty.Rows.Count >= 1)
                    {
                        db.executeData("update Products_Qty set Qty=Qty + " + qtyFromSalesRebh + " where ID=" + tblQty.Rows[0][5] + "", "", "");
                    }
                    else
                    {
                        db.executeData("insert into Products_Qty values (" + proID + " , " + cbxStore1.SelectedValue + ", " + qtyFromSalesRebh + " , " + requiredBuyPrice + " , " + Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value) * QtyInMain + ")", "", "");
                    }
                }

                totalTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value);
            }

            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + txtTotalOrderAfterTax.Text + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مبيعات', N'') ", "", "");
            db.executeData("update stock set Money=Money - " + txtTotalOrderAfterTax.Text + " where Stock_ID=" + stock_ID + "", "", "");

            decimal totalBeforeTax = 0;
            totalBeforeTax = Convert.ToDecimal(txtTotalOrderAfterTax.Text) - totalTax;

            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة' ,N'لا يوجد' ,N'" + txtName1.Text + "' ," + totalBeforeTax + " ," + totalTax + " ," + txtTotalOrderAfterTax.Text + " ,N'" + d + "')", "", "");

            //db.executeData("delete from Sales where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

            db.executeData("delete from Sales_Details where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
            db.executeData("delete from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();

        }
        //when press return all order buy
        private void returnAllBuy()
        {
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName1.Text == "") {
                MessageBox.Show("من فضلك ادخل اسم المورد", "تاكيد");
                return;
            }

            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                int proID = Convert.ToInt32(DgvSearch.Rows[i].Cells[16].Value);

                decimal QtyInMainTest = 0;
                decimal realQtyTest = 0;
                DataTable tblUnitTest = new DataTable();
                tblUnitTest.Clear();
                tblUnitTest = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.Rows[i].Cells[15].Value + "'", "");

                try
                {
                    QtyInMainTest = Convert.ToDecimal(tblUnitTest.Rows[0][2]);
                }
                catch (Exception) { }

                realQtyTest = Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value) / QtyInMainTest;

                DataTable tblQty = new DataTable();
                tblQty.Clear();
                tblQty = db.readData("select SUM(Qty) from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore1.SelectedValue + "", "");
                decimal def = 0;
                def = Convert.ToDecimal(tblQty.Rows[0][0]) - realQtyTest;
                if (def < 0)
                {
                    MessageBox.Show("الكمية المراد ارجاعها غير موجوده فى المخزن", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //db.executeData("delete from Buy where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
            db.executeData("delete from Buy_Details where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مشتريات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }
            decimal totalTax = 0;
            //insert into return detalis 
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                db.executeData("insert into Returns_Details values (" + id + " ,N'" + txtName1.Text + "' ,N'' ,N'" + DgvSearch.Rows[i].Cells[16].Value + "' , N'" + d + "' ," + DgvSearch.Rows[i].Cells[4].Value + " ," + DgvSearch.Rows[i].Cells[6].Value + " ," + DgvSearch.Rows[i].Cells[10].Value + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + DgvSearch.Rows[i].Cells[7].Value + " , " + DgvSearch.Rows[i].Cells[8].Value + " ,N'" + DgvSearch.Rows[i].Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");
                int proID = Convert.ToInt32(DgvSearch.Rows[i].Cells[16].Value);


                decimal QtyInMain = 0;
                decimal realQty = 0;
                DataTable tblUnit = new DataTable();
                tblUnit.Clear();
                tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.Rows[i].Cells[15].Value + "'", "");

                try
                {
                    QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
                }
                catch (Exception) { }

                realQty = Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value) / QtyInMain;

                db.executeData("update Products set Qty=Qty - " + realQty + " where Pro_ID=" + proID + "", "", "");
                DataTable tblQty = new DataTable();
                decimal Qty = 0;
                Int64 RawID = 0;

                while (realQty > 0)
                {
                    db.executeData("delete from Products_Qty where Qty <=0", "", "");
                    tblQty.Clear();


                    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore1.SelectedValue + " and Buy_Price="+ Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value) * QtyInMain + "", "");
                    if(tblQty.Rows.Count <= 0)
                    {
                        tblQty.Clear();
                        tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore1.SelectedValue + "", "");
                    }
                    
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
                        db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                        realQty = 0;
                    }
                    else if (Qty - realQty < 0)
                    {
                        db.executeData("update Products_Qty set Qty=Qty - " + Qty + " where ID=" + RawID + "", "", "");
                        db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                        realQty = Math.Abs(Qty - realQty);
                    }
                }

                totalTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);
            }
            db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + txtTotalOrderAfterTax.Text + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مشتريات', N'') ", "", "");
            db.executeData("update Stock set Money=Money + " + txtTotalOrderAfterTax.Text + " where Stock_ID=" + stock_ID + "", "", "");

            decimal totalBeforeTax = 0;
            totalBeforeTax = Convert.ToDecimal(txtTotalOrderAfterTax.Text) - totalTax;
            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مشتريات' ,N'قيمة مضافة' ,N'" + txtName1.Text + "' ,N'لا يوجد' ," + totalBeforeTax + " ," + totalTax + " ," + txtTotalOrderAfterTax.Text + " ,N'" + d + "')", "", "");

            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                if (rbtnSales.Checked)
                {
                    returnAllSales();
                }
                else if (rbtnBuy.Checked)
                {
                    returnAllBuy();
                }
            }
        }

        //when user press return item only in sales
        private void returnItemSaleOnly()
        {
            DataTable tblStock = new DataTable();
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "") { 
                MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد"); 
                return; 
            }
            decimal stock_Money = 0;
            tblStock.Clear();
            tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");
            stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);

            if (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            int proID = Convert.ToInt32(DgvSearch.CurrentRow.Cells[16].Value);

 

            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مبيعات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }

            //insert into return detalis 
            db.executeData("insert into Returns_Details values (" + id + " ,N'' ,N'" + txtName2.Text + "' ,N'" + DgvSearch.CurrentRow.Cells[16].Value + "' , N'" + d + "' ," + DgvSearch.CurrentRow.Cells[3].Value + " ," + DgvSearch.CurrentRow.Cells[4].Value + " ," + DgvSearch.CurrentRow.Cells[8].Value + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + DgvSearch.CurrentRow.Cells[5].Value + " , " + DgvSearch.CurrentRow.Cells[6].Value + " ,N'" + DgvSearch.CurrentRow.Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");

            decimal QtyInMain = 0;
            decimal realQty = 0;
            decimal totalTax = 0;

            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");

            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
            }
            catch (Exception) { }

            realQty = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) / QtyInMain;

            db.executeData("update Products set Qty=Qty + " + realQty + " where Pro_ID=" + proID + "", "", "");
            
            // sales rebh table
            DataTable tblSalesRebh = new DataTable();
            tblSalesRebh.Clear();
            tblSalesRebh = db.readData("select * from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Pro_ID =" + DgvSearch.CurrentRow.Cells[16].Value + "", "");


            for (int x = 0; x < tblSalesRebh.Rows.Count; x++)
            {

                Int64 unit_ID = Convert.ToInt64(DgvSearch.CurrentRow.Cells[15].Value);
                decimal qtyOfUnit = Convert.ToDecimal(db.readData("select QtyNmain from Products_Unit where Pro_ID=" + DgvSearch.CurrentRow.Cells[16].Value + " and Unit_ID=" + unit_ID + "", "").Rows[0][0]);

                decimal requiredBuyPrice = Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * qtyOfUnit;
                decimal qtyFromSalesRebh = Convert.ToDecimal(tblSalesRebh.Rows[x][4]) / qtyOfUnit;



                DataTable tblQty = new DataTable();
                tblQty.Clear();
                tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + requiredBuyPrice + "", "");


                if (tblQty.Rows.Count >= 1)
                {
                    db.executeData("update Products_Qty set Qty=Qty + " + qtyFromSalesRebh + " where ID=" + tblQty.Rows[0][5] + "", "", "");
                }
                else
                {
                    db.executeData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + ", " + qtyFromSalesRebh + " , " + requiredBuyPrice + " , " + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * QtyInMain + ")", "", "");
                }
            }

            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[8].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مبيعات', N'') ", "", "");

            db.executeData("update stock set Money=Money - " + DgvSearch.CurrentRow.Cells[8].Value + " where Stock_ID=" + stock_ID + "", "", "");

            ////insert into tax report
            totalTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[5].Value);
            decimal totalItemBeforeTax = 0;
            decimal totalItemAfterTax = 0;

            totalItemAfterTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) * Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value);

            totalItemBeforeTax = Convert.ToDecimal(totalItemAfterTax) - totalTax;

            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة' ,N'لا يوجد' ,N'" + txtName2.Text + "' ," + totalItemBeforeTax + " ," + totalTax + " ," + totalItemAfterTax + " ,N'" + d + "')", "", "");


            // delete from Sales_Details
            db.executeData("delete from Sales_Details where ID=" + DgvSearch.CurrentRow.Cells[17].Value + "", "", "");
            decimal totalOrder = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[10].Value) - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value);
            decimal totalBakey = totalOrder - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[11].Value);

            db.executeData("update Sales_Details set TotalOrder=" + totalOrder + ", Baky ="+ totalBakey + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
            // delete from sales Rb7h
            db.executeData("delete from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Pro_ID=" + proID + "", "", "");

            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }
        //when user press return item only in buy
        private void returnItemBuyOnly()
        {
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "") 
            { 
                MessageBox.Show("من فضلك ادخل اسم المورد", "تاكيد"); 
                return; 
            }

            // check if qty exist or not
            int proID = Convert.ToInt32(DgvSearch.CurrentRow.Cells[16].Value);

            decimal QtyInMainTest = 0;
            decimal realQtyTest = 0;
            DataTable tblUnitTest = new DataTable();
            tblUnitTest.Clear();
            tblUnitTest = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");

            try
            {
                QtyInMainTest = Convert.ToDecimal(tblUnitTest.Rows[0][2]);
            }
            catch (Exception) { }

            realQtyTest = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) / QtyInMainTest;

            DataTable tblQtyTest = new DataTable();
            tblQtyTest.Clear();
            tblQtyTest = db.readData("select SUM(Qty) from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + "", "");
            decimal def = 0;
            def = Convert.ToDecimal(tblQtyTest.Rows[0][0]) - realQtyTest;
            if (def < 0)
            {
                MessageBox.Show("الكمية المراد ارجاعها غير موجوده فى المخزن", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // end of check


            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مشتريات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }
            //insert into return detalis 

            db.executeData("insert into Returns_Details values (" + id + " ,N'" + txtName2.Text + "' ,N'' ,N'" + DgvSearch.CurrentRow.Cells[16].Value + "' , N'" + d + "' ," + DgvSearch.CurrentRow.Cells[4].Value + " ," + DgvSearch.CurrentRow.Cells[6].Value + " ," + DgvSearch.CurrentRow.Cells[10].Value + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + DgvSearch.CurrentRow.Cells[7].Value + " , " + DgvSearch.CurrentRow.Cells[8].Value + " ,N'" + DgvSearch.CurrentRow.Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");


            decimal QtyInMain = 0;
            decimal realQty = 0;
            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");
            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
            }
            catch (Exception) { }

            realQty = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) / QtyInMain;

            db.executeData("update Products set Qty=Qty - " + realQty + " where Pro_ID=" + proID + "", "", "");
            DataTable tblQty = new DataTable();
            decimal Qty = 0;
            Int64 RawID = 0;

            while (realQty > 0)
            {
                db.executeData("delete from Products_Qty where Qty <=0", "", "");
                tblQty.Clear();

                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * QtyInMain + "", "");
                if (tblQty.Rows.Count <= 0)
                {
                    tblQty.Clear();
                    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + "", "");
                }

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
                    db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                    realQty = 0;
                }
                else if (Qty - realQty < 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + Qty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                    realQty = Math.Abs(Qty - realQty);
                }
            }


            db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + (DgvSearch.CurrentRow.Cells[10].Value) + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مشتريات', N'') ", "", "");
            db.executeData("update stock set Money=Money + " + (DgvSearch.CurrentRow.Cells[10].Value) + " where Stock_ID=" + stock_ID + "", "", "");


            //insert into tax report
            decimal totalTax = 0;
            totalTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[7].Value);
            decimal totalBeforeTax = 0; 
            decimal totalItem = 0;
            totalItem = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) * Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value);
            totalBeforeTax = Convert.ToDecimal(totalItem) - totalTax;

            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مشتريات' ,N'قيمة مضافة'  ,N'" + txtName2.Text + "',N'لا يوجد' ," + totalBeforeTax + " ," + totalTax + " ," + totalItem + " ,N'" + d + "')", "", "");

            decimal totalOrder = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[11].Value) - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[10].Value);
            decimal totalBakey = totalOrder - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[13].Value);

            db.executeData("update Buy_Details set TotalOrder=" + totalOrder + ", Baky =" + totalBakey + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

            db.executeData("delete from Buy_Details where ID=" + DgvSearch.CurrentRow.Cells[17].Value + "", "", "");


            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }
        //when user press return qty in sales 
        // i am here
        private void returnQtySaleOnly()
        {
            DataTable tblStock = new DataTable();
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد");
                return;
            }
            decimal stock_Money = 0;
            tblStock.Clear();
            tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");
            stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);

            if ((Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) - NudQty.Value) < 0)
            {
                MessageBox.Show("الكمية المراد ترجعيها اكبر من الكمية التى تم بيعها", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if ((Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) - NudQty.Value) == 0)
            {
                MessageBox.Show(" الكمية المراد ترجعيها هي الكمية التى تم بيعها اختر ارجاع الكمية المحددة بدلا من ارجاع جزء من المحدد ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if ((Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * NudQty.Value) > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            

            int proID = Convert.ToInt32(DgvSearch.CurrentRow.Cells[16].Value);

            decimal QtyInMain = 0;
            decimal realQty = 0;
            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");

            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
            }
            catch (Exception) { }
            //realQty = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) / QtyInMain;
            realQty = NudQty.Value / QtyInMain;

            // sales rebh table
            DataTable tblSalesRebh = new DataTable();
            

            decimal nudValue = NudQty.Value;
            //DataTable tblNud = new DataTable();
            DataTable tblInsertIntoProductQty = new DataTable();
            decimal Qty = 0;
            Int64 RawID = 1;
            decimal TotalProductPrice = 0;
            decimal totalOrderPrice = 0;

            tblSalesRebh.Clear();
            tblSalesRebh = db.readData("select * from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Pro_ID =" + DgvSearch.CurrentRow.Cells[16].Value + "", "");

            for (int x = 0; x < tblSalesRebh.Rows.Count; x++)
            {
                db.executeData("delete from Sales_Rb7h where Qty <=0", "", "");



                Qty = Convert.ToDecimal(tblSalesRebh.Rows[x][4]);
                RawID = Convert.ToInt64(tblSalesRebh.Rows[x][17]);
                

                if (Qty - nudValue >= 1)
                {
                    TotalProductPrice = (Qty - nudValue) * Convert.ToInt64(tblSalesRebh.Rows[x][14]);
                    totalOrderPrice = nudValue * Convert.ToInt64(tblSalesRebh.Rows[x][14]);
                    // update sales rebh table
                    db.executeData("update Sales_Rb7h set Qty=Qty - " + nudValue + ",TotalProductPrice=" + TotalProductPrice + " where ID=" + RawID + "", "", "");

                    // update total order for all products of the same order
                    db.executeData("update Sales_Rb7h set TotalOrder= TotalOrder- " + totalOrderPrice + " where Order_ID=" + Convert.ToInt64(tblSalesRebh.Rows[x][0]) + "", "", "");

                    // update Products_Qty table
                    tblInsertIntoProductQty.Clear();
                    tblInsertIntoProductQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + "", "");
                    if (tblInsertIntoProductQty.Rows.Count >= 1)
                    {
                        db.executeData("update Products_Qty set Qty=Qty + " + realQty + " where ID=" + tblInsertIntoProductQty.Rows[0][5] + "", "", "");
                    }
                    else
                    {
                        db.executeData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + ", " + realQty + " , " + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + " , " + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * QtyInMain + ")", "", "");
                    }
                    // end of update Products_Qty table
                    nudValue = 0;
                    break;
                }
                else if (Qty - nudValue == 0)
                {
                    TotalProductPrice = Qty * Convert.ToInt64(tblSalesRebh.Rows[x][14]);
                    totalOrderPrice = nudValue * Convert.ToInt64(tblSalesRebh.Rows[x][14]);


                    // update sales rebh table
                    db.executeData("update Sales_Rb7h set Qty=Qty - " + nudValue + ",TotalProductPrice=" + TotalProductPrice + " where ID=" + RawID + "", "", "");

                    // update total order for all products of the same order
                    db.executeData("update Sales_Rb7h set TotalOrder= TotalOrder- " + totalOrderPrice + " where Order_ID=" + Convert.ToInt64(tblSalesRebh.Rows[x][0]) + "", "", "");

                    // update Products_Qty table
                    tblInsertIntoProductQty.Clear();
                    tblInsertIntoProductQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + "", "");
                    if (tblInsertIntoProductQty.Rows.Count >= 1)
                    {
                        db.executeData("update Products_Qty set Qty=Qty + " + realQty + " where ID=" + tblInsertIntoProductQty.Rows[0][5] + "", "", "");
                    }
                    else
                    {
                        db.executeData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + ", " + realQty + " , " + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + " , " + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * QtyInMain + ")", "", "");
                    }
                    // end of update Products_Qty table
                    nudValue = 0;

                    db.executeData("delete from Sales_Rb7h where Qty <=0", "", "");
                    break;
                }
                else if (Qty - nudValue < 0)
                {
                    TotalProductPrice = Qty * Convert.ToInt64(tblSalesRebh.Rows[x][14]);
                    totalOrderPrice = nudValue * Convert.ToInt64(tblSalesRebh.Rows[x][14]);

                    // update sales rebh table and this will be deleted
                    db.executeData("update Sales_Rb7h set Qty=Qty - " + Qty + ",TotalProductPrice=" + TotalProductPrice + " where ID=" + RawID + "", "", "");

                    // update total order for all products of the same order
                    db.executeData("update Sales_Rb7h set TotalOrder= TotalOrder- " + totalOrderPrice + " where Order_ID=" + Convert.ToInt64(tblSalesRebh.Rows[x][0]) + "", "", "");

                    // update Products_Qty table
                    tblInsertIntoProductQty.Clear();
                    tblInsertIntoProductQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + "", "");

                    if (tblInsertIntoProductQty.Rows.Count >= 1)
                    {
                        db.executeData("update Products_Qty set Qty=Qty + " + Qty/QtyInMain + " where ID=" + tblInsertIntoProductQty.Rows[0][5] + "", "", "");
                    }
                    else
                    {
                        db.executeData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + ", " + Qty / QtyInMain + " , " + Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * QtyInMain + " , " + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * QtyInMain + ")", "", "");
                    }
                    // end of update Products_Qty table

                    nudValue = Math.Abs(Qty - nudValue);
                    realQty = nudValue / QtyInMain;
                }

                db.executeData("delete from Sales_Rb7h where Qty <=0", "", "");
            }

    
            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مبيعات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }

            decimal total_TaxValue_ForProduct = (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value)) * NudQty.Value;

            //insert into return detalis 
            db.executeData("insert into Returns_Details values (" + id + " ,N'' ,N'" + txtName2.Text + "' ," + DgvSearch.CurrentRow.Cells[16].Value + ",N'" + d + "' ," + NudQty.Value + " ," + DgvSearch.CurrentRow.Cells[4].Value + " ," + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * NudQty.Value + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + total_TaxValue_ForProduct + " , " + DgvSearch.CurrentRow.Cells[6].Value + " ,N'" + DgvSearch.CurrentRow.Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");

            db.executeData("update Products set Qty=Qty + " + NudQty.Value / QtyInMain + " where Pro_ID=" + proID + "", "", "");

            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * NudQty.Value) + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مبيعات', N'') ", "", "");
            db.executeData("update stock set Money=Money - " + (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * NudQty.Value) + " where Stock_ID=" + stock_ID + "", "", "");

            //insert into tax report
            decimal totalTax = 0, itemTax = 0;

            itemTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[5].Value) / Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value);

            totalTax = itemTax * NudQty.Value;

            decimal totalBeforeTax = 0; 
            decimal totalItem = 0;
            totalItem = NudQty.Value * Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value);
            totalBeforeTax = totalItem - totalTax;

            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة'  ,N'لا يوجد' ,N'" + txtName2.Text + "'," + totalBeforeTax + " ," + totalTax + " ," + totalItem + " ,N'" + d + "')", "", "");


            
            decimal TotalProductPrice2 = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) - NudQty.Value);

            db.executeData("update Sales_Details set Qty=Qty - " + NudQty.Value + ",TotalProductPrice=" + TotalProductPrice2 + " where ID=" + DgvSearch.CurrentRow.Cells[17].Value + "", "", "");

            // update sales details for all rows
            decimal minusedPrice = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value) * NudQty.Value;
            decimal totalOrder = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[10].Value) - minusedPrice;
            decimal totalBakey = totalOrder - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[11].Value);

            db.executeData("update Sales_Details set TotalOrder=" + totalOrder + ", Baky =" + totalBakey + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");


            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }
        //when user press return qty in buy 
        private void returnQtyBuyOnly()
        {
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "") {
                MessageBox.Show("من فضلك ادخل اسم المورد", "تاكيد"); 
                return; 
            }
            if ((Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) - NudQty.Value) < 0)
            {
                MessageBox.Show("الكمية المراد ترجعيها اكبر من الكمية التى تم شرائها", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if ((Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) - NudQty.Value) == 0)
            {
                MessageBox.Show(" الكمية المراد ترجعيها هي الكمية التى تم شرائها اختر ارجاع الكمية المحددة بدلا من ارجاع جزء من المحدد ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // check if qty exist or not
            int proID = Convert.ToInt32(DgvSearch.CurrentRow.Cells[16].Value);

            decimal QtyInMainTest = 0;
            decimal realQtyTest = 0;
            DataTable tblUnitTest = new DataTable();
            tblUnitTest.Clear();
            tblUnitTest = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");

            try
            {
                QtyInMainTest = Convert.ToDecimal(tblUnitTest.Rows[0][2]);
            }
            catch (Exception) { }

            realQtyTest = NudQty.Value / QtyInMainTest;

            DataTable tblQtyTest = new DataTable();
            tblQtyTest.Clear();
            tblQtyTest = db.readData("select SUM(Qty) from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + "", "");
            decimal def = 0;
            def = Convert.ToDecimal(tblQtyTest.Rows[0][0]) - realQtyTest;
            if (def < 0)
            {
                MessageBox.Show("الكمية المراد ارجاعها غير موجوده فى المخزن", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // end of check

            //insert data into return table
            db.executeData("insert into Returns (Order_Date , Order_Type) values ('" + d + "',N'مرتجعات مشتريات')", "", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }
            //insert into return detalis 
            decimal total_TaxValue_ForProduct = (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[6].Value)) * NudQty.Value;

            db.executeData("insert into Returns_Details values (" + id + " ,N'" + txtName2.Text + "' ,N'' ,N'" + DgvSearch.CurrentRow.Cells[16].Value + "' , N'" + d + "' ," + NudQty.Value + " ," + DgvSearch.CurrentRow.Cells[6].Value + " ," + NudQty.Value * Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) + " , N'" + Properties.Settings.Default.User_ID + "' ," + txtTotalOrderAfterTax.Text + " ," + txtMadfou3.Text + " , " + txtbaky.Text + " ," + total_TaxValue_ForProduct + " , " + DgvSearch.CurrentRow.Cells[8].Value + " ,N'" + DgvSearch.CurrentRow.Cells[15].Value + "',"+ DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");



            decimal QtyInMain = 0;
            decimal realQty = 0;

            // to get qty in main
            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + DgvSearch.CurrentRow.Cells[15].Value + "'", "");

            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
            }
            catch (Exception) { }

            realQty = NudQty.Value / QtyInMain;

            db.executeData("update Products set Qty=Qty - " + realQty + " where Pro_ID=" + proID + "", "", "");

            // update product qty
            DataTable tblQty = new DataTable();
            decimal Qty = 0;
            Int64 RawID = 0;

            while (realQty > 0)
            {
                db.executeData("delete from Products_Qty where Qty <=0", "", "");
                tblQty.Clear();

                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Buy_Price=" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * QtyInMain + "", "");

                if (tblQty.Rows.Count <= 0)
                {
                    tblQty.Clear();
                    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + "", "");
                }
                   
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
                    db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                    realQty = 0;
                }
                else if (Qty - realQty < 0)
                {
                    db.executeData("update Products_Qty set Qty=Qty - " + Qty + " where ID=" + RawID + "", "", "");
                    db.executeData("delete from Products_Qty where Qty <= 0", "", "");
                    realQty = Math.Abs(Qty - realQty);
                }
            }
            // end of update product qty

            // update tock values
            db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * NudQty.Value) + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مشتريات', N'') ", "", "");
            db.executeData("update stock set Money=Money + " + (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * NudQty.Value) + " where Stock_ID=" + stock_ID + "", "", ""); 

            //insert into tax report
            decimal totalTax = 0;
            decimal itemTax = 0;
            itemTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[7].Value) / Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value);
            totalTax = itemTax * NudQty.Value;
            decimal totalBeforeTax = 0;
            decimal totalItem = 0;

            totalItem = NudQty.Value * Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value);
            totalBeforeTax = totalItem - totalTax;
            db.executeData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مشتريات' ,N'قيمة مضافة'  ,N'" + txtName2.Text + "',N'لا يوجد' ," + totalBeforeTax + " ," + totalTax + " ," + totalItem + " ,N'" + d + "')", "", "");

            // update buy details for that row
            decimal TotalProductPrice = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[4].Value) - NudQty.Value);
            db.executeData("update Buy_Details set Qty=Qty - " + NudQty.Value + ",TotalProductPrice=" + TotalProductPrice + " where ID=" + DgvSearch.CurrentRow.Cells[17].Value + "", "", "");

            // update buy details for all rows
            decimal minusedPrice = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[8].Value) * NudQty.Value;
            decimal totalOrder = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[11].Value) - minusedPrice;
            decimal totalBakey = totalOrder - Convert.ToDecimal(DgvSearch.CurrentRow.Cells[13].Value);

            db.executeData("update Buy_Details set TotalOrder=" + totalOrder + ", Baky =" + totalBakey + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }

        private void btnReturnItemOnly_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (rbtnReturnItemOnly.Checked)
                {
                    if (rbtnSales.Checked)
                    {
                        returnItemSaleOnly();

                    }
                    else if (rbtnBuy.Checked)
                    {
                        returnItemBuyOnly();
                    }
                }
                else if (rbtnReturnQtyonly.Checked)
                {
                    if (rbtnSales.Checked)
                    {
                        returnQtySaleOnly();

                    }
                    else if (rbtnBuy.Checked)
                    {
                        returnQtyBuyOnly();
                    }
                }
            }
        }
    }
}
