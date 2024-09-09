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
    public partial class Frm_SupplierReport : Form
    {
        public Frm_SupplierReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_SupplierReport_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxSupplier, "select * from Suppliers where CurrentState=1", "Sup_Name", "Sup_ID");
            }
            catch (Exception) { }
            txtTotal.Text = "0";
            //DtpDate.Text = DateTime.Now.ToShortDateString();


            //tbl.Clear();
            //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Price] as 'المبلغ المدفوع',[Date] as 'تاريخ ادفع' ,Suppliers.Sup_Name as 'اسم المورد' FROM [dbo].[Supplier_Report] ,Suppliers where Suppliers.Sup_ID =Supplier_Report.Sup_ID", "");

            //DgvSearch.DataSource = tbl;

            //decimal totalPrice = 0;
            //for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            //{
            //    totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[1].Value);

            //}
            //txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            

            tbl.Clear();
            if (rbtnAllSup.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Price] as 'المبلغ المدفوع',[Date] as 'تاريخ الدفع' ,Suppliers.Sup_Name as 'اسم المورد',Users.User_Name as 'المستخدم' FROM [dbo].[Supplier_Report] ,Suppliers,Users where Suppliers.Sup_ID =Supplier_Report.Sup_ID and Supplier_Report.User_ID = Users.User_Id", "");
            }
            else if (rbtnOneSupplier.Checked)
            {
                if (cbxSupplier.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مورد صحيح");
                    return;
                }
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Price] as 'المبلغ المدفوع',[Date] as 'تاريخ الدفع' ,Suppliers.Sup_Name as 'اسم المورد',Users.User_Name as 'المستخدم' FROM [dbo].[Supplier_Report] ,Suppliers,Users where Suppliers.Sup_ID =Supplier_Report.Sup_ID and Supplier_Report.User_ID = Users.User_Id and Suppliers.Sup_ID =" + cbxSupplier.SelectedValue + "", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[1].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnOneSupplier.Checked)
                    {

                        db.executeData("delete from Supplier_Report where Sup_ID=" + cbxSupplier.SelectedValue + "", "تم مسح البيانات بنجاح", "");

                        btnSearch_Click(null, null);
                    }
                    else { 
                        MessageBox.Show("من فضلك حدد مورد اولا", "تاكيد"); 
                        return; 
                    }
                }
            }
        }
    }
}
