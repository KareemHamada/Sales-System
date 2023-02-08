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
    public partial class Frm_BuyReport : Form
    {
        public Frm_BuyReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();

        private void Frm_BuyReport_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxSupplier, "select * from Suppliers", "Sup_Name", "Sup_ID");
            }
            catch (Exception) { }
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Buy_Details   Madfoua
            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            if (rbtnAllSup.Checked)
            {

                if (checkOrderNumber.Checked == false)
                {
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم' ,Store.Store_Name as 'المخزن',Buy_Details.Store_ID as 'رقم المخزن',Buy_Details.Stock_ID as 'رقم الخزنة' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users,Store where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Store.Store_ID = Buy_Details.Store_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (checkOrderNumber.Checked)
                {
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم' ,Store.Store_Name as 'المخزن',Buy_Details.Store_ID as 'رقم المخزن',Buy_Details.Stock_ID as 'رقم الخزنة' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users,Store where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Store.Store_ID = Buy_Details.Store_ID and Order_ID =" + txtOrderNumber.Text + " and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'  ORDER BY Order_ID ASC", "");
                }

            }
            else if (rbtnOneSupplier.Checked)
            {
                if(cbxSupplier.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك ادخل اسم مورد صحيح");
                    return;
                }
                if (checkOrderNumber.Checked == false)
                {
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم',Store.Store_Name as 'المخزن',Buy_Details.Store_ID as 'رقم المخزن',Buy_Details.Stock_ID as 'رقم الخزنة' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users,Store where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Store.Store_ID = Buy_Details.Store_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' and [Buy_Details].Sup_ID=" + cbxSupplier.SelectedValue + " ORDER BY Order_ID ASC ", "");
                }
                else if (checkOrderNumber.Checked)
                {
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم',Store.Store_Name as 'المخزن',Buy_Details.Store_ID as 'رقم المخزن',Buy_Details.Stock_ID as 'رقم الخزنة' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users,Store where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Store.Store_ID = Buy_Details.Store_ID and Order_ID =" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' and [Buy_Details].Sup_ID=" + cbxSupplier.SelectedValue + " ORDER BY Order_ID ASC ", "");

                }
            }

            
            DgvSearch.DataSource = tbl;
            DgvSearch.Columns[14].Visible = false;
            DgvSearch.Columns[15].Visible = false;

            decimal totalPrice = 0, totalTax = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[10].Value);
                totalTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalTax.Text = Math.Round(totalTax, 2).ToString();
        }

        private void Print()
        {
            int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
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
                    rpt.PrintToPrinter(1, true, 0, 0);
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
                    rpt.PrintToPrinter(1, true, 0, 0);
                    //frm.ShowDialog();
                    
                }




            }
            catch (Exception) { }
        }



        private void PrintAll()
        {
            string date1;
            string date2;//dd/MM/yyyy == yyyy-MM-dd
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'اسم المنتج',[Date] as 'تاريخ الفاتورة',[Buy_Details].[Qty] as 'الكمية' ,Unit.Unit_Name as 'الوحدة',[priceBeforeTax] as 'السعر قبل الضريبة',[Buy_Details].taxValue_ForOneProduct as 'الضريبة',Price_AfterTax_ForOneProduct as 'السعر بعد الضريبة',[Discount] as 'الخصم',[TotalProductPrice] as 'اجمالى الصنف',[TotalOrder] as 'الاجمالى العام',Users.User_Name as 'اسم المستخدم' ,[Madfoua] as 'المدفوع',[Baky] as 'المبلغ المتبقى' FROM [dbo].[Buy_Details],Suppliers,Products,Unit,Users where  Suppliers.Sup_ID =[Buy_Details].Sup_ID and Products.Pro_ID =[Buy_Details].Pro_ID and Users.User_Id = [Buy_Details].User_ID and Unit.Unit_ID = [Buy_Details].Unit_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID ASC", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptBuyReport rpt = new RptBuyReport();


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



        private void txtOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (DgvSearch.Rows.Count >= 1)
            //{
            //    if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        db.executeData("delete from Buy where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح");

            //        btnSearch_Click(null, null);
            //    }
            //}

            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string d = DateTime.Now.ToString("dd/MM/yyyy");

                    DataTable checkExists = new DataTable();
                    checkExists.Clear();
                    checkExists = db.readData("SELECT * FROM Buy_Details where Order_ID ="+ DgvSearch.CurrentRow.Cells[0].Value + "", "");
                    if(checkExists.Rows.Count > 0)
                    {
                        for (int i = 0; i < checkExists.Rows.Count; i++)
                        {
                            
                            int proID = Convert.ToInt32(checkExists.Rows[i][2]);

                            decimal QtyInMainTest = 0;
                            decimal realQtyTest = 0;
                            DataTable tblUnitTest = new DataTable();
                            tblUnitTest.Clear();
                            tblUnitTest = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + checkExists.Rows[i][15] + "'", "");

                            try
                            {
                                QtyInMainTest = Convert.ToDecimal(tblUnitTest.Rows[0][2]);
                            }
                            catch (Exception) { }

                            realQtyTest = Convert.ToDecimal(checkExists.Rows[i][4]) / QtyInMainTest;

                            DataTable tblQty = new DataTable();
                            tblQty.Clear();
                            tblQty = db.readData("select SUM(Qty) from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + checkExists.Rows[i][17] + "", "");
                            decimal def = 0;
                            def = Convert.ToDecimal(tblQty.Rows[0][0]) - realQtyTest;
                            if (def < 0)
                            {
                                MessageBox.Show("الكمية المراد حذفها غير موجوده فى المخزن " + DgvSearch.CurrentRow.Cells[13].Value, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                    }

                    db.executeData("delete from Buy_Details where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
                    db.executeData("insert into Deleted_Orders (Order_Date , Order_Type) values ('" + d + "',N'فاتورة مشتريات')", "", "");
                    int id = 1;
                    try
                    {
                        id = Convert.ToInt32(db.readData("select max(Order_ID) from Deleted_Orders", "").Rows[0][0]);
                    }
                    catch (Exception) { }

                    for (int i = 0; i < checkExists.Rows.Count ; i++)
                    {
                        db.executeData("insert into Deleted_Order_Details values (" + id + " ,N'" + checkExists.Rows[i][1] + "',N'' ,N'" + checkExists.Rows[i][2] + "' , N'" + d + "' ," + checkExists.Rows[i][4] + " ," + checkExists.Rows[i][6] + " ," + checkExists.Rows[i][8] + " , N'" + Properties.Settings.Default.User_ID + "' ," + checkExists.Rows[i][9] + " ," + checkExists.Rows[i][12] + " , " + checkExists.Rows[i][13] + " ," + checkExists.Rows[i][15] + "," + DgvSearch.CurrentRow.Cells[0].Value + ")", "", "");
                        int proID = Convert.ToInt32(checkExists.Rows[i][2]);


                        decimal QtyInMain = 0;
                        decimal realQty = 0;
                        DataTable tblUnit = new DataTable();
                        tblUnit.Clear();
                        tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_ID=N'" + checkExists.Rows[i][15] + "'", "");

                        try
                        {
                            QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][2]);
                        }
                        catch (Exception) { }

                        realQty = Convert.ToDecimal(checkExists.Rows[i][4]) / QtyInMain;

                        db.executeData("update Products set Qty=Qty - " + realQty + " where Pro_ID=" + proID + "", "", "");
                        DataTable tblQty = new DataTable();
                        decimal Qty = 0;
                        Int64 RawID = 0;

                        while (realQty > 0)
                        {
                            db.executeData("delete from Products_Qty where Qty <=0", "", "");
                            tblQty.Clear();


                            tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + checkExists.Rows[i][17] + " and Buy_Price=" + Convert.ToDecimal(checkExists.Rows[i][13]) * QtyInMain + "", "");
                            if (tblQty.Rows.Count <= 0)
                            {
                                tblQty.Clear();
                                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + checkExists.Rows[i][17] + "", "");
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

                    }

                    db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + DgvSearch.CurrentRow.Cells[15].Value + " ," + DgvSearch.CurrentRow.Cells[11].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'فاتورة مشتريات محذوفة', N'') ", "", "");
                    db.executeData("update Stock set Money=Money + " + DgvSearch.CurrentRow.Cells[11].Value + " where Stock_ID=" + DgvSearch.CurrentRow.Cells[15].Value + "", "", "");

                   
                    db.executeData("delete from Taxes_Report where Order_Num="+ DgvSearch.CurrentRow.Cells[0].Value + "  and Order_Type=N'فاتورة مشتريات'", "", "");
                    // delete money added to tables Supplier_Money and Supplier_Report
                    db.executeData("delete from Supplier_Money where Order_ID="+ DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
                    db.executeData("delete from Supplier_Report where Order_ID="+ DgvSearch.CurrentRow.Cells[0].Value + "", "", "");
                    MessageBox.Show("تمت عمليه الحذف بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearch_Click(null, null);
                }
            }


        }

        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                Print();
            }
        }

     
    }
}
