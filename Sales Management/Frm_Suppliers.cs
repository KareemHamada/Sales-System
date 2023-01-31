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
    public partial class Frm_Suppliers : Form
    {
        public Frm_Suppliers()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();
        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT [Sup_ID] as 'رقم المورد',[Sup_Name] as 'اسم المورد',[Sup_CardID] as 'رقم البطاقة',[Sup_Phone] as 'رقم التليفون',[Notes] as 'ملاحظات'FROM [dbo].[Suppliers]", "");
            DgvSearch.DataSource = tblSearch;

            tbl.Clear();
            tbl = db.readData("Select Max(Sup_ID) from Suppliers", "");
            if (tbl.Rows[0][0].ToString() == DBNull.Value.ToString())
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

            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }
        //int row;
        //private void show()
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select * from Suppliers", "");
        //    if (tbl.Rows.Count <= 0)
        //    {
        //        MessageBox.Show("لا يوجد بيانات في هذه الشاشة");
        //    }
        //    else
        //    {
        //        txtID.Text = tbl.Rows[row][0].ToString();
        //        txtName.Text = tbl.Rows[row][1].ToString();
        //        txtPhone.Text = tbl.Rows[row][2].ToString();
        //        txtCardID.Text = tbl.Rows[row][3].ToString();
        //        txtNotes.Text = tbl.Rows[row][4].ToString();
        //    }

        //    btnAdd.Enabled = false;
        //    btnNew.Enabled = true;
        //    btnDelete.Enabled = true;
        //    btnDeleteAll.Enabled = true;
        //    btnSave.Enabled = true;
        //}
        private void Frm_Suppliers_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المورد");
                return;
            }
            db.executeData("insert into Suppliers values (" + txtID.Text + ",N'" + txtName.Text + "',N'" + txtCardID.Text + "' ,N'" + txtPhone.Text + "' ,N'" + txtNotes.Text + "')", "تم الادخال بنجاح", "");


            AutoNumber();
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
            db.executeData("Update Suppliers set Sup_Name = N'" + txtName.Text + "' ,Sup_CardID = N'" + txtCardID.Text + "' ,Sup_Phone = N'" + txtPhone.Text + "' ,Notes = N'" + txtNotes.Text + "' where Sup_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح", "");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Delete from Suppliers where Sup_ID=" + txtID.Text + "", "تم مسح المورد بنجاح", "");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Delete from Suppliers", "تم مسح جميع الموردين بنجاح", "");
                AutoNumber();
            }
        }

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Sup_ID) from Suppliers", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    show();
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Sup_ID) from Suppliers", "");

        //    if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
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
        //    tbl = db.readData("select count(Sup_ID) from Suppliers", "");

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

       
        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    DataTable tblSearch = new DataTable();
        //    tblSearch.Clear();

        //    tblSearch = db.readData("select * from Suppliers where Sup_Name like N'%" + txtSearch.Text + "%'", "");

        //    try
        //    {
        //        txtID.Text = tblSearch.Rows[0][0].ToString();
        //        txtName.Text = tblSearch.Rows[0][1].ToString();
        //        txtAddress.Text = tblSearch.Rows[0][2].ToString();
        //        txtPhone.Text = tblSearch.Rows[0][3].ToString();
        //        txtNotes.Text = tblSearch.Rows[0][4].ToString();
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    btnAdd.Enabled = false;
        //    btnNew.Enabled = true;
        //    btnDelete.Enabled = true;
        //    btnDeleteAll.Enabled = true;
        //    btnSave.Enabled = true;
        //}

        private void Frm_Suppliers_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Frm_Buy.GetFormBuy.FillSupplierComboBox();
            }
            catch (Exception) { }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    //DataTable tblShow = new DataTable();
                    //tblShow.Clear();
                    //tblShow = db.readData("select * from Unit where Unit_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");

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
