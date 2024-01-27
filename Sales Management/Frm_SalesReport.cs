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
    public partial class Frm_SalesReport : Form
    {
        public Frm_SalesReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();

        private void fillUser()
        {
            cbxUser.DataSource = db.readData("select * from Users where CurrentState=1", "");
            cbxUser.DisplayMember = "User_Name";
            cbxUser.ValueMember = "User_ID";
        }
        string stock_ID = "";

        private void Frm_SalesReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();

            try
            {
                fillUser();
            }
            catch (Exception) { }
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (checkOrderNumber.Checked == false)
            {
                if (rbtnAllUser.Checked)
                {

                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',priceBeforeTax_ForOneProduct as 'السعر قبل الضريبة',[Sales_Details].taxValue_ForOneProduct as'الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر شامل الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'اجمالى الفاتورة',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',[Sales_Details].Pro_ID as 'رقم المنتج',Sales_Details.Unit_ID as 'رقم الوحدة',Sales_Details.Stock_ID FROM [dbo].[Sales_Details] , Products,Users,Unit where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID =  Sales_Details.Unit_ID and Users.User_Id = Sales_Details.User_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneUser.Checked)
                {
                    if(cbxUser.SelectedValue == null)
                    {
                        MessageBox.Show("من فضلك حدد مستخدم صحيح");
                        return;
                    }
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',priceBeforeTax_ForOneProduct as 'السعر قبل الضريبة',[Sales_Details].taxValue_ForOneProduct as'الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر شامل الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'اجمالى الفاتورة',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',[Sales_Details].Pro_ID as 'رقم المنتج',Sales_Details.Unit_ID as 'رقم الوحدة',Sales_Details.Stock_ID FROM [dbo].[Sales_Details] , Products,Users,Unit where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID =  Sales_Details.Unit_ID and Users.User_Id = Sales_Details.User_ID and Sales_Details.User_ID=N'" + cbxUser.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
            }
            else if (checkOrderNumber.Checked)
            {
                if (rbtnAllUser.Checked)
                {

                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',priceBeforeTax_ForOneProduct as 'السعر قبل الضريبة',[Sales_Details].taxValue_ForOneProduct as'الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر شامل الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'اجمالى الفاتورة',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',[Sales_Details].Pro_ID as 'رقم المنتج',Sales_Details.Unit_ID as 'رقم الوحدة',Sales_Details.Stock_ID FROM [dbo].[Sales_Details] , Products,Users,Unit where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID =  Sales_Details.Unit_ID and Users.User_Id = Sales_Details.User_ID and Order_ID=" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneUser.Checked)
                {
                    if (cbxUser.SelectedValue == null)
                    {
                        MessageBox.Show("من فضلك حدد مستخدم صحيح");
                        return;
                    }
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',priceBeforeTax_ForOneProduct as 'السعر قبل الضريبة',[Sales_Details].taxValue_ForOneProduct as'الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر شامل الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'اجمالى الفاتورة',Users.User_Name as 'الكاشير',[Date] as 'التاريخ',[Sales_Details].Pro_ID as 'رقم المنتج',Sales_Details.Unit_ID as 'رقم الوحدة',Sales_Details.Stock_ID FROM [dbo].[Sales_Details] , Products,Users,Unit where Products.Pro_ID = Sales_Details.Pro_ID and Unit.Unit_ID =  Sales_Details.Unit_ID and Users.User_Id = Sales_Details.User_ID and Sales_Details.User_ID=N'" + cbxUser.SelectedValue + "' and Order_ID=" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");

                }
            }
            DgvSearch.DataSource = tbl;
            DgvSearch.Columns[13].Visible = false;
            DgvSearch.Columns[14].Visible = false;
            DgvSearch.Columns[15].Visible = false;



            decimal totalPrice = 0, totalTax = 0, totalPriceTax = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += (Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value)) - Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);

                totalTax += (Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value));
                totalPriceTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[9].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalTax.Text = Math.Round(totalTax, 2).ToString();
            txtPriceTax.Text = Math.Round(totalPriceTax, 2).ToString();
            decimal rb7h = 0;
            if (rbtnOneUser.Checked)
            {
                try
                {
                    rb7h = Convert.ToDecimal(db.readData("select * from Users where User_ID=" + cbxUser.SelectedValue + "", "").Rows[0][5]);
                    txtUserRb7h.Text = ((Convert.ToDecimal(txtPriceTax.Text) / 100) * rb7h) + "";
                }
                catch (Exception) { }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                string date1;
                string date2;
                date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
                date2 = DtpTo.Value.ToString("yyyy-MM-dd");

                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.executeData("delete from Sales where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم المسح بنجاح", "");

                    btnSearch_Click(null, null);
                }
            }
        }

        private void PrintAll()
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Sales_Details].[Qty] as 'الكمية',[Sales_Details].[Price_AfterTax_ForOneProduct] as 'السعر',[Discount] as 'الخصم',[TotalProductPrice] as 'الاجمالى',[TotalOrder] as 'اجمالى الفاتورة',[Madfoua] as 'المبلغ المدفوع',[Baky] as 'المتبقى',Users.User_Name as 'الكاشير',[Date] as 'التاريخ' FROM [dbo].[Sales_Details] , Products,Users where Products.Pro_ID = Sales_Details.Pro_ID and Users.User_Id = Sales_Details.User_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptSalesReport rpt = new RptSalesReport();


                rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                rpt.SetDataSource(tblRpt);

                rpt.SetParameterValue("From", date1);
                rpt.SetParameterValue("To", date2);
                frm.crystalReportViewer1.ReportSource = rpt;

                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                rpt.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                if (Properties.Settings.Default.ShowBeforePrint)
                {
                    frm.ShowDialog();
                }
                else
                {
                    rpt.PrintToPrinter(1, true, 0, 0);
                }
            }
            catch (Exception) { }
        }

        private void Print()
        {
            int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
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
                else if (Properties.Settings.Default.SalePrintKind == "A4")
                {
                    RptOrderSalesA4 rpt = new RptOrderSalesA4();
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
     

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintAll();
            }
        }

        private void txtOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

    

        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                Print();
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    DataTable tblStock = new DataTable();
                    string d = DateTime.Now.ToString("dd/MM/yyyy");

                    decimal stock_Money = 0;
                    tblStock.Clear();
                    tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");

                    stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);

                    if (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[10].Value) > stock_Money)
                    {
                        MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    DataTable checkExists = new DataTable();
                    checkExists.Clear();
                    checkExists = db.readData("SELECT * FROM Sales_Details where Order_ID =" + DgvSearch.CurrentRow.Cells[0].Value + "", "");


                    db.executeData("insert into Deleted_Orders (Order_Date , Order_Type) values ('" + d + "',N'فاتورة مبيعات')", "", "");
                    int id = 1;
                    try
                    {
                        id = Convert.ToInt32(db.readData("select max(Order_ID) from Deleted_Orders", "").Rows[0][0]);
                    }
                    catch (Exception) { }


                    for (int i = 0; i < checkExists.Rows.Count; i++)
                    {
                        db.executeData("insert into Deleted_Order_Details values (" + id + " ,N'' ,N'" + checkExists.Rows[i][1] + "' ,N'" + checkExists.Rows[i][2] + "' , N'" + d + "' ," + checkExists.Rows[i][4] + " ," + checkExists.Rows[i][6] + " ," + checkExists.Rows[i][8] + " , N'" + Properties.Settings.Default.User_ID + "' ," + checkExists.Rows[i][9] + " ," + checkExists.Rows[i][13] + " , " + checkExists.Rows[i][14] + " ," + checkExists.Rows[i][12] + "," + DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");


                        int proID = Convert.ToInt32(checkExists.Rows[i][2]);

                        decimal QtyInMain = 0;
                        decimal realQty = 0;
                        DataTable tblUnit = new DataTable();
                        tblUnit.Clear();
                        tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + checkExists.Rows[i][12] + "'", "");


                        try
                        {
                            QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
                        }
                        catch (Exception) { }


                        realQty = Convert.ToDecimal(checkExists.Rows[i][4]) / QtyInMain;
                        db.executeData("update Products set Qty=Qty + " + realQty + " where Pro_ID=" + proID + "", "", "");

                        // sales rebh table
                        DataTable tblSalesRebh = new DataTable();
                        tblSalesRebh.Clear();
                        tblSalesRebh = db.readData("select * from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Pro_ID =" + proID + "", "");


                        for (int x = 0; x < tblSalesRebh.Rows.Count; x++)
                        {
                            Int64 unit_ID = Convert.ToInt64(checkExists.Rows[i][12]);
                            decimal qtyOfUnit = Convert.ToDecimal(db.readData("select QtyNmain from Products_Unit where Pro_ID=" + proID + " and Unit_ID=" + unit_ID + "", "").Rows[0][0]);

                            decimal requiredBuyPrice = Convert.ToDecimal(tblSalesRebh.Rows[x][16]) * qtyOfUnit;
                            decimal qtyFromSalesRebh = Convert.ToDecimal(tblSalesRebh.Rows[x][4]) / qtyOfUnit;
                            DataTable tblQty = new DataTable();
                            tblQty.Clear();
                            tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + tblSalesRebh.Rows[x][18] + " and Buy_Price=" + requiredBuyPrice + "", "");


                            if (tblQty.Rows.Count >= 1)
                            {
                                db.executeData("update Products_Qty set Qty=Qty + " + qtyFromSalesRebh + " where ID=" + tblQty.Rows[0][5] + "", "", "");
                            }
                            else
                            {
                                db.executeData("insert into Products_Qty values (" + proID + " , " + tblSalesRebh.Rows[x][18] + ", " + qtyFromSalesRebh + " , " + requiredBuyPrice + " , " + Convert.ToDecimal(checkExists.Rows[i][14]) * QtyInMain + ")", "", "");
                            }
                        }

                    }

                    db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[10].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'فاتورة مبيعات محذوفة', N'') ", "", "");
                    db.executeData("update stock set Money=Money - " + DgvSearch.CurrentRow.Cells[10].Value + " where Stock_ID=" + stock_ID + "", "", "");

                    db.executeData("delete from Taxes_Report where Order_Num=" + DgvSearch.CurrentRow.Cells[0].Value + "  and Order_Type=N'فاتورة مبيعات'", "", "");


                    db.executeData("delete from Sales_Details where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
                    db.executeData("delete from Sales_Rb7h where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

                    db.executeData("delete from Customer_Money where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
                    db.executeData("delete from Customer_Report where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

                    MessageBox.Show("تمت عمليه الحذف بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    btnSearch_Click(null, null);
                }
            }
        }
    }
}
