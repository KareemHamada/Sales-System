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
    public partial class Frm_ReturnReport : Form
    {
        public Frm_ReturnReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_ReturnReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
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
                    db.executeData("delete  from Returns where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "", "");

                    db.executeData("delete  from Returns_Details where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح", "");

                    btnSearch_Click(null, null);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnAllReturn.Checked)
            {
                tbl = db.readData("SELECT [Returns_Details].[Order_ID] as 'رقم العملية' ,[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Returns_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',[priceBeforeTax_ForOneProduct] as 'السعر قبل الضريبة',[total_TaxValue_ForProduct] as 'اجمالي الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر بعد الضريبة',[TotalProductPrice] as 'اجمالى الصنف',[Date] as 'التاريخ', Returns.Order_Type as 'نوع العملية',Order_ID_Returned as 'رقم الفاتورة',Users.User_Name as 'اسم المستخدم' FROM [dbo].[Returns_Details],Products,Unit,Users,Returns where Returns_Details.Pro_ID = Products.Pro_ID and Unit.Unit_ID = Returns_Details.Unit_ID and Users.User_Id = Returns_Details.User_ID and [Returns_Details].Order_ID=Returns.Order_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");
            }
            else if (rbtnSalesReturn.Checked)
            {
                tbl = db.readData("SELECT [Returns_Details].[Order_ID] as 'رقم العملية' ,[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Returns_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',[priceBeforeTax_ForOneProduct] as 'السعر قبل الضريبة',[total_TaxValue_ForProduct] as 'اجمالي الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر بعد الضريبة',[TotalProductPrice] as 'اجمالى الصنف',[Date] as 'التاريخ', Returns.Order_Type as 'نوع العملية',Order_ID_Returned as 'رقم الفاتورة',Users.User_Name as 'اسم المستخدم' FROM [dbo].[Returns_Details],Products,Unit,Users,Returns where Returns_Details.Pro_ID = Products.Pro_ID and Unit.Unit_ID = Returns_Details.Unit_ID and Users.User_Id = Returns_Details.User_ID and [Returns_Details].Order_ID=Returns.Order_ID and Order_Type=N'مرتجعات مبيعات' and  Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            }
            else if (rbtnBuyReturn.Checked)
            {
                tbl = db.readData("SELECT [Returns_Details].[Order_ID] as 'رقم العملية' ,[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',Products.Pro_Name as 'المنتج',[Returns_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',[priceBeforeTax_ForOneProduct] as 'السعر قبل الضريبة',[total_TaxValue_ForProduct] as 'اجمالي الضريبة',[Price_AfterTax_ForOneProduct] as 'السعر بعد الضريبة',[TotalProductPrice] as 'اجمالى الصنف',[Date] as 'التاريخ', Returns.Order_Type as 'نوع العملية',Order_ID_Returned as 'رقم الفاتورة',Users.User_Name as 'اسم المستخدم' FROM [dbo].[Returns_Details],Products,Unit,Users,Returns where Returns_Details.Pro_ID = Products.Pro_ID and Unit.Unit_ID = Returns_Details.Unit_ID and Users.User_Id = Returns_Details.User_ID and [Returns_Details].Order_ID=Returns.Order_ID and Order_Type=N'مرتجعات مشتريات' and  Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0, totalTax = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[9].Value);
                totalTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalTax.Text = Math.Round(totalTax, 2).ToString();
        }
    }
}
