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
    public partial class Frm_Buy_Deleted_Report : Form
    {
        public Frm_Buy_Deleted_Report()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_Buy_Deleted_Report_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();

            tbl = db.readData("SELECT [Deleted_Order_Details].[Order_ID] as 'رقم العملية',Suppliers.Sup_Name as 'اسم المورد',Products.Pro_Name as 'المنتج',[Deleted_Order_Details].[Qty] as 'الكمية',Unit.Unit_Name as 'الوحدة',[priceBeforeTax_ForOneProduct] as 'السعر قبل الضريبة',[TaxValue_ForOneProduct]  as'الضريبة',[Price_AfterTax_ForOneProduct]  as 'السعر شامل الضريبة',[TotalProductPrice] as 'اجمالى الصنف',[Total_Order] as 'اجمالى الفاتورة',Users.User_Name as 'اسم المستخدم',[Date] as 'التاريخ',[Order_ID_Deleted] as 'رقم الفاتورة' FROM [dbo].[Deleted_Order_Details],Products,Users,Unit,Suppliers,Deleted_Orders where Deleted_Orders.Order_ID = [Deleted_Order_Details].Order_ID and Deleted_Orders.Order_Type = 'فاتورة مشتريات' and [Deleted_Order_Details].Pro_ID = Products.Pro_ID and Users.User_Id = [Deleted_Order_Details].User_ID and Unit.Unit_ID = [Deleted_Order_Details].Unit_ID and Suppliers.Sup_ID = [Deleted_Order_Details].Sup_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            DgvSearch.DataSource = tbl;
        }
    }
}
