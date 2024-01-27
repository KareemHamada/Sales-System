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
    public partial class Frm_Employee : Form
    {
        public Frm_Employee()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();

        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT [Emp_ID] as 'رقم الموظف',[Emp_Name] as 'اسم الموظف',[Salary] as 'الراتب الشهرى',[Date] as 'تاريخ الاستحقاق',[National_ID] as 'تحقيق الشخصية',[Emp_Phone] as 'التليفون',[Emp_Address] as 'العنوان' FROM [dbo].[Employee] where CurrentState=1", "");
            DgvSearch.DataSource = tblSearch;

            tbl.Clear();
            tbl = db.readData("select max (Emp_ID) from Employee", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.Clear();
            txtPhone.Clear();
            txtSearch.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtNationalID.Clear();

            DtbDate.Text = DateTime.Now.ToShortDateString();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }



        private void Frm_Employee_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الموظف");
                return;
            }
            if (txtSalary.Text == "")
            {
                MessageBox.Show("من فضلك ادخل راتب الموظف");
                return;
            }
            string d = DtbDate.Value.ToString("dd/MM/yyyy");
            db.executeData("insert into Employee Values (" + txtID.Text + " ,N'" + txtName.Text + "' ," + txtSalary.Text + ", N'" + d + "', N'" + txtNationalID.Text + "',N'" + txtPhone.Text + "',N'" + txtAddress.Text + "',1)", "تم الادخال بنجاح", "");

            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الموظف");
                return;
            }
            if (txtSalary.Text == "")
            {
                MessageBox.Show("من فضلك ادخل راتب الموظف");
                return;
            }
            string d = DtbDate.Value.ToString("dd/MM/yyyy");
            db.executeData("update Employee set  Emp_Name=N'" + txtName.Text + "' ,Salary=" + txtSalary.Text + ",Date= N'" + d + "',National_ID= N'" + txtNationalID.Text + "',Emp_Phone=N'" + txtPhone.Text + "',Emp_Address=N'" + txtAddress.Text + "'  where Emp_ID=" + txtID.Text + " ", "تم التعديل بنجاح", "");

            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Employee set CurrentState=0 where Emp_ID=" + txtID.Text + " ", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Employee set CurrentState=0" + txtID.Text + " ", "تم الحذف بنجاح", "");

                AutoNumber();
            }
        }

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    row = 0;
        //    Show();
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    if (row == 0)
        //    {
        //        tbl.Clear();
        //        tbl = db.readData("select count(Emp_ID) from Employee", "");
        //        row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //        Show();
        //    }
        //    else
        //    {


        //        row--;
        //        Show();
        //    }
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Emp_ID) from Employee", "");
        //    if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
        //    {
        //        row = 0;
        //        Show();
        //    }
        //    else
        //    {
        //        row++;
        //        Show();
        //    }
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Emp_ID) from Employee", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    Show();
        //}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable tblSearch = new DataTable();
            tblSearch.Clear();
            tblSearch = db.readData("select * from Employee where CurrentState=1 and Emp_Name like N'%" + txtSearch.Text + "%'", "");

            if (tblSearch.Rows.Count <= 0)
            {
            }
            else
            {
                try
                {
                    txtID.Text = tblSearch.Rows[0][0].ToString();
                    txtName.Text = tblSearch.Rows[0][1].ToString();
                    txtSalary.Text = tblSearch.Rows[0][2].ToString();

                    txtNationalID.Text = tblSearch.Rows[0][4].ToString();
                    txtAddress.Text = tblSearch.Rows[0][6].ToString();
                    txtPhone.Text = tblSearch.Rows[0][5].ToString();
                    this.Text = tblSearch.Rows[0][3].ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    DtbDate.Value = dt;
                }
                catch (Exception) { }
            }

            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnDeleteAll.Enabled = true;
            btnSave.Enabled = true;
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    txtID.Text = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                    txtName.Text = DgvSearch.CurrentRow.Cells[1].Value.ToString();
                    txtSalary.Text = DgvSearch.CurrentRow.Cells[2].Value.ToString();

                    this.Text = DgvSearch.CurrentRow.Cells[3].Value.ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    DtbDate.Value = dt;

                    //DtbDate.Text = DgvSearch.CurrentRow.Cells[3].Value.ToString();
                    txtNationalID.Text = DgvSearch.CurrentRow.Cells[4].Value.ToString();
                    txtPhone.Text = DgvSearch.CurrentRow.Cells[5].Value.ToString();
                    txtAddress.Text = DgvSearch.CurrentRow.Cells[6].Value.ToString();


                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                }

            }
            catch (Exception) { }
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
