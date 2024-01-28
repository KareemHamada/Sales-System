using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// if there is no اسم الدائن هيكمل عادي
namespace Sales_Management
{
    public partial class Frm_EmployeeBorrowMoney : Form
    {
        public Frm_EmployeeBorrowMoney()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Employee_BorrowMoney", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            NudPrice.Value = 1;
            //txtname.Clear();
            txtNotes.Clear();
            DtpDate.Text = DateTime.Now.ToShortDateString();
            DtpReminder.Text = DateTime.Now.ToShortDateString();
            txtCreditor.Text = "";
            //rbtnNormal_CheckedChanged(null, null);
            //rbtnNormal.Checked = true;
        }
        string stock_ID = "";
        private void Frm_EmployeeBorrowMoney_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxEmployee, "select * from Employee where CurrentState=1", "Emp_Name", "Emp_ID");
                AutoNumber();

            }
            catch (Exception) { }
            stock_ID = Properties.Settings.Default.Stock_ID.ToString();
        }

        //private void rbtnNormal_CheckedChanged(object sender, EventArgs e)
        //{
        //    cbxEmployee.Enabled = false;
        //    txtname.Enabled = true;
        //}

        //private void rbtnEmployee_CheckedChanged(object sender, EventArgs e)
        //{
        //    cbxEmployee.Enabled = true;
        //    txtname.Enabled = false;
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            string dReminder = DtpReminder.Value.ToString("dd/MM/yyyy");

            if (cbxEmployee.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك تاكيد من اكتمال بيانات الموظفين", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbxEmployee.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر موظفا صحيحا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (rbtnEmployee.Checked)
            //{
            //    if (cbxEmployee.Items.Count <= 0)
            //    {
            //        MessageBox.Show("من فضلك تاكيد من اكتمال بيانات الموظفين", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    if (cbxEmployee.SelectedValue == null)
            //    {
            //        MessageBox.Show("من فضلك اختر موظفا صحيحا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //}

            if (NudPrice.Value <= 0)
            {
                MessageBox.Show("لابد ان يكون المبلغ لا يقل عن 1", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string name = cbxEmployee.Text;
            //string name ="";

            //if (rbtnEmployee.Checked)
            //{ 
            //    name = cbxEmployee.Text; 
            //}
            //else{
            //    if (txtname.Text == "")
            //    { 
            //        MessageBox.Show("من فضلك ادخل اسم الشخص المدين", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    name = txtname.Text;
            //}
            decimal stock_Money = 0;
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");
            stock_Money = Convert.ToDecimal(tbl.Rows[0][1]);

            if (NudPrice.Value > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            db.executeData("insert into Employee_SalaryMinus (Emp_ID ,Date ,Price,Pay) values (" + cbxEmployee.SelectedValue + " ,N'" + d + "' ," + NudPrice.Value + " ,'NO')", "", "");
            //if (rbtnEmployee.Checked)
            //{
            //    db.executeData("insert into Employee_SalaryMinus (Emp_ID ,Date ,Price,Pay) values (" + cbxEmployee.SelectedValue + " ,N'" + d + "' ," + NudPrice.Value + " ,'NO')", "", "");
            //}
            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + txtCreditor.Text + "' ,N'سفليات', N'" + txtNotes.Text + "') ", "", "");
            db.executeData("update stock set Money=Money - " + NudPrice.Value + " where Stock_ID=" + stock_ID + "", "", "");
            db.executeData("insert into Employee_BorrowMoney values (" + txtID.Text + " , N'" + txtCreditor.Text + "' ,N'" + name + "' ,N'" + d + "' ,N'" + dReminder + "' ," + NudPrice.Value + " ,N'" + txtNotes.Text + "')", "تمت العملية بنجاح", "");


            AutoNumber();
        }
    }
}
