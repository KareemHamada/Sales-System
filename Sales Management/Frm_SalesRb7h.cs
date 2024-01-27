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
    public partial class Frm_SalesRb7h : Form
    {
        public Frm_SalesRb7h()
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
        private void Frm_SalesRb7h_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
            try
            {
                fillUser();
            }
            catch (Exception) { }
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

                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية',[buyPriceForBigUnit] as 'سعر الشراء',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'الضريبة للوحدة الواحدة',[priceAfterTaxOfBigUnit] as 'سعر البيع بعد الضريبة',(([priceAfterTaxOfBigUnit] * [Sales_Rb7h].[Qty]) -  ([buyPriceForBigUnit] * [Sales_Rb7h].[Qty])) - [Discount] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneUser.Checked)
                {
                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية',[buyPriceForBigUnit] as 'سعر الشراء',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'الضريبة للوحدة الواحدة',[priceAfterTaxOfBigUnit] as 'سعرالبيع بعد الضريبة',(([priceAfterTaxOfBigUnit] * [Sales_Rb7h].[Qty]) -  ([buyPriceForBigUnit] * [Sales_Rb7h].[Qty])) - [Discount] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Sales_Rb7h.User_ID =N'" + cbxUser.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
            }
            else if (checkOrderNumber.Checked)
            {
                if (rbtnAllUser.Checked)
                {

                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية',[buyPriceForBigUnit] as 'سعر الشراء',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'الضريبة للوحدة الواحدة',[priceAfterTaxOfBigUnit] as 'سعر البيع بعد الضريبة',(([priceAfterTaxOfBigUnit] * [Sales_Rb7h].[Qty]) -  ([buyPriceForBigUnit] * [Sales_Rb7h].[Qty])) - [Discount] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Order_ID =" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneUser.Checked)
                {
                    //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية',[buyPriceForBigUnit] as 'سعر الشراء',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'الضريبة للوحدة الواحدة',[priceAfterTaxOfBigUnit] as 'سعر البيع بعد الضريبة',([priceAfterTaxOfBigUnit] -  [buyPriceForBigUnit]) * [Sales_Rb7h].[Qty] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Sales_Rb7h.User_ID =N'" + cbxUser.SelectedValue + "' and Order_ID=" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");

                    tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية',[buyPriceForBigUnit] as 'سعر الشراء',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'الضريبة للوحدة الواحدة',[priceAfterTaxOfBigUnit] as 'سعر البيع بعد الضريبة',(([priceAfterTaxOfBigUnit] * [Sales_Rb7h].[Qty]) -  ([buyPriceForBigUnit] * [Sales_Rb7h].[Qty])) - [Discount] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Sales_Rb7h.User_ID =N'" + cbxUser.SelectedValue + "' and Order_ID=" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");

                }
            }
            DgvSearch.DataSource = tbl;


            decimal totalOrder = 0, TotalRb7h = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[10].Value);
                TotalRb7h += Convert.ToDecimal(DgvSearch.Rows[i].Cells[9].Value);
            }
            txtTotal.Text = Math.Round(totalOrder, 2).ToString();
            txtTotalRb7h.Text = Math.Round(TotalRb7h, 2).ToString();
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
                    db.executeData("delete from Sales_Rb7h where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم المسح بنجاح", "");

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
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',Unit.Unit_Name as 'الوحدة',[Sales_Rb7h].[Qty] as 'الكمية بالوحدة الكبري',[buyPriceForBigUnit] as 'سعر الشراء للوحدة الكبري',[Discount] as 'الخصم',[Sales_Rb7h].[taxValue_ForOneBigUnit] as 'قيمة الضريبة للوحدة الكبري',[priceAfterTaxOfBigUnit] as 'السعر بعد الضريبة للوحدةالكبري',(([priceAfterTaxOfBigUnit] * [Sales_Rb7h].[Qty]) -  ([buyPriceForBigUnit] * [Sales_Rb7h].[Qty])) - [Discount] as 'الربح',[TotalProductPrice] as 'اجمالى الصنف' ,[TotalOrder] as 'اجمالى الفاتورة',[Madfoua] as 'المبلغ المدفوع',[Baky] as 'المتبقى',[Date]  as 'التاريخ',Users.User_Name as 'اسم الكاشير',[Time] as 'الوقت' FROM [dbo].[Sales_Rb7h] ,Products,Unit,Users where Products.Pro_ID =Sales_Rb7h.Pro_ID and Unit.Unit_ID = Sales_Rb7h.Unit_ID and Users.User_Id = Sales_Rb7h.User_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptSalesRb7h rpt = new RptSalesRb7h();


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
    }
}
