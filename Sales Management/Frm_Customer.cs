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
    public partial class Frm_Customer : Form
    {
        public Frm_Customer()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();

        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT [Cust_ID] as 'رقم العميل',[Cust_Name] as 'اسم العميل',[Cust_CardID] as 'رقم البطافة',[Cust_Phone] as 'التليفون',[Notes] as 'ملاحظات' FROM [dbo].[Customers] where CurrentState=1", "");
            DgvSearch.DataSource = tblSearch;

            tbl.Clear();
            tbl = db.readData("Select Max(Cust_ID) from Customers","");
            if(tbl.Rows[0][0].ToString() == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            txtName.Clear();
            txtNotes.Clear();
            txtPhone.Clear();
            txtCardID.Clear();
            txtSearch.Clear();

            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }

        private void Frm_Customer_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم العميل");
                return;
            }
            DataTable tblCheck = new DataTable();
            tblCheck.Clear();
            tblCheck = db.readData("select * from Customers where Cust_Name=N'" + txtName.Text + "'", "");
            if (tblCheck.Rows.Count > 0)
            {
                MessageBox.Show("هذا الاسم موجود من قبل ادخل اسم اخر حتي لا يوجد تشابه في الاسماء", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                db.executeData("insert into Customers values (" + txtID.Text + ",N'" + txtName.Text + "',N'" + txtCardID.Text + "' ,N'" + txtPhone.Text + "' ,N'" + txtNotes.Text + "',1)", "تم الادخال بنجاح", "");

                AutoNumber();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    row = 0;
        //    show();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.executeData("Update Customers set Cust_Name = N'"+ txtName.Text+ "' ,Cust_CardID = N'" + txtCardID.Text + "' ,Cust_Phone = N'" + txtPhone.Text + "' ,Notes = N'" + txtNotes.Text + "' where Cust_ID="+ txtID.Text + "", "تم تعديل البيانات بنجاح", "");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("هل انت متاكد من مسح البيانات","تاكيد",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Update Customers set CurrentState = 0 where Cust_ID=" + txtID.Text + "", "تم الحذف بنجاح", "");
                AutoNumber();
            }
            
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Update Customers set CurrentState = 0", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Cust_ID) from Customers", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    show();
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Cust_ID) from Customers", "");
            
        //    if(Convert.ToInt32(tbl.Rows[0][0])-1 == row)
        //    {
        //        row = 0;
        //        show();
        //    }
        //    else
        //    {
        //        row++;
        //        show();
        //    }
            
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Cust_ID) from Customers", "");

        //    if (row == 0)
        //    {
        //        row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //        show();
        //    }
        //    else
        //    {
        //        row--;
        //        show();
        //    }
            
        //}

  
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataTable tblSear = new DataTable();
            tblSearch.Clear();
            tblSearch = db.readData("SELECT [Cust_ID] as 'رقم العميل',[Cust_Name] as 'اسم العميل',[Cust_CardID] as 'رقم البطافة',[Cust_Phone] as 'التليفون',[Notes] as 'ملاحظات' FROM [dbo].[Customers] where CurrentState=1 and Cust_Name like N'%" + txtSearch.Text + "%'", "");

            DgvSearch.DataSource = tblSearch;
        }

        private void Frm_Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                Frm_Sales.GetFormSales.FillCustomer();

            }
            catch (Exception) { }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    txtID.Text = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                    txtName.Text = DgvSearch.CurrentRow.Cells[1].Value.ToString();
                    txtCardID.Text = DgvSearch.CurrentRow.Cells[2].Value.ToString();
                    txtPhone.Text = DgvSearch.CurrentRow.Cells[3].Value.ToString();
                    txtNotes.Text = DgvSearch.CurrentRow.Cells[4].Value.ToString();

                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                }

            }
            catch (Exception) { }
        }
    }
}
