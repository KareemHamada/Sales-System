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
    public partial class Frm_StoreTransfireReport : Form
    {
        public Frm_StoreTransfireReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();


        private void FillStore()
        {
            try
            {
                cbxStoreFrom.DataSource = db.readData("select * from Store where CurrentState=1", "");
                cbxStoreFrom.DisplayMember = "Store_Name";
                cbxStoreFrom.ValueMember = "Store_ID";

                cbxStoreTo.DataSource = db.readData("select * from Store where CurrentState=1", "");
                cbxStoreTo.DisplayMember = "Store_Name";
                cbxStoreTo.ValueMember = "Store_ID";
            }
            catch { }
            
        }
        private void Frm_StoreTransfireReport_Load(object sender, EventArgs e)
        {
            FillStore();

            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if(cbxStoreFrom.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك ادخل المخازن اولا ", "تاكيد");
                return;
            }
            if (cbxStoreFrom.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر مخزن صحيح", "تاكيد");
                return;
            }
            if (cbxStoreTo.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر مخزن صحيح", "تاكيد");
                return;
            }

            if (rbtnAllStoreFrom.Checked)
            {
                if (rbtnAllStoreTo.Checked )
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_Transfire.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_From) as 'المخزن المحول منه' ,(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_To) as 'المخزن المحول له',[Qty] as 'الكمية المحولة',(select Unit_Name from Unit where Unit.Unit_ID = Products_Transfire.Unit) as 'الوحدة',[Buy_Price] as 'سعر الشراء للوحدة الكبري',[Sale_Price] as 'سعر البيع للوحدة الكبري',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreTo.Checked)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_Transfire.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_From) as 'المخزن المحول منه' ,(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_To) as 'المخزن المحول له',[Qty] as 'الكمية المحولة',(select Unit_Name from Unit where Unit.Unit_ID = Products_Transfire.Unit) as 'الوحدة',[Buy_Price] as 'سعر الشراء للوحدة الكبري',[Sale_Price] as 'سعر البيع للوحدة الكبري',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_To=N'" + cbxStoreTo.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }
            else if (rbtnOneStoreFrom.Checked)
            {
                if (rbtnAllStoreTo.Checked)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_Transfire.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_From) as 'المخزن المحول منه' ,(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_To) as 'المخزن المحول له',[Qty] as 'الكمية المحولة',(select Unit_Name from Unit where Unit.Unit_ID = Products_Transfire.Unit) as 'الوحدة',[Buy_Price] as 'سعر الشراء للوحدة الكبري' ,[Sale_Price] as 'سعر البيع للوحدة الكبري',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_From=N'" + cbxStoreFrom.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreTo.Checked)
                    tbl = db.readData("SELECT [Order" +
                        "_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_Transfire.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_From) as 'المخزن المحول منه' ,(select Store_Name from Store where Store.Store_ID = Products_Transfire.Store_To) as 'المخزن المحول له',[Qty] as 'الكمية المحولة',(select Unit_Name from Unit where Unit.Unit_ID = Products_Transfire.Unit) as 'الوحدة',[Buy_Price] as 'سعر الشراء للوحدة الكبري',[Sale_Price] as 'سعر البيع للوحدة الكبري',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_To=N'" + cbxStoreTo.SelectedValue + "' and Store_From=N'" + cbxStoreFrom.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }


            DgvSearch.DataSource = tbl;

            if (tbl.Rows.Count >= 1)
            {
                decimal totalQty = 0;
                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    totalQty += Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);

                }
                txtTotalQty.Text = Math.Round(totalQty, 2).ToString();
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
                    db.executeData("delete from Products_Transfire where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم المسح بنجاح", "");

                    btnSearch_Click(null, null);
                }
            }
        }
    }
}
