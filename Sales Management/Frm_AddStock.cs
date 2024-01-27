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
    public partial class Frm_AddStock : Form
    {
        public Frm_AddStock()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblStock = new DataTable();
        private void AutoNumber()
        {
            tblStock.Clear();
            tblStock = db.readData("select Stock_ID as 'رقم الخزنة' ,Stock_Name as 'اسم الخزنة' from Stock_Data where CurrentState=1", "");
            DgvSearch.DataSource = tblStock;

            tbl.Clear();
            tbl = db.readData("select max (Stock_ID) from Stock_Data", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }


        private void Frm_AddStock_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الخزنة");
                return;
            }
            db.executeData("insert into Stock_Data Values (" + txtID.Text + " ,N'" + txtName.Text + "',1)", "تم الادخال بنجاح","");
            db.executeData("insert into Stock Values (" + txtID.Text + ",0)", "", "");
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الخزنة");
                return;
            }
            db.executeData("update Stock_Data set Stock_Name=N'" + txtName.Text + "' where Stock_ID=" + txtID.Text + " ", "تم الحفظ بنجاح", "");

            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Stock_Data set CurrentState=0 where Stock_ID=" + txtID.Text + " ", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Stock_Data set CurrentState=0", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    DataTable tblShow = new DataTable();
                    tblShow.Clear();
                    tblShow = db.readData("select * from Stock_Data where Stock_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");

                    txtID.Text = tblShow.Rows[0][0].ToString();
                    txtName.Text = tblShow.Rows[0][1].ToString();

                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                }

            }
            catch (Exception) { }
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
        //        tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
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
        //    tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
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
        //    tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    Show();
        //}
    }
}
