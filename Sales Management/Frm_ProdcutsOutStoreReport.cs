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
    public partial class Frm_ProdcutsOutStoreReport : Form
    {
        public Frm_ProdcutsOutStoreReport()
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
            }
            catch { }
        }
        private void Frm_ProdcutsOutStoreReport_Load(object sender, EventArgs e)
        {
            try
            {
                FillStore();
            }
            catch { }

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

            if (rbtnAllStoreFrom.Checked)
                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_OutStore.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_OutStore.Store_ID) as 'اسم المخزن',[Qty] as 'الكمية المخرجة' ,(select Unit_Name from Unit where Unit.Unit_ID = Products_OutStore.Unit_ID) as 'الوحدة',[Date] as 'التاريخ',[Name] as 'اسم المسؤل',[Reason] as 'ملاحظات' FROM [dbo].[Products_OutStore] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            else
            {
                if (cbxStoreFrom.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مخزن صحيح");
                    return;
                }

                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',(select Pro_Name from Products where Products.Pro_ID = Products_OutStore.Pro_ID) as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_OutStore.Store_ID) as 'اسم المخزن',[Qty] as 'الكمية المخرجة' ,(select Unit_Name from Unit where Unit.Unit_ID = Products_OutStore.Unit_ID) as 'الوحدة',[Date] as 'التاريخ',[Name] as 'اسم المسؤل',[Reason] as 'ملاحظات' FROM [dbo].[Products_OutStore] where Store_ID=N'" + cbxStoreFrom.SelectedValue + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
                



            DgvSearch.DataSource = tbl;

            if (tbl.Rows.Count >= 1)
            {
                decimal totalQty = 0;
                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    totalQty += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);

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
                    db.executeData("delete from Products_OutStore where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم المسح بنجاح", "");

                    btnSearch_Click(null, null);
                }
            }
        }
    }
}
