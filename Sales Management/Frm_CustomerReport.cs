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
    public partial class Frm_CustomerReport : Form
    {
        public Frm_CustomerReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();

        private void Frm_CustomerReport_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxCustomer, "select * from Customers where CurrentState=1", "Cust_Name", "Cust_ID");
            }
            catch (Exception) { }
            txtTotal.Text = "0";
            //DtpDate.Text = DateTime.Now.ToShortDateString();
            //tbl.Clear();
            //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,[Cust_Name] as 'اسم العميل' FROM [dbo].[Customer_Report]", "");

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
            if (rbtnAllCust.Checked)
            {
                //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,[Cust_Name] as 'اسم العميل' FROM [dbo].[Customer_Report]", "");
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع' ,COALESCE(NULLIF(Cust_Name,''),(select Customers.Cust_Name from Customers where Customers.Cust_ID=[Customer_Report].Cust_ID)) as 'اسم العميل'FROM [dbo].[Customer_Report]", "");
            }
            else
            {

                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,Customers.Cust_Name as 'اسم العميل' FROM [dbo].[Customer_Report],Customers where Customer_Report.Cust_ID = Customers.Cust_ID and Customer_Report.Cust_ID=" + cbxCustomer.SelectedValue + "", "");

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
                    if (rbtnOneCustomer.Checked)
                    {
                        db.executeData("delete from Customer_Report where Cust_ID=" + cbxCustomer.SelectedValue + "", "تم مسح البيانات بنجاح", "");
                        btnSearch_Click(null, null);
                    }
                    else {
                        db.executeData("delete from Customer_Report", "تم مسح البيانات بنجاح", "");
                        btnSearch_Click(null, null);
                    }
                }
            }
        }
    }
}
