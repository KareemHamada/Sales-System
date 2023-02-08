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
    public partial class Frm_EmployeeSalaryReport : Form
    {
        public Frm_EmployeeSalaryReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_EmployeeSalaryReport_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxEmployee, "select * from Employee", "Emp_Name", "Emp_ID");
            }
            catch (Exception) { }
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
            
            if (rbtnAllEmp.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',Employee.[Emp_Name] as 'اسم الموظف',[Total_Salary] as 'اجمالى المرتب',[Total_Borrow] as 'اجمالى السلفيات',[Safy_Salary] as 'صافى المرتب',[Order_Date] as 'تاريخ الصرف',[Date_Reminder] as 'تاريخ الاستحقاق',[Notes] as 'ملاحظات' FROM [dbo].[Employee_Salary],Employee where Employee.Emp_ID=[Employee_Salary].Emp_ID and Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
            else
            {
                if (cbxEmployee.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر موظفا صحيحا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',Employee.[Emp_Name] as 'اسم الموظف',[Total_Salary] as 'اجمالى المرتب',[Total_Borrow] as 'اجمالى السلفيات',[Safy_Salary] as 'صافى المرتب',[Order_Date] as 'تاريخ الصرف',[Date_Reminder] as 'تاريخ الاستحقاق',[Notes] as 'ملاحظات' FROM [dbo].[Employee_Salary],Employee where Employee.Emp_ID=[Employee_Salary].Emp_ID and [Employee_Salary].Emp_ID=" + cbxEmployee.SelectedValue + " and Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][2]);
                }

                txtTotal.Text = Math.Round(Sum, 2).ToString();
            }
            else
            { 
                txtTotal.Text = "0"; 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (rbtnOneEmp.Checked)
                {
                    db.executeData("delete from Employee_Salary where Emp_ID="+cbxEmployee.SelectedValue+" and Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                    btnSearch_Click(null, null);
                }
                else
                {
                    db.executeData("delete from Employee_Salary where Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                    btnSearch_Click(null, null);

                }

            }
        }
    }
}
